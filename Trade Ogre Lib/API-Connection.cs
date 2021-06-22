﻿using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Trade_Ogre_Lib
{
    public static class API_Connection
    {
        public static async Task<T> DoRequest<T>(string path, string method = "GET", Dictionary<string,object> Fields = null, bool ProvideAuth = false)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod(method), "https://tradeogre.com/api/v1" + path))
                {
                    if (Fields != null)
                    {
                        string str_Fields = JsonConvert.SerializeObject(Fields);
                        request.Content = new StringContent(str_Fields);
                    }

                    var response = await httpClient.SendAsync(request);

                    string s = await response.Content.ReadAsStringAsync();

                    var o = JsonConvert.DeserializeObject<T>(s);
                    return o;
                }
            }
        }
    }
}