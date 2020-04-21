using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using trackpuls.ViewModels;
using trackpuls.Models;
using System.Windows.Threading;
using System.Windows.Input;

namespace trackpuls.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue();
        private bool _btnProfilePic = false;
        public MainViewModel() {
            UserData user = (UserData)App.Current.Properties["userdata"];
            if (user != null)
            {
              //qd  System.Windows.MessageBox.Show(" Exist Email : " + user.email + " User Password : " + user.password + " User Logout : " + user.logout);
                if (user.email != null && user.password != null && user.logout != true)
                {   

                    ActivateItem(new TrackMainViewModel(this));
                }
                else
                {
                    ActivateItem(new LoginViewModel(this));
                }
            }
            else {
                ActivateItem(new LoginViewModel(this));
            }
            if (this != null) {
                App.Current.Properties["app"] = this;
            }
            
        }
        public void ProfBtn() {
           
        }
        public void showMessage(string message ) {

            BoundMessageQueue.Enqueue(message);
        }
        public bool IsProfilePic{

            get { return _btnProfilePic;  }
            set {
                _btnProfilePic = value;
                NotifyOfPropertyChange(() => IsProfilePic);
            }
        }
        public void btnSignOut() {

            UserData user = (UserData)App.Current.Properties["userdata"];
            user.logout = true;
            App.Current.Properties["userdata"] = user;
            IsProfilePic = false;
            ActivateItem(new LoginViewModel(this));
        }

       
    }
}
