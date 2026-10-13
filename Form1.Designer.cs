namespace cs_mxdc_exp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            richTextBox_output = new RichTextBox();
            button_run = new Button();
            timer_refresh_clock = new System.Windows.Forms.Timer(components);
            formsPlot_main = new ScottPlot.WinForms.FormsPlot();
            numericUpDown_timeval = new NumericUpDown();
            label1 = new Label();
            groupBox1 = new GroupBox();
            label_rt = new Label();
            label_avg_win = new Label();
            label_avg_all = new Label();
            label_dura = new Label();
            label2 = new Label();
            button_top = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_timeval).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(138, 12);
            button1.Name = "button1";
            button1.Size = new Size(118, 50);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(155, 65);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(198, 42);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // richTextBox_output
            // 
            richTextBox_output.BorderStyle = BorderStyle.FixedSingle;
            richTextBox_output.Location = new Point(408, 12);
            richTextBox_output.Name = "richTextBox_output";
            richTextBox_output.Size = new Size(537, 244);
            richTextBox_output.TabIndex = 2;
            richTextBox_output.Text = "";
            // 
            // button_run
            // 
            button_run.Location = new Point(12, 12);
            button_run.Name = "button_run";
            button_run.Size = new Size(120, 50);
            button_run.TabIndex = 3;
            button_run.Text = "开始";
            button_run.UseVisualStyleBackColor = true;
            button_run.Click += button_run_Click;
            // 
            // timer_refresh_clock
            // 
            timer_refresh_clock.Tick += timer_refresh_clock_Tick;
            // 
            // formsPlot_main
            // 
            formsPlot_main.Dock = DockStyle.Bottom;
            formsPlot_main.Location = new Point(0, 252);
            formsPlot_main.Name = "formsPlot_main";
            formsPlot_main.Size = new Size(957, 263);
            formsPlot_main.TabIndex = 5;
            // 
            // numericUpDown_timeval
            // 
            numericUpDown_timeval.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpDown_timeval.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown_timeval.Location = new Point(82, 70);
            numericUpDown_timeval.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            numericUpDown_timeval.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown_timeval.Name = "numericUpDown_timeval";
            numericUpDown_timeval.Size = new Size(67, 34);
            numericUpDown_timeval.TabIndex = 6;
            numericUpDown_timeval.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 79);
            label1.Name = "label1";
            label1.Size = new Size(64, 20);
            label1.TabIndex = 7;
            label1.Text = "窗口(分)";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label_rt);
            groupBox1.Controls.Add(label_avg_win);
            groupBox1.Controls.Add(label_avg_all);
            groupBox1.Controls.Add(label_dura);
            groupBox1.Location = new Point(12, 116);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(390, 140);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "统计数据";
            // 
            // label_rt
            // 
            label_rt.AutoSize = true;
            label_rt.Font = new Font("Segoe UI", 9F);
            label_rt.Location = new Point(6, 112);
            label_rt.Name = "label_rt";
            label_rt.Size = new Size(60, 20);
            label_rt.TabIndex = 8;
            label_rt.Text = "实时值:";
            // 
            // label_avg_win
            // 
            label_avg_win.AutoSize = true;
            label_avg_win.Font = new Font("Segoe UI", 9F);
            label_avg_win.Location = new Point(6, 84);
            label_avg_win.Name = "label_avg_win";
            label_avg_win.Size = new Size(74, 20);
            label_avg_win.TabIndex = 7;
            label_avg_win.Text = "区间均值:";
            // 
            // label_avg_all
            // 
            label_avg_all.AutoSize = true;
            label_avg_all.Font = new Font("Segoe UI", 9F);
            label_avg_all.Location = new Point(6, 56);
            label_avg_all.Name = "label_avg_all";
            label_avg_all.Size = new Size(73, 20);
            label_avg_all.TabIndex = 6;
            label_avg_all.Text = "整体均值:";
            // 
            // label_dura
            // 
            label_dura.AutoSize = true;
            label_dura.Font = new Font("Segoe UI", 9F);
            label_dura.Location = new Point(6, 28);
            label_dura.Name = "label_dura";
            label_dura.Size = new Size(74, 20);
            label_dura.TabIndex = 5;
            label_dura.Text = "运行时间:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(301, 42);
            label2.Name = "label2";
            label2.Size = new Size(41, 20);
            label2.TabIndex = 9;
            label2.Text = "预览";
            // 
            // button_top
            // 
            button_top.Location = new Point(366, 12);
            button_top.Name = "button_top";
            button_top.Size = new Size(36, 35);
            button_top.TabIndex = 10;
            button_top.Text = "T";
            button_top.UseVisualStyleBackColor = true;
            button_top.Click += button_top_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(957, 515);
            Controls.Add(button_top);
            Controls.Add(label2);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(numericUpDown_timeval);
            Controls.Add(formsPlot_main);
            Controls.Add(button_run);
            Controls.Add(richTextBox_output);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown_timeval).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private PictureBox pictureBox1;
        private RichTextBox richTextBox_output;
        private Button button_run;
        private System.Windows.Forms.Timer timer_refresh_clock;
        private ScottPlot.WinForms.FormsPlot formsPlot_main;
        private NumericUpDown numericUpDown_timeval;
        private Label label1;
        private GroupBox groupBox1;
        private Label label_dura;
        private Label label2;
        private Label label_avg_all;
        private Label label_avg_win;
        private Label label_rt;
        private Button button_top;
    }
}
