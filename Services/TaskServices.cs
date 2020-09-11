using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using trackpuls.Models;
using trackpuls.Core;

namespace trackpuls.Services
{
    class TaskServices
    {
        public static async Task<TaskResp> p_TaskList(string userid)
        {
            TaskResp resp = new TaskResp();
            try
            {

                bool isNetwork = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                if (!isNetwork)
                    throw new Exception(" No Internet Connection !!! ");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://trackpuls.younggeeks.net/");

                    //Set API Timeout 
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    //Setting Parameter
                    var content = new FormUrlEncodedContent(new[]
                    {
                         new KeyValuePair<string, string>("user_id", userid)
                    });
                    //HTTP POST
                    response = await client.PostAsync("api/ApiTaskController/task_details", content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;

                      //  System.Windows.MessageBox.Show(result);
                        resp = JsonConvert.DeserializeObject<TaskResp>(result);
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
        public static async Task<TaskResp> p_TaskList(string userid, string project_id)
        {
            TaskResp resp = new TaskResp();
            try
            {

                bool isNetwork = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                if (!isNetwork)
                    throw new Exception(" No Internet Connection !!! ");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://trackpuls.younggeeks.net/");

                    //Set API Timeout 
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    //Setting Parameter
                    var content = new FormUrlEncodedContent(new[]
                    {
                         new KeyValuePair<string, string>("user_id", userid),
                         new KeyValuePair<string, string>("project_id", project_id)
                    });
                    //HTTP POST
                    response = await client.PostAsync("api/ApiTaskController/task_details", content).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;

                        //  System.Windows.MessageBox.Show(result);
                        resp = JsonConvert.DeserializeObject<TaskResp>(result);
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
