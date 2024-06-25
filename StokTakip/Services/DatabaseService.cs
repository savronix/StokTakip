using System.Data.SQLite;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using StokTakip.Views.Pages;
using Wpf.Ui.Controls;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using DataGrid = System.Windows.Controls.DataGrid;

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
            try
            {
                string nameSurname = string.Empty;
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT nameSurname FROM userinfo";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                nameSurname = reader["nameSurname"].ToString();
                            }
                        }
                    }
                }
                return nameSurname;
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
                return string.Empty;
            }
        }

        private static string GetPassword()
        {
            try
            {
                string password = string.Empty;
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT password FROM userinfo";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                password = reader["password"].ToString();
                            }
                        }
                    }
                }
                return password;
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
                return string.Empty;
            }
        }

        public static string GetMail()
        {
            try
            {
                string mail = string.Empty;
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT mail FROM userinfo";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mail = reader["mail"].ToString();
                            }
                        }
                    }
                }
                return mail;
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
                return string.Empty;
            }
        }

        public static string GetImageSource()
        {
            try
            {
                string imageSource = string.Empty;
                using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT imageSource FROM userinfo";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                imageSource = reader["imageSource"].ToString();
                            }
                        }
                    }
                }
                return imageSource;
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
                return string.Empty;
            }
        }

        public static bool ValidateUser(string password)
        {
            try
            {
                bool isValid = false;
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT nameSurname FROM userinfo WHERE password = @password";
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@password", password);

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
                return isValid;
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
                return false;
            }
        }

        public static void FillStockList(DataGrid dataGrid)
        {
            try
            {
                dataGrid.ItemsSource = null;
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT StockNumber, StockCategory, StockName, StockUnit, StockLogin, StockOut, StockLast FROM stocks";
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "stocks");
                        DataView dataView = dataSet.Tables["stocks"].DefaultView;
                        dataView.Sort = "StockNumber ASC";
                        dataGrid.ItemsSource = dataView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
            }
        }

        public static void FillStockCardList(DataGrid dataGrid)
        {
            try
            {
                dataGrid.ItemsSource = null;
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT StockNumber, StockCategory, StockName, StockUnit FROM stocks";
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "stocks");
                        DataView dataView = dataSet.Tables["stocks"].DefaultView;
                        dataView.Sort = "StockNumber ASC";
                        dataGrid.ItemsSource = dataView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
            }
        }

        public static void FillStockMovementsList(DataGrid dataGrid)
        {
            try
            {
                dataGrid.ItemsSource = null;
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM stockmovements";
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "stockmovements");
                        DataView dataView = dataSet.Tables["stockmovements"].DefaultView;
                        dataView.Sort = "TaskNo ASC";
                        dataGrid.ItemsSource = dataView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
            }
        }
        ///asdasdsda
        public static void TaskAdd(string taskNo, string task, string taskTime, string amount, string taskDescription, string stockNumber, string stockCategory, string stockName, string stockUnit)
        {
            try
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
                        command.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount)); // Consider handling conversion errors
                        command.Parameters.AddWithValue("@TaskDescription", taskDescription);
                        command.Parameters.AddWithValue("@StockNumber", stockNumber);
                        command.Parameters.AddWithValue("@StockCategory", stockCategory);
                        command.Parameters.AddWithValue("@StockName", stockName);
                        command.Parameters.AddWithValue("@StockUnit", stockUnit);

                        command.ExecuteNonQuery();
                    }

                    // Update stocks based on task type
                    if (task == "Giriş" || task == "Çıkış")
                    {
                        string updateSql = task == "Giriş"
                            ? "UPDATE stocks SET StockLogin = StockLogin + @Amount, StockLast = (StockLogin + @Amount) - StockOut WHERE StockNumber = @StockNumber"
                            : "UPDATE stocks SET StockOut = StockOut + @Amount, StockLast = StockLogin - (StockOut + @Amount) WHERE StockNumber = @StockNumber";

                        using (SQLiteCommand updateCommand = new SQLiteCommand(updateSql, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount)); // Handle conversion errors
                            updateCommand.Parameters.AddWithValue("@StockNumber", stockNumber);

                            updateCommand.ExecuteNonQuery();
                        }
                    }

                    MessageService.ShowSuccessSnackbar("İşlem başarıyla kaydedildi.");
                    FrameService.Navigate(typeof(HomePage));
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
            }
        }

        public static void TaskDelete(string taskNo, string task, string stockNumber, string amount)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM stockmovements WHERE TaskNo = @TaskNo";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TaskNo", taskNo);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Update stocks based on task type
                            if (task == "Giriş" || task == "Çıkış")
                            {
                                string updateSql = task == "Giriş"
                                    ? "UPDATE stocks SET StockLogin = StockLogin - @Amount, StockLast = (StockLogin - @Amount) - StockOut WHERE StockNumber = @StockNumber"
                                    : "UPDATE stocks SET StockOut = StockOut - @Amount, StockLast = StockLogin - (StockOut - @Amount) WHERE StockNumber = @StockNumber";

                                using (SQLiteCommand updateCommand = new SQLiteCommand(updateSql, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@Amount", Convert.ToInt32(amount)); // Handle conversion errors
                                    updateCommand.Parameters.AddWithValue("@StockNumber", stockNumber);

                                    updateCommand.ExecuteNonQuery();
                                }
                            }

                            MessageService.ShowSuccessSnackbar("İşlem başarıyla silindi.");
                        }
                        else
                        {
                            MessageService.ShowWarningSnackbar("Silinecek işlem bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
            }
        }

        public static int GetStockLast(string stockNumber)
        {
            int stockLast = 0;

            try
            {
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
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
            }

            return stockLast;
        }

        public static void TaskUpdate(string taskNo, string taskTime, string taskDescription, string amount, int StockLast)
        {
            try
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
                        MessageService.ShowWarningSnackbar("Görev bulunamadı.");
                        return;
                    }

                    int newAmount = Convert.ToInt32(amount);
                    int amountDifference = newAmount - originalAmount;

                    // Stok güncellemesi için kontrol
                    if (originalTask == "Çıkış" && amountDifference > StockLast)
                    {
                        MessageService.ShowWarningSnackbar("Depoda bulunan miktardan fazla çıkış yapamazsınız.");
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
                    if (originalTask == "Giriş" || originalTask == "Çıkış")
                    {
                        string stockUpdateSql = originalTask == "Giriş"
                            ? "UPDATE stocks SET StockLogin = StockLogin + @AmountDifference, StockLast = (StockLogin + @AmountDifference) - StockOut WHERE StockNumber = @StockNumber"
                            : "UPDATE stocks SET StockOut = StockOut + @AmountDifference, StockLast = StockLogin - (StockOut + @AmountDifference) WHERE StockNumber = @StockNumber";

                        using (SQLiteCommand stockUpdateCommand = new SQLiteCommand(stockUpdateSql, connection))
                        {
                            stockUpdateCommand.Parameters.AddWithValue("@AmountDifference", amountDifference);
                            stockUpdateCommand.Parameters.AddWithValue("@StockNumber", stockNumber);

                            stockUpdateCommand.ExecuteNonQuery();
                        }
                    }

                    MessageService.ShowSuccessSnackbar("İşlem başarıyla güncellendi.");
                    FrameService.Navigate(typeof(StockMovementsPage));
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
            }
        }

        ///
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
                        MessageService.ShowSuccessSnackbar("Stok başarıyla kaydedildi.");
                        FrameService.Navigate(typeof(StockCardTaskPage));
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
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
                            DeleteRecordsByStockNumber(StockNumber);
                            MessageService.ShowSuccessSnackbar("Stok başarıyla silindi.");
                        }
                        else
                        {
                            MessageService.ShowWarningSnackbar("Silinecek stok bulunamadı.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
                }
            }
        }
        public static void DeleteRecordsByStockNumber(string stockNumber)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM stockmovements WHERE StockNumber = @StockNumber";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StockNumber", stockNumber);
                        int rowsAffected = command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
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
                            MessageService.ShowWarningSnackbar("Şifre güncellenemedi. Eski şifre yanlış olabilir");
                        }
                        else
                        {
                            MessageService.ShowSuccessSnackbar("Şifre başarıyla güncellendi");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
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
                            MessageService.ShowWarningSnackbar("Şifre ayarlanamadı.");
                        }
                        else
                        {
                            MessageService.ShowSuccessSnackbar("Şifre başarıyla ayarlandı");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
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
                            MessageService.ShowWarningSnackbar("Mail ayarlanamadı. Şifre yanlış olabilir");
                        }
                        else
                        {
                            MessageService.ShowSuccessSnackbar("Mail ayarlandı");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
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
                            MessageService.ShowWarningSnackbar("İsim Soyisim güncellenemedi. Şifre yanlış olabilir");
                        }
                        else
                        {
                            MessageService.ShowSuccessSnackbar("İsim Soyisim başarıyla güncellendi");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
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
                            MessageService.ShowWarningSnackbar("Profil Resmi güncellenemedi. Şifre yanlış olabilir");
                        }
                        else
                        {
                            MessageService.ShowSuccessSnackbar("Profil Resmi başarıyla güncellendi");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
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
                                MessageService.ShowSuccessSnackbar("Stok başarıyla güncellendi.");
                            }
                            else
                            {
                                transaction.Rollback();
                                MessageService.ShowWarningSnackbar("Stok numarası bulunamadı.");
                            }
                        }

                        FrameService.Navigate(typeof(StockCardTaskPage));
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
                }
            }
        }


        public static string GetNextStockNumber()
        {
            try
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
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
                return string.Empty;
            }
            
        }

        public static string GetNextTaskNumber()
        {
            try
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
            catch (Exception ex)
            {
                MessageService.ShowErrorSnackbar("Veritabanı hatası: " + ex.Message);
                return string.Empty;
            }
            
        }

    }
}
