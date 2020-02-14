using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using System.IO;
using System.IO.IsolatedStorage;
using trackpuls.ViewModels;

namespace trackpuls
{
    class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper() {
            Initialize();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            System.Windows.MessageBox.Show(" On Start ");
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
                        App.Current.Properties["email"] = item.ToString();
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
                if (App.Current.Properties["email"] != null)
                {
                    //wriet to the isolated storage created in the above code section.
                    System.Windows.MessageBox.Show("Data : " + App.Current.Properties["email"].ToString());
                    //srWriter.WriteLine(App.Current.Properties["email"].ToString() + " : (Stored at : " + System.DateTime.Now.ToLongTimeString() + ")");
                    srWriter.WriteLine(App.Current.Properties["email"].ToString());

                }
               

                srWriter.Flush();
                srWriter.Close();
            }
            catch (System.Security.SecurityException sx)
            {
                MessageBox.Show(sx.Message);
                throw;
            }
        }

    }
}
