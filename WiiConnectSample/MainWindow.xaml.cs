using System;
using System.Threading;
using System.Windows;
using WiimoteLib;

namespace WiiConnectSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Wiimote Wii = new Wiimote();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Initial();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        public void Initial()
        {
            try
            {
                Wii.WiimoteChanged += WiimoteChanged;  //イベント関数の登録
                Wii.Connect();
                Wii.SetReportType(InputReport.ButtonsAccel, true);   //リモコンのイベント取得条件を設定
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void WiimoteChanged(object sender, WiimoteChangedEventArgs args)
        {
            WiimoteState wiiState = args.WiimoteState;

            this.Dispatcher.BeginInvoke(
                new Action(() =>
                {
                    XAxis.Content = wiiState.AccelState.RawValues.X.ToString();
                    YAxis.Content = wiiState.AccelState.RawValues.Y.ToString();
                    ZAxis.Content = wiiState.AccelState.RawValues.Z.ToString();
                })
            );
        }
    }
}
