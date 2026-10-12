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
            timer_main = new System.Windows.Forms.Timer(components);
            button_run = new Button();
            timer_refresh_clock = new System.Windows.Forms.Timer(components);
            formsPlot_main = new ScottPlot.WinForms.FormsPlot();
            numericUpDown1 = new NumericUpDown();
            label1 = new Label();
            groupBox1 = new GroupBox();
            label_avg_win = new Label();
            label_avg_all = new Label();
            label_dura = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(155, 12);
            button1.Name = "button1";
            button1.Size = new Size(137, 87);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(225, 105);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(340, 67);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // richTextBox_output
            // 
            richTextBox_output.BorderStyle = BorderStyle.FixedSingle;
            richTextBox_output.Location = new Point(612, 12);
            richTextBox_output.Name = "richTextBox_output";
            richTextBox_output.Size = new Size(390, 404);
            richTextBox_output.TabIndex = 2;
            richTextBox_output.Text = "";
            // 
            // timer_main
            // 
            timer_main.Interval = 5000;
            timer_main.Tick += timer_main_Tick;
            // 
            // button_run
            // 
            button_run.Location = new Point(12, 12);
            button_run.Name = "button_run";
            button_run.Size = new Size(137, 87);
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
            formsPlot_main.Location = new Point(12, 422);
            formsPlot_main.Name = "formsPlot_main";
            formsPlot_main.Size = new Size(990, 248);
            formsPlot_main.TabIndex = 5;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numericUpDown1.Increment = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown1.Location = new Point(98, 121);
            numericUpDown1.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(67, 34);
            numericUpDown1.TabIndex = 6;
            numericUpDown1.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 130);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 7;
            label1.Text = "跨度(分钟)";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label_avg_win);
            groupBox1.Controls.Add(label_avg_all);
            groupBox1.Controls.Add(label_dura);
            groupBox1.Location = new Point(12, 178);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(594, 238);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "统计数据";
            // 
            // label_avg_win
            // 
            label_avg_win.AutoSize = true;
            label_avg_win.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_avg_win.Location = new Point(24, 88);
            label_avg_win.Name = "label_avg_win";
            label_avg_win.Size = new Size(96, 28);
            label_avg_win.TabIndex = 7;
            label_avg_win.Text = "区间均值:";
            // 
            // label_avg_all
            // 
            label_avg_all.AutoSize = true;
            label_avg_all.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_avg_all.Location = new Point(24, 60);
            label_avg_all.Name = "label_avg_all";
            label_avg_all.Size = new Size(96, 28);
            label_avg_all.TabIndex = 6;
            label_avg_all.Text = "整体均值:";
            // 
            // label_dura
            // 
            label_dura.AutoSize = true;
            label_dura.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label_dura.Location = new Point(24, 32);
            label_dura.Name = "label_dura";
            label_dura.Size = new Size(96, 28);
            label_dura.TabIndex = 5;
            label_dura.Text = "运行时间:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(178, 130);
            label2.Name = "label2";
            label2.Size = new Size(41, 20);
            label2.TabIndex = 9;
            label2.Text = "预览";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1014, 682);
            Controls.Add(label2);
            Controls.Add(groupBox1);
            Controls.Add(label1);
            Controls.Add(numericUpDown1);
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
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private PictureBox pictureBox1;
        private RichTextBox richTextBox_output;
        private System.Windows.Forms.Timer timer_main;
        private Button button_run;
        private System.Windows.Forms.Timer timer_refresh_clock;
        private ScottPlot.WinForms.FormsPlot formsPlot_main;
        private NumericUpDown numericUpDown1;
        private Label label1;
        private GroupBox groupBox1;
        private Label label_dura;
        private Label label2;
        private Label label_avg_all;
        private Label label_avg_win;
    }
}
