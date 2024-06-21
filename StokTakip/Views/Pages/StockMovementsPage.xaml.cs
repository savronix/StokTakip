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
    /// StockMovementsPage.xaml etkileşim mantığı
    /// </summary>
    public partial class StockMovementsPage : Page
    {
        public StockMovementsPage()
        {
            InitializeComponent();
            Loaded += StockMovementsPage_Loaded;
        }

        private void StockMovementsPage_Loaded(object sender, RoutedEventArgs e)
        {
            DatabaseService.FillStockMovementsList(dtgrdStockMovementsList);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(HomePage));
        }

        private void dtgrdGuncelle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dtgrdSil_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
