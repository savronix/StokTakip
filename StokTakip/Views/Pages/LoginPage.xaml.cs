using StokTakip.Services;
using System;
using System.Collections.Generic;
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
using Wpf.Ui.Controls;

namespace StokTakip.Views.Pages
{
    /// <summary>
    /// LoginPage.xaml etkileşim mantığı
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            PasswordBox.Focus();
            txtblckNameSurname.Text = DatabaseService.GetNameSurname();
            string imageSource = DatabaseService.GetImageSource();

            // Check if the imageSource is "personImage" and use dynamic resource if true
            if (imageSource == "personImage")
            {
                imgPerson.Source = (ImageSource)FindResource("personImage");
            }
            else
            {
                // Update the image source from the URI
                imgPerson.Source = new BitmapImage(new Uri(imageSource, UriKind.Absolute));
            }
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnGirisYap.RaiseEvent(new RoutedEventArgs(Wpf.Ui.Controls.Button.ClickEvent));
            }
        }

        private void btnGirisYap_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(PasswordBox.Text))
                {
                    string password = PasswordBox.Password;
                    bool userStatus = false;
                    userStatus = DatabaseService.ValidateUser(password);

                    if (userStatus == true)
                    {
                        MessageService.ShowSnackBar("Şifre doğru.", "Giriş Başarılı!", new SymbolIcon(SymbolRegular.ApprovalsApp24), ControlAppearance.Success, 1);
                        FrameService.Navigate(typeof(HomePage));
                    }
                    else
                    {
                        MessageService.ShowSnackBar("Şifre yanlış", "Bir şeyler ters gitti!", new SymbolIcon(SymbolRegular.Person16), ControlAppearance.Danger, 2);
                    }
                }
                else
                {
                    MessageService.ShowSnackBar("Şifre boş bırakılamaz", "Boş değerler var!", new SymbolIcon(SymbolRegular.Person16), ControlAppearance.Danger, 2);
                }
            }
            catch (Exception ex)
            {
                new MessageService(ex.Message, "Bir şeyler ters gitti!", false);
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(DatabaseService.GetMail()))
            {
                FrameService.Navigate(typeof(FactoryPasswordPage));
            }
            else
            {
                MessageService.ShowSnackBar("Mail Tanımlama işlemi yapmamışsınız. Şifre sıfırlanamaz", "!", new SymbolIcon(SymbolRegular.Dismiss20), ControlAppearance.Dark, 3);

            }
        }
    }
}
