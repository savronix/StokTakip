using System.Data.SQLite;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using StokTakip.Views.Pages;
using Wpf.Ui.Controls;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace StokTakip.Services
{
    public class DatabaseService
    {
        static string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;

        // Veritabanı dosyasının yolunu oluştur
        static string databasePath = Path.Combine(applicationDirectory, "database.db");

        private static string ConnectionString = $@"Data Source={databasePath};Version=3;";
        public DatabaseService() { }
        public static string GetNameSurname()
        {
            string nameSurname = string.Empty;
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // SQL sorgusunu tanımlayın
                string query = "SELECT nameSurname FROM userinfo";

                // SQL komutunu oluşturun
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    // Veritabanından veriyi okuyun
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        // Sonuçları işleyin
                        while (reader.Read())
                        {
                            nameSurname = reader["nameSurname"].ToString();
                        }
                    }
                }
            }
            return nameSurname;
        }
        public static bool ValidateUser(string password)
        {
            bool isValid = false;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open(); // Veritabanı bağlantısını aç

                string query = "SELECT nameSurname FROM userinfo WHERE password = @password";

                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@password", password);

                try
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nameSurname = reader["nameSurname"].ToString();
                            isValid = true;
                        }
                        else
                        {
                            isValid = false;
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    new MessageService("Veritabanı hatası: " + ex.Message, "Bir hata meydana geldi!",false);
                }
            }

            return isValid;
        }
        public static void FillStockList(System.Windows.Controls.DataGrid dataGrid)
        {
            dataGrid.ItemsSource = null;

            // Veritabanı bağlantısını oluştur
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    // Bağlantıyı aç
                    connection.Open();

                    // SQL sorgusunu tanımla
                    string query = "SELECT StockNumber, StockCategory, StockName, StockUnit, StockLogin, StockOut, StockLast FROM stocks";

                    // SQLite veri adaptörünü oluştur ve sorguyu çalıştır
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                    {
                        // Veri seti oluştur ve adaptörü kullanarak verileri doldur
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "stocks");

                        // DataGrid'in veri kaynağını ayarla ve sıralama işlemini yap
                        DataView dataView = dataSet.Tables["stocks"].DefaultView;
                        dataView.Sort = "StockNumber ASC";
                        dataGrid.ItemsSource = dataView;
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                }
            }
        }

        public static void FillStockCardList(System.Windows.Controls.DataGrid dataGrid)
        {
            dataGrid.ItemsSource = null;

            // Veritabanı bağlantısını oluştur
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    // Bağlantıyı aç
                    connection.Open();

                    // SQL sorgusunu tanımla
                    string query = "SELECT StockNumber, StockCategory, StockName, StockUnit FROM stocks";

                    // SQLite veri adaptörünü oluştur ve sorguyu çalıştır
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                    {
                        // Veri seti oluştur ve adaptörü kullanarak verileri doldur
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "stocks");

                        // DataGrid'in veri kaynağını ayarla ve sıralama işlemini yap
                        DataView dataView = dataSet.Tables["stocks"].DefaultView;
                        dataView.Sort = "StockNumber ASC";
                        dataGrid.ItemsSource = dataView;
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                }
            }
        }
        public static void FillStockMovementsList(System.Windows.Controls.DataGrid dataGrid)
        {
            dataGrid.ItemsSource = null;

            // Veritabanı bağlantısını oluştur
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    // Bağlantıyı aç
                    connection.Open();

                    // SQL sorgusunu tanımla
                    string query = "SELECT * FROM stockmovements";

                    // SQLite veri adaptörünü oluştur ve sorguyu çalıştır
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                    {
                        // Veri seti oluştur ve adaptörü kullanarak verileri doldur
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "stockmovements");

                        // DataGrid'in veri kaynağını ayarla ve sıralama işlemini yap
                        DataView dataView = dataSet.Tables["stockmovements"].DefaultView;
                        dataView.Sort = "TaskNo ASC";
                        dataGrid.ItemsSource = dataView;
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                }
            }
        }
        public static void TaskAdd(string taskNo, string task, string taskTime, string amount, string taskDescription, string stockNumber, string stockCategory, string stockName, string stockUnit)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string sql = "INSERT INTO stockmovements (TaskNo, Task, TaskTime, Amount, TaskDescription, StockNumber, StockCategory, StockName, StockUnit) " +
                             "VALUES (@TaskNo, @Task, @TaskTime, @Amount, @TaskDescription, @StockNumber, @StockCategory, @StockName, @StockUnit)";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@TaskNo", taskNo);
                    command.Parameters.AddWithValue("@Task", task);
                    command.Parameters.AddWithValue("@TaskTime", taskTime);
                    command.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount));
                    command.Parameters.AddWithValue("@TaskDescription", taskDescription);
                    command.Parameters.AddWithValue("@StockNumber", stockNumber);
                    command.Parameters.AddWithValue("@StockCategory", stockCategory);
                    command.Parameters.AddWithValue("@StockName", stockName);
                    command.Parameters.AddWithValue("@StockUnit", stockUnit);

                    command.ExecuteNonQuery();
                }
                if (task=="Giriş")
                {
                    string updateSql = "UPDATE stocks SET StockLogin = StockLogin + @Amount, StockLast = (StockLogin + @Amount) - StockOut WHERE StockNumber = @StockNumber";

                    using (SQLiteCommand updateCommand = new SQLiteCommand(updateSql, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount)); // Amount INTEGER olduğu için Int32 olarak ekledik
                        updateCommand.Parameters.AddWithValue("@StockNumber", stockNumber);

                        updateCommand.ExecuteNonQuery();
                    }
                }
                else if (task == "Çıkış")
                {
                    string updateSql = "UPDATE stocks SET StockOut = StockOut + @Amount, StockLast = StockLogin - (StockOut + @Amount) WHERE StockNumber = @StockNumber";

                    using (SQLiteCommand updateCommand = new SQLiteCommand(updateSql, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount)); // Amount INTEGER olduğu için Int32 olarak ekledik
                        updateCommand.Parameters.AddWithValue("@StockNumber", stockNumber);

                        updateCommand.ExecuteNonQuery();
                    }
                }
                

                MessageService.ShowSnackBar("İşlem başarıyla kaydedildi.", "Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                FrameService.Navigate(typeof(HomePage));

                connection.Close();
            }
        }
        public static void StockCardAdd(string StockNumber,string StockCategory,string StockName,string StockUnit)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO stocks (StockNumber, StockCategory, StockName, StockUnit, StockLogin, StockOut, StockLast) " +
                                   "VALUES (@StockNumber, @StockCategory, @StockName, @StockUnit, 0, 0, 0)";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StockNumber", StockNumber);
                        command.Parameters.AddWithValue("@StockCategory", StockCategory);
                        command.Parameters.AddWithValue("@StockName", StockName);
                        command.Parameters.AddWithValue("@StockUnit", StockUnit);
                        command.ExecuteNonQuery();
                        MessageService.ShowSnackBar("Stok başarıyla kaydedildi.", "Başarılı",new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20),Wpf.Ui.Controls.ControlAppearance.Dark,1);
                        FrameService.Navigate(typeof(StockCardTaskPage));
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                }
            }
        }
        public static void StockCardDelete(string StockNumber)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM stocks WHERE StockNumber = @StockNumber";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StockNumber", StockNumber);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageService.ShowSnackBar("Stok başarıyla silindi.", "Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                        }
                        else
                        {
                            MessageService.ShowSnackBar("Silinecek stok bulunamadı.", "Uyarı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.ErrorCircle20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                }
            }
        }

        public static void StockCardUpdate(string StockNumber, string StockCategory, string StockName, string StockUnit)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE stocks SET StockCategory = @StockCategory, StockName = @StockName, StockUnit = @StockUnit " +
                                   "WHERE StockNumber = @StockNumber";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StockNumber", StockNumber);
                        command.Parameters.AddWithValue("@StockCategory", StockCategory);
                        command.Parameters.AddWithValue("@StockName", StockName);
                        command.Parameters.AddWithValue("@StockUnit", StockUnit);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageService.ShowSnackBar("Stok başarıyla güncellendi.", "Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                        }
                        else
                        {
                            MessageService.ShowSnackBar("Stok numarası bulunamadı.", "Uyarı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                        }

                        FrameService.Navigate(typeof(StockCardTaskPage));
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                }
            }
        }

        public static string GetNextStockNumber()
        {
            List<int> stockNumbers = new List<int>();

            // SQLite bağlantısını oluştur
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                // Bağlantıyı aç
                connection.Open();

                // Sorguyu tanımla
                string query = "SELECT stocknumber FROM stocks";

                // SQLite komutunu oluştur
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // Sorguyu çalıştır ve verileri oku
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stockNumbers.Add(Convert.ToInt32(reader["stocknumber"].ToString().Substring(2))); // ST'yi çıkar
                        }
                    }
                }
            }

            // Stock numaralarını sırala
            stockNumbers.Sort();

            // Eksik olanları bul ve tamamla
            int nextStockNumber = 1;
            for (int i = 0; i < stockNumbers.Count; i++)
            {
                if (stockNumbers[i] != nextStockNumber)
                {
                    return nextStockNumber.ToString("D5"); // 5 haneli formatla
                }
                nextStockNumber++;
            }

            // Tüm mevcut stock numaraları tamamlanmışsa bir sonrakini döndür
            return nextStockNumber.ToString("D5"); // 5 haneli formatla
        }

        public static string GetNextTaskNumber()
        {
            List<int> taskNumbers = new List<int>();

            // SQLite bağlantısını oluştur
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                // Bağlantıyı aç
                connection.Open();

                // Sorguyu tanımla
                string query = "SELECT TaskNo FROM stockmovements";

                // SQLite komutunu oluştur
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    // Sorguyu çalıştır ve verileri oku
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taskNumbers.Add(Convert.ToInt32(reader["TaskNo"].ToString().Substring(2))); // TN'yi çıkar
                        }
                    }
                }
            }

            // Stock numaralarını sırala
            taskNumbers.Sort();

            // Eksik olanları bul ve tamamla
            int nextTaskNumber = 1;
            for (int i = 0; i < taskNumbers.Count; i++)
            {
                if (taskNumbers[i] != nextTaskNumber)
                {
                    return nextTaskNumber.ToString("D5"); // 5 haneli formatla
                }
                nextTaskNumber++;
            }

            // Tüm mevcut stock numaraları tamamlanmışsa bir sonrakini döndür
            return nextTaskNumber.ToString("D5"); // 5 haneli formatla
        }

    }
}
