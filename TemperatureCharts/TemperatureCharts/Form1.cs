using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TemperatureCharts
{
    /// <summary>
    /// 
    /// </summary>
    public struct TemperatureData
    {
        public Double DateTimeOADate;
        public string SensorId;
        public Decimal Temperature;
    }

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

        /// <summary>
        /// Handles the Load event of the Form1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Form1_Load( object sender, EventArgs e )
        {

            cboChartType.Items.Clear();
            foreach ( var chartType in Enum.GetValues( typeof( SeriesChartType ) ) )
            {
                cboChartType.Items.Add( chartType );
            }

            cboChartType.SelectedItem = SeriesChartType.FastLine;

            cboPalette.Items.Clear();
            foreach ( var chartType in Enum.GetValues( typeof( ChartColorPalette ) ) )
            {
                cboPalette.Items.Add( chartType );
            }
            
            cboPalette.SelectedItem = ChartColorPalette.Pastel;

            SetControlsEnabled();

            BackupDatabase();

            UpdateChart();

            // backup every 4 hours
            tmrBackupDatabase.Interval = (int)new TimeSpan( 4, 0, 0 ).TotalMilliseconds;
            tmrBackupDatabase.Enabled = true;
        }

        /// <summary>
        /// Updates the chart.
        /// </summary>
        private void UpdateChart()
        {
            this.Text = string.Format( "Last Update: {0}", DateTime.Now.ToShortTimeString() );
            var temperatureDataList = new List<TemperatureData>();

            using ( SQLiteConnection conn = new SQLiteConnection( "Data Source=P:\\temperatureDatabase.db;Version=3;Read Only=True" ) )
            {
                conn.Open();

                using ( SQLiteCommand cmd = new SQLiteCommand( "SELECT * FROM temperatureLog WHERE tdatetime > @param1 and Id > @param2 ORDER BY Id", conn ) )
                {

                    var startDateTime = DateTime.MinValue;
                    if ( rbLastX.Checked )
                    {
                        double hours = 24;
                        Double.TryParse( tbHours.Text, out hours );

                        startDateTime = DateTime.Now.AddHours( -hours );
                    }

                    cmd.Parameters.Add( new SQLiteParameter( "@param1", System.Data.DbType.DateTime ) { Value = startDateTime.ToUniversalTime() } );
                    cmd.Parameters.Add( new SQLiteParameter( "@param2", System.Data.DbType.Int32 ) { Value = 0 } );
                    using ( SQLiteDataReader sqlReader = cmd.ExecuteReader() )
                    {
                        while ( sqlReader.Read() )
                        {
                            var tDateTime = (DateTime)sqlReader["tdatetime"];

                            temperatureDataList.Add( new TemperatureData
                            {
                                DateTimeOADate = tDateTime.ToLocalTime().ToOADate(),
                                SensorId = sqlReader["sensorId"] as string,
                                Temperature = Convert.ToDecimal( sqlReader["temperature"] )
                            } );
                        }
                    }
                }

                conn.Close();
            }

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

            SeriesChartType? chartType = (SeriesChartType?)cboChartType.SelectedItem;

            foreach ( var seriesName in temperatureDataList.Select( a => a.SensorId ).Distinct() )
            {
                var series = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = seriesName,
                    XValueType = ChartValueType.DateTime,
                    ChartType = chartType ?? SeriesChartType.Point,
                    BorderWidth = 3,
                    Font = this.Font,
                    IsVisibleInLegend = false,
                    YValueType = ChartValueType.Double
                };

                seriesDictionary.Add( seriesName, series );
            }

            foreach ( var item in seriesDictionary )
            {
                var series = item.Value;
                var sensorId = item.Key;
                var dataList = temperatureDataList.Where( a => a.SensorId == sensorId ).Select( a => new { a.DateTimeOADate, a.Temperature } ).ToList();
                series.Points.DataBind( dataList, "DateTimeOADate", "Temperature", "" );

                //var outputSeries = temperatureChart.Series.FirstOrDefault( a => a.Name == "stats_" + seriesItem.Key );
                //var period = Math.Min( dataList.Count() / 10, 36 );
                //temperatureChart.DataManipulator.FinancialFormula( FinancialFormula.WeightedMovingAverage, period.ToString(), seriesItem.Value, outputSeries );

                temperatureChart.Series.Add( series );
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
                    e.Text = string.Format(
@"{1}F @ {0}
Sensor: {2}",
                        DateTime.FromOADate( dataPoint.XValue ).ToShortTimeString(),
                        dataPoint.YValues[0].ToString( "N1" ),
                        e.HitTestResult.Series.Name );
                    break;
            }

        }

        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void timer1_Tick( object sender, EventArgs e )
        {
            UpdateChart();
        }

        /// <summary>
        /// Handles the Tick event of the tmrBackupDatabase control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tmrBackupDatabase_Tick( object sender, EventArgs e )
        {
            BackupDatabase();
        }

        /// <summary>
        /// Backs up the database.
        /// </summary>
        private static void BackupDatabase()
        {
            try
            {
                string backupFile = string.Format( "D:\\OneDrive_Folder\\OneDrive\\TemperatureData\\temperatureDatabase_{0}.db", DateTime.Now.ToString( "o" ).Replace( ":", "_" ) );

                SQLiteBackupCallback sqlBackupCallback = (source, sourceName, destination, destinationName, pages, remainingPages, totalPages, retry) =>
                {
                    Debug.WriteLine( "{0} {1} {2}", pages, remainingPages, totalPages );
                    return true;
                };
                
                using ( var source = new SQLiteConnection( "Data Source=P:\\temperatureDatabase.db; Version=3;" ) )
                {
                    using ( var destination = new SQLiteConnection( "Data Source=" + backupFile + "; Version=3;" ) )
                    {
                        source.Open();
                        destination.Open();
                        source.BackupDatabase( destination, "main", "main", -1, sqlBackupCallback, 0 );
                        source.Close();
                        destination.Close();
                    }

                    var backupFileZipName = backupFile + ".zip";

                    using ( var zip = ZipFile.Open( backupFileZipName, ZipArchiveMode.Create ) )
                    {
                        zip.CreateEntryFromFile( backupFile, Path.GetFileName( backupFile ) );
                    }
                }

                File.Delete( backupFile );
            }
            catch ( Exception ex )
            {
                Debug.WriteLine( ex );
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the tbHours control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void tbHours_TextChanged( object sender, EventArgs e )
        {
            if ( !string.IsNullOrWhiteSpace( tbHours.Text ) )
            {
                UpdateChart();
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

            if ( ( sender as RadioButton ).Checked == true )
            {
                UpdateChart();
            }
        }

        /// <summary>
        /// Sets the controls enabled.
        /// </summary>
        private void SetControlsEnabled()
        {
            tbHours.Enabled = rbLastX.Checked;
            lbHours.Enabled = rbLastX.Checked;
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cboChartType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cboChartType_SelectedIndexChanged( object sender, EventArgs e )
        {
            UpdateChart();

            
        }

        private void cboPalette_SelectedIndexChanged( object sender, EventArgs e )
        {
            temperatureChart.Palette = (ChartColorPalette)cboPalette.SelectedItem;
        }
    }
}
