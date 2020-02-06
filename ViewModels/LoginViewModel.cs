using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace trackpuls.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _email;
        private string _password;

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
    }
}
