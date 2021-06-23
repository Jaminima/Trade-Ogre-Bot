using System;
using Trade_Ogre_Lib;
using Trade_Ogre_Lib.Objects;

namespace Auto_Trader
{
    public class HighLowTrader : Trader
    {
        #region Fields

        private float buysellMultiplyer;

        private float lowBuyTrigger, highSellTrigger;

        private State state = State.Neutral;

        #endregion Fields

        #region Enums

        private enum State
        {
            IsLow, IsHigh, Neutral
        }

        #endregion Enums

        #region Constructors

        public HighLowTrader(string currencyCode = "GRLC", float _lowBuyTrigger = 0.1f, float _highSellTrigger = 0.9f, float _buysellMultiplyer = 0.2f) : base(currencyCode)
        {
            this.lowBuyTrigger = _lowBuyTrigger;
            this.highSellTrigger = _highSellTrigger;
            this.buysellMultiplyer = _buysellMultiplyer;
        }

        #endregion Constructors

        #region Methods

        public override async void CheckState()
        {
            float gapPos = await getHLGapPosition();
            float btcCurBal = await PrivateRequests.GetBalance("BTC");
            float currencyCurBal = await PrivateRequests.GetBalance(currencyCode);

            Console.WriteLine($"Current price {ticker.price:N8} BTC");

            if (gapPos <= lowBuyTrigger)
            {
                if (state != State.IsLow)
                {
                    state = State.IsLow;

                    float buySize = btcCurBal * buysellMultiplyer / ticker.price;

                    if (buySize > 0.00005f)
                    {   
                        PlacedOrder order = await PrivateRequests.SubmitBuy("BTC-" + currencyCode, buySize, ticker.price);
                        Console.WriteLine($"Placed Buy Order for {buySize} {currencyCode}");
                    }
                }
            }
            else if (gapPos >= highSellTrigger)
            {
                if (state != State.IsHigh)
                {
                    state = State.IsHigh;

                    float sellSize = currencyCurBal * buysellMultiplyer;

                    if (sellSize * ticker.price > 0.00005f)
                    {
                        PlacedOrder order = await PrivateRequests.SubmitSell("BTC-" + currencyCode, sellSize, ticker.price);
                        Console.WriteLine($"Placed Sell Order for {sellSize} {currencyCode}");
                    }
                }
            }
            else
            {
                state = State.Neutral;
                Console.WriteLine($"No order placed, gap position is {gapPos:N8}");
            }
        }

        #endregion Methods
    }
}