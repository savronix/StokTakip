using StokTakip.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// FactoryPasswordPage.xaml etkileşim mantığı
    /// </summary>
    public partial class FactoryPasswordPage : Page
    {
        public FactoryPasswordPage()
        {
            InitializeComponent();
            Loaded += FactoryPasswordPage_Loaded;


        }

        private async void FactoryPasswordPage_Loaded(object sender, RoutedEventArgs e)
        {
            string mail = DatabaseService.GetMail();
            Random random = new Random();
            string randomCode = random.Next(100000, 999999).ToString();
            sendRandomCode = int.Parse(randomCode);
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                await Task.Run(() =>
                {
                    SendEmail(mail, randomCode);
                });
            });
        }

        int sendRandomCode = 0;
        private async void btnContiune_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                if (int.Parse(txtCode.Text) == sendRandomCode)
                {
                    await Application.Current.Dispatcher.Invoke(async () =>
                    {
                        codeStackPanel.Visibility = Visibility.Hidden;
                        passwordStackPanel.Visibility = Visibility.Visible;
                    });
                    MessageService.ShowSnackBar("Şifrenizi ayarlamak üzere yönlendiriliyorsunuz", "Şifre Sıfırlama İşlemi", new SymbolIcon(SymbolRegular.Checkmark20), ControlAppearance.Dark, 4);

                }
                else
                {
                    MessageService.ShowSnackBar("6 haneli kod yanlış girildi.", "Şifre Sıfırlama İşlemi", new SymbolIcon(SymbolRegular.DismissCircle20), ControlAppearance.Dark, 4);
                }
            }
            else
            {
                MessageService.ShowSnackBar("Lütfen 6 haneli kodu girin", "Şifre Sıfırlama İşlemi", new SymbolIcon(SymbolRegular.DismissCircle20), ControlAppearance.Dark, 4);
            }

        }
        public void SendEmail(string _mail, string _body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Credentials = new NetworkCredential("savronix@hotmail.com", "Erdem2006&");
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.office365.com";
                smtpClient.EnableSsl = true;
                mailMessage.To.Add(_mail);
                mailMessage.From = new MailAddress("savronix@hotmail.com", "Şifre Sıfırlama İşlemi");
                mailMessage.Subject = "Doğrulama Kodunuz";
                string html = "<!DOCTYPE html>\r\n<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n    <title>Doğrulama Kodu</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n        }\r\n        .container {\r\n            width: 100%;\r\n            padding: 20px;\r\n            background-color: #f8f9fa;\r\n            color: #333;\r\n        }\r\n        .code {\r\n            font-size: 20px;\r\n            font-weight: bold;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h2>Merhaba,</h2>\r\n        <p>Şifre sıfırlama işlemine devam edebilmek için gerekli kodunuz</p>\r\n        <p class=\"code\">" + _body + "</p>  \r\n\t</div>\r\n</body></html>";
                mailMessage.Body = html;
                mailMessage.IsBodyHtml = true;
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                new MessageService(ex.Message, "Bir hata meydana geldi!", false);
            }


        }
        private void txtCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnContiune.RaiseEvent(new RoutedEventArgs(Wpf.Ui.Controls.Button.ClickEvent));
            }
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password.ToString()))
            {
                DatabaseService.PasswordUpdate(txtPassword.Password.ToString());
                FrameService.Navigate(typeof(LoginPage));

            }
            else
            {
                MessageService.ShowSnackBar("Lütfen yeni şifre alanını doldurun", "Şifre Sıfırlama İşlemi", new SymbolIcon(SymbolRegular.DismissCircle20), ControlAppearance.Dark, 4);
            }
        }
    }
}
