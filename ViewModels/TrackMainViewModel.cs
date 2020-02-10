using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace trackpuls.ViewModels
{
    public class TrackMainViewModel : Conductor<object>
    {
        public void btnAttendanceTrack() {

             //System.Windows.MessageBox.Show(" Attendane Track View ");
             ActivateItem(new AttendanceViewModel());
        }
        public void btnTimeTrack() {

            //System.Windows.MessageBox.Show(" Time Track ");
            ActivateItem(new TimeTrackViewModel());
        }
    }

}
