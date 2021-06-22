using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade_Ogre_Lib
{
    public static class PrivateRequests
    {
        public static async Task<float> GetBalance(string currencyCode)
        {
            return (await GetAllBalances()).balances[currencyCode];
        }

        public static async Task<Objects.AllBalances> GetAllBalances()
        {
            return await API_Connection.DoRequest<Objects.AllBalances>("/account/balances", "GET", null, true);
        }

        public static async Task<Objects.Order> GetOrder(string orderid)
        {
            return await API_Connection.DoRequest<Objects.Order>("/account/order/"+orderid, "GET", null, true);
        }

        public static async Task<Objects.Order[]> GetAllOrders()
        {
            return await API_Connection.DoRequest<Objects.Order[]>("/account/orders", "POST", null, true);
        }

        public static async Task<Objects.Order[]> GetAllOrders(string market)
        {
            return await API_Connection.DoRequest<Objects.Order[]>("/account/orders", "POST", new Dictionary<string, object>() { { "market", market } }, true);
        }

        public static async Task<Objects.PlacedOrder> SubmitBuy(string market, float quantity, float price)
        {
            return await API_Connection.DoRequest<Objects.PlacedOrder>("/order/buy", "POST", new Dictionary<string, object>() { { "market", market }, { "quantity", quantity.ToString("N2") }, { "price",price.ToString("N8")} }, true);
        }

        public static async Task<Objects.PlacedOrder> SubmitSell(string market, float quantity, float price)
        {
            return await API_Connection.DoRequest<Objects.PlacedOrder>("/order/sell", "POST", new Dictionary<string, object>() { { "market", market }, { "quantity", quantity.ToString("N2") }, { "price", price.ToString("N8") } }, true);
        }

        public static async Task CancelOrder(string orderid)
        {
            await API_Connection.DoRequest<string>("/order/cancel", "POST", new Dictionary<string, object>() { { "uuid", orderid } }, true);
        }
    }
}
