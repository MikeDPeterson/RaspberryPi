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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(42520.713888888888D, "75.45,0");
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(42520.714583333334D, "94.55,0");
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            chartArea3.AlignWithChartArea = "BarChartArea";
            chartArea3.AxisX.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea3.AxisY.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.Name = "LinesChartArea";
            chartArea4.AlignWithChartArea = "LinesChartArea";
            chartArea4.Name = "BarChartArea";
            this.temperatureChart.ChartAreas.Add(chartArea3);
            this.temperatureChart.ChartAreas.Add(chartArea4);
            legend2.Name = "Sensors";
            legend2.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.temperatureChart.Legends.Add(legend2);
            this.temperatureChart.Location = new System.Drawing.Point(0, 138);
            this.temperatureChart.Margin = new System.Windows.Forms.Padding(0);
            this.temperatureChart.Name = "temperatureChart";
            this.temperatureChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series3.ChartArea = "LinesChartArea";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series3.IsValueShownAsLabel = true;
            series3.IsXValueIndexed = true;
            series3.Label = "#VALY F @ #VALX{h:mm tt }";
            series3.Legend = "Sensors";
            series3.Name = "Sensor1";
            dataPoint3.IsVisibleInLegend = false;
            dataPoint4.IsVisibleInLegend = false;
            series3.Points.Add(dataPoint3);
            series3.Points.Add(dataPoint4);
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series4.ChartArea = "BarChartArea";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn;
            series4.Legend = "Sensors";
            series4.Name = "Series2";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Date;
            this.temperatureChart.Series.Add(series3);
            this.temperatureChart.Series.Add(series4);
            this.temperatureChart.Size = new System.Drawing.Size(1018, 491);
            this.temperatureChart.TabIndex = 0;
            this.temperatureChart.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.temperatureChart_GetToolTipText);
            this.temperatureChart.Click += new System.EventHandler(this.temperatureChart_Click);
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
            this.tbHours.TextChanged += new System.EventHandler(this.tbHours_TextChanged);
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
            this.groupBox2.Size = new System.Drawing.Size(239, 110);
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
            this.cboTempChartType.SelectedIndexChanged += new System.EventHandler(this.cboChartType_SelectedIndexChanged);
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
            this.clbSeries.Size = new System.Drawing.Size(138, 84);
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
            this.cbShowDelta.Location = new System.Drawing.Point(507, 105);
            this.cbShowDelta.Name = "cbShowDelta";
            this.cbShowDelta.Size = new System.Drawing.Size(80, 17);
            this.cbShowDelta.TabIndex = 7;
            this.cbShowDelta.Text = "Show Delta";
            this.cbShowDelta.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(948, 98);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 24);
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
            this.cbPowerUsageOffPeak.CheckedChanged += new System.EventHandler(this.cbPowerUsageOffPeak_CheckedChanged);
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
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 638);
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
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "Charts";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
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
    }
}

