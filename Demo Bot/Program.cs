using System;
using System.Threading;
using Trade_Ogre_Lib;

namespace Demo_Bot
{
    internal class Program
    {
        #region Methods

        private static async void App()
        {
            var q = await PublicRequests.GetTicker("BTC-GRLC");

            Console.WriteLine($"Current Low High Gap Position: {(q.price - q.low) / (q.high - q.low)}");

            //var t = await PrivateRequests.SubmitSell("BTC-GRLC", 25, 0.00000324f);
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            App();

            while (true) { Thread.Sleep(10000); }
        }

        #endregion Methods
    }
}