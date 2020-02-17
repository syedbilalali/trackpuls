using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Windows.Interactivity;
using Caliburn.Micro.Validation;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using trackpuls;

namespace trackpuls.ViewModels
{
    [Export(typeof(IShell))]
    public class LoginViewModel : ValidatingScreen<LoginViewModel>, IShell
    {
        private string _email;
        private string _password;


        #region Validating Screen Ovverides 
        
        /// <summary>
        /// Allows this class to refine the error(s) reported
        /// </summary>
        /// <param name="columnName">The name of the field being validated</param>
        /// <param name="errors">Any existing errors and their type.</param>
        protected override void OnColumnrror(string columnName, System.Collections.Generic.Dictionary<System.Type, string> errors)
        {
            // Do nothing at the moment.
        }
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            LoginViewModel shellView = view as LoginViewModel;

           // shellView.Password.Password = Password;
        }
        #endregion
        public LoginViewModel() {
            System.Windows.MessageBox.Show(" On Login Load.. " );
            if (App.Current.Properties["email"]!= null && App.Current.Properties["password"] != null) {
                System.Windows.MessageBox.Show("Properties : " + App.Current.Properties["email"].ToString() );
                Email = App.Current.Properties["email"].ToString();
                Password = App.Current.Properties["password"].ToString();
            }
        }
        /// </summary>
		[Required(ErrorMessage = "Email is required")]
        [EmailValidator(ErrorMessage = "The format of the email address is not valid")]
        //Define Group : ExcludeFromButton
        [ValidationGroup(IncludeInErrorsValidation = false, GroupName = "Default")]
        public string Email {
            get {
                return _email;
            }
            set {
                _email = value;
                NotifyOfPropertyChange(() => Email);
                NotifyOfPropertyChange(() => NoValidationErrors);
            }
        }
        [RequiredEx(ErrorMessage = "A password is required", GuardProperty = "Guard", ValidateWhileDisabled = false)]
        [StrongPasswordValidator(ErrorMessage = "The password must be at least 8 characters and contain 1 lower-case, 1 upper-case, 1 number and 1 of &^%$#@!")]
        public string Password {
            get { return _password; }
            set { 
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => NoValidationErrors);
            }

        }
        /// <summary>
		/// The PasswordBox.Password cannot be bound because its not a dependency property
		/// So the Xaml is marked with the Caliburn Message.Attach attached property to
		/// fire this method.  It can then save the password and update the notification.
		/// </summary>
		/// <param name="control"></param>
		public void PasswordChanged(Control control)
        {
            if (!(control is PasswordBox)) return;
            this.Password = (control as PasswordBox).Password;
        }
        /// <summary>
		/// Example guard method
		/// </summary>
		public bool Guard { get; set; }

        #region Close button



        public void btnsignIn() {

            App.Current.Properties["email"] = Email;
            App.Current.Properties["password"] = Password;
            //ActivateItem(new CompanyViewModels());
            // App.Current.Properties[0] = Email;
            System.Windows.MessageBox.Show("Email " + App.Current.Properties["email"].ToString() + " Password " + App.Current.Properties["password"].ToString());
         //   App.Current.Properties[1] = Password;
               // var conductor = this.Parent as IConductor;
              
           //  conductor.ActivateItem(new CompanyViewModel());
           // conductor.showMessage("");

        }
        /// <summary>
        /// This property is bound to the IsEnabled property of the 'Close' 
        /// button so the enabled state can be controlled by the validation 
        /// state of all controls
        /// </summary>
        /// <remarks>HasErrors is implemented by ValidatingScreen</remarks>
        public bool NoValidationErrors
        {
            get { return !HasErrorsByGroup(); }
        }

        /// <summary>
        /// Just displays an acknowledgement that the button was pressed
        /// </summary>
        #endregion
    }
}
