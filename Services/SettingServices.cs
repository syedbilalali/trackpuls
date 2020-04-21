using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using trackpuls.Models;

namespace trackpuls.Services
{
    public class SettingServices
    {
        public static async Task<SettingResp> p_Setting(string userid)
        {

            SettingResp resp = new SettingResp();

            try
            {

                bool isNetwork = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                if (!isNetwork)
                    throw new Exception(" No Internet Connection !!! ");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://trackpuls.younggeeks.net/");

                  //  Set API Timeout
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    //Initialization.
                   HttpResponseMessage response = new HttpResponseMessage();

                    //Setting Parameter
                    var content = new FormUrlEncodedContent(new[]
                    {
                         new KeyValuePair<string, string>("user_id", userid)
                    });
                    //HTTP POST
                    response = await client.PostAsync("api/ApiSettingController/apisettings", content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                      //  Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<SettingResp>(result);
                        //Releasing.
                       response.Dispose();
                    }
                    else
                    {
                        //Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        resp.status = "false";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resp;
        }
    }
}
