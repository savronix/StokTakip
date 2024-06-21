using StokTakip.Services;
using StokTakip.Views.Pages;
using System.Windows;
using Wpf.Ui.Controls;

namespace StokTakip.Views.Windows
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            FrameService.Navigate(this, typeof(LoginPage));
        }

        private void FluentWindow_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
