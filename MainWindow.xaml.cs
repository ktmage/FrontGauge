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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrontGauge
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.Show();
            PointBox.DataContext = new { Num = UserData.Default.StudyPoint };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangeWindow();
        }

        void ChangeWindow()
        {
            UserData.Default.Save();
            new GaugeWindow();
            this.Close();
        }

        void Close_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        void Minimalize_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }
    }
}
