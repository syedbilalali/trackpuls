using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Configuration.Assemblies;
using trackpuls.Models;

namespace trackpuls.Services
{
    public class LoginService
    {   
        /// <summary>
        /// This POST method validate the Login Details to Client Crednetials 
        /// </summary>
        /// <param name="email">Put Email of the Employee </param>
        /// <param name="Password">Put Password of the Employee</param>
        /// <returns></returns>
        public static async Task<LoginResp> p_Login(string email , string password) {

            LoginResp resp = new LoginResp();
            try {

                bool isNetwork = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                if (!isNetwork) 
                   throw new Exception(" No Internet Connection !!! ");
    
                using (var client = new HttpClient()) {

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
                             new KeyValuePair<string, string>("email", email)
                            ,new KeyValuePair<string, string>("password", password)
                    });

                    //HTTP POST
                    response = await client.PostAsync("api/Login/post_user_login", content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        System.Windows.MessageBox.Show(result);
                        resp = JsonConvert.DeserializeObject<LoginResp>(result);
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
            catch (Exception ex) {
               throw ex;
            }
            return resp;
        }
    }
}
