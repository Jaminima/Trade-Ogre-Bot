using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade_Ogre_Lib
{
    public static class PrivateRequests
    {
        public static async Task<float> GetBalance(string currencyCode)
        {
            return (await GetAllBalances()).balances[currencyCode];
        }


        public static async Task<Objects.AllBalances> GetAllBalances()
        {
            return await API_Connection.DoRequest<Objects.AllBalances>("/account/balances", "GET", null, true);
        }
    }
}
