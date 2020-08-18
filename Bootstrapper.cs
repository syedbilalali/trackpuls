using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using System.IO;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using trackpuls.ViewModels;
using System.Drawing.Imaging;
using System.Drawing;
using trackpuls.Services;
using trackpuls.Models;
using Newtonsoft.Json;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;
using System.Diagnostics;
using trackpuls.Helper;
/**
 * --------trackpuls VER(1.0.0)-----------
 * Developed By : Syed Bilal Ali 
 * Project Manager : Raju Kumar
 * ---------------------------------------
 */

namespace trackpuls
{
    class Bootstrapper : BootstrapperBase
    {
        private DispatcherTimer timer;
        private DispatcherTimer app_web_timr;
        private bool isOn = false;
        private int default_time = 3600;
        private List<ProcessInfo> proList;
        public Bootstrapper() {

            Initialize();
            timer = new DispatcherTimer(DispatcherPriority.Background);
            app_web_timr = new DispatcherTimer(DispatcherPriority.Background);
            proList = new List<ProcessInfo>();


        }
      
        protected async override void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                StreamReader srReader = new StreamReader(new IsolatedStorageFileStream("isotest.txt", FileMode.OpenOrCreate, isolatedStorage));
                SettingResp settingResp = await SettingServices.p_Setting("15");
                if (srReader == null)
                {
                    System.Windows.MessageBox.Show("No Data stored!");
                }
                else
                {
                    while (!srReader.EndOfStream)
                    {   
                        string item = srReader.ReadLine();
                        UserData data = JsonConvert.DeserializeObject<UserData>(item);
                        App.Current.Properties["userdata"] = data;
                        if (App.Current.Properties["userdata"] != null) {
                            if (data.email != null && data.password != null) {
                               // System.Windows.MessageBox.Show(" Exist Email : " + data.email + " Exist Password : " + data.password + " Timer RUn : " + isOn);
                                isOn = true;
                            }
                        }
                    }
                }
                srReader.Close();
                if (settingResp.status == "true")
                {
                    Settings set = settingResp.data[0];
                    if (set != null) {
                        string freq = set.screenshot_frequency;
                        if (freq != null) {
                            //  System.Windows.MessageBox.Show(" Freq : " + freq);
                            switch (freq) {
                                case "No Screenshots":
                                    isOn = false;
                                    break;
                                case "1 per hour":
                                    isOn = true;
                                    default_time = 3600;
                                    break;
                                case "3 per hour":
                                    isOn = true;
                                    default_time = 1200;
                                    break;
                                case "6 per hour":
                                    isOn = true;
                                    default_time = 600;
                                    break;
                                case "12 per hour":
                                    isOn = true;
                                    default_time = 300;
                                    break;
                                case "24 per hour":
                                    isOn = true;
                                    default_time = 150;
                                    break;
                                case "30 per hour":
                                    isOn = true;
                                    default_time = 120;
                                    break;
                            }
                        }

                    }
                }
                else{

                    System.Windows.MessageBox.Show(" Not Load Data ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            DisplayRootViewFor<MainViewModel>();
            if (isOn) {

                timer.Interval = TimeSpan.FromSeconds(default_time);
                timer.Tick += timer_Tick;
                timer.Start();

                app_web_timr.Interval = TimeSpan.FromSeconds(60);
                app_web_timr.Tick += timer_Tick1;
                app_web_timr.Start();

            }
        }
        protected override void OnExit(object sender, EventArgs e)
        {
            string userdata;
            try
            {
                IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                StreamWriter srWriter = new StreamWriter(new IsolatedStorageFileStream("isotest.txt", FileMode.Create, isolatedStorage));  
                if (App.Current.Properties["userdata"] != null) {
                        UserData data = (UserData)App.Current.Properties["userdata"];
                        userdata = JsonConvert.SerializeObject(data);
                        srWriter.WriteLine(userdata);
                }
                srWriter.Flush();
                srWriter.Close();
                timer.Stop();
                app_web_timr.Stop();
            }
            catch (System.Security.SecurityException sx)
            {
                MessageBox.Show(sx.Message);
                throw;
            }
        }
        async void timer_Tick1(object sender, EventArgs e) {
            try
            {
                Process[] process = Process.GetProcesses();
                foreach (Process P in process)
                {
                    if (P.MainWindowTitle.Length > 1)
                    {   
                        if (!proList.Contains(new ProcessInfo() { ID = P.Id, Name = P.ProcessName }))
                        {
                            proList.Add(new ProcessInfo()
                            {

                                ID = P.Id,
                                Name = P.ProcessName,
                                Title = P.MainWindowTitle,
                                StartTime = P.StartTime.ToString()
                            });
                            if (App.Current.Properties["userdata"] != null)
                            {

                                UserData userdata = (UserData)App.Current.Properties["userdata"];
                                string name = P.ProcessName;
                                string title = P.MainWindowTitle;
                                Response resp = await AppWebServices.p_addAppWeb(userdata.id, name, title, P.StartTime.ToShortTimeString());
                                if (resp.status == "true")
                                {
                                    ///  MessageBox.Show(" Msg : " + resp.message);
                                }
                            }
                        }


                    }
                }
            }
            catch (Exception ex) {

                MainViewModel mn = App.Current.Properties["app"] as MainViewModel;
                if (mn != null)
                {
                    mn.showMessage(ex.Message);
                }

            }

        }
        async void timer_Tick(object sender, EventArgs e)
        {

           try {

            Bitmap captureBitmap = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Random random = new Random();

            System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.AllScreens[0].Bounds;
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);

            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
            string applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "shots"));
                if (!dir.Exists) { dir.Create(); GrantAccess( dir.FullName); }

                    string imagePath = System.IO.Path.Combine(dir.FullName, "Capture_" + random.Next() + ".jpg");
                    captureBitmap.Save(imagePath, ImageFormat.Jpeg);
                    FileInfo fileInfo = new FileInfo(imagePath);
                    byte[] data = new byte[fileInfo.Length];
                    using (FileStream fs = fileInfo.OpenRead()) {
                            fs.Read(data, 0, data.Length);
                    }
                    if (App.Current.Properties["userdata"] != null) {
                        
                        UserData userdata = (UserData)App.Current.Properties["userdata"];
                        ScreenResp resp = await ScreenshotService.p_UploadScreenshot(userdata.id, data);
                        if (resp.status == "true")
                        {
                         // System.Windows.MessageBox.Show("Screenshot Uploaded " + resp.message +" User ID " + userdata.id);
                        }
                        else { 
                         //  System.Windows.MessageBox.Show("Screenshot not Uploaded " + resp.message + " User ID " + userdata.id);
                        }
                    }
                    captureBitmap.Dispose();
                    captureGraphics.Dispose();
                    fileInfo.Delete();
            }
            catch (Exception ex)
            {
                MainViewModel mn = App.Current.Properties["app"] as MainViewModel;
                if (mn != null) {
                    mn.showMessage(ex.Message);
                }
               
            }
        }
        private static void GrantAccess(string file)
        {
            bool exists = System.IO.Directory.Exists(file);
            if (!exists)
            {
                DirectoryInfo di = System.IO.Directory.CreateDirectory(file);
                System.Windows.MessageBox.Show("The Folder is created Sucessfully");
            }
            else
            {
                System.Windows.MessageBox.Show("The Folder already exists");
            }
            DirectoryInfo dInfo = new DirectoryInfo(file);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);

        }
    }
}
