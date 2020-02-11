using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using trackpuls.ViewModels;


namespace trackpuls.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
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
            System.Windows.MessageBox.Show("CLick");
        }
    }
    
}
