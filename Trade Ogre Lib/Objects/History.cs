using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Trade_Ogre_Lib.Objects
{
    public class History
    {
        public long date;
        public string type;
        public float price, quantity;
    }
}
