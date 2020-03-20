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

namespace trackpuls
{
    class Bootstrapper : BootstrapperBase
    {
        private DispatcherTimer timer;
        public Bootstrapper() {

            Initialize();
            timer = new DispatcherTimer();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //System.Windows.MessageBox.Show(" On Start ");
            try
            {
                //First get the 'user-scoped' storage information location reference in the assembly
                IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                //create a stream reader object to read content from the created isolated location
                StreamReader srReader = new StreamReader(new IsolatedStorageFileStream("isotest.txt", FileMode.OpenOrCreate, isolatedStorage));

                //Open the isolated storage
                if (srReader == null)
                {
                    System.Windows.MessageBox.Show("No Data stored!");
                }
                else
                {
                    //MessageBox.Show(stateReader.ReadLine());
                    while (!srReader.EndOfStream)
                    {   
                    
                        string item = srReader.ReadLine();
                     //   System.Windows.MessageBox.Show(item);
                        UserData data = JsonConvert.DeserializeObject<UserData>(item);
                        App.Current.Properties["userdata"] = data;
                       // System.Windows.MessageBox.Show(data.email);
                    }
                }
                //close reader
                srReader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }

            DisplayRootViewFor<MainViewModel>();
            timer.Interval = TimeSpan.FromSeconds(60);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        protected override void OnExit(object sender, EventArgs e)
        {
            string userdata;
            try
            {

                //First get the 'user-scoped' storage information location reference in the assembly
                IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                //create a stream writer object to write content in the location
                StreamWriter srWriter = new StreamWriter(new IsolatedStorageFileStream("isotest.txt", FileMode.Create, isolatedStorage));
                //  System.Windows.MessageBox.Show(" On Exit " + srWriter.ToString());
                //check the Application property collection contains any values.
             //   System.Windows.MessageBox.Show(App.Current.Properties["email"].ToString() + " : " + App.Current.Properties["password"].ToString());
                if (App.Current.Properties["email"] != null && App.Current.Properties["password"] != null)
                {
                    if (App.Current.Properties["userdata"] != null) {
                        UserData data = (UserData)App.Current.Properties["userdata"];
                        userdata = JsonConvert.SerializeObject(data);
                        srWriter.WriteLine(userdata);
                    }
                    //wriet to the isolated storage created in the above code section.
                    //srWriter.WriteLine(App.Current.Properties["email"].ToString());
                }
                srWriter.Flush();
                srWriter.Close();
                timer.Stop();
            }
            catch (System.Security.SecurityException sx)
            {
                MessageBox.Show(sx.Message);
                throw;
            }
        }
       async void timer_Tick(object sender, EventArgs e)
        {

           try {

          //  System.Windows.MessageBox.Show(" Taking Picture");
            Bitmap captureBitmap = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Random random = new Random();

            //Creating a Rectangle object which will capture our Current Screen
            System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.AllScreens[0].Bounds;

            //Creating a New Graphics Object
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);

            //Copying Image from The Screen
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                //string Path = System.IO.Path.GetDirectoryName(Assem);
           string applicationPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
           var dir = new System.IO.DirectoryInfo(System.IO.Path.Combine(applicationPath, "shots"));
           if (!dir.Exists)
            dir.Create();
             //   System.Windows.MessageBox.Show(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
                //captureBitmap.Save(Path.GetDirectoryName(Environment.CurrentDirectory + "/shots/") + "Capture_" + random.Next() + ".jpg", ImageFormat.Jpeg);
                string imagePath = System.IO.Path.Combine(dir.FullName, "Capture_" + random.Next() + ".jpg");
                captureBitmap.Save(imagePath, ImageFormat.Bmp);
                //captureBitmap.Save(@"D:\Screenshot\Capture_"+random.Next()+".jpg", ImageFormat.Jpeg);
                FileInfo fileInfo = new FileInfo(imagePath);
                // The byte[] to save the data in
                byte[] data = new byte[fileInfo.Length];
                // Load a filestream and put its content into the byte[]
                using (FileStream fs = fileInfo.OpenRead())
                {
                    fs.Read(data, 0, data.Length);
                }
                if (App.Current.Properties["userdata"] != null) {
                    UserData userdata = (UserData)App.Current.Properties["userdata"];
                  //  System.Windows.MessageBox.Show("userid " + userdata.id);
                    ScreenResp resp = await ScreenshotService.p_UploadScreenshot(userdata.id, data);
                }
                // Delete the temporary file
                fileInfo.Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception In Time Ticker Methods.. " + ex.Message);
            }
        }

    }
}
