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
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(42520.713888888888D, "75.45,0");
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(42520.714583333334D, "94.55,0");
            this.temperatureChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tmrUpdateChart = new System.Windows.Forms.Timer(this.components);
            this.tmrBackupDatabase = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbHours = new System.Windows.Forms.Label();
            this.tbHours = new System.Windows.Forms.TextBox();
            this.rbLastX = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboChartType = new System.Windows.Forms.ComboBox();
            this.cboPalette = new System.Windows.Forms.ComboBox();
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
            chartArea3.AxisX.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea3.AxisY.TitleFont = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea3.Name = "ChartArea1";
            this.temperatureChart.ChartAreas.Add(chartArea3);
            this.temperatureChart.Location = new System.Drawing.Point(0, 95);
            this.temperatureChart.Margin = new System.Windows.Forms.Padding(0);
            this.temperatureChart.Name = "temperatureChart";
            this.temperatureChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series3.IsValueShownAsLabel = true;
            series3.IsVisibleInLegend = false;
            series3.IsXValueIndexed = true;
            series3.Label = "#VALY F @ #VALX{h:mm tt }";
            series3.Name = "Sensor1";
            dataPoint5.IsVisibleInLegend = false;
            dataPoint6.IsVisibleInLegend = false;
            series3.Points.Add(dataPoint5);
            series3.Points.Add(dataPoint6);
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.temperatureChart.Series.Add(series3);
            this.temperatureChart.Size = new System.Drawing.Size(1018, 534);
            this.temperatureChart.TabIndex = 0;
            this.temperatureChart.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(this.temperatureChart_GetToolTipText);
            // 
            // tmrUpdateChart
            // 
            this.tmrUpdateChart.Enabled = true;
            this.tmrUpdateChart.Interval = 30000;
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
            this.groupBox1.Size = new System.Drawing.Size(244, 67);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // lbHours
            // 
            this.lbHours.AutoSize = true;
            this.lbHours.Location = new System.Drawing.Point(105, 41);
            this.lbHours.Name = "lbHours";
            this.lbHours.Size = new System.Drawing.Size(33, 13);
            this.lbHours.TabIndex = 6;
            this.lbHours.Text = "hours";
            // 
            // tbHours
            // 
            this.tbHours.Location = new System.Drawing.Point(60, 38);
            this.tbHours.Name = "tbHours";
            this.tbHours.Size = new System.Drawing.Size(35, 20);
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
            this.groupBox2.Controls.Add(this.cboPalette);
            this.groupBox2.Controls.Add(this.cboChartType);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(262, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(613, 67);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chart";
            // 
            // cboChartType
            // 
            this.cboChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChartType.FormattingEnabled = true;
            this.cboChartType.Location = new System.Drawing.Point(16, 33);
            this.cboChartType.Name = "cboChartType";
            this.cboChartType.Size = new System.Drawing.Size(208, 21);
            this.cboChartType.TabIndex = 0;
            this.cboChartType.SelectedIndexChanged += new System.EventHandler(this.cboChartType_SelectedIndexChanged);
            // 
            // cboPalette
            // 
            this.cboPalette.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPalette.FormattingEnabled = true;
            this.cboPalette.Location = new System.Drawing.Point(230, 33);
            this.cboPalette.Name = "cboPalette";
            this.cboPalette.Size = new System.Drawing.Size(208, 21);
            this.cboPalette.TabIndex = 1;
            this.cboPalette.SelectedIndexChanged += new System.EventHandler(this.cboPalette_SelectedIndexChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 638);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.temperatureChart);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "Last Update: ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.temperatureChart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ComboBox cboChartType;
        private System.Windows.Forms.ComboBox cboPalette;
    }
}

