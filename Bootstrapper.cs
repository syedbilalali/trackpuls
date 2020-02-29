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
                StreamReader srReader = new StreamReader(new IsolatedStorageFileStream("isotest", FileMode.OpenOrCreate, isolatedStorage));

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
                       // App.Current.Properties["email"] = item.ToString();
                        System.Windows.MessageBox.Show(item);
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

            try
            {

                //First get the 'user-scoped' storage information location reference in the assembly
                IsolatedStorageFile isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
                //create a stream writer object to write content in the location
                StreamWriter srWriter = new StreamWriter(new IsolatedStorageFileStream("isotest", FileMode.Create, isolatedStorage));
                System.Windows.MessageBox.Show(" On Exit " + srWriter.ToString());
                //check the Application property collection contains any values.
                if (App.Current.Properties["email"] != null && App.Current.Properties["password"] != null)
                {
                    //wriet to the isolated storage created in the above code section.
                    System.Windows.MessageBox.Show("Data : " + App.Current.Properties["email"].ToString());
                    //srWriter.WriteLine(App.Current.Properties["email"].ToString() + " : (Stored at : " + System.DateTime.Now.ToLongTimeString() + ")");
                    srWriter.WriteLine(App.Current.Properties["password"].ToString());
                    srWriter.WriteLine(App.Current.Properties["email"].ToString());

                }
                timer.Stop();
                srWriter.Flush();
                srWriter.Close();
            }
            catch (System.Security.SecurityException sx)
            {
                MessageBox.Show(sx.Message);
                throw;
            }
        }
        void timer_Tick(object sender, EventArgs e)
        {

           try {
                
            Bitmap captureBitmap = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Random random = new Random();

            //Creating a Rectangle object which will capture our Current Screen
            System.Drawing.Rectangle captureRectangle = System.Windows.Forms.Screen.AllScreens[0].Bounds;

            //Creating a New Graphics Object
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);

            //Copying Image from The Screen
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                //string Path = System.IO.Path.GetDirectoryName(Assem);

           captureBitmap.Save(Path.GetDirectoryName(Environment.CurrentDirectory) + "Capture_" + random.Next() + ".jpg", ImageFormat.Jpeg);
           
               
          //  captureBitmap.Save(@"D:\Screenshot\Capture_"+random.Next()+".jpg", ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception In Time Ticker Methods.. " + ex.Message);
            }
        }

    }
}
