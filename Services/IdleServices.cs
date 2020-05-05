using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace trackpuls.Services
{
    class IdleServices
    {  /// <summary>
       /// This POST method validate the Login Details to Client Crednetials 
       /// </summary>
       /// <param name="user_id">Set the User_ID of the User </param>
       /// <param name="start_ideal">Set the Start Idle Time</param>
       /// <param name="ideal_time">Set Idle Time from the Setting API Response</param>
       /// <param name="status">Set the Status of Idle Time 1 </param>
       /// <returns> Response of the API Response </returns>
        public static async Task<trackpuls.Models.IdealResp> p_IdleIn(string user_id, string start_ideal, string ideal_time , string status)
        {
            trackpuls.Models.IdealResp resp = new trackpuls.Models.IdealResp();
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
                            ,new KeyValuePair<string, string>("start_idle", start_ideal)
                            ,new KeyValuePair<string, string>("idle_time", ideal_time)
                            ,new KeyValuePair<string, string>("status", status)
                    });
                    //HTTP POST
                    response = await client.PostAsync("api/ApiIdealController/ideal_api", content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        // System.Windows.MessageBox.Show(result);
                        resp = JsonConvert.DeserializeObject<trackpuls.Models.IdealResp>(result);
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
        /// <summary>
        /// This POST method validate the Login Details to Client Crednetials 
        /// </summary>
        /// <param name="user_id">Set the User_ID of the User </param>
        /// <param name="end_Idle">Set end Idle time of the Idle Duration</param>
        /// <param name="status">Set the Status of Idle for 1</param>
        /// <param name="Id">Id of the Last Idle In Reponse ID </param>
        /// <returns></returns>
        public static async Task<trackpuls.Models.IdealResp> p_IdleOut(string user_id, string end_Idle , string status , string Id)
        {
            trackpuls.Models.IdealResp resp = new trackpuls.Models.IdealResp();
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
                            ,new KeyValuePair<string, string>("end_ideal", end_Idle)
                            ,new KeyValuePair<string, string>("status", status)
                            ,new KeyValuePair<string, string>("id", Id)
                    });
                    //HTTP POST
                    response = await client.PostAsync("api/ApiIdealController/ideal_api", content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        // System.Windows.MessageBox.Show(result);
                        resp = JsonConvert.DeserializeObject<trackpuls.Models.IdealResp>(result);
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
