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
    /// StockCardEditPage.xaml etkileşim mantığı
    /// </summary>
    public partial class StockCardEditPage : Page
    {
        public StockCardEditPage()
        {
            InitializeComponent();
            this.Loaded += StockCardEditPage_Loaded; ;

        }

        private void StockCardEditPage_Loaded(object sender, RoutedEventArgs e)
        {
            var page = PageCache.GetPage(typeof(StockCardTaskPage)) as StockCardTaskPage;

            if (page != null)
            {
                txtStockNumber.Text = StockCardTaskPage.StockNumber;
                txtStockCategory.Text = StockCardTaskPage.StockCategory;
                txtStockName.Text = StockCardTaskPage.StockName;
                foreach (ComboBoxItem item in cmbbxStockUnit.Items)
                {
                    // Assuming item.Content is a string
                    if (item.Content.ToString() == StockCardTaskPage.StockUnit)
                    {
                        cmbbxStockUnit.SelectedItem = item;
                        break; // Exit loop once found
                    }
                }

            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Verilerin boş olup olmadığını kontrol et
            if (string.IsNullOrWhiteSpace(txtStockNumber.Text) ||
                string.IsNullOrWhiteSpace(txtStockCategory.Text) ||
                string.IsNullOrWhiteSpace(txtStockName.Text) ||
                cmbbxStockUnit.SelectedItem == null)
            {
                MessageService.ShowSnackBar("Lütfen tüm alanları doldurun.", "Eksik Bilgi", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                return;
            }
            DatabaseService.StockCardUpdate(txtStockNumber.Text, txtStockCategory.Text, txtStockName.Text, ((ComboBoxItem)cmbbxStockUnit.SelectedItem).Content.ToString());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(StockCardTaskPage));
        }
    }
}
