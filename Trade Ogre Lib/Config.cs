using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Trade_Ogre_Lib
{
    public class Config
    {
        public string key, secret;

        public static Config activeConv = Load();

        public static Config Load()
        {
            if (File.Exists("./config.json"))
            {
                string r = File.ReadAllText("./config.json");
                return JsonConvert.DeserializeObject<Config>(r);
            }
            else
            {
                File.WriteAllText("./config.json", JsonConvert.SerializeObject(new Config()));
                throw new Exception("Please fill the config.json file with your auth details");
            }
        }
    }
}
