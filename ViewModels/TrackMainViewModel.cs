﻿using System;
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
        private IConductor parent;
        private Screen attendence;
        private Screen timetrack;
        public TrackMainViewModel(IConductor parent) {

            this.parent = parent;
            showProf();
            attendence = new AttendanceViewModel(this.parent);
            ActivateItem(attendence);
        }
        private void showProf() {

            var parent = this.parent as MainViewModel;
            parent.IsProfilePic = true;
        }
        public void btnAttendanceTrack() {

            if (attendence != null)
            {
                ActivateItem(this.attendence);
            }
            else {
                attendence = new AttendanceViewModel(this.parent);
                ActivateItem(attendence);
            }
            
        }
        public void btnTimeTrack() {

            if (timetrack != null) {
                ActivateItem(this.timetrack);
            }
            else {
                timetrack = new TimeTrackViewModel(this.parent , this);
                ActivateItem(timetrack);
            }
            
        }
        public void setScreen(Screen item) {

            if (item != null) { 
                ActivateItem(item);
            }
        }

    }

}
