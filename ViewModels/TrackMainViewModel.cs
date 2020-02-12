using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Caliburn.Micro;

namespace trackpuls.ViewModels
{
    public class TrackMainViewModel : Conductor<object>
    {
        
        public TrackMainViewModel() {
           
        }
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
