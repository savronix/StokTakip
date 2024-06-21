using StokTakip.Services;
using System;
using System.Collections;
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
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace StokTakip.Views.Pages
{
    /// <summary>
    /// StockCardTaskPage.xaml etkileşim mantığı
    /// </summary>
    public partial class StockCardTaskPage : Page
    {
        public static string StockNumber = string.Empty;
        public static string StockCategory = string.Empty;
        public static string StockName = string.Empty;
        public static string StockUnit = string.Empty;
        public StockCardTaskPage()
        {
            InitializeComponent();
            this.Loaded += StockCardTaskPage_Loaded;
        }

        private void StockCardTaskPage_Loaded(object sender, RoutedEventArgs e)
        {
            StockNumber = string.Empty;
            StockCategory = string.Empty;
            StockName = string.Empty;
            StockUnit = string.Empty;
            DatabaseService.FillStockCardList(dtgrdStockList);
        }

        private void btnStockCardAdd_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(StockCardAddPage));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(HomePage));
        }

        private void dtgrdGuncelle_Click(object sender, RoutedEventArgs e)
        {
            MessageService.ShowSnackBar("Bilgileri güncelledikten sonra kaydetmeyi unutmayın!", "Dikkat", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Info, 1);
            int selectedRowIndex = dtgrdStockList.SelectedIndex;

            if (selectedRowIndex >= 0)
            {
                var itemsSource = dtgrdStockList.ItemsSource as IList;
                if (itemsSource != null && selectedRowIndex < itemsSource.Count)
                {
                    var selectedItem = itemsSource[selectedRowIndex] as DataRowView;

                    if (selectedItem != null)
                    {
                        StockNumber = selectedItem["StockNumber"].ToString();
                        StockCategory = selectedItem["StockCategory"].ToString();
                        StockName = selectedItem["StockName"].ToString();
                        StockUnit = selectedItem["StockUnit"].ToString();
                    }
                    FrameService.Navigate(typeof(StockCardEditPage));
                }
            }
            else
            {

            }
        }

        private async void dtgrdSil_Click(object sender, RoutedEventArgs e)
        {
            int selectedRowIndex = dtgrdStockList.SelectedIndex;

            if (selectedRowIndex >= 0)
            {
                var itemsSource = dtgrdStockList.ItemsSource as IList;
                if (itemsSource != null && selectedRowIndex < itemsSource.Count)
                {
                    var selectedItem = itemsSource[selectedRowIndex] as DataRowView;

                    if (selectedItem != null)
                    {
                        StockNumber = selectedItem["StockNumber"].ToString();
                        StockCategory = selectedItem["StockCategory"].ToString();
                        StockName = selectedItem["StockName"].ToString();
                        StockUnit = selectedItem["StockUnit"].ToString();
                        Wpf.Ui.Controls.MessageBox messageBox = new Wpf.Ui.Controls.MessageBox();
                        messageBox.Title = "Dikkat!";
                        messageBox.Content = $"{StockNumber} numaralı {StockCategory} kategorisine ait {StockName} silinecektir. Onaylıyor musunuz?";
                        messageBox.IsSecondaryButtonEnabled = false;
                        messageBox.PrimaryButtonText = "Evet";
                        messageBox.CloseButtonText = "Hayır";
                        var result = await messageBox.ShowDialogAsync();
                        if (result == Wpf.Ui.Controls.MessageBoxResult.Primary)
                        {
                            DatabaseService.StockCardDelete(StockNumber);
                            DatabaseService.FillStockCardList(dtgrdStockList);
                        }
                        else
                        {
                            MessageService.ShowSnackBar("Silme işlemi iptal edildi.", "Uyarı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                        }
                    }
                }
            }
        }
    }
}
