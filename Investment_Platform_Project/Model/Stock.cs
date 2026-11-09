using Investment_Platform_Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment_Platform_Project.Model
{
    public class Stock:BaseEntity
    {
        private DataBaseConnector dbHelper = new DataBaseConnector();

        private string symbol;// The stock's ticker symbol, e.g., "AAPL" for Apple Inc.
        private string companyName;// The full name of the company, e.g., "Apple Inc."
        private string sector;// The sector in which the company operates, e.g., "Technology"
        private string currency;// The currency in which the stock is traded, e.g., "USD"
        private int currentPrice;// The current price of the stock in the specified currency.

        public string Symbol { get => symbol; set => symbol = value; }
        public string CompanyName { get => companyName; set => companyName = value; }
        public string Sector { get => sector; set => sector = value; }
        public string Currency { get => currency; set => currency = value; }
        public int CurrentPrice { get => currentPrice; set => currentPrice = value; }

        public async Task UpdateStockPriceAsync(string symbol, double newPrice)
        {
            string query = "UPDATE Stocks SET CurrentPrice = @Price WHERE Symbol = @Symbol";
            var parameters = new Dictionary<string, object>
            {
                { "@Price", newPrice },
                { "@Symbol", symbol }
            };
            await dbHelper.ExecuteNonQueryAsync(query, parameters);
        }
    }
}
