using StokTakip.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public static string StockNumber = string.Empty;
        public static string StockCategory = string.Empty;
        public static string StockName = string.Empty;
        public static string StockUnit = string.Empty;
        public static string TaskNo = string.Empty;
        public static string Task = string.Empty;
        public static string TaskTime = string.Empty;
        public static string TaskDescription = string.Empty;
        public static string Amount = string.Empty;
        public static int StockLast = 0;

        public StockMovementsPage()
        {
            InitializeComponent();
            Loaded += StockMovementsPage_Loaded;
        }

        private void StockMovementsPage_Loaded(object sender, RoutedEventArgs e)
        {
            StockNumber = string.Empty;
            StockCategory = string.Empty;
            StockName = string.Empty;
            StockUnit = string.Empty;
            TaskNo = string.Empty;
            Task = string.Empty;
            TaskTime = string.Empty;
            TaskDescription = string.Empty;
            Amount = string.Empty;
            StockLast = 0;
            DatabaseService.FillStockMovementsList(dtgrdStockMovementsList);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(HomePage));
        }

        private void dtgrdGuncelle_Click(object sender, RoutedEventArgs e)
        {
            MessageService.ShowSnackBar("Bilgileri güncelledikten sonra kaydetmeyi unutmayın!", "Dikkat", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Info, 1);
            int selectedRowIndex = dtgrdStockMovementsList.SelectedIndex;

            if (selectedRowIndex >= 0)
            {
                var itemsSource = dtgrdStockMovementsList.ItemsSource as IList;
                if (itemsSource != null && selectedRowIndex < itemsSource.Count)
                {
                    var selectedItem = itemsSource[selectedRowIndex] as DataRowView;

                    if (selectedItem != null)
                    {
                        StockNumber = selectedItem["StockNumber"].ToString();
                        StockCategory = selectedItem["StockCategory"].ToString();
                        StockName = selectedItem["StockName"].ToString();
                        StockUnit = selectedItem["StockUnit"].ToString();
                        TaskNo = selectedItem["TaskNo"].ToString();
                        Task = selectedItem["Task"].ToString();
                        TaskTime = selectedItem["TaskTime"].ToString();
                        TaskDescription = selectedItem["TaskDescription"].ToString();
                        Amount = selectedItem["Amount"].ToString();
                        StockLast=DatabaseService.GetStockLast(StockNumber);
                    }
                    FrameService.Navigate(typeof(StockTaskEditPage));
                }
            }
        }

        private void dtgrdSil_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
