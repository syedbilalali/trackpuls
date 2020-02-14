using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;

namespace trackpuls.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _email;
        private string _password;
        public LoginViewModel() {
            System.Windows.MessageBox.Show(" On Login Load.. " );
            if (App.Current.Properties["email"]!= null) {
                System.Windows.MessageBox.Show("Properties : " + App.Current.Properties["email"].ToString() );
                Email = App.Current.Properties["email"].ToString();
            }
        }
        public string Email {
            get {
                return _email;
            }
            set {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }
        public string Password {
            get { return _password; }
            set { 
                _password = value;
                NotifyOfPropertyChange(() => Password);
            }

        }
        public void btnsignIn() {

            App.Current.Properties["email"] = Email;
            //ActivateItem(new CompanyViewModels());
            // App.Current.Properties[0] = Email;
            System.Windows.MessageBox.Show(" On Siging " + App.Current.Properties["email"].ToString());
         //   App.Current.Properties[1] = Password;
                var conductor = this.Parent as IConductor;
              
              conductor.ActivateItem(new CompanyViewModel());
           // conductor.showMessage("");

        }
    }
}
