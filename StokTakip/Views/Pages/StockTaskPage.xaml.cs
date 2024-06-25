using StokTakip.Services;
using StokTakip.Views.Windows;
using System;
using System.Collections.Generic;
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
    /// StockAddPage.xaml etkileşim mantığı
    /// </summary>
    public partial class StockTaskPage : Page
    {
        private int StockLast = 0;
        public StockTaskPage()
        {
            InitializeComponent();
            Loaded += StockTaskPage_Loaded;
        }

        private void StockTaskPage_Loaded(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            StockLast = 0;
            txtStockCategory.Clear();
            txtStockName.Clear();
            txtStockNumber.Clear();
            cmbbxStockUnit.SelectedIndex = 0;
            cmbbxTask.SelectedIndex = 0;
            cmbbxTask.IsEnabled = true;
            txtTime.Clear();
            txtAmount.Clear();
            DateTime tarih = DateTime.Now;
            txtTime.Text = tarih.ToString("dd.MM.yyyy");
            txtDescription.Clear();
            txtTaskNumber.Text = "TN" + (DatabaseService.GetNextTaskNumber());
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            FrameService.Navigate(typeof(HomePage));
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            StockSelectWindow stockSelectWindow = new StockSelectWindow();
            string task = ((ComboBoxItem)cmbbxTask.SelectedItem).Content.ToString();
            if (task== "İşlem Seçiniz")
            {
                MessageService.ShowSnackBar("Lütfen işlem türünü seçiniz!.", "Eksik Bilgi", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
            }
            else
            {
                if (stockSelectWindow != null)
                {
                    stockSelectWindow.Task = task;
                    stockSelectWindow.ShowDialog();
                    txtStockNumber.Text = stockSelectWindow.StockNumber;
                    txtStockCategory.Text = stockSelectWindow.StockCategory;
                    txtStockName.Text = stockSelectWindow.StockName;
                    cmbbxStockUnit.SelectedItem = stockSelectWindow.StockUnit;
                    StockLast = stockSelectWindow.StockLast;

                }
            }
            

            

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Verilerin boş olup olmadığını kontrol et
            if ((string.IsNullOrWhiteSpace(txtTaskNumber.Text) ||
                string.IsNullOrWhiteSpace(txtTime.Text) ||
                string.IsNullOrWhiteSpace(txtAmount.Text) ||
                cmbbxTask.SelectedItem == null) || (string.IsNullOrWhiteSpace(txtStockNumber.Text) ||
                string.IsNullOrWhiteSpace(txtStockCategory.Text) ||
                string.IsNullOrWhiteSpace(txtStockName.Text) ||
                cmbbxStockUnit.SelectedItem == null))
            {
                MessageService.ShowSnackBar("Lütfen tüm alanları doldurun.", "Eksik Bilgi", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                return;
            }
            else
            {
                if (((ComboBoxItem)cmbbxTask.SelectedItem).Content.ToString() == "Çıkış")
                {
                    if (int.Parse(txtAmount.Text) <= StockLast)
                    {
                        DatabaseService.TaskAdd(txtTaskNumber.Text, ((ComboBoxItem)cmbbxTask.SelectedItem).Content.ToString(), txtTime.Text, txtAmount.Text, txtDescription.Text, txtStockNumber.Text, txtStockCategory.Text, txtStockName.Text, ((ComboBoxItem)cmbbxStockUnit.SelectedItem).Content.ToString());
                    }
                    else
                    {
                        MessageService.ShowSnackBar("Depoda bulunan miktardan fazla çıkış yapamazsın!.", "Dikkat", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);

                    }
                }
                else
                {
                    DatabaseService.TaskAdd(txtTaskNumber.Text, ((ComboBoxItem)cmbbxTask.SelectedItem).Content.ToString(), txtTime.Text, txtAmount.Text, txtDescription.Text, txtStockNumber.Text, txtStockCategory.Text, txtStockName.Text, ((ComboBoxItem)cmbbxStockUnit.SelectedItem).Content.ToString());
                }
            }
           
        }

        private void cmbbxTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string cmbbxTaskText = ((ComboBoxItem)cmbbxTask.SelectedItem).Content.ToString();
            if (cmbbxTaskText=="Giriş"||cmbbxTaskText=="Çıkış")
            {
                cmbbxTask.IsEnabled = false;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
    }
}
