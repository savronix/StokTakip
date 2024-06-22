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
        private static string GetPassword()
        {
            string password = string.Empty;
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // SQL sorgusunu tanımlayın
                string query = "SELECT password FROM userinfo";

                // SQL komutunu oluşturun
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    // Veritabanından veriyi okuyun
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        // Sonuçları işleyin
                        while (reader.Read())
                        {
                            password = reader["password"].ToString();
                        }
                    }
                }
            }
            return password;
        }
        public static string GetMail()
        {
            string mail = string.Empty;
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // SQL sorgusunu tanımlayın
                string query = "SELECT mail FROM userinfo";

                // SQL komutunu oluşturun
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    // Veritabanından veriyi okuyun
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        // Sonuçları işleyin
                        while (reader.Read())
                        {
                            mail = reader["mail"].ToString();
                        }
                    }
                }
            }
            return mail;
        }
        public static string GetImageSource()
        {
            string imageSource = string.Empty;
            using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
            {
                conn.Open();

                // SQL sorgusunu tanımlayın
                string query = "SELECT imageSource FROM userinfo";

                // SQL komutunu oluşturun
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    // Veritabanından veriyi okuyun
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        // Sonuçları işleyin
                        while (reader.Read())
                        {
                            imageSource = reader["imageSource"].ToString();
                        }
                    }
                }
            }
            return imageSource;
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
                    new MessageService("Veritabanı hatası: " + ex.Message, "Bir hata meydana geldi!", false);
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
                if (task == "Giriş")
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
        public static void TaskDelete(string taskNo,string task, string stockNumber,string amount)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM stockmovements WHERE TaskNo = @TaskNo";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TaskNo", taskNo);
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            if (task == "Giriş")
                            {
                                string updateSql = "UPDATE stocks SET StockLogin = StockLogin - @Amount, StockLast = (StockLogin - @Amount) - StockOut WHERE StockNumber = @StockNumber";

                                using (SQLiteCommand updateCommand = new SQLiteCommand(updateSql, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount)); // Amount INTEGER olduğu için Int32 olarak ekledik
                                    updateCommand.Parameters.AddWithValue("@StockNumber", stockNumber);

                                    updateCommand.ExecuteNonQuery();
                                }
                            }
                            else if (task == "Çıkış")
                            {
                                string updateSql = "UPDATE stocks SET StockOut = StockOut - @Amount, StockLast = StockLogin - (StockOut - @Amount) WHERE StockNumber = @StockNumber";

                                using (SQLiteCommand updateCommand = new SQLiteCommand(updateSql, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount)); // Amount INTEGER olduğu için Int32 olarak ekledik
                                    updateCommand.Parameters.AddWithValue("@StockNumber", stockNumber);

                                    updateCommand.ExecuteNonQuery();
                                }
                            }
                            MessageService.ShowSnackBar("İşlem başarıyla silindi.", "Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                        }
                        else
                        {
                            MessageService.ShowSnackBar("Silinecek işlem bulunamadı.", "Uyarı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.ErrorCircle20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                }
            }
        }
        public static int GetStockLast(string stockNumber)
        {
            int stockLast = 0;

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string sql = "SELECT StockLast FROM stocks WHERE StockNumber = @StockNumber";

                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@StockNumber", stockNumber);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stockLast = Convert.ToInt32(reader["StockLast"]);
                        }
                    }
                }

                connection.Close();
            }

            return stockLast;
        }

        public static void TaskUpdate(string taskNo, string taskTime, string taskDescription, string amount, int StockLast)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                // Task bilgilerini çek
                string selectSql = "SELECT Task, Amount, StockNumber FROM stockmovements WHERE TaskNo = @TaskNo";
                string originalTask = string.Empty;
                int originalAmount = 0;
                string stockNumber = string.Empty;

                using (SQLiteCommand selectCommand = new SQLiteCommand(selectSql, connection))
                {
                    selectCommand.Parameters.AddWithValue("@TaskNo", taskNo);

                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            originalTask = reader["Task"].ToString();
                            originalAmount = Convert.ToInt32(reader["Amount"]);
                            stockNumber = reader["StockNumber"].ToString();
                        }
                    }
                }

                if (string.IsNullOrEmpty(originalTask) || string.IsNullOrEmpty(stockNumber))
                {
                    MessageService.ShowSnackBar("Görev bulunamadı.", "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                    return;
                }

                int newAmount = Convert.ToInt32(amount);
                int amountDifference = newAmount - originalAmount;

                // Stok güncellemesi için kontrol
                if (originalTask == "Çıkış" && amountDifference > StockLast)
                {
                    MessageService.ShowSnackBar("Depoda bulunan miktardan fazla çıkış yapamazsınız.", "Dikkat", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                    return;
                }

                // Task güncelle
                string updateSql = "UPDATE stockmovements SET TaskTime = @TaskTime, TaskDescription = @TaskDescription, Amount = @Amount WHERE TaskNo = @TaskNo";

                using (SQLiteCommand updateCommand = new SQLiteCommand(updateSql, connection))
                {
                    updateCommand.Parameters.AddWithValue("@TaskTime", taskTime);
                    updateCommand.Parameters.AddWithValue("@TaskDescription", taskDescription);
                    updateCommand.Parameters.AddWithValue("@Amount", newAmount);
                    updateCommand.Parameters.AddWithValue("@TaskNo", taskNo);

                    updateCommand.ExecuteNonQuery();
                }

                // Stok güncelle
                if (originalTask == "Giriş")
                {
                    string stockUpdateSql = "UPDATE stocks SET StockLogin = StockLogin + @AmountDifference, StockLast = (StockLogin + @AmountDifference) - StockOut WHERE StockNumber = @StockNumber";
                    using (SQLiteCommand stockUpdateCommand = new SQLiteCommand(stockUpdateSql, connection))
                    {
                        stockUpdateCommand.Parameters.AddWithValue("@AmountDifference", amountDifference);
                        stockUpdateCommand.Parameters.AddWithValue("@StockNumber", stockNumber);

                        stockUpdateCommand.ExecuteNonQuery();
                    }
                }
                else if (originalTask == "Çıkış")
                {
                    string stockUpdateSql = "UPDATE stocks SET StockOut = StockOut + @AmountDifference, StockLast = StockLogin - (StockOut + @AmountDifference) WHERE StockNumber = @StockNumber";
                    using (SQLiteCommand stockUpdateCommand = new SQLiteCommand(stockUpdateSql, connection))
                    {
                        stockUpdateCommand.Parameters.AddWithValue("@AmountDifference", amountDifference);
                        stockUpdateCommand.Parameters.AddWithValue("@StockNumber", stockNumber);

                        stockUpdateCommand.ExecuteNonQuery();
                    }
                }

                MessageService.ShowSnackBar("İşlem başarıyla güncellendi.", "Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                FrameService.Navigate(typeof(StockMovementsPage));

                connection.Close();
            }
        }

        public static void StockCardAdd(string StockNumber, string StockCategory, string StockName, string StockUnit)
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
                        MessageService.ShowSnackBar("Stok başarıyla kaydedildi.", "Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
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
        public static void PasswordUpdate(string oldPassword, string newPassword)
        {

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    string passwordQuery = "UPDATE userinfo SET password = @NewPassword WHERE password = @OldPassword";
                    using (SQLiteCommand passwordCommand = new SQLiteCommand(passwordQuery, connection))
                    {
                        passwordCommand.Parameters.AddWithValue("@NewPassword", newPassword);
                        passwordCommand.Parameters.AddWithValue("@OldPassword", oldPassword);

                        int rowsAffected = passwordCommand.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            MessageService.ShowSnackBar("Şifre güncellenemedi. Eski şifre yanlış olabilir", "Güncelleme Başarısız", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.ArrowClockwise20), Wpf.Ui.Controls.ControlAppearance.Dark, 2);
                        }
                        else
                        {
                            MessageService.ShowSnackBar("Şifre başarıyla güncellendi", "Güncelleme Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.ArrowClockwise20), Wpf.Ui.Controls.ControlAppearance.Dark, 2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                }
            }

        }
        public static void PasswordUpdate(string newPassword)
        {

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string password = GetPassword();
                    string passwordQuery = "UPDATE userinfo SET password = @NewPassword WHERE password = @OldPassword";
                    using (SQLiteCommand passwordCommand = new SQLiteCommand(passwordQuery, connection))
                    {
                        passwordCommand.Parameters.AddWithValue("@NewPassword", newPassword);
                        passwordCommand.Parameters.AddWithValue("@OldPassword", password);
                        int rowsAffected = passwordCommand.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            MessageService.ShowSnackBar("Şifre ayarlanamadı.", "İşlem Başarısız", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.DismissCircle20), Wpf.Ui.Controls.ControlAppearance.Dark, 2);
                        }
                        else
                        {
                            MessageService.ShowSnackBar("Şifre başarıyla ayarlandı", "İşlem Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                }
            }

        }
        public static void MailSetandUpdate(string mail, string password)
        {

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    string mailQuery = "UPDATE userinfo SET mail = @Mail WHERE password = @Password";
                    using (SQLiteCommand mailCommand = new SQLiteCommand(mailQuery, connection))
                    {
                        mailCommand.Parameters.AddWithValue("@Mail", mail);
                        mailCommand.Parameters.AddWithValue("@Password", password);

                        int rowsAffected = mailCommand.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            MessageService.ShowSnackBar("Mail ayarlanamadı. Şifre yanlış olabilir", "İşlem Başarısız", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.ArrowClockwise20), Wpf.Ui.Controls.ControlAppearance.Dark, 2);
                        }
                        else
                        {
                            MessageService.ShowSnackBar("Mail ayarlandı", "İşlem Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.ArrowClockwise20), Wpf.Ui.Controls.ControlAppearance.Dark, 2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                }
            }

        }
        public static void NameSurnameUpdate(string nameSurname,string password)
        {

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    string nameSurnameQuery = "UPDATE userinfo SET nameSurname = @NameSurname WHERE password = @Password";
                    using (SQLiteCommand nameSurnameCommand = new SQLiteCommand(nameSurnameQuery, connection))
                    {
                        nameSurnameCommand.Parameters.AddWithValue("@Password", password);
                        nameSurnameCommand.Parameters.AddWithValue("@NameSurname", nameSurname);

                        int rowsAffected = nameSurnameCommand.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            MessageService.ShowSnackBar("İsim Soyisim güncellenemedi. Şifre yanlış olabilir", "Güncelleme Başarısız", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.ArrowClockwise20), Wpf.Ui.Controls.ControlAppearance.Dark, 2);
                        }
                        else
                        {
                            MessageService.ShowSnackBar("İsim Soyisim başarıyla güncellendi", "Güncelleme Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.ArrowClockwise20), Wpf.Ui.Controls.ControlAppearance.Dark, 2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                }
            }

        }
        public static void ProfilePictureUpdate(string password,string imagePath)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    string nameSurnameQuery = "UPDATE userinfo SET imageSource = @ImagePath WHERE password = @Password";
                    using (SQLiteCommand nameSurnameCommand = new SQLiteCommand(nameSurnameQuery, connection))
                    {
                        nameSurnameCommand.Parameters.AddWithValue("@Password", password);
                        nameSurnameCommand.Parameters.AddWithValue("@ImagePath", imagePath);

                        int rowsAffected = nameSurnameCommand.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            MessageService.ShowSnackBar("Profil Resmi güncellenemedi. Şifre yanlış olabilir", "Güncelleme Başarısız", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.ArrowClockwise20), Wpf.Ui.Controls.ControlAppearance.Dark, 2);
                        }
                        else
                        {
                            MessageService.ShowSnackBar("Profil Resmi başarıyla güncellendi", "Güncelleme Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.ArrowClockwise20), Wpf.Ui.Controls.ControlAppearance.Dark, 2);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowSnackBar("Bir hata oluştu: " + ex.Message, "Hata", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
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

                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        string stockQuery = "UPDATE stocks SET StockCategory = @StockCategory, StockName = @StockName, StockUnit = @StockUnit " +
                                            "WHERE StockNumber = @StockNumber";
                        using (SQLiteCommand stockCommand = new SQLiteCommand(stockQuery, connection))
                        {
                            stockCommand.Parameters.AddWithValue("@StockNumber", StockNumber);
                            stockCommand.Parameters.AddWithValue("@StockCategory", StockCategory);
                            stockCommand.Parameters.AddWithValue("@StockName", StockName);
                            stockCommand.Parameters.AddWithValue("@StockUnit", StockUnit);
                            int stockRowsAffected = stockCommand.ExecuteNonQuery();

                            if (stockRowsAffected > 0)
                            {
                                string stockMovementsQuery = "UPDATE stockmovements SET StockCategory = @StockCategory, StockName = @StockName, StockUnit = @StockUnit " +
                                                             "WHERE StockNumber = @StockNumber";
                                using (SQLiteCommand stockMovementsCommand = new SQLiteCommand(stockMovementsQuery, connection))
                                {
                                    stockMovementsCommand.Parameters.AddWithValue("@StockNumber", StockNumber);
                                    stockMovementsCommand.Parameters.AddWithValue("@StockCategory", StockCategory);
                                    stockMovementsCommand.Parameters.AddWithValue("@StockName", StockName);
                                    stockMovementsCommand.Parameters.AddWithValue("@StockUnit", StockUnit);
                                    stockMovementsCommand.ExecuteNonQuery();
                                }

                                transaction.Commit();
                                MessageService.ShowSnackBar("Stok başarıyla güncellendi.", "Başarılı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Checkmark20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                            }
                            else
                            {
                                transaction.Rollback();
                                MessageService.ShowSnackBar("Stok numarası bulunamadı.", "Uyarı", new Wpf.Ui.Controls.SymbolIcon(Wpf.Ui.Controls.SymbolRegular.Warning20), Wpf.Ui.Controls.ControlAppearance.Dark, 1);
                            }
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
