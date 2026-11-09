using Investment_Platform_Project.Model;
using Microsoft.Data.Sqlite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Investment_Platform_Project.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        // נתיב ההתחברות הישיר למסד הנתונים
        private const string connectionString = @"Data Source=C:\Users\Kobi Shemer\Documents\Investment Project DB\Investment Project DataBase Updated.db;Version=3;";

        // משתנה גנרי שמשמש כרגע למייל, אך יתאים אוטומטית גם לשם משתמש
        private string _loginIdentifier;
        public string LoginIdentifier
        {
            get => _loginIdentifier;
            set { _loginIdentifier = value; OnPropertyChanged(); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        private async void ExecuteLogin(object parameter)
        {
            var passwordBox = parameter as PasswordBox;
            string password = passwordBox?.Password;

            if (string.IsNullOrWhiteSpace(LoginIdentifier) || string.IsNullOrWhiteSpace(password))
            {
                ErrorMessage = "נא להזין פרטי התחברות וסיסמה.";
                return;
            }

            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // הכנה לעתיד: אם תרצה לשנות לשם משתמש, פשוט שנה את "Email" ל-"Username" כאן
                    string columnToSearch = "Email";

                    string query = $"SELECT User_Id, FName, LName, Email, Role, RegistrationDate FROM Users WHERE {columnToSearch} = @Identifier AND Password = @Password";

                    using (var command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Identifier", LoginIdentifier);
                        command.Parameters.AddWithValue("@Password", password);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // קריאה ל-Model ליצירת אובייקט המשתמש כפי שביקשת
                                User loggedInUser = User.CreateFromDatabase(
                                    reader.GetInt32(0), reader.GetString(1),
                                    reader.GetString(2), reader.GetString(3),
                                    reader.GetString(4), reader.GetString(5)
                                );

                                MessageBox.Show($"התחברת בהצלחה! שלום {loggedInUser.FName}", "התחברות");
                                ErrorMessage = string.Empty;
                            }
                            else
                            {
                                ErrorMessage = "פרטי ההתחברות שגויים.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "שגיאה בחיבור למסד הנתונים.";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}