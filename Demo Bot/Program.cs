using System;
using Trade_Ogre_Lib;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace Demo_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            App();

            while (true) { Thread.Sleep(10000); }
        }

        static async void App()
        {
            var q = await PublicRequests.GetTicker("BTC-GRLC");
            var t = await PrivateRequests.SubmitSell("BTC-GRLC", 25, 0.00000324f);
        }
    }
}
