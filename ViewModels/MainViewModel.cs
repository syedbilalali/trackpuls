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
        //public RelayCommand<string> SendMessageCommand { get; }
      //  public RelayCommand<string> SendWithPubSubCommand { get; }
        public MainViewModel() {

            ActivateItem( new LoginViewModel());
           
        }
        public void LoadLoginPage()
        {
            //ActivateItem(new LoginViewModel());
        }
        public void btnsignIn()
        {
            // ActivateItem(new CompanyViewModel());
          //  System.Diagnostics.Debug.WriteLine(" Hello WOrld");

        }
        public void ProfBtn() {
            // System.Diagnostics.Debug.WriteLine(" Hello WOrld");
            //  System.Windows.MessageBox.Show("CLick");
            BoundMessageQueue.Enqueue("Hello World");
        }
        public void showMessage(string message ) {
            BoundMessageQueue.Enqueue(message);
        }
    }
    public interface MConductor<T> : IConductor {
        void showMessage(string message);
    }
}
