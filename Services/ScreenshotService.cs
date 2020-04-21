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
using trackpuls.ViewModels;

namespace trackpuls.Services
{
    public class ScreenshotService
    {
        public static async Task<ScreenResp> p_UploadScreenshot(string user_id , byte[] image)
        {

            ScreenResp resp = new ScreenResp();
            try
            {
                Random random = new Random();
                bool isNetwork = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
                if (!isNetwork)
                throw new Exception(" No Internet Connection !!! ");
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://trackpuls.younggeeks.net/");
                    var reqcontent = new MultipartFormDataContent();
                    var imageContent = new ByteArrayContent(image);
                    imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    reqcontent.Add(imageContent, "picture", "Screen_Cap" + random.Next() + ".jpg");
                    reqcontent.Add(new StringContent(user_id) , "user_id");                  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.PostAsync("api/ScreenshotController/upload_img_api" , reqcontent).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        //System.Windows.MessageBox.Show(" Respnse " + result);
                        resp = JsonConvert.DeserializeObject<ScreenResp>(result);
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
                MainViewModel mn = App.Current.Properties["app"] as MainViewModel;
                if (mn != null)
                {
                    mn.showMessage(ex.Message);
                }
                throw ex;
            }
            return resp;
        }

    }
}
