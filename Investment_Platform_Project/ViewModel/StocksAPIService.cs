using System.Threading.Tasks;
using YahooFinanceApi;

namespace Investment_Platform_Project
{
    public class StocksAPIService
    {
        public async Task<double> GetStockPriceAsync(string symbol)
        {
            var securities = await Yahoo.Symbols(symbol).Fields(Field.RegularMarketPrice).QueryAsync();
            var stock = securities[symbol];
            return stock[Field.RegularMarketPrice];
        }
    }
}