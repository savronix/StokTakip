using System.Windows;
using Wpf.Ui.Appearance;

namespace StokTakip
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SetColorTheme();
        }

        public void SetColorTheme()
        {
            if (ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Dark)
            {
                Resources["AppForeground"] = Resources["ForegroundDark"];
                Resources["AppBackground"] = Resources["BackgroundDark"];
            }
            else if (ApplicationThemeManager.GetAppTheme() == ApplicationTheme.Light)
            {
                Resources["AppForeground"] = Resources["ForegroundLight"];
                Resources["AppBackground"] = Resources["BackgroundLight"];
            }
        }
    }
}


