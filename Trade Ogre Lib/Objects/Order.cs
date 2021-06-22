using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade_Ogre_Lib.Objects
{
    public class Order
    {
        public long date;
        public string type, market, uuid;
        public float price, quantity, fulfilled;
    }
}
