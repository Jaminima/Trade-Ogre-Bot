using Newtonsoft.Json;
using System;
using System.IO;

namespace Trade_Ogre_Lib
{
    public class Config
    {
        #region Fields

        public static Config activeConv = Load();
        public string key, secret;

        #endregion Fields

        #region Methods

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

        #endregion Methods
    }
}