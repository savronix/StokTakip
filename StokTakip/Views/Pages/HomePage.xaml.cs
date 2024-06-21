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

namespace StokTakip.Views.Pages
{
    /// <summary>
    /// HomePage.xaml etkileşim mantığı
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            this.Loaded += HomePage_Loaded;
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseService.FillStockList(dtgrdStockList);
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(SettingsPage));
        }

       

        private void btnStockCardTask_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(StockCardTaskPage));
        }

        private void btnStockAdd_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(StockTaskPage));
        }

        private void btnStockMovements_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(StockMovementsPage));
        }
    }
}
