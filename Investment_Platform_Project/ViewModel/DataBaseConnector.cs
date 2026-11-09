using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

namespace Investment_Platform_Project.ViewModel
{
    public class DataBaseConnector
    {
        private const string connectionString = @"Data Source=C:\Users\Kobi Shemer\Documents\Investment Project DB\Investment Project DataBase Updated.db;"; 
        
        public async Task ExecuteNonQueryAsync(string query, Dictionary<string, object> parameters = null)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqliteCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                            command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}