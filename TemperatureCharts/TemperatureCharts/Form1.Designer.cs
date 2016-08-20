namespace TemperatureCharts
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(42520.713888888888D, "75.45,0");
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(42520.714583333334D, "94.55,0");
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.temperatureChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tmrUpdateChart = new System.Windows.Forms.Timer(this.components);
            this.tmrBackupDatabase = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbHours = new System.Windows.Forms.Label();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.rbLastX = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboPowerChartType = new System.Windows.Forms.ComboBox();
            this.cboTempChartType = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.clbSeries = new System.Windows.Forms.CheckedListBox();
            this.cbPowerUsageOnPeak = new System.Windows.Forms.CheckBox();
            this.cbShowDelta = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cbPowerUsageOffPeak = new System.Windows.Forms.CheckBox();
            this.cbTotalPowerUsage = new System.Windows.Forms.CheckBox();
            this.cbBackupDatabase = new System.Windows.Forms.CheckBox();
            this.cbSaveChartJPGs = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureChart)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // temperatureChart
            // 
            this.temperatureChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AlignWithChartArea = "BarChartArea";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.Name = "LinesChartArea";
            chartArea2.AlignWithChartArea = "LinesChartArea";
            chartArea2.Name = "BarChartArea";
            this.temperatureChart.ChartAreas.Add(chartArea1);
            this.temperatureChart.ChartAreas.Add(chartArea2);
            legend1.Name = "Sensors";
            legend1.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.temperatureChart.Legends.Add(legend1);
            this.temperatureChart.Location = new System.Drawing.Point(0, 138);
            this.temperatureChart.Margin = new System.Windows.Forms.Padding(0);
            this.temperatureChart.Name = "temperatureChart";
            this.temperatureChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series1.ChartArea = "LinesChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.IsValueShownAsLabel = true;
            series1.IsXValueIndexed = true;
            series1.Label = "#VALY F @ #VALX{h:mm tt }";
            series1.Legend = "Sensors";
            series1.Name = "Sensor1";
            dataPoint1.IsVisibleInLegend = false;
            dataPoint2.IsVisibleInLegend = false;
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.ChartArea = "BarChartArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series2.Legend = "Sensors";
            series2.Name = "Series2";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            this.temperatureChart.Series.Add(series1);
            this.temperatureChart.Series.Add(series2);
            this.temperatureChart.Size = new System.Drawing.Size(1018, 491);
            this.temperatureChart.TabIndex = 0;
            this.temperatureChart.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.temperatureChart_GetToolTipText);
            // 
            // tmrUpdateChart
            // 
            this.tmrUpdateChart.Enabled = true;
            this.tmrUpdateChart.Interval = 300000;
            this.tmrUpdateChart.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tmrBackupDatabase
            // 
            this.tmrBackupDatabase.Tick += new System.EventHandler(this.tmrBackupDatabase_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbHours);
            this.groupBox1.Controls.Add(this.tbHours);
            this.groupBox1.Controls.Add(this.rbLastX);
            this.groupBox1.Controls.Add(this.rbAll);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(244, 84);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // lbHours
            // 
            this.lbHours.AutoSize = true;
            this.lbHours.Location = new System.Drawing.Point(105, 41);
            this.lbHours.Name = "lbHours";
            this.lbHours.Size = new System.Drawing.Size(34, 13);
            this.lbHours.TabIndex = 6;
            this.lbHours.Text = "hours";
            // 
            // tbHours
            // 
            this.tbHours.Location = new System.Drawing.Point(60, 38);
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(35, 21);
            this.tbHours.TabIndex = 5;
            this.tbHours.Text = "24";
            this.tbHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // rbLastX
            // 
            this.rbLastX.AutoSize = true;
            this.rbLastX.Checked = true;
            this.rbLastX.Location = new System.Drawing.Point(15, 39);
            this.rbLastX.Name = "rbLastX";
            this.rbLastX.Size = new System.Drawing.Size(45, 17);
            this.rbLastX.TabIndex = 4;
            this.rbLastX.TabStop = true;
            this.rbLastX.Text = "Last";
            this.rbLastX.UseVisualStyleBackColor = true;
            this.rbLastX.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(15, 19);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 3;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboPowerChartType);
            this.groupBox2.Controls.Add(this.cboTempChartType);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(262, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 84);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chart";
            // 
            // cboPowerChartType
            // 
            this.cboPowerChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPowerChartType.FormattingEnabled = true;
            this.cboPowerChartType.Location = new System.Drawing.Point(16, 46);
            this.cboPowerChartType.Name = "cboPowerChartType";
            this.cboPowerChartType.Size = new System.Drawing.Size(208, 21);
            this.cboPowerChartType.TabIndex = 1;
            // 
            // cboTempChartType
            // 
            this.cboTempChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTempChartType.FormattingEnabled = true;
            this.cboTempChartType.Location = new System.Drawing.Point(16, 19);
            this.cboTempChartType.Name = "cboTempChartType";
            this.cboTempChartType.Size = new System.Drawing.Size(208, 21);
            this.cboTempChartType.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(9, 108);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(46, 14);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status:";
            // 
            // clbSeries
            // 
            this.clbSeries.FormattingEnabled = true;
            this.clbSeries.Location = new System.Drawing.Point(507, 12);
            this.clbSeries.Name = "clbSeries";
            this.clbSeries.Size = new System.Drawing.Size(138, 116);
            this.clbSeries.TabIndex = 1;
            // 
            // cbPowerUsageOnPeak
            // 
            this.cbPowerUsageOnPeak.AutoSize = true;
            this.cbPowerUsageOnPeak.Location = new System.Drawing.Point(669, 15);
            this.cbPowerUsageOnPeak.Name = "cbPowerUsageOnPeak";
            this.cbPowerUsageOnPeak.Size = new System.Drawing.Size(158, 17);
            this.cbPowerUsageOnPeak.TabIndex = 6;
            this.cbPowerUsageOnPeak.Text = "Show OnPeak Power Usage";
            this.cbPowerUsageOnPeak.UseVisualStyleBackColor = true;
            // 
            // cbShowDelta
            // 
            this.cbShowDelta.AutoSize = true;
            this.cbShowDelta.Location = new System.Drawing.Point(669, 111);
            this.cbShowDelta.Name = "cbShowDelta";
            this.cbShowDelta.Size = new System.Drawing.Size(80, 17);
            this.cbShowDelta.TabIndex = 7;
            this.cbShowDelta.Text = "Show Delta";
            this.cbShowDelta.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(913, 8);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(92, 40);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // cbPowerUsageOffPeak
            // 
            this.cbPowerUsageOffPeak.AutoSize = true;
            this.cbPowerUsageOffPeak.Location = new System.Drawing.Point(669, 38);
            this.cbPowerUsageOffPeak.Name = "cbPowerUsageOffPeak";
            this.cbPowerUsageOffPeak.Size = new System.Drawing.Size(160, 17);
            this.cbPowerUsageOffPeak.TabIndex = 9;
            this.cbPowerUsageOffPeak.Text = "Show OffPeak Power Usage";
            this.cbPowerUsageOffPeak.UseVisualStyleBackColor = true;
            // 
            // cbTotalPowerUsage
            // 
            this.cbTotalPowerUsage.AutoSize = true;
            this.cbTotalPowerUsage.Checked = true;
            this.cbTotalPowerUsage.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTotalPowerUsage.Location = new System.Drawing.Point(669, 62);
            this.cbTotalPowerUsage.Name = "cbTotalPowerUsage";
            this.cbTotalPowerUsage.Size = new System.Drawing.Size(145, 17);
            this.cbTotalPowerUsage.TabIndex = 10;
            this.cbTotalPowerUsage.Text = "Show Total Power Usage";
            this.cbTotalPowerUsage.UseVisualStyleBackColor = true;
            // 
            // cbBackupDatabase
            // 
            this.cbBackupDatabase.AutoSize = true;
            this.cbBackupDatabase.Location = new System.Drawing.Point(896, 54);
            this.cbBackupDatabase.Name = "cbBackupDatabase";
            this.cbBackupDatabase.Size = new System.Drawing.Size(109, 17);
            this.cbBackupDatabase.TabIndex = 11;
            this.cbBackupDatabase.Text = "Backup Database";
            this.cbBackupDatabase.UseVisualStyleBackColor = true;
            // 
            // cbSaveChartJPGs
            // 
            this.cbSaveChartJPGs.AutoSize = true;
            this.cbSaveChartJPGs.Location = new System.Drawing.Point(896, 77);
            this.cbSaveChartJPGs.Name = "cbSaveChartJPGs";
            this.cbSaveChartJPGs.Size = new System.Drawing.Size(106, 17);
            this.cbSaveChartJPGs.TabIndex = 12;
            this.cbSaveChartJPGs.Text = "Save Chart JPGs";
            this.cbSaveChartJPGs.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 638);
            this.Controls.Add(this.cbSaveChartJPGs);
            this.Controls.Add(this.cbBackupDatabase);
            this.Controls.Add(this.cbTotalPowerUsage);
            this.Controls.Add(this.cbPowerUsageOffPeak);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.cbShowDelta);
            this.Controls.Add(this.cbPowerUsageOnPeak);
            this.Controls.Add(this.clbSeries);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.temperatureChart);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "Charts";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.temperatureChart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart temperatureChart;
        private System.Windows.Forms.Timer tmrUpdateChart;
        private System.Windows.Forms.Timer tmrBackupDatabase;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbLastX;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.TextBox tbHours;
        private System.Windows.Forms.Label lbHours;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboTempChartType;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckedListBox clbSeries;
        private System.Windows.Forms.CheckBox cbPowerUsageOnPeak;
        private System.Windows.Forms.CheckBox cbShowDelta;
        private System.Windows.Forms.ComboBox cboPowerChartType;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox cbPowerUsageOffPeak;
        private System.Windows.Forms.CheckBox cbTotalPowerUsage;
        private System.Windows.Forms.CheckBox cbBackupDatabase;
        private System.Windows.Forms.CheckBox cbSaveChartJPGs;
    }
}

