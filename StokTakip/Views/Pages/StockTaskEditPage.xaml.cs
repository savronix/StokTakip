using StokTakip.Services;
using StokTakip.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public partial class StockTaskEditPage : Page
    {
        private int StockLast = 0;
        public StockTaskEditPage()
        {
            InitializeComponent();
            Loaded += StockTaskPage_Loaded;
        }

        private void StockTaskPage_Loaded(object sender, RoutedEventArgs e)
        {
            Clear();
            var page = PageCache.GetPage(typeof(StockMovementsPage)) as StockMovementsPage;

            if (page != null)
            {
                if (StockMovementsPage.StockNumber != null && StockMovementsPage.StockCategory != null && StockMovementsPage.StockName != null &&
        StockMovementsPage.StockUnit != null && StockMovementsPage.TaskNo != null && StockMovementsPage.Task != null &&
        StockMovementsPage.TaskTime != null && StockMovementsPage.TaskDescription != null && StockMovementsPage.Amount != null && StockMovementsPage.StockLast!=0)
                {
                    txtStockNumber.Text = StockMovementsPage.StockNumber;
                    txtStockCategory.Text = StockMovementsPage.StockCategory;
                    txtStockName.Text = StockMovementsPage.StockName;
                    foreach (ComboBoxItem item in cmbbxStockUnit.Items)
                    {
                        // Assuming item.Content is a string
                        if (item.Content.ToString() == StockMovementsPage.StockUnit)
                        {
                            cmbbxStockUnit.SelectedItem = item;
                            break; // Exit loop once found
                        }
                    }
                    txtAmount.Text = StockMovementsPage.Amount;
                    txtTaskNumber.Text = StockMovementsPage.TaskNo;
                    txtTime.Text = StockMovementsPage.TaskTime;
                    txtDescription.Text = StockMovementsPage.TaskDescription;
                    foreach (ComboBoxItem item in cmbbxTask.Items)
                    {
                        // Assuming item.Content is a string
                        if (item.Content.ToString() == StockMovementsPage.Task)
                        {
                            cmbbxTask.SelectedItem = item;
                            break; // Exit loop once found
                        }
                    }
                    StockLast=StockMovementsPage.StockLast;
                    txtTaskNumber.IsEnabled = false;
                    cmbbxTask.IsEnabled = false;
                }
                else
                {
                    cmbbxTask.IsEnabled = true;
                }
            }
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
            FrameService.Navigate(typeof(StockMovementsPage));
        }





        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskNumber.Text) ||
                string.IsNullOrWhiteSpace(txtTime.Text) ||
                string.IsNullOrWhiteSpace(txtAmount.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageService.ShowSnackBar("Lütfen tüm alanları doldurun.", "Eksik Bilgi", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                return;
            }

            string taskNo = txtTaskNumber.Text;
            string taskTime = txtTime.Text;
            string taskDescription = txtDescription.Text;
            string amount = txtAmount.Text;

            if (!int.TryParse(amount, out int amountValue) || amountValue <= 0)
            {
                MessageService.ShowSnackBar("Geçerli bir miktar girin.", "Geçersiz Miktar", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                return;
            }

            DatabaseService.TaskUpdate(taskNo, taskTime, taskDescription, amount,StockLast);
        }
    }
}
