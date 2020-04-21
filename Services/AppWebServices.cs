using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trackpuls.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace trackpuls.Services
{
    class AppWebServices
    {
        /// <summary>
        /// This POST method validate the Login Details to Client Crednetials 
        /// </summary>
        /// <param name="user_id">Set the User_ID of the User </param>
        /// <param name="name">Set the Name Of the Application</param>
        /// <param name="time">Set Start Time of the Apps</param>
        /// <returns></returns>
        public static async Task<trackpuls.Models.Response>  p_addAppWeb(string user_id, string name , string title , string time)
        {
            trackpuls.Models.Response resp = new trackpuls.Models.Response();
            try
            {
                bool isNetwork = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                if (!isNetwork)
                    throw new Exception(" No Internet Connection !!! ");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://trackpuls.younggeeks.net/");

                    //Set Content Type If Needed
                    //client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Set API Timeout 
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    //Setting Parameter
                    var content = new FormUrlEncodedContent(new[]
                    {
                             new KeyValuePair<string, string>("user_id", user_id)
                            ,new KeyValuePair<string, string>("name", name)
                            ,new KeyValuePair<string, string>("time", time)
                            ,new KeyValuePair<string, string>("title", title)
                    });
                    //HTTP POST
                    response = await client.PostAsync("api/AppWebsiteController/app_web", content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        // System.Windows.MessageBox.Show(result);
                        resp = JsonConvert.DeserializeObject<trackpuls.Models.Response>(result);
                        // Releasing.
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.
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
