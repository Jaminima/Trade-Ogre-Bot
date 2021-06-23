using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade_Ogre_Lib;
using Trade_Ogre_Lib.Objects;

namespace Auto_Trader
{
    public class SmartTrader : Trader
    {

        public SmartTrader(string currencyCode = "GRLC") : base(currencyCode)
        {

        }

        public override async void CheckState()
        {
            OrderBook book = await PublicRequests.GetOrderBook("BTC-" + currencyCode);

            float buyAverage = 0, buyCount=0;
            book.buy.ToList().ForEach(x=> { buyAverage += x.Key * x.Value; buyCount += x.Value;  });
            buyAverage /= buyCount;

            Console.WriteLine($"Current Price            {(await GetTicker()).price:N8} BTC");

            Console.WriteLine($"Average Buy Order Price  {buyAverage:N8} BTC, Buying  {buyCount} {currencyCode}");

            float sellAverage = 0, sellCount = 0;
            book.sell.ToList().ForEach(x => { sellAverage += x.Key * x.Value; sellCount += x.Value; });
            sellAverage /= sellCount;

            Console.WriteLine($"Average Sell Order Price {sellAverage:N8} BTC, Selling {sellCount} {currencyCode}");
        }
    }
}
