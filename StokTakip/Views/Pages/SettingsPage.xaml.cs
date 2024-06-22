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


        private void btnPasswordApply_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOldPassword.Password.ToString()) && !string.IsNullOrEmpty(txtNewPassword.Password.ToString()))
            {
                DatabaseService.PasswordUpdate(txtOldPassword.Password.ToString(), txtNewPassword.Password.ToString());
                txtNewPassword.Clear();
                txtOldPassword.Clear();
            }
            else
            {
                MessageService.ShowSnackBar("Gerekli alanları doldurmanız gerekli!", "Dikkat!", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Info, 2);
            }
        }

        private void btnNameSurnameApply_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNameSurname.Text.ToString()) && !string.IsNullOrEmpty(txtPassword.Password.ToString()))
            {
                DatabaseService.NameSurnameUpdate(txtNameSurname.Text.ToString(), txtPassword.Password.ToString());
                txtNameSurname.Clear();
                txtPassword.Clear();
            }
            else
            {
                MessageService.ShowSnackBar("Gerekli alanları doldurmanız gerekli!", "Dikkat!", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Info, 2);
            }
        }

        private void btnImageSelectandApply_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtProfilPassword.Password.ToString()))
            {
                // Open file dialog to select an image
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
                {
                    Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"
                };

                bool? result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    string imagePath = openFileDialog.FileName;

                    // Assuming DatabaseService.ProfilePictureUpdate is a method that updates the profile picture
                    DatabaseService.ProfilePictureUpdate(txtProfilPassword.Password.ToString(), imagePath);

                    // Clear the password box after the operation
                    txtProfilPassword.Clear();
                }
            }
            else
            {
                MessageService.ShowSnackBar("Gerekli alanları doldurmanız gerekli!", "Dikkat!", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Info, 2);
            }
        }

        private void btnMailApply_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMail.Text.ToString()) && !string.IsNullOrEmpty(txtMailPassword.Password.ToString()))
            {
                if (IsValidEmail(txtMail.Text.ToString()))
                {
                    DatabaseService.MailSetandUpdate(txtMail.Text.ToString(), txtMailPassword.Password.ToString());
                    txtMail.Clear();
                    txtMailPassword.Clear();
                }
                else
                {
                    MessageService.ShowSnackBar("Geçerli bir e-posta adresi girin!", "Hata!", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Dismiss20), Wpf.Ui.Controls.ControlAppearance.Danger, 2);
                }
            }
            else
            {
                MessageService.ShowSnackBar("Gerekli alanları doldurmanız gerekli!", "Dikkat!", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Info, 2);
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
