using System;
using Trade_Ogre_Lib;
using System.Threading;

namespace Auto_Trader
{
    class Program
    {
        private static async void App()
        {
            Trader trader = new HighLowTrader(_buysellMultiplyer: 0.5f);

            while (true)
            {
                trader.CheckState();
                Thread.Sleep(60000);
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            App();

            while (true) { Thread.Sleep(10000); }
        }
    }
}
