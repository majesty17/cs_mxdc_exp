using ScottPlot;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static cs_mxdc_exp.Form1;
using Color = System.Drawing.Color;

namespace cs_mxdc_exp
{
    public partial class Form1 : Form
    {
        private string app_name = "MXCD经验统计";
        private string win_title = "冒险岛怀旧服";

        // 采集间隔,秒
        public const int INTVAL_SEC = 5;
        // 采集状态标志
        private bool isRunning = false;
        // 采集数据列表，key为时间戳，value为经验值
        private Dictionary<long, DataPoint> dataList = new Dictionary<long, DataPoint>();
        // 开始时间戳
        long startTime;


        // 创建一个时间间隔为 5 秒的定时器
        PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(INTVAL_SEC));
        
        CancellationTokenSource? cts; // = new CancellationTokenSource();


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
            button_top.Text = this.TopMost ? "T" : "-";
        }

        // 日志输出函数，支持不同级别的日志
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
            Bitmap bmp = CaptureWindowByTitle(win_title);
            if (bmp != null)
            {
                pictureBox1.Image = bmp;

                string imgPath_full = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    $"full_{DateTime.Now:yyyyMMdd_HHmmss}.png"
                );
                bmp.Save(imgPath_full, System.Drawing.Imaging.ImageFormat.Png);

                string res_price;

                bool ocr_suc_price = WechatOCR.call_wechat_ocr(imgPath_full, out res_price);
                Log($"OCR识别结果：{res_price}; 识别成功={ocr_suc_price}", LogLevel.Info);
            }
        }

        // 1. 查找窗口句柄 API
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

        // 2. 获取窗口尺寸矩形
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

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

            var bmp = new Bitmap(120, 17);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.CopyFromScreen(
                    windowRect.Left + 1050,
                    windowRect.Top + 953,
                    0,
                    0,
                    bmp.Size,
                    CopyPixelOperation.SourceCopy);
            }

            return bmp;

        }

        private int getExp()
        {
            Bitmap bmp = CaptureWindowByTitle("冒险岛怀旧服");
            if(bmp == null)
            {
                Log("未能获取窗口截图，无法读取经验值！", LogLevel.Warning);
                return -1;
            }
            // 反色处理
            bmp = InvertImage(bmp);
            pictureBox1.Image = bmp;
            string imgPath = Path.Combine(
            Directory.GetCurrentDirectory(),
                $"full_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                    bmp.Save(imgPath, System.Drawing.Imaging.ImageFormat.Png);


            string res_price;

            bool ocr_suc_price = WechatOCR.call_wechat_ocr(imgPath, out res_price);
            Log($"OCR识别结果：{res_price}; 识别成功={ocr_suc_price}", LogLevel.Info);
            try
            {
                return getExpFromOcr(res_price);
            }
            catch (Exception ex)
            {
                Log($"解析OCR结果时发生异常：{ex.Message}", LogLevel.Error);
                return -1;
            }
        }

        private int getExpFromOcr(string j)
        {
            // 处理经验值，去掉非数字字符
            using (JsonDocument doc = JsonDocument.Parse(j))
            {
                JsonElement root = doc.RootElement;

                // 检查是否存在 ocr_response 数组且不为空
                if (root.TryGetProperty("ocr_response", out JsonElement ocrArray) && ocrArray.GetArrayLength() > 0)
                {
                    // 遍历 ocr_response 数组，拼接所有 text 字段的内容
                    string text = "";
                    for (int i = 0; i < ocrArray.GetArrayLength(); i++)
                    {
                        text = $"{text}{ocrArray[i].GetProperty("text").GetString().Trim()}";
                    }

                    string result = string.Join("", Regex.Matches(text, @"\d"));


                    if (result.Length > 0 && int.TryParse(result, out int ret))
                    {
                        return ret;
                    }
                    else
                    {
                        Log("OCR识别结果无效或无法解析为整数！", LogLevel.Warning);
                        return -1;
                    }

                }
                return -1;
            }
        }

        // 定时器主循环事件，每5秒采集一次经验数据
        private async Task DoTaskAsync()
        {
            // 采集当前经验，加入list中；
            long nowTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            int value = getExp();
            Log($"当前时间：{nowTime}, 经验值：{value}", LogLevel.Info);

            // 取最后一个，然后计算跟这次的差值，用来计算实时speed
            int lastvalue = value;  // 默认当前值,也就是第一个增速为0
            if (dataList.Count > 0)
            {
                lastvalue = dataList.Values.Last().Value;
            }

            dataList.Add(nowTime, new DataPoint(value, nowTime, lastvalue, INTVAL_SEC));

            // 窗口内的数据点数，二者取最小
            int all_len = dataList.Count;
            int time_win_len = ((int)numericUpDown_timeval.Value) * 60 / INTVAL_SEC;
            int real_len = Math.Min(all_len, time_win_len);


            long[] xs = new long[real_len];
            string[] x_str = new string[real_len];  // x轴文字
            int[] ys = new int[real_len];   // 经验净值
            double[] y2s = new double[real_len];  // 经验实时增速(万/小时)

            // 取出最近的 real_len 个数据点
            for (int i = 0; i < real_len; i++)
            {
                long key = dataList.Keys.ElementAt(all_len - real_len + i);
                xs[i] = key;
                ys[i] = dataList[key].Value;
                x_str[i] = dataList[key].TimeStr;
                y2s[i] = dataList[key].Speed;
            }

            // 
            double avg_speed_all = ((double)(dataList.Values.Last().Value - dataList.Values.First().Value)) / dataList.Count / INTVAL_SEC * 3600 / 10000.0;
            double avg_speed_win = (ys[real_len - 1] - ys[0]) / real_len / INTVAL_SEC * 3600 / 10000.0;
            double rt_speed = (y2s.Last());
            int exp_incr_all = dataList.Values.Last().Value - dataList.Values.First().Value;
            int exp_incr_winwin = ys[real_len - 1] - ys[0];


            // 更新两个label
            label_avg_all.Text = $"经验增速(总): {avg_speed_all:F2} 万/小时; 增量(总): {exp_incr_all}";
            label_avg_win.Text = $"经验增速(窗): {avg_speed_win:F2} 万/小时; 增量(窗): {exp_incr_winwin}";
            label_rt.Text = $"实时增速: {rt_speed:F2} 万/小时";

            // 绘制曲线图
            drawChart(x_str, xs, ys, y2s);
        }

        // 按钮点击事件，开始或停止采集
        async private void button_run_Click(object sender, EventArgs e)
        {

            if(CaptureWindowByTitle(win_title) ==null)
            {
                Log($"未能找到'{win_title}'窗口，请确保游戏已启动并登录！", LogLevel.Error);
                return;
            }


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
                //timer_main.Interval = 10000;
                timer_refresh_clock.Interval = 200;
                startTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                Log($"采集开始时间戳：{startTime}", LogLevel.Info);
                //timer_main.Start();
                timer_refresh_clock.Start();
                try
                {
                    cts = new CancellationTokenSource();
                    while (await timer.WaitForNextTickAsync(cts.Token))
                    {
                        await DoTaskAsync();
                    }
                }
                catch (OperationCanceledException)
                {
                    // Log("采集任务已取消。", LogLevel.Info);
                }

            }
            else
            {
                cts?.Cancel();
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
            isRunning = false;
            cts?.Cancel();
            timer_refresh_clock.Stop();
        }

        // 绘制经验曲线图
        private void drawChart(string[] x_str, long[] x, int[] y, double[] y2)
        {
            int len = x.Length;
            if (len <= 0 || y.Length != len)
            {
                Log("绘制曲线图失败：x和y数组长度不一致或为空！", LogLevel.Error);
                return;
            }
            formsPlot_main.Plot.Clear();



            /*
            // 设置X轴为时间戳对应的字符串
            formsPlot_main.Plot.Axes.Bottom.SetTicks(x, x_str);
            formsPlot_main.Plot.Add.Scatter(x, y);
            */

            var sig1 = formsPlot_main.Plot.Add.Signal(y);
            sig1.Axes.YAxis = formsPlot_main.Plot.Axes.Left;
            sig1.Axes.YAxis.Label.Text = "Exp";
            sig1.Axes.YAxis.Label.ForeColor = sig1.Color;


            var sig2 = formsPlot_main.Plot.Add.Signal(y2);
            sig2.Axes.YAxis = formsPlot_main.Plot.Axes.Right;
            sig2.Axes.YAxis.Label.Text = "Exp Speed(w/Hour)";
            sig2.Axes.YAxis.Label.ForeColor = sig2.Color;
            sig2.MarkerShape = MarkerShape.OpenSquare;
            sig2.MarkerStyle.Size = 20;

            // 计算平均值并绘制水平线

            double avg = y2.Average();
            var avg_lineline = formsPlot_main.Plot.Add.HorizontalLine(avg);
            avg_lineline.Color = sig2.Color;
            avg_lineline.Axes.YAxis = formsPlot_main.Plot.Axes.Right;
            avg_lineline.LinePattern = LinePattern.Dashed;
            avg_lineline.LineWidth = 2;


            // 自动调整+刷新
            formsPlot_main.Plot.Axes.Margins();
            formsPlot_main.Plot.Axes.AutoScale();
            formsPlot_main.Refresh();

        }

        private void button_top_Click(object sender, EventArgs e)
        {
            // 切换 TopMost
            this.TopMost = !this.TopMost;
            button_top.Text = this.TopMost ? "T" : "-";
        }

        public static Bitmap InvertImage(Bitmap original)
        {
            // 创建一个同尺寸的新位图
            Bitmap inverted = new Bitmap(original.Width, original.Height);

            // 定义反色颜色矩阵
            // R' = 1 - R, G' = 1 - G, B' = 1 - B
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
            new float[] {-1,  0,  0,  0,  0},
            new float[] { 0, -1,  0,  0,  0},
            new float[] { 0,  0, -1,  0,  0},
            new float[] { 0,  0,  0,  1,  0}, // Alpha 通道不变
            new float[] { 1,  1,  1,  0,  1}  // 颜色偏移量（平移1，实现 1 - 颜色值）
            });

            using (ImageAttributes attributes = new ImageAttributes())
            {
                attributes.SetColorMatrix(colorMatrix);

                using (Graphics g = Graphics.FromImage(inverted))
                {
                    g.DrawImage(original,
                        new Rectangle(0, 0, original.Width, original.Height),
                        0, 0, original.Width, original.Height,
                        GraphicsUnit.Pixel,
                        attributes);
                }
            }

            return inverted;
        }
    }

    class DataPoint
    {
        public string TimeStr { get; set; } = string.Empty;   // 时间字符串
        public int Value { get; set; }   // 当前采集经验
        public double Speed { get; set; }  // 经验增速(万/小时)
        public DataPoint(int value, long timestamp, int lastvalue, int pt)
        {
            Value = value;
            Speed = ((double)(value - lastvalue)) / pt * 3600.0 / 10000.0;
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp).ToLocalTime();
            TimeStr = dateTimeOffset.ToString("HH:mm:ss");
        }
    }
}
