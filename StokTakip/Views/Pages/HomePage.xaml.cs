using StokTakip.Services;
using System;
using System.Collections.Generic;
using System.Data;
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

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            if (dtgrdStockList.ItemsSource is DataView dataView)
            {
                if (rdbtnCategory.IsChecked == true)
                {
                    string searchText = txtStockCategory.Text.Trim();

                    // Apply the filter to the DataView
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        // Example filter logic: filter by StockName containing the searchText
                        dataView.RowFilter = $"StockCategory LIKE '%{searchText}%'";
                    }
                    else
                    {
                        // Clear the filter if the search box is empty
                        dataView.RowFilter = string.Empty;
                    }
                }
                else if (rdbtnName.IsChecked == true)
                {
                    string searchText = txtStockName.Text.Trim();

                    // Apply the filter to the DataView
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        // Example filter logic: filter by StockName containing the searchText
                        dataView.RowFilter = $"StockName LIKE '%{searchText}%'";
                    }
                    else
                    {
                        // Clear the filter if the search box is empty
                        dataView.RowFilter = string.Empty;
                    }
                }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            if (dtgrdStockList.ItemsSource is DataView dataView)
            {
                dataView.RowFilter = string.Empty;
            }
            txtStockName.Clear();
            txtStockCategory.Clear();
            rdbtnName.IsChecked = false;
            rdbtnCategory.IsChecked = false;
        }
    }
}
