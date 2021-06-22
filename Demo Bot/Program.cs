using System;
using Trade_Ogre_Lib;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Demo_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var t = PrivateRequests.SubmitSell("BTC-GRLC", 50, 0.00000224f);
            t.Wait();
            var x = t.Result;
        }
    }
}
