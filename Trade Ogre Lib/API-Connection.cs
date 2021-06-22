using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Trade_Ogre_Lib
{
    public static class API_Connection
    {
        #region Methods

        public static async Task<T> DoRequest<T>(string path, string method = "GET", Dictionary<string, object> Fields = null, bool ProvideAuth = false)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod(method), "https://tradeogre.com/api/v1" + path))
                {
                    if (Fields != null)
                    {
                        string str_Fields = JsonConvert.SerializeObject(Fields);

                        List<KeyValuePair<string?, string?>> iFields = new List<KeyValuePair<string?, string?>>();
                        Fields.ToList().ForEach(x => iFields.Add(new KeyValuePair<string?, string?>(x.Key, x.Value.ToString())));

                        request.Content = new FormUrlEncodedContent(iFields);
                    }

                    if (ProvideAuth)
                    {
                        var base64authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Config.activeConv.key}:{Config.activeConv.secret}"));
                        request.Headers.TryAddWithoutValidation("Authorization", $"Basic {base64authorization}");
                    }

                    var response = await httpClient.SendAsync(request);

                    string s = await response.Content.ReadAsStringAsync();

                    JObject j = JObject.Parse(s);
                    if (j.ContainsKey("success") && j["success"].ToString() == "False")
                    {
                        throw new Exception(j["error"].ToString());
                    }

                    var o = JsonConvert.DeserializeObject<T>(s);
                    return o;
                }
            }
        }

        #endregion Methods
    }
}