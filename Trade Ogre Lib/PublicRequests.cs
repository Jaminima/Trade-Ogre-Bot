using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trade_Ogre_Lib
{
    public static class PublicRequests
    {
        #region Methods

        public static async Task<List<Objects.History>> GetHistory(string marketCode)
        {
            return await API_Connection.DoRequest<List<Objects.History>>("/history/" + marketCode);
        }

        public static async Task<Objects.OrderBook> GetOrderBook(string marketCode)
        {
            return await API_Connection.DoRequest<Objects.OrderBook>("/orders/" + marketCode);
        }

        public static async Task<Objects.Ticker> GetTicker(string marketCode)
        {
            return await API_Connection.DoRequest<Objects.Ticker>("/ticker/" + marketCode);
        }

        #endregion Methods
    }
}