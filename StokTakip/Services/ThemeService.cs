using System.Windows;

namespace StokTakip.Services
{
    public class ThemeService
    {
        public static void SetDarkTheme()
        {
            Wpf.Ui.Appearance.ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Dark, Wpf.Ui.Controls.WindowBackdropType.Mica, false);
            (Application.Current as App).SetColorTheme();
        }
        public static void SetLightTheme()
        {
            Wpf.Ui.Appearance.ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Light, Wpf.Ui.Controls.WindowBackdropType.Mica, false);
            (Application.Current as App).SetColorTheme();
        }
    }
}
