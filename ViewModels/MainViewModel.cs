using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using trackpuls.ViewModels;


namespace trackpuls.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        public SnackbarMessageQueue BoundMessageQueue { get; } = new SnackbarMessageQueue();
        private bool _btnProfilePic = false;
        public MainViewModel() {

            ActivateItem( new LoginViewModel(this));
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

          IsProfilePic = false;
          //  LeftDrawerHost.IsLeftDrawerOpen = false;
          ActivateItem(new LoginViewModel(this));
        }
    }
}
