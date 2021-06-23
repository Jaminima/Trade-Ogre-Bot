using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade_Ogre_Lib;
using Trade_Ogre_Lib.Objects;

namespace Auto_Trader
{
    public class HighLowTrader
    {
        private string currencyCode;
        private float lowBuyTrigger, highSellTrigger;
        private float buysellMultiplyer;

        private Ticker ticker;
        private DateTime tickerAt;

        public async Task<Ticker> GetTicker()
        {
            if (ticker == null || DateTime.Now > tickerAt.AddMinutes(1))
            {
                ticker = await PublicRequests.GetTicker("BTC-"+currencyCode);
                tickerAt = DateTime.Now;
            }
            return ticker;
        }

        public async Task<float> getHighLowGap()
        {
            Ticker t = await GetTicker();
            return t.high - t.low;
        }

        public async Task<float> getHLGapPosition()
        {
            Ticker t = await GetTicker();
            return (t.price-t.low)/(t.high - t.low);
        }

        public HighLowTrader(string currencyCode="GRLC", float _lowBuyTrigger = 0.1f, float _highSellTrigger = 0.9f, float _buysellMultiplyer = 0.2f)
        {
            this.currencyCode = currencyCode;
            this.lowBuyTrigger = _lowBuyTrigger;
            this.highSellTrigger = _highSellTrigger;
            this.buysellMultiplyer = _buysellMultiplyer;
        }

        public async void CheckState()
        {
            float gapPos = await getHLGapPosition();
            float btcCurBal = await PrivateRequests.GetBalance("BTC");
            float currencyCurBal = await PrivateRequests.GetBalance(currencyCode);

            if (gapPos <= lowBuyTrigger)
            {
                float buySize = btcCurBal * buysellMultiplyer / ticker.price;
                PlacedOrder order = await PrivateRequests.SubmitBuy("BTC-" + currencyCode, buySize, ticker.price);
                Console.WriteLine($"Placed Buy Order for {buySize} {currencyCode}");
            }

            else if (gapPos >= highSellTrigger)
            {
                float sellSize = currencyCurBal * buysellMultiplyer;
                PlacedOrder order = await PrivateRequests.SubmitSell("BTC-" + currencyCode, sellSize, ticker.price);
                Console.WriteLine($"Placed Sell Order for {sellSize} {currencyCode}");
            }

            else
            {
                Console.WriteLine($"No order placed, gap position is {gapPos:N4}");
            }
        }
    }
}
