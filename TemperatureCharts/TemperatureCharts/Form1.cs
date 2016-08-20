using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TemperatureCharts
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmMain : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="frmMain"/> class.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        private Dictionary<string, string> SensorNames = null;
        private Dictionary<string, System.Drawing.Color> SensorColors = null;
        private ToolTip chartToolTip = new ToolTip();
        private List<TemperatureData> allTemperatureDataList = new List<TemperatureData>();
        List<PowerUseData> powerUseDataList = null;

        private int lastTemperatureDataId = 0;

        /// <summary>
        /// Handles the Load event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Form1_Load( object sender, EventArgs e )
        {
            SensorNames = new Dictionary<string, string>();

            SensorNames.Add( "28-031635fb46ff", "Master Bedroom" );
            SensorNames.Add( "28-031636b23aff", "Lower Attic" );
            SensorNames.Add( "28-0416361444ff", "Upper Attic" );
            SensorNames.Add( "28-031635fff1ff", "Outdoor South" );
            SensorNames.Add( "28-03163622ebff", "Outer Attic" );
            SensorNames.Add( "28-0416374e05ff", "Upstairs" );

            SensorColors = new Dictionary<string, System.Drawing.Color>();
            SensorColors.Add( "28-0416374e05ff", Color.FromArgb( 128, Color.CornflowerBlue ) );
            SensorColors.Add( "28-031635fb46ff", Color.FromArgb( 128, Color.Blue ) );
            SensorColors.Add( "28-031636b23aff", Color.FromArgb( 128, Color.Green ) );
            SensorColors.Add( "28-0416361444ff", Color.FromArgb( 128, Color.Goldenrod ) );
            SensorColors.Add( "28-031635fff1ff", Color.FromArgb( 128, Color.Plum ) );
            SensorColors.Add( "28-03163622ebff", Color.FromArgb( 128, Color.Red ) );

            clbSeries.DataSource = SensorNames.ToList();
            clbSeries.DisplayMember = "Value";
            for ( int i = 0; i < clbSeries.Items.Count; i++ )
            {
                clbSeries.SetItemChecked( i, true );
            }

            cboTempChartType.Items.Clear();
            cboPowerChartType.Items.Clear();
            foreach ( var chartType in Enum.GetValues( typeof( SeriesChartType ) ) )
            {
                cboTempChartType.Items.Add( chartType );
                cboPowerChartType.Items.Add( chartType );
            }

            cboTempChartType.SelectedItem = SeriesChartType.FastPoint;
            cboPowerChartType.SelectedItem = SeriesChartType.Column;

            SetControlsEnabled();

            // backup every 4 hours
            tmrBackupDatabase.Interval = (int)new TimeSpan( 4, 0, 0 ).TotalMilliseconds;
            tmrBackupDatabase.Enabled = true;

            UpdateChart();
        }

        /// <summary>
        /// Updates the chart.
        /// </summary>
        private void UpdateChart()
        {
            tmrUpdateChart.Enabled = false;
            try
            {

                var startDateTime = DateTime.Now.AddYears( -10 );

                using ( SQLiteConnection conn = new SQLiteConnection( "Data Source=P:\\temperatureDatabase.db;Version=3;Read Only=True" ) )
                {
                    conn.Open();

                    using ( SQLiteCommand cmd = new SQLiteCommand( "SELECT Id, tdatetime, sensorId, temperature FROM temperatureLog WHERE id > @param2 ORDER BY Id", conn ) )
                    {
                        if ( rbLastX.Checked )
                        {
                            double hours = 24;
                            Double.TryParse( tbHours.Text, out hours );

                            startDateTime = DateTime.Now.AddHours( -hours );
                        }

                        //cmd.Parameters.Add( new SQLiteParameter( "@param1", System.Data.DbType.DateTime ) { Value = startDateTime.ToUniversalTime() } );
                        cmd.Parameters.Add( new SQLiteParameter( "@param2", System.Data.DbType.Int32 ) { Value = this.lastTemperatureDataId } );
                        using ( SQLiteDataReader sqlReader = cmd.ExecuteReader( System.Data.CommandBehavior.SingleResult ) )
                        {
                            while ( sqlReader.Read() )
                            {
                                lastTemperatureDataId = Convert.ToInt32( sqlReader[0] );
                                var tDateTime = (DateTime)sqlReader[1];

                                var temperatureData = new TemperatureData
                                {
                                    UtcDateTime = tDateTime,
                                    SensorId = sqlReader[2] as string,
                                    Temperature = (decimal)sqlReader[3]
                                };

                                allTemperatureDataList.Add( temperatureData );
                            }
                        }
                    }
                }

                var temperatureDataList = allTemperatureDataList.Where( a => a.UtcDateTime >= startDateTime.ToUniversalTime() );

                temperatureChart.Series.Clear();

                var seriesDictionary = new Dictionary<string, System.Windows.Forms.DataVisualization.Charting.Series>();

                var chartArea = temperatureChart.ChartAreas[0];
                chartArea.CursorX.IsUserEnabled = true;
                chartArea.CursorY.IsUserEnabled = true;
                chartArea.CursorX.IsUserSelectionEnabled = true;
                chartArea.CursorY.IsUserSelectionEnabled = true;
                chartArea.AxisX.LabelStyle.Format = "g";
                if ( temperatureDataList.Any() )
                {
                    chartArea.AxisY.Minimum = Math.Round( (double)temperatureDataList.Min( a => a.Temperature ) - 1, 0 );
                    chartArea.AxisY.Maximum = Math.Round( (double)temperatureDataList.Max( a => a.Temperature ) + 1, 0 );
                }

                chartArea.AxisY.MinorGrid.Enabled = true;
                chartArea.AxisY.MinorGrid.Interval = .5;
                chartArea.AxisY.MinorGrid.LineColor = Color.LightGray;

                chartArea.AxisX.MinorGrid.Enabled = true;
                chartArea.AxisX.MinorGrid.Interval = 1 / 48;
                chartArea.AxisX.MinorGrid.LineColor = Color.LightGreen;

                chartArea.AxisY2.Minimum = 0;
                chartArea.AxisY2.LineColor = Color.FromArgb( 128, Color.PowderBlue );


                SeriesChartType tempChartType = (SeriesChartType?)cboTempChartType.SelectedItem ?? SeriesChartType.FastLine;


                foreach ( var seriesName in temperatureDataList.Select( a => a.SensorId ).Distinct() )
                {
                    var series = new Series
                    {
                        Name = SensorNames[seriesName],
                        XValueType = ChartValueType.DateTime,
                        ChartType = tempChartType,
                        BorderWidth = 3,
                        Font = this.Font,
                        IsVisibleInLegend = true,
                        YValueType = ChartValueType.Double,
                        IsValueShownAsLabel = false,
                        Color = SensorColors[seriesName]
                    };

                    seriesDictionary.Add( seriesName, series );
                }

                foreach ( var item in seriesDictionary )
                {
                    var sensorId = item.Key;

                    if ( clbSeries.CheckedItems.OfType<KeyValuePair<string, string>>().Any( a => a.Key == sensorId ) )
                    {
                        var series = item.Value;
                        var dataList = temperatureDataList.Where( a => a.SensorId == sensorId ).Select( a => new { a.DateTimeOADate, a.Temperature } ).ToList();
                        series.Points.DataBindXY( dataList.Select( a => a.DateTimeOADate ).ToList(), dataList.Select( a => a.Temperature ).ToList() );
                        temperatureChart.Series.Add( series );
                    }
                }

                if ( cbShowDelta.Checked )
                {
                    var deltaDataList = temperatureDataList.GroupBy( a => a.DateTimeOADate )
                    .Select( a => new
                    {
                        DateTimeOADate = a.Key,
                        SensorId = "Delta1",
                        Sensor1Data = a.FirstOrDefault( x => x.SensorId == "28-03163622ebff" ),
                        Sensor2Data = a.FirstOrDefault( x => x.SensorId == "28-0416361444ff" )
                    } )
                    .Where( a => a.Sensor1Data != null && a.Sensor2Data != null )
                    .Select( a => new
                    {
                        a.DateTimeOADate,
                        Temperature = a.Sensor1Data.Temperature - a.Sensor2Data.Temperature
                    } )
                    .ToList();

                    var deltaSeries = new System.Windows.Forms.DataVisualization.Charting.Series
                    {
                        Name = "Delta1",
                        XValueType = ChartValueType.DateTime,
                        ChartType = tempChartType,
                        BorderWidth = 1,
                        BorderDashStyle = ChartDashStyle.Dash,
                        Font = this.Font,
                        IsVisibleInLegend = true,
                        YValueType = ChartValueType.Double,
                        IsValueShownAsLabel = false,
                        Color = Color.FromArgb( 128, Color.Black ),
                        YAxisType = AxisType.Secondary
                    };

                    deltaSeries.Points.DataBindXY( deltaDataList.Select( a => a.DateTimeOADate ).ToList(), deltaDataList.Select( a => a.Temperature ).ToList() );


                    temperatureChart.Series.Add( deltaSeries );
                }


                if ( temperatureDataList.Any() && ( cbPowerUsageOnPeak.Checked || cbPowerUsageOffPeak.Checked || cbTotalPowerUsage.Checked ) )
                {
                    UpdatePowerUsageChart( DateTime.FromOADate( temperatureDataList.Min( a => a.DateTimeOADate ) ) );
                }

                lblStatus.Text = string.Format( "Updated: {0}", DateTime.Now.ToShortTimeString() );
            }
            finally
            {
                tmrUpdateChart.Enabled = true;
            }
        }

        private void UpdatePowerUsageChart( DateTime firstTemperatureDate )
        {
            SeriesChartType powerChartType = (SeriesChartType?)cboPowerChartType.SelectedItem ?? SeriesChartType.Area;
            DirectoryInfo downloadDir = new DirectoryInfo( @"C:\Users\Mike\Downloads" );
            List<string> downloadFileLines = new List<string>();
            foreach ( var excelFile in downloadDir.EnumerateFiles().Where( a => a.Name.StartsWith( "Excel" ) && a.Extension == ".xls" ).ToList().OrderBy( a => a.LastWriteTime ) )
            {
                downloadFileLines.AddRange( File.ReadAllLines( excelFile.FullName ) );
                var destFile = Path.Combine( @"C:\Users\Mike\Downloads\ImportedExcelFiles\", excelFile.Name );
                if ( File.Exists( destFile ) )
                {
                    File.Delete( destFile );
                }

                File.Move( excelFile.FullName, destFile ); ;
            }

            string apsDailyFile = "P:\\aps_daily.csv";
            string apsDailyFileUpdated = "P:\\aps_daily_Updated.csv";
            powerUseDataList = new List<PowerUseData>();
            List<string> combinedFileLines = new List<string>();

            Dictionary<DateTime, decimal?> avgTemps = new Dictionary<DateTime, decimal?>();

            if ( File.Exists( apsDailyFile ) )
            {
                downloadFileLines.AddRange( File.ReadAllLines( apsDailyFile ) );

                foreach ( var lineEnum in downloadFileLines )
                {
                    string line = lineEnum;
                    if ( line.Contains( "<td>" ) )
                    {
                        line = line.Replace( "&nbsp;", string.Empty );
                        line = line.Replace( "\t", string.Empty );
                        line = line.Replace( "<td>", string.Empty );
                        line = line.Replace( "</td>", "\t" );
                    }
                    // <td>jun 27, 2016</td><td>12:00 AM</td><td>1.767</td><td>&nbsp;</td><td>&nbsp;</td>
                    var parts = line.Split( '\t' );
                    if ( parts.Count() >= 4 && parts[0] != "day" )
                    {
                        DateTime dateTime = DateTime.MinValue;
                        if ( !DateTime.TryParse( parts[0] + " " + parts[1], out dateTime ) )
                        {
                            if ( DateTime.TryParse( parts[0], out dateTime ) )
                            {
                                decimal? avgTemp = null;
                                if ( parts.Count() >= 6 )
                                {
                                    decimal result = 0;
                                    if ( Decimal.TryParse( parts[5], out result ) )
                                    {
                                        avgTemp = result;

                                        if ( !avgTemps.ContainsKey( dateTime ) )
                                        {
                                            avgTemps.Add( dateTime, avgTemp );
                                        }
                                    }

                                    continue;
                                }
                            }
                        }

                        if ( dateTime != DateTime.MinValue )
                        {
                            decimal offpeakKW = 0;
                            Decimal.TryParse( parts[2], out offpeakKW );
                            decimal onpeakKW = 0;
                            Decimal.TryParse( parts[3], out onpeakKW );

                            if ( onpeakKW == 0 && offpeakKW != 0 )
                            {
                                if ( dateTime.TimeOfDay.Hours >= 12 && dateTime.TimeOfDay.Hours < 17 )
                                {
                                    onpeakKW = offpeakKW;
                                    offpeakKW = 0;
                                }
                            }

                            decimal? avgTemp = null;
                            if ( parts.Count() >= 6 )
                            {
                                decimal result = 0;
                                if ( Decimal.TryParse( parts[5], out result ) )
                                {
                                    avgTemp = result;
                                }
                            }

                            if ( offpeakKW != 0 || onpeakKW != 0 )
                            {
                                combinedFileLines.Add( line );

                                var powerUseData = powerUseDataList.FirstOrDefault( x => x.DateTime == dateTime );
                                if ( powerUseData == null )
                                {
                                    powerUseDataList.Add( new PowerUseData
                                    {
                                        DateTime = dateTime,
                                        OffpeakKW = offpeakKW,
                                        OnpeakKW = onpeakKW,
                                        AvgTemp = avgTemp
                                    } );
                                }
                            }
                        }
                    }
                }
            }

            foreach ( var item in powerUseDataList.Where( a => !a.AvgTemp.HasValue ) )
            {
                item.AvgTemp = avgTemps.Where( a => a.Key == item.DateTime.Date && a.Value.HasValue ).Select( a => a.Value ).FirstOrDefault();
            }

            powerUseDataList = powerUseDataList.OrderBy( a => a.DateTime ).ToList();

            File.WriteAllLines( apsDailyFileUpdated, powerUseDataList.Select( a => string.Format( "{0}\t{1}\t{2}\t{3}\t{4}\t{5}", a.DateTime.Date.ToString( "d" ), a.DateTime.TimeOfDay, a.OffpeakKW, a.OnpeakKW, null, a.AvgTemp ) ) );

            if ( File.ReadAllText( apsDailyFile ) != File.ReadAllText( apsDailyFileUpdated ) )
            {
                File.Delete( apsDailyFile );
                File.Move( apsDailyFileUpdated, apsDailyFile );
            }
            else
            {
                File.Delete( apsDailyFileUpdated );
            }

            powerUseDataList = powerUseDataList.OrderBy( a => a.DateTime ).GroupBy( a => a.DateTime.Date ).Select( a => new PowerUseData
            {
                DateTime = a.Key,
                OnpeakKW = a.Sum( x => x.OnpeakKW ),
                OffpeakKW = a.Sum( x => x.OffpeakKW ),
                AvgTemp = a.Where( x => x.AvgTemp.HasValue ).Max( x => x.AvgTemp )
            } ).ToList();

            if ( powerUseDataList.Any() )
            {
                temperatureChart.ChartAreas["BarChartArea"].AxisY.Minimum = 0;

                if ( cbTotalPowerUsage.Checked )
                {
                    temperatureChart.ChartAreas["BarChartArea"].AxisY.Maximum = Math.Round( (double)powerUseDataList.Max( a => a.TotalKW ) + 1, 0 );
                }
                else if ( cbPowerUsageOffPeak.Checked )
                {
                    temperatureChart.ChartAreas["BarChartArea"].AxisY.Maximum = Math.Round( (double)powerUseDataList.Max( a => a.OffpeakKW ) + 1, 0 );
                }
                else
                {
                    temperatureChart.ChartAreas["BarChartArea"].AxisY.Maximum = Math.Round( (double)powerUseDataList.Max( a => a.OnpeakKW ) + 1, 0 );
                }
            }

            var powerUseDataListLastYear = powerUseDataList.Where( a => a.DateTime.AddYears( 1 ) <= DateTime.Now && a.DateTime.Year == ( DateTime.Now.Year - 1 ) && a.DateTime >= firstTemperatureDate.AddYears( -1 ) ).ToList();
            var powerUseDataListCurrentYear = powerUseDataList.Where( a => a.DateTime >= firstTemperatureDate ).ToList();

            if ( cbPowerUsageOffPeak.Checked )
            {
                var powerUseSeriesOffpeak = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "PowerUsage_Offpeak",
                    XValueType = ChartValueType.Date,
                    ChartType = powerChartType,
                    Font = this.Font,
                    IsVisibleInLegend = true,
                    YValueType = ChartValueType.Double,
                    IsValueShownAsLabel = true,
                    Color = Color.FromArgb( 128, Color.Green ),
                    ChartArea = "BarChartArea"
                };

                var powerUseSeriesOffpeakLastYear = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "PowerUsage_OffpeakLastYear",
                    XValueType = ChartValueType.Date,
                    ChartType = powerChartType,
                    Font = this.Font,
                    IsVisibleInLegend = true,
                    YValueType = ChartValueType.Double,
                    IsValueShownAsLabel = true,
                    Color = Color.FromArgb( 128, Color.Blue ),
                    ChartArea = "BarChartArea"
                };

                powerUseSeriesOffpeak.Points.DataBindXY( powerUseDataListCurrentYear.Select( a => a.DateTimeOADate ).ToList(), powerUseDataListCurrentYear.Select( a => a.OffpeakKW ).ToList() );
                powerUseSeriesOffpeakLastYear.Points.DataBindXY( powerUseDataListLastYear.Select( a => a.DateTime.AddYears( 1 ).ToOADate() ).ToList(), powerUseDataListLastYear.Select( a => a.OffpeakKW ).ToList() );

                temperatureChart.Series.Add( powerUseSeriesOffpeak );
                temperatureChart.Series.Add( powerUseSeriesOffpeakLastYear );
            }

            if ( cbPowerUsageOnPeak.Checked )
            {
                var powerUseSeriesOnpeak = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "PowerUsage_Onpeak",
                    XValueType = ChartValueType.Date,
                    ChartType = powerChartType,
                    Font = this.Font,
                    IsVisibleInLegend = true,
                    YValueType = ChartValueType.Double,
                    IsValueShownAsLabel = true,
                    Color = Color.FromArgb( 128, Color.LightGreen ),
                    ChartArea = "BarChartArea"
                };

                var powerUseSeriesOnpeakLastYear = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "PowerUsage_OnpeakLastYear",
                    XValueType = ChartValueType.Date,
                    ChartType = powerChartType,
                    Font = this.Font,
                    IsVisibleInLegend = true,
                    YValueType = ChartValueType.Double,
                    IsValueShownAsLabel = true,
                    Color = Color.FromArgb( 128, Color.LightBlue ),
                    ChartArea = "BarChartArea"
                };

                powerUseSeriesOnpeak.Points.DataBindXY( powerUseDataListCurrentYear.Select( a => a.DateTimeOADate ).ToList(), powerUseDataListCurrentYear.Select( a => a.OnpeakKW ).ToList() );

                powerUseSeriesOnpeakLastYear.Points.DataBindXY( powerUseDataListLastYear.Select( a => a.DateTime.AddYears( 1 ).ToOADate() ).ToList(), powerUseDataListLastYear.Select( a => a.OnpeakKW ).ToList() );

                temperatureChart.Series.Add( powerUseSeriesOnpeak );
                temperatureChart.Series.Add( powerUseSeriesOnpeakLastYear );
            }


            if ( cbTotalPowerUsage.Checked )
            {
                var powerUseSeriesTotal = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "PowerUsage_Total",
                    XValueType = ChartValueType.Date,
                    ChartType = powerChartType,
                    Font = this.Font,
                    IsVisibleInLegend = true,
                    YValueType = ChartValueType.Double,
                    IsValueShownAsLabel = true,
                    Color = Color.FromArgb( 128, Color.Lime ),
                    ChartArea = "BarChartArea"
                };

                var powerUseSeriesTotalLastYear = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "PowerUsage_TotalLastYear",
                    XValueType = ChartValueType.Date,
                    ChartType = powerChartType,
                    BorderDashStyle = ChartDashStyle.Solid,
                    Font = this.Font,
                    IsVisibleInLegend = true,
                    YValueType = ChartValueType.Double,
                    IsValueShownAsLabel = true,
                    Color = Color.FromArgb( 128, Color.DarkBlue ),
                    ChartArea = "BarChartArea"
                };

                powerUseSeriesTotal.Points.DataBindXY( powerUseDataListCurrentYear.Select( a => a.DateTimeOADate ).ToList(), powerUseDataListCurrentYear.Select( a => a.TotalKW ).ToList() );
                powerUseSeriesTotalLastYear.Points.DataBindXY( powerUseDataListLastYear.Select( a => a.DateTime.AddYears( 1 ).ToOADate() ).ToList(), powerUseDataListLastYear.Select( a => a.TotalKW ).ToList() );

                temperatureChart.Series.Add( powerUseSeriesTotalLastYear );
                temperatureChart.Series.Add( powerUseSeriesTotal );
            }
        }

        /// <summary>
        /// Handles the GetToolTipText event of the temperatureChart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ToolTipEventArgs"/> instance containing the event data.</param>
        void temperatureChart_GetToolTipText( object sender, ToolTipEventArgs e )
        {
            switch ( e.HitTestResult.ChartElementType )
            {
                case ChartElementType.DataPoint:
                    var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    string tooltipFormat;
                    if ( e.HitTestResult.Series.Name.StartsWith( "PowerUsage" ) )
                    {
                        tooltipFormat = "{1}KWh @{0:d}\nAvg Temp:{5}F";
                    }
                    else
                    {
                        tooltipFormat = "{1}F @ {0:g}\nSensor: {2}\n{3}F/hr_last60\n{4}F/hr_last10";
                    }


                    var sensorId = SensorNames.FirstOrDefault( a => a.Value == e.HitTestResult.Series.Name ).Key;
                    decimal degreesPerMinute60 = GetSlopeForWindow( dataPoint, sensorId, 60 );
                    decimal degreesPerMinute10 = GetSlopeForWindow( dataPoint, sensorId, 10 );

                    string tooltipText = string.Format(
                        tooltipFormat,
                        e.HitTestResult.Series.Name.Contains( "LastYear" )
                            ? DateTime.FromOADate( dataPoint.XValue ).AddYears( -1 )
                            : DateTime.FromOADate( dataPoint.XValue ),
                        dataPoint.YValues[0].ToString( "N1" ),
                        e.HitTestResult.Series.Name,
                        ( degreesPerMinute60 * 60 ).ToString( "F2" ),
                        ( degreesPerMinute10 * 60 ).ToString( "F2" ),
                        e.HitTestResult.Series.Name.Contains( "LastYear" )
                            ? powerUseDataList.Where( a => a.DateTime == DateTime.FromOADate( dataPoint.XValue ).Date.AddYears( -1 ) ).Select( a => a.AvgTemp ).FirstOrDefault()
                            : powerUseDataList.Where( a => a.DateTime == DateTime.FromOADate( dataPoint.XValue ).Date ).Select( a => a.AvgTemp ).FirstOrDefault()
                        );

                    chartToolTip.UseAnimation = true;
                    chartToolTip.UseFading = true;
                    chartToolTip.BackColor = Color.Black;
                    chartToolTip.ForeColor = Color.LightGreen;
                    chartToolTip.Show( tooltipText, temperatureChart, e.X, e.Y + 20 );
                    break;
                default:
                    chartToolTip.Hide( temperatureChart );
                    break;
            }
        }

        private decimal GetSlopeForWindow( DataPoint dataPoint, string sensorId, int timeWindowMinutes )
        {
            var degreesPerMinute = 0.0M;
            var xMinutesAgo = DateTime.FromOADate( dataPoint.XValue ).AddMinutes( -timeWindowMinutes ).ToOADate();
            var dataXMinutesAgo = allTemperatureDataList.Where( a => a.SensorId == sensorId && a.DateTimeOADate <= xMinutesAgo ).OrderByDescending( a => a.DateTimeOADate ).FirstOrDefault();
            if ( dataXMinutesAgo != null )
            {
                degreesPerMinute = ( ( (decimal)dataPoint.YValues[0] - dataXMinutesAgo.Temperature ) / timeWindowMinutes );
            }

            return degreesPerMinute;
        }

        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void timer1_Tick( object sender, EventArgs e )
        {
            try
            {
                UpdateChart();
                SaveChartImage();
            }
            catch ( Exception ex )
            {
                lblStatus.Text = ex.Message;
            }
        }

        private void SaveChartImage()
        {
            if (!cbSaveChartJPGs.Checked)
            {
                return;
            }

            DateTime lastChartImageDateTime = DateTime.MinValue;
            string imageFile = "D:\\OneDrive_Folder\\OneDrive\\TemperatureData\\Chart.jpg";
            if ( File.Exists( imageFile ) )
            {
                lastChartImageDateTime = File.GetLastWriteTime( imageFile );
            }

            if ( ( DateTime.Now - lastChartImageDateTime ).TotalMinutes > 60 )
            {
                var origState = this.WindowState;
                var origLastXChecked = rbLastX.Checked;
                var origLastXHours = tbHours.Text;
                //var origSize = new Point( temperatureChart.Width, temperatureChart.Height );

                try
                {
                    this.WindowState = FormWindowState.Normal;
                    //temperatureChart.Width = 1920;
                    //temperatureChart.Height = 1200;

                    rbAll.Checked = true;
                    UpdateChart();
                    temperatureChart.SaveImage( imageFile, ChartImageFormat.Jpeg );

                    tbHours.Text = "24";
                    rbLastX.Checked = true;
                    UpdateChart();
                    string imageFileLast24 = "D:\\OneDrive_Folder\\OneDrive\\TemperatureData\\ChartLast24.jpg";
                    temperatureChart.SaveImage( imageFileLast24, ChartImageFormat.Jpeg );


                    tbHours.Text = ( 24 * 7 ).ToString();
                    UpdateChart();
                    string imageFileLast7days = "D:\\OneDrive_Folder\\OneDrive\\TemperatureData\\ChartLast7days.jpg";
                    temperatureChart.SaveImage( imageFileLast7days, ChartImageFormat.Jpeg );
                }
                finally
                {
                    //  temperatureChart.Width = origSize.X;
                    //temperatureChart.Height = origSize.Y;
                    rbLastX.Checked = origLastXChecked;
                    tbHours.Text = origLastXHours;
                    UpdateChart();
                    this.WindowState = origState;
                }
            }
        }

        /// <summary>
        /// Handles the Tick event of the tmrBackupDatabase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmrBackupDatabase_Tick( object sender, EventArgs e )
        {
            if ( cbBackupDatabase.Checked )
            {
                BackupDatabase();
            }
        }

        /// <summary>
        /// Backs up the database.
        /// </summary>
        private void BackupDatabase()
        {
            try
            {
                File.Copy( "P:\\MikeTemperatureReader.py", "D:\\OneDrive_Folder\\OneDrive\\TemperatureData\\MikeTemperatureReader.py", true );

                this.Update();
                DateTime lastBackupDateTime = DateTime.MinValue;
                string backupFile = string.Format( "D:\\OneDrive_Folder\\OneDrive\\TemperatureData\\temperatureDatabase_{0}.db", DateTime.Now.ToString( "o" ).Replace( ":", "_" ) );
                string lastBackupTimeFile = "D:\\OneDrive_Folder\\OneDrive\\TemperatureData\\_LastBackupTime";
                if ( File.Exists( lastBackupTimeFile ) )
                {
                    lastBackupDateTime = File.GetLastWriteTime( lastBackupTimeFile );
                }

                if ( ( DateTime.Now - lastBackupDateTime ).TotalHours < 4 )
                {
                    return;
                }

                SQLiteBackupCallback sqlBackupCallback = ( source, sourceName, destination, destinationName, pages, remainingPages, totalPages, retry ) =>
                {
                    Debug.WriteLine( "{0} {1} {2}", pages, remainingPages, totalPages );
                    return true;
                };

                lblStatus.Text = "Backing up database...";
                lblStatus.Update();

                using ( var source = new SQLiteConnection( "Data Source=P:\\temperatureDatabase.db; Version=3;" ) )
                {
                    using ( var destination = new SQLiteConnection( "Data Source=" + backupFile + "; Version=3;" ) )
                    {
                        source.Open();
                        destination.Open();
                        source.BackupDatabase( destination, "main", "main", -1, sqlBackupCallback, 0 );
                    }

                    var backupFileZipName = backupFile + ".zip";

                    using ( var zip = ZipFile.Open( backupFileZipName, ZipArchiveMode.Create ) )
                    {
                        zip.CreateEntryFromFile( backupFile, Path.GetFileName( backupFile ) );
                    }
                }

                File.Delete( backupFile );
                File.Create( lastBackupTimeFile );

                lblStatus.Text = "";
            }
            catch ( Exception ex )
            {
                lblStatus.Text = ex.Message;
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the rbAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void rbAll_CheckedChanged( object sender, EventArgs e )
        {
            SetControlsEnabled();
        }

        /// <summary>
        /// Sets the controls enabled.
        /// </summary>
        private void SetControlsEnabled()
        {
            tbHours.Enabled = rbLastX.Checked;
            lbHours.Enabled = rbLastX.Checked;
        }

        private void btnRefresh_Click( object sender, EventArgs e )
        {
            UpdateChart();
            SaveChartImage();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TemperatureData
    {
        public DateTime UtcDateTime { get; set; }
        public double DateTimeOADate
        {
            get
            {
                return UtcDateTime.ToLocalTime().ToOADate();
            }
        }

        public string SensorId;
        public Decimal Temperature;
    }

    public class PowerUseData
    {
        public DateTime DateTime { get; set; }
        public double DateTimeOADate
        {
            get
            {
                return DateTime.ToOADate();
            }
        }
        public decimal OffpeakKW { get; set; }
        public decimal OnpeakKW { get; set; }
        public decimal? AvgTemp { get; set; }

        public decimal TotalKW
        {
            get
            {
                return OffpeakKW + OnpeakKW;
            }
        }

        public override string ToString()
        {
            return string.Format( "{0} TotalKW:{1} AveTemp:{2}", this.DateTime, this.TotalKW, this.AvgTemp );
        }
    }
}
