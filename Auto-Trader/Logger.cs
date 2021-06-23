using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Trader
{
    public static class Logger
    {
        public static void Log(string message)
        {
            File.AppendAllText("./log.txt", message);
        }
    }
}
