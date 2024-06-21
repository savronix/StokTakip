using StokTakip.Services;
using StokTakip.Views.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace StokTakip.Views.Windows
{
    /// <summary>
    /// StockSelectWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class StockSelectWindow : FluentWindow
    {
        public string Task = string.Empty;
        public string StockNumber = string.Empty;
        public string StockCategory = string.Empty;
        public string StockName = string.Empty;
        public string StockUnit = string.Empty;
        public int StockLast = 0;
        public StockSelectWindow()
        {
            InitializeComponent();
            this.Loaded += StockSelectPage_Loaded;
        }

        private void StockSelectPage_Loaded(object sender, RoutedEventArgs e)
        {
            StockNumber = string.Empty;
            StockCategory = string.Empty;
            StockName = string.Empty;
            StockUnit = string.Empty;
            DatabaseService.FillStockList(dtgrdStockList);

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (dtgrdStockList.ItemsSource is DataView dataView)
                {
                    // Get the text from the search TextBox
                    string searchText = txtSearch.Text.Trim();

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
            catch (Exception ex)
            {
                MessageService.ShowSnackBar($"Bir hata oluştu: {ex.Message}", "Uyarı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 5);
            }
        }



        private void dtgrdSec_Click(object sender, RoutedEventArgs e)
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
                        if (Task == "Çıkış" && int.Parse(selectedItem["StockLast"].ToString()) <= 0)
                        {
                            ShowSnackBar($"Ürün yokken nasıl çıkış yapacaksın!", "Uyarı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Danger, 5);
                        }
                        else
                        {
                            StockNumber = selectedItem["StockNumber"].ToString();
                            StockCategory = selectedItem["StockCategory"].ToString();
                            StockName = selectedItem["StockName"].ToString();
                            StockUnit = selectedItem["StockUnit"].ToString();
                            StockLast = int.Parse(selectedItem["StockLast"].ToString());
                            Close();
                        }


                    }
                }
            }
        }
        void ShowSnackBar(string _content, string _title, SymbolIcon _icon, ControlAppearance _controlAppearance, int _second)
        {
            var snackbar = new Snackbar(mySnackbarPresenter);
            snackbar.Appearance = _controlAppearance;
            snackbar.Title = _title;
            snackbar.Content = _content;
            snackbar.Icon = _icon;
            snackbar.Timeout = TimeSpan.FromSeconds(_second);
            snackbar.Show();
        }
    }
}
