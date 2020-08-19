using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using trackpuls.Core;
using trackpuls.Models;
using System.Net.Http;
using Newtonsoft.Json; 

namespace trackpuls.Services
{
    public class NLoginServices : BaseClient
    {

        public async Task<LoginResp> p_Login(string email , string password )

        {

            LoginResp resp = new LoginResp();
            try
            {

                bool isNetwork = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                if (!isNetwork)
                    throw new Exception(" No Internet Connection !!! ");


                if (Client.BaseAddress == null) {
                    Client.BaseAddress = new Uri("http://trackpuls.younggeeks.net/");
                    Client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                }
                    

                    //Set Content Type If Needed
                    //client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Set API Timeout 
                    

                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    //Setting Parameter
                    var content = new FormUrlEncodedContent(new[]
                    {
                             new KeyValuePair<string, string>("email", email)
                            ,new KeyValuePair<string, string>("password", password)
                    });
                    //HTTP POST
                    response = await Client.PostAsync("api/Login/post_user_login", content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        // System.Windows.MessageBox.Show(result);
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
            catch (Exception ex)
            {
                throw ex;
            }
            return resp;
        }
    }
}
