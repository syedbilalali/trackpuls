using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using trackpuls.Models;
using trackpuls.Services;

namespace trackpuls.ViewModels
{   

    public class TaskViewModel : Screen
    {
        
        private BindableCollection<TaskData> _task = new BindableCollection<TaskData>();
        private BindableCollection<TaskData> _NTtask = new BindableCollection<TaskData>();
        private List<TaskData> _taskList = new List<TaskData>();
        private IConductor parent;
        private Screen view_parent;
        public TaskViewModel(IConductor parent , Screen view_parent) {
            this.parent = parent;
            this.view_parent = view_parent;
            UserData userData = (UserData)App.Current.Properties["userdata"];
            if (userData != null)
            {
                GetTasks(userData.id.ToString());
            }
        }
        #region Getter Setter Properties 
        public List<TaskData> Tasks
        {
            get
            {
                return _taskList;
            }
            set
            {
                _taskList = value;
                NotifyOfPropertyChange(() => Tasks);
            }
        }
        public BindableCollection<TaskData> TaskList
        {
            get { return _task; }
            set { _task = value; }
        }
        public BindableCollection<TaskData> NTTaskList
        {
            get { return _NTtask; }
            set { _NTtask = value; }
        }
        #endregion
        public void btnSelectCompany() {
           // System.Windows.MessageBox.Show(" Hello World");
        }
        public void btnBackProjects() {

            TrackMainViewModel tsd = (TrackMainViewModel)this.view_parent;
            tsd.setScreen(new TimeTrackViewModel(this.parent , this.view_parent));

        }
        public async void GetTasks(string userid) {
            try {

                TaskResp tr = await GetTasks1(userid);
                if (tr.status == "true")
                {
                    // System.Windows.MessageBox.Show(" Task Count : " + tr.data.Count());
                    foreach (var task in tr.data) {
                       
                        if (task.status == "1") {
                           // System.Windows.MessageBox.Show(" To do " + task.status + " Task : " + task.name);
                            NTTaskList.Add(task);
                        }
                        //if (task.status =="0") {
                        //    TaskList.Add(task);
                        //}
                      //  TaskList.Add(task);

                    }
                }
                else {

                }

            } catch (Exception e) {

                var parent = this.parent as MainViewModel;
                parent.showMessage(" " + e.Message + " ");

            }
        }
        async Task<TaskResp> GetTasks1(string userid)
        {
            var tsk = Task <Employee>.Run(() =>
            {
                Thread.Sleep(100);
                return TaskServices.p_TaskList(userid);
            });
            return await tsk;
        }
        public void RunTimer(TaskData td) {
            System.Windows.MessageBox.Show("Details" + td.name );
        }
    }
}
