using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TemperatureCharts
{
    public struct TemperatureData
    {
        public DateTime DateTime;
        public string SensorId;
        public Decimal Temperature;
    }

    
    
    public partial class Form1 : Form
    {
        static string sensor1 = "28-031635fb46ff";
        static string sensor2 = "28-031636b23aff";
        static string sensor3 = "28-0416361444ff";
        public int _lastId;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load( object sender, EventArgs e )
        {
            BackupDatabase();
            
            UpdateChart();

            tmrBackupDatabase.Interval = (int)new TimeSpan( 0, 30, 0 ).TotalMilliseconds;
            tmrBackupDatabase.Enabled = true;
        }

        private void button1_Click( object sender, EventArgs e )
        {
            UpdateChart();
        }

        private void UpdateChart()
        {
            this.Text = string.Format( "Last Update: {0}", DateTime.Now.ToShortTimeString() );
            
            SQLiteConnection conn = new SQLiteConnection( "Data Source=P:\\temperatureDatabase.db;Version=3;" );
            

            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand( "SELECT * FROM temperatureLog ORDER BY Id", conn );
            //SQLiteCommand cmd = new SQLiteCommand( "SELECT rowid, * FROM temperatureLog where Id > @param1 ORDER BY Id", conn );
            //cmd.Parameters.Add( new SQLiteParameter( "@param1", _lastId ) );
            SQLiteDataReader sqlReader = cmd.ExecuteReader();
            var temperatureDataList = new List<TemperatureData>();

            //var dataTable = sqlReader.GetSchemaTable();

            //SQLiteDataAdapter sqlAdapter = new SQLiteDataAdapter( cmd );
            

            while ( sqlReader.Read() )
            {
                var tDateTime = (DateTime)sqlReader["tdatetime"];

                temperatureDataList.Add( new TemperatureData
                {
                    DateTime = tDateTime.ToLocalTime(),
                    SensorId = sqlReader["sensorId"] as string,
                    Temperature = Convert.ToDecimal( sqlReader["temperature"] )
                } );
            }

            temperatureDataList = temperatureDataList.Where( a => a.Temperature < 130 ).ToList();

            conn.Close();

            temperatureChart.Series.Clear();

            var seriesDictionary = new Dictionary<string, System.Windows.Forms.DataVisualization.Charting.Series>();

            var chartArea = temperatureChart.ChartAreas[0];
            chartArea.AxisX.LabelStyle.Format = "g";
            chartArea.AxisY.Minimum = Math.Round( (double)temperatureDataList.Min( a => a.Temperature )-1, 0 );
            chartArea.AxisY.Maximum = Math.Round( (double)temperatureDataList.Max( a => a.Temperature )+1, 0 );

            foreach ( var seriesName in temperatureDataList.Select( a => a.SensorId ).Distinct() )
            {
                var series = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = seriesName,
                    XValueType = ChartValueType.DateTime,
                    ChartType = SeriesChartType.Point,
                    Font = this.Font,
                    ToolTip = "#VALY, #VALX",
                    LabelFormat = "g",
                    LabelToolTip = "#VALY, #VALX",
                    IsVisibleInLegend = false,
                    YValueType = ChartValueType.Double
                };

                seriesDictionary.Add( seriesName, series );
                temperatureChart.Series.Add( series );

            }

            foreach ( var item in temperatureDataList.OrderBy( a => a.DateTime ) )
            {
                var point = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                point.SetValueXY( item.DateTime.ToOADate(), item.Temperature );
                point.ToolTip = string.Format( "{2}: {0}F @ {1}", item.Temperature, item.DateTime, item.SensorId );
                point.LabelToolTip = point.ToolTip;
                seriesDictionary[item.SensorId].Points.Add( point );
            }

            temperatureChart.GetToolTipText += temperatureChart_GetToolTipText;
        }

        void temperatureChart_GetToolTipText( object sender, ToolTipEventArgs e )
        {
            switch ( e.HitTestResult.ChartElementType )
            {
                case ChartElementType.DataPoint:
                    var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];
                    e.Text = string.Format(
@"{1}F @ {0}
Sensor: {2}", 
                        DateTime.FromOADate(dataPoint.XValue).ToShortTimeString(),
                        dataPoint.YValues[0].ToString("N1"),
                        e.HitTestResult.Series.Name );
                    break;
            }

        }

        private void timer1_Tick( object sender, EventArgs e )
        {
            UpdateChart();
        }

        private void tmrBackupDatabase_Tick( object sender, EventArgs e )
        {
            BackupDatabase();
        }

        private static void BackupDatabase()
        {
            try
            {
                string backupFile = string.Format( "D:\\OneDrive_Folder\\OneDrive\\TemperatureData\\temperatureDatabase_{0}.db", DateTime.Now.ToString( "o" ).Replace( ":", "_" ) );
                
                using ( var source = new SQLiteConnection( "Data Source=P:\\temperatureDatabase.db; Version=3;" ) )
                using ( var destination = new SQLiteConnection( "Data Source=" + backupFile + "; Version=3;" ) )
                {
                    source.Open();
                    destination.Open();
                    source.BackupDatabase( destination, "main", "main", -1, null, 0 );
                    source.Close();
                    destination.Close();
                }
            }
            catch ( Exception ex )
            {
                Debug.WriteLine( ex );
            }
        }
    }
}
