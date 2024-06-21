using StokTakip.Services;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Appearance;

namespace StokTakip.Views.Pages
{
    /// <summary>
    /// SettingsPage.xaml etkileşim mantığı
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            if (ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Dark)
            {
                rdbtnKoyu.IsChecked = true;
            }
            else if (ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Light)
            {
                rdbtnAcik.IsChecked = true;
            }
        }

        private void rdbtnAcik_Checked(object sender, RoutedEventArgs e)
        {
            ThemeService.SetLightTheme();
        }
        
        private void rdbtnKoyu_Checked(object sender, RoutedEventArgs e)
        {
            ThemeService.SetDarkTheme();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(HomePage));
        }
    }
}
