using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace FrontGauge
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class GaugeWindow : Window
    {
        // TimerLimit / hours
        double MaxValue;
        DispatcherTimer timer = new DispatcherTimer();


        public GaugeWindow()
        {
            InitializeComponent();
            this.Show();

            this.Resize();
            this.SetTimer();

            MaxValue = -MeterMask.X;
        }

        void Resize()
        {
            this.Top = 0;
            this.Left = 0;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight / 12;
        }

        void Close_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        void Minimalize_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        void SetTimer()
        {
            this.timer.Interval = new TimeSpan(0, 0, 0, 1);
            this.timer.Tick += new EventHandler(TimerMethod);
           
            this.timer.Start();
        }

        void TimerMethod(object sender, EventArgs e)
        {
            Debug.Print(MeterMask.X.ToString());

            MeterMask.X += this.GetOneSecondValue(10,3);

            // マスクとメーターが重なるかそれ以上になったら終了。
            if (MeterMask.X >= Meter.Margin.Left) {
                this.timer.Stop();
                UserData.Default.StudyPoint++;

                this.ChangeWindow();
            }
        }

        /// <summary>
        /// 一秒間分の値を取得
        /// </summary>
        /// <param name="Min"> タイマーの時間 分 </param>
        /// <param name="digits"> 四捨五入する桁 </param>
        /// <returns></returns>
        double GetOneSecondValue(double Min,int digits)
        {
           return Math.Round(this.MaxValue / (Min * 60), digits, MidpointRounding.AwayFromZero);
        }

        void ChangeWindow()
        {
            UserData.Default.Save();
            new MainWindow();
            this.Close();
        }
    }
}
