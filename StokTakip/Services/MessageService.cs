using StokTakip.Views.Windows;
using System.Windows;
using Wpf.Ui.Controls;

namespace StokTakip.Services
{
    public class MessageService
    {
        public MessageService(string _content, string _title, bool _secondaryEnabled)
        {
            Wpf.Ui.Controls.MessageBox messageBox = new Wpf.Ui.Controls.MessageBox();
            messageBox.Title = _title;
            messageBox.IsSecondaryButtonEnabled = _secondaryEnabled;
            messageBox.Content = _content;
            messageBox.CloseButtonText = "Tamam";
            messageBox.ShowDialogAsync();
        }

        public static void ShowSnackBar(string _content, string _title, SymbolIcon _icon, ControlAppearance _controlAppearance, int _second)
        {
            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var mainWindow = activeWindow as MainWindow;
            var snackbar = new Snackbar(mainWindow.mainSnackbarPresenter);
            snackbar.Appearance = _controlAppearance;
            snackbar.Title = _title;
            snackbar.Content = _content;
            snackbar.Icon = _icon;
            snackbar.Timeout = TimeSpan.FromSeconds(_second);
            snackbar.Show();
        }
    }
}
