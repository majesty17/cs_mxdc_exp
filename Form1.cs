using System.Runtime.InteropServices;
using ScottPlot;
using static cs_mxdc_exp.Form1;
using Color = System.Drawing.Color;

namespace cs_mxdc_exp
{
    public partial class Form1 : Form
    {
        private string app_name = "MXCD经验统计";
        // 采集状态标志
        private bool isRunning = false;
        // 采集数据列表，key为时间戳，value为经验值
        private Dictionary<long, int> dataList = new Dictionary<long,int >();
        // 开始时间戳
        long startTime;


        private enum LogLevel
        {
            Info,
            Success,
            Warning,
            Error
        }

        public Form1()
        {
            InitializeComponent();
            this.Text = app_name;
        }

        private void Log(string message, LogLevel level = LogLevel.Info)
        {
            if (richTextBox_output.InvokeRequired)
            {
                richTextBox_output.BeginInvoke(() => Log(message, level));
                return;
            }

            Color color = level switch
            {
                LogLevel.Success => Color.ForestGreen,
                LogLevel.Warning => Color.DarkOrange,
                LogLevel.Error => Color.Red,
                _ => Color.Black
            };

            string levelText = level switch
            {
                LogLevel.Success => "成功",
                LogLevel.Warning => "警告",
                LogLevel.Error => "错误",
                _ => "信息"
            };

            string prefix = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ";
            string levelTextWithBrackets = $"[{levelText}]";
            string suffix = $" {message}{Environment.NewLine}";

            richTextBox_output.SelectionColor = richTextBox_output.ForeColor;
            richTextBox_output.AppendText(prefix);

            int levelStart = richTextBox_output.TextLength;
            richTextBox_output.AppendText(levelTextWithBrackets);
            richTextBox_output.Select(levelStart, levelTextWithBrackets.Length);
            richTextBox_output.SelectionColor = color;

            richTextBox_output.SelectionStart = richTextBox_output.TextLength;
            richTextBox_output.SelectionColor = richTextBox_output.ForeColor;
            richTextBox_output.AppendText(suffix);

            richTextBox_output.SelectionLength = 0;
            richTextBox_output.SelectionStart = richTextBox_output.TextLength;
            richTextBox_output.ScrollToCaret();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Bitmap bmp = CaptureWindowByTitle("冒险岛怀旧服");
            Bitmap bmp = CaptureWindowByTitle("launcher3.0");
            if (bmp != null)
            {
                pictureBox1.Image = bmp;
            }
        }


        // 1. 查找窗口句柄 API
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // 2. 获取窗口尺寸矩形
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

        // 3. 将窗口内容绘制到设备上下文 (支持后台或部分遮挡截屏)
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, uint nFlags);
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        private const uint PW_RENDERFULLCONTENT = 0x00000002;

        // 4. 辅助结构体：窗口位置和坐标
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width => Right - Left;
            public int Height => Bottom - Top;
        }

        /// <summary>
        /// 根据窗口标题获取句柄并截屏
        /// </summary>
        /// <param name="windowTitle">精确的窗口标题</param>
        /// <returns>返回 Bitmap 图像，若未找到窗口返回 null</returns>
        public Bitmap CaptureWindowByTitle(string windowTitle)
        {
            // 步骤 1：根据 Title 获取窗口句柄
            IntPtr hWnd = FindWindow(null, windowTitle);

            if (hWnd == IntPtr.Zero)
            {
                Log($"未找到标题为 \"{windowTitle}\" 的窗口！", LogLevel.Warning);
                return null;
            }
            Log($"窗口句柄={hWnd}");
            // 步骤 2：获取窗口的大小
            if (!GetWindowRect(hWnd, out var windowRect) || windowRect.Width <= 0 || windowRect.Height <= 0)
            {
                Log("无法获取窗口大小！", LogLevel.Error);
                return null;
            }

            Log($"窗口大小：width={windowRect.Width};height={windowRect.Height}");
            Log($"窗口左上角坐标：x={windowRect.Left};y={windowRect.Top}");

            var bmp = new Bitmap(windowRect.Width, windowRect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.CopyFromScreen(
                    windowRect.Left,
                    windowRect.Top,
                    0,
                    0,
                    bmp.Size,
                    CopyPixelOperation.SourceCopy);
            }

            return bmp;

        }


        // 定时器主循环事件，每5秒采集一次经验数据
        private void timer_main_Tick(object sender, EventArgs e)
        {
            // 采集当前经验，加入list中；
            Log("采集当前经验数据...", LogLevel.Info);
            long nowTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            int value = (int)(nowTime % 100000 + Random.Shared.Next(10, 80));
            Log($"当前时间戳：{nowTime}, 当前经验值：{value}", LogLevel.Info);


            dataList.Add(nowTime, value); // 这里的100是示例经验值，实际应替换为采集到的经验值
            drawChart();
        }

        // 按钮点击事件，开始或停止采集
        private void button_run_Click(object sender, EventArgs e)
        {
            if (this.isRunning)
            {
                this.isRunning = false;
                button_run.Text = "开始";
                this.Text = $"{app_name} - 已停止采集";
                Log("已停止采集。", LogLevel.Info);
            }
            else
            {
                this.isRunning = true;
                button_run.Text = "停止";
                this.Text = $"{app_name} - 正在采集";
                Log("已开始采集。", LogLevel.Info);
            }

            // 根据当前状态启动或停止定时器
            if (this.isRunning)
            {
                dataList.Clear();
                timer_main.Interval = 10000;
                timer_refresh_clock.Interval = 200;
                startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                Log($"采集开始时间戳：{startTime}", LogLevel.Info);
                timer_main.Start();
                timer_refresh_clock.Start();
            }
            else
            {
                timer_main.Stop();
                timer_refresh_clock.Stop();
            }




        }

        // 刷新运行时间显示
        private void timer_refresh_clock_Tick(object sender, EventArgs e)
        {
            if (this.isRunning)
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds() - startTime);
                label_dura.Text = $"运行时间：{timeSpan.ToString(@"hh\:mm\:ss")}";
            }
        }

        // 窗体关闭事件，停止定时器
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_refresh_clock.Stop();
            timer_main.Stop();
        }

        // 绘制经验曲线图
        private void drawChart()
        {
            // 把datalist里的值画在formsPlot_main里
            formsPlot_main.Plot.Clear();
            if (dataList.Count > 0)
            {
                double[] xs = new double[dataList.Count];
                string[] x_str= new string[dataList.Count];
                double[] ys = new double[dataList.Count];

                int i = 0;
                foreach (var kvp in dataList)
                {
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(kvp.Key).ToLocalTime();
                    x_str[i] = dateTimeOffset.ToString("HH:mm:ss");
                    xs[i] = kvp.Key; 
                    ys[i] = kvp.Value;
                    i++;
                }



                // 计算平均值并绘制水平线
                double avg = ys.Average();
                var avg_lineline = formsPlot_main.Plot.Add.HorizontalLine(avg);
                avg_lineline.Color= ScottPlot.Color.FromColor(Color.Purple);
                avg_lineline.LinePattern = LinePattern.Dashed;
                avg_lineline.LineWidth = 2;

                // 设置x轴的刻度密度
                var tickGen = new ScottPlot.TickGenerators.NumericAutomatic();
                tickGen.TargetTickCount = 8;
                formsPlot_main.Plot.Axes.Bottom.TickGenerator = tickGen;

                
                // 设置X轴为时间戳对应的字符串
                formsPlot_main.Plot.Axes.Bottom.SetTicks(xs, x_str);
                formsPlot_main.Plot.Add.Scatter(xs, ys);



                // 设置图表标题和轴标签
                formsPlot_main.Plot.Axes.AutoScale();                formsPlot_main.Refresh();
            }
        }
    }
}
