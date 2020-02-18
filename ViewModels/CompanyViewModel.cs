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

        private IConductor parent;
        public CompanyViewModel(IConductor parent) {

            this.parent = parent;
        }
        public void SayHello(string name)
        {

            var conductor = this.Parent as IConductor;
            var parent = this.parent as MainViewModel;
            parent.showMessage(" Company Sucessfully... ");
            conductor.ActivateItem(new TrackMainViewModel(this.parent));
        }
    }
}
