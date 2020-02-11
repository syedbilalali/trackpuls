using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;

namespace trackpuls.ViewModels
{
    public class AttendanceViewModel : Screen
    {
        private string _todayDate = DateTime.Now.ToString("MMMM, dd yyyy");
        private string _startedTime = "Not Started Yet";
        private string _timeSpan = "00:00:00 h";
        private DateTime _lastTime;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private bool _btnClockOut = false;
        private bool _btnClockIn = true;
        public enum Visibility { Collapsed , Hidden , Visible };
        private Visibility _CIvisibility  = Visibility.Hidden;
        private Visibility _COvisibility = Visibility.Hidden;
        public AttendanceViewModel() {

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0,0,1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {   
            TimeSpan value = _lastTime.Subtract(DateTime.Now);
            lblTimeSpan = value.Duration().ToString(@"hh\:mm\:ss") + " h";
        }
        public string lbltoday {
            get {
                return _todayDate;
            }
            set {
                _todayDate = value;
                NotifyOfPropertyChange(() => lbltoday);
            }
        }
        public string lblStarted {
            get {
                return _startedTime;
            }
            set {
                _startedTime = value;
                NotifyOfPropertyChange(() => lblStarted);
            }
        }
        public string lblTimeSpan {
            get {
                return _timeSpan;
            }
            set {
                _timeSpan = value;
                NotifyOfPropertyChange(()=> lblTimeSpan);
            }
        }
        public bool IsClockOut {
            get {
                return _btnClockOut;
            }
            set {
                _btnClockOut = value;
                NotifyOfPropertyChange(() => _btnClockOut);
            }
        }
        public bool IsClockIn {
            get {
                return _btnClockOut;
            }
            set {
                _btnClockIn = value;
                NotifyOfPropertyChange(()=> _btnClockOut);
            }
        }
        public void btnClockIn(){

            lblStarted = "Started At " + DateTime.Now.ToString("h:mm tt");
            _lastTime = DateTime.Now;
          //  lblTimeSpan = DateTime.Now.ToLongTimeString();
            //System.Windows.MessageBox.Show(" On Clock In");
            dispatcherTimer.Start();
        }
        public void btnClockOut() {
             
            System.Windows.MessageBox.Show(" On Clock Out");
            dispatcherTimer.Stop();
        }
        public Visibility CIVisibility {
            get { 
                return _CIvisibility; 
            }
            set { 
                _CIvisibility = value;
                NotifyOfPropertyChange(()=> _CIvisibility);
            }
        }
        public Visibility COVisibility {

            get {

                return _COvisibility;
            }
            set {
                _COvisibility = value;
                NotifyOfPropertyChange(() => _COvisibility);
            }
        }
    }
}
