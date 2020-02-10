using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace trackpuls.ViewModels
{
    public class CompanyViewModel : Screen
    {
        public void btnSelectCompany() {

            var conductor = this.Parent as IConductor;
            conductor.ActivateItem(new TrackMainViewModel());
        }
        public void SayHello()
        {
            System.Windows.MessageBox.Show("Hello World From the Model");
        }
        public void SayHello(string name)
        {
           // System.Windows.MessageBox.Show("Hello World From the Model " + name);
            var conductor = this.Parent as IConductor;
            conductor.ActivateItem(new TrackMainViewModel());

        }
    }
}
