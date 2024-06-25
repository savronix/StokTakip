using StokTakip.Views.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows;
using StokTakip.Views.Pages;

namespace StokTakip.Services
{
    public class FrameService
    {
        public FrameService() { }
        public static void Navigate(Window mainWindow, Type pageType)
        {
            if (mainWindow != null)
            {
                Page gotoPage = PageCache.GetPage(pageType);
                if (gotoPage == null)
                {
                    // Cache'de yoksa yeni oluştur ve cache'e ekle
                    gotoPage = (Page)Activator.CreateInstance(pageType);
                    PageCache.AddPage(pageType, gotoPage);
                }
                if (pageType == typeof(LoginPage))
                {
                    mainWindow.MinWidth = 600;
                    mainWindow.MinHeight = 400;
                    mainWindow.Width = 600;
                    mainWindow.Height = 400;
                }
                var animation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                Frame frame = mainWindow.FindName("mainFrame") as Frame;

                frame.Navigate(gotoPage);
                gotoPage.BeginAnimation(UIElement.OpacityProperty, animation);
            }
        }

        public static void Navigate(Type pageType)
        {
            Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var mainWindow = activeWindow as MainWindow;
            Page gotoPage = null;
            if (mainWindow != null)
            {
                gotoPage = PageCache.GetPage(pageType);
                if (gotoPage == null)
                {
                    gotoPage = (Page)Activator.CreateInstance(pageType);
                    PageCache.AddPage(pageType, gotoPage);
                }
                
                if (pageType != typeof(LoginPage)&& pageType != typeof(FactoryPasswordPage))
                {

                    mainWindow.Width = 1200;
                    mainWindow.Height = 650;
                }
                else
                {
                    mainWindow.MinWidth = 600;
                    mainWindow.MinHeight = 400;
                    mainWindow.Width = 600;
                    mainWindow.Height = 400;
                }
                var scaleTransform = new ScaleTransform(0.4, 0.4);
                gotoPage.RenderTransform = scaleTransform;
                gotoPage.RenderTransformOrigin = new Point(0.5, 0.5);
                var opacityAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.1)
                };
                var scaleXAnimation = new DoubleAnimation
                {
                    From = 0.8,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.2)
                };
                var scaleYAnimation = new DoubleAnimation
                {
                    From = 0.8,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(0.2)
                };
                gotoPage.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
                mainWindow.mainFrame.Navigate(gotoPage);
            }
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;

            // Pencerenin genişliğini ve yüksekliğini alın
            double windowWidth = mainWindow.Width;
            double windowHeight = mainWindow.Height;

            // Yeni sol ve üst pozisyonlarını hesaplayın
            double newLeft = (screenWidth / 2) - (windowWidth / 2);
            double newTop = (screenHeight / 2) - (windowHeight / 2);

            // Pencerenin yeni pozisyonlarını ayarlayın
            mainWindow.Left = newLeft;
            mainWindow.Top = newTop;

        }
        public static void HomeFrameNavigate(Frame homeFrame, Type pageType)
        {
            Page gotoPage = null;
            gotoPage = PageCache.GetPage(pageType);
            if (gotoPage == null)
            {
                gotoPage = (Page)Activator.CreateInstance(pageType);
                PageCache.AddPage(pageType, gotoPage);
            }
            var scaleTransform = new ScaleTransform(0.4, 0.4);
            gotoPage.RenderTransform = scaleTransform;
            gotoPage.RenderTransformOrigin = new Point(0.5, 0.5);
            var opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            var scaleXAnimation = new DoubleAnimation
            {
                From = 0.8,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            var scaleYAnimation = new DoubleAnimation
            {
                From = 0.8,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.2)
            };
            gotoPage.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
            homeFrame.Navigate(gotoPage);
        }
    }
}
