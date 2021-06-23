using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trade_Ogre_Lib;
using Trade_Ogre_Lib.Objects;

namespace Auto_Trader
{
    public abstract class Trader
    {
        protected string currencyCode;

        protected Ticker ticker;
        private DateTime tickerAt;

        public Trader(string currencyCode = "GRLC")
        {
            this.currencyCode = currencyCode;
        }

        public async Task<Ticker> GetTicker()
        {
            if (ticker == null || DateTime.Now > tickerAt.AddMinutes(1))
            {
                ticker = await PublicRequests.GetTicker("BTC-" + currencyCode);
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
            return (t.price - t.low) / (t.high - t.low);
        }

        public virtual async void CheckState()
        {
            float gapSize = await getHighLowGap();
        }
    }
}
