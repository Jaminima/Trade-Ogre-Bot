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
            var t = API_Connection.DoRequest<List<JObject>>("/markets");
            t.Wait();
            var x = t.Result;
        }
    }
}
