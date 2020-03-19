using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using trackpuls.Models;
using Newtonsoft.Json;

namespace trackpuls.Services
{
    public class AttendenceService
    {
        public static async Task<AttendenceResp> p_attendence(string user_id , string clockIn , string clockOut , string computer_act , string office_time , string productive)
        {

             AttendenceResp resp = new AttendenceResp();
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
                             new KeyValuePair<string, string>("user_id", user_id),
                             new KeyValuePair<string, string>("clock_in", clockIn),
                             new KeyValuePair<string, string>("clock_out", clockOut),
                             new KeyValuePair<string, string>("computer_act", computer_act),
                             new KeyValuePair<string, string>("office_time", office_time),
                             new KeyValuePair<string, string>("productive", productive),
                    });
                    //HTTP POST
                    response = await client.PostAsync("api/ApiAttendenceController/attendenceapi", content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        System.Windows.MessageBox.Show(result);
                        resp = JsonConvert.DeserializeObject<AttendenceResp>(result);
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
