using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Caliburn.Micro;
using trackpuls.Models;
using trackpuls.Services;

namespace trackpuls.ViewModels
{

    public class TaskViewModel : Screen
    {
        private ObservableCollection<TaskData> _nt = new ObservableCollection<TaskData>();
        private ObservableCollection<TaskData> _oh = new ObservableCollection<TaskData>();
        private ObservableCollection<TaskData> _ip = new ObservableCollection<TaskData>();
        private ObservableCollection<TaskData> _cp = new ObservableCollection<TaskData>();

        private IConductor parent;
        private Screen view_parent;
        private TaskData _selectedTaskData;
        private int _selectedIndex;

        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;

        public TaskViewModel(IConductor parent, Screen view_parent)
        {
            this.parent = parent;
            this.view_parent = view_parent;
            UserData userData = (UserData)App.Current.Properties["userdata"];
            if (userData != null)
            {
                GetTasks(userData.id.ToString());
            }
            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }
        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds);
                // clocktxt.Text = currentTime;
                Nt_Task[_selectedIndex].duration = currentTime;
            }
        }
        #region Getter Setter Properties 
        public ObservableCollection<TaskData> Nt_Task {
            get { return _nt; }
            set { _nt = value; NotifyOfPropertyChange(() => Nt_Task); }
        }
        public ObservableCollection<TaskData> Oh_Task
        {
            get { return _oh; }
            set { _oh = value; NotifyOfPropertyChange(() => Oh_Task); }
        }
        public ObservableCollection<TaskData> Ip_Task
        {
            get { return _ip; }
            set { _ip = value; NotifyOfPropertyChange(() => Ip_Task); }
        }
        public ObservableCollection<TaskData> Cp_Task
        {
            get { return _cp; }
            set { _cp = value; NotifyOfPropertyChange(() => Cp_Task); }
        }
        #endregion
        public void btnSelectCompany()
        {
           //  System.Windows.MessageBox.Show(" Hello World");
        }
        public void btnBackProjects()
        {

            TrackMainViewModel tsd = (TrackMainViewModel)this.view_parent;
            tsd.setScreen(new TimeTrackViewModel(this.parent, this.view_parent));

        }
        public async void GetTasks(string userid)
        {
            try
            {

                TaskResp tr = await GetTasks1(userid);
                if (tr.status == "true")
                {
                    // System.Windows.MessageBox.Show(" Task Count : " + tr.data.Count());
                    foreach (var task in tr.data)
                    {   

                        if (task.status == "0")
                        {
                            //On Hold
                            Oh_Task.Add(task);
                        }
                        if (task.status == "1")
                        {   
                            //To do
                            Nt_Task.Add(task);
                        }
                        if (task.status == "2")
                        {
                            //In Progress
                            Ip_Task.Add(task);
                        }
                        if (task.status == "3")
                        {
                            //Completed
                            Cp_Task.Add(task);
                        }


                    }
                }
                else
                {

                }

            }
            catch (Exception e)
            {

                var parent = this.parent as MainViewModel;
                parent.showMessage(" " + e.Message + " ");

            }
        }
        async Task<TaskResp> GetTasks1(string userid)
        {
            var tsk = Task<Employee>.Run(() =>
            {
                Thread.Sleep(100);
                return TaskServices.p_TaskList(userid);
            });
            return await tsk;
        }
        public void RunTimer(TaskData td)
        {
           // System.Windows.MessageBox.Show("Details" + td.name);
            _selectedIndex = Nt_Task.IndexOf(td);
            if (!td.isRunning) {
                Nt_Task[_selectedIndex].status  = "S";
                Nt_Task[_selectedIndex].isRunning = true;
                startTime();
            }else
            {
                Nt_Task[_selectedIndex].status = "P";
                Nt_Task[_selectedIndex].isRunning = false;
                // System.Windows.MessageBox.Show(" Already Running ");
                stopTime();
            }
           
        }
        private void startTime() {
            
             stopWatch.Start(); 
             dispatcherTimer.Start();  
        }
        private void stopTime() {
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                dispatcherTimer.Stop();
            }
        }
    }
}