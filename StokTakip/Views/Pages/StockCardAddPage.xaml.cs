using StokTakip.Services;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
    /// StockCardAddPage.xaml etkileşim mantığı
    /// </summary>
    public partial class StockCardAddPage : Page
    {
        public StockCardAddPage()
        {
            InitializeComponent();
            this.Loaded += StockCardAddPage_Loaded;
        }

        private void StockCardAddPage_Loaded(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtStockCategory.Clear();
            txtStockName.Clear();
            txtStockNumber.Clear();
            cmbbxStockUnit.SelectedIndex = 0;
            txtStockNumber.Text = "ST" + (DatabaseService.GetNextStockNumber());
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Verilerin boş olup olmadığını kontrol et
            if (string.IsNullOrWhiteSpace(txtStockNumber.Text) ||
                string.IsNullOrWhiteSpace(txtStockCategory.Text) ||
                string.IsNullOrWhiteSpace(txtStockName.Text) ||
                cmbbxStockUnit.SelectedItem == null)
            {
                MessageService.ShowSnackBar("Lütfen tüm alanları doldurun.", "Eksik Bilgi",new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20),Wpf.Ui.Controls.ControlAppearance.Dark,1);
                return;
            }
            DatabaseService.StockCardAdd(txtStockNumber.Text,txtStockCategory.Text, txtStockName.Text, ((ComboBoxItem)cmbbxStockUnit.SelectedItem).Content.ToString());
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(StockCardTaskPage));
        }
    }
}
