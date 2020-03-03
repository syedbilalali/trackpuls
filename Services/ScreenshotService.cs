using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using trackpuls.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace trackpuls.Services
{
    public class ScreenshotService
    {
        public static async Task<LoginResp> p_UploadScreenshot(string user_id , byte[] image)
        {

            LoginResp resp = new LoginResp();
            try
            {

                bool isNetwork = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                if (!isNetwork)
                    throw new Exception(" No Internet Connection !!! ");

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://trackpuls.younggeeks.net/");
                    var reqcontent = new MultipartFormDataContent();
                    var imageContent = new ByteArrayContent(image);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    var content = new FormUrlEncodedContent(new[]
                    {
                             new KeyValuePair<string, string>("user_id","1")
                    });
                    reqcontent.Add(content, "user_id");
                    reqcontent.Add(imageContent, "picture", "image.jpg");

                    //Set API Timeout 
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    //Setting Parameter
                    //HTTP POST
                    response = await client.PostAsync("api/ScreenshotController/upload_img_api" , reqcontent).ConfigureAwait(false);
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
            catch (Exception ex)
            {
                throw ex;
            }
            return resp;
        }

    }
}
