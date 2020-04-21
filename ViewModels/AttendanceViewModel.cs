using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;
using System.Windows.Input;
using System.Threading;
using trackpuls.Models;
using trackpuls.Services;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Windows.Interop;
using trackpuls.Helper;
using System.Globalization;

namespace trackpuls.ViewModels
{
    public class AttendanceViewModel : Screen
    {
        #region Private Properties
        private string _todayDate = DateTime.Now.ToString("MMMM, dd yyyy");
        private string _startedTime = "Not Started Yet";
        private string _timeSpan = "00:00:00 h";
        private DateTime _lastTime;
        private System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private System.Windows.Threading.DispatcherTimer inactiveTimer;
        private bool _btnClockOut = false;
        private bool _btnClockIn = true;
        private List<Timeslab> _timeslabs = new List<Timeslab>();
        private BindableCollection<Timeslab> _people = new BindableCollection<Timeslab>();
        private UserData userData;
        private string tempID = "0";
        public int logOffTime = 1;
        private IConductor parent;
        private AllInputSources lastInput;
        private KeyboardInput keyboard;
        private MouseInput mouse;
        #endregion



        #region Constructor
        public AttendanceViewModel(IConductor parent ) {

            this.parent = parent;
            userData = (UserData)App.Current.Properties["userdata"];
            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0,0,0);
            dispatcherTimer.Tick += DispatcherTimer_Tick;

            inactiveTimer = new System.Windows.Threading.DispatcherTimer();
            inactiveTimer.Interval = new TimeSpan(0, 0, 0);
            inactiveTimer.Tick += inactiveTimer_Tick;

            TimeSpan duration = DateTime.Now.Subtract(DateTime.Now);

            keyboard = new KeyboardInput();
         //   keyboard.KeyBoardKeyPressed += keyboard_KeyBoardKeyPressed;

            mouse = new MouseInput();
          //  mouse.MouseMoved += mouse_MouseMoved;

            lastInput = new AllInputSources();

        }
        #endregion

        #region Getter Setter Properties 
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
                NotifyOfPropertyChange(() => IsClockOut);
            }
        }
        public bool IsClockIn {
            get {
                return _btnClockIn;
            }
            set {
                _btnClockIn = value;
                NotifyOfPropertyChange(()=> IsClockIn);
            }
        }
        public List<Timeslab> Timeslabs {
            get
            {
                return _timeslabs;
            }
            set
            {
                _timeslabs = value;
                NotifyOfPropertyChange(() => Timeslabs);
            }
        }
        public BindableCollection<Timeslab> People
        {
            get { return _people; }
            set { _people = value; }
        }
        #endregion
        #region Methods
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan value = _lastTime.Subtract(DateTime.Now);
            lblTimeSpan = value.Duration().ToString(@"hh\:mm\:ss") + " h";
        }
        
        private void inactiveTimer_Tick(object sender, EventArgs e)
        {
            DateTime lastTime = lastInput.GetLastInputTime();
            if (DateTime.Now.CompareTo(lastTime.AddSeconds(30)) == 0 || DateTime.Now.CompareTo(lastTime.AddSeconds(30)) > 0)
            {
                if (IsClockOut)
                {
                    dispatcherTimer.Stop();
                    MessageBoxResult result = MessageBox.Show(" Do you want to resume ?", "Resume", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        dispatcherTimer.Start();
                    }
                    else {
                        btnClockOut();
                    }
                }
            }
        }
        public async void btnClockIn(){

            lblStarted = "Started At " + DateTime.Now.ToString("h:mm tt");
            _lastTime = DateTime.Now;    
            IsClockIn = false;
            IsClockOut = true;
            dispatcherTimer.Start();
            inactiveTimer.Start();
            try {

                    if (userData != null) {

                        AttendenceResp resp = await AttendenceService.p_clockIn(userData.id, _lastTime.ToString("HH:mm:ss"), "0");
                        if (resp.status == "true")
                        {
                          tempID = resp.data.id;
                        }
                    }

            } catch (Exception e) {

                var parent = this.parent as MainViewModel;
                parent.showMessage(" " + e.Message + " ");
            }
            
        }
        public async void btnClockOut() {

           //Set Button Visibility
           TimeSpan duration = _lastTime.Subtract(DateTime.Now);
            IsClockIn = true;
            IsClockOut = false;
            lblStarted = "Not Started Yet";
            TimeSpan value = TimeSpan.Zero;
            lblTimeSpan = value.Duration().ToString(@"hh\:mm\:ss") + " h";
            dispatcherTimer.Stop();
            inactiveTimer.Stop();
            People.Add(new Timeslab() { ID= People.Count, ClockIn = _lastTime.ToString("h:mm:ss tt"), ClockOut = DateTime.Now.ToString("h:mm:ss tt"), Duration = duration.Duration().ToString(@"hh\:mm\:ss") });
            try
            {
                if (tempID != null)
                {
                    AttendenceResp resp = await AttendenceService.p_clockout(userData.id, "1", DateTime.Now.ToString("HH:mm:ss"), tempID);
                    if (resp != null) {
                        if (resp.status == "true")
                        {
                            ///System.Windows.MessageBox.Show(" After Status");
                          //  MessageBox.Show(" Successfully Save the Details " + _lastTime.ToString("HH:mm:ss"));
                        }
                    }
                   
                }
                
            }
            catch (Exception e)
            {
                var parent = this.parent as MainViewModel;
                parent.showMessage(" " + e.Message + " ");
            }
        }
        #endregion
    }
}
