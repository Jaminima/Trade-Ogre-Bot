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

        private Tuple<float,float> CalculateWeightedAvg(Dictionary<float,float> values)
        {
            float Average = 0, Count = 0;
            values.ToList().ForEach(x => { Average += x.Key * x.Value; Count += x.Value; });
            Average /= Count;
            return new Tuple<float, float>(Average, Count);
        }

        public override async void CheckState()
        {
            OrderBook book = await PublicRequests.GetOrderBook("BTC-" + currencyCode);

            Tuple<float, float> buyAvg = CalculateWeightedAvg(book.buy),
                sellAvg = CalculateWeightedAvg(book.sell);

            Console.WriteLine($"Current Price            {(await GetTicker()).price:N8} BTC");

            Console.WriteLine($"Average Buy Order Price  {buyAvg.Item1:N8} BTC, Buying  {buyAvg.Item2} {currencyCode}");

            Console.WriteLine($"Average Sell Order Price {sellAvg.Item1:N8} BTC, Selling {sellAvg.Item2} {currencyCode}");
        }
    }
}
