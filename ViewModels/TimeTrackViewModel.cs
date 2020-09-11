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
    public class TimeTrackViewModel : Screen
    {
        #region Private Properties
        private BindableCollection<Project> _projects = new BindableCollection<Project>();
        private ObservableCollection<Project> proj;
        private List<Project> _projectsList = new List<Project>();
        private IConductor parent;
        private Screen view_parent;
        #endregion

        #region Getter Setter Properties 
        public List<Project> Projects
        {
            get
            {
                return _projectsList;
            }
            set
            {
                _projectsList = value;
                NotifyOfPropertyChange(() => Projects);
            }
        }
        public BindableCollection<Project> ProjectList
        {
            get { return _projects; }
            set { _projects = value; }
        }
        #endregion
        #region Constructor
        public TimeTrackViewModel(IConductor parent , Screen view_parent)
        {
            this.parent = parent;
            this.view_parent = view_parent;
            proj = new ObservableCollection<Project>();
            UserData userData = (UserData)App.Current.Properties["userdata"];
            if (userData != null)
            {
                GetProjects(userData.id.ToString());
            }

        }
        public async void GetProjects(string userid)
        {
            try
            {
                Projects proj = await GetProjects1(userid);
                if (proj.status == "true")
                {
                    foreach (var pro in proj.data)
                    {
                       // pro.shname = pro.name.First<char>().ToString();
                        pro.shname = pro.name.Substring(0, 2).ToUpper();
                        string task_count = "0";
                         TaskCount tc = Array.Find<TaskCount>(proj.task, ByProjectID(pro.project_id));
                    //    TaskCount tc = proj.task.Find(ByProjectID(pro.project_id));
                        if(tc != null)
                            task_count = tc.total_task;
                        pro.total_task = task_count  + " TASKS";
                        ProjectList.Add(pro);
                    }
                }
                else
                {
                    ProjectList.Add(new Project() {  project_id = "01" , user_id="02" , name=" No Projects Found "});
                    var parent = this.parent as MainViewModel;
                    parent.showMessage("No Project Found !!!");
                }
            }
            catch (Exception e ) {

                var parent = this.parent as MainViewModel;
                parent.showMessage(" " + e.Message + " ");
                System.Windows.MessageBox.Show("Error " + e.Message);

            }

        }
        private Predicate<TaskCount> ByProjectID(string projectID)
        {
            return delegate (TaskCount OrgElem)
            {
                return OrgElem.project_id == projectID;
            };
        }
        async Task<Projects> GetProjects1(string  userid)
        {
            var tsk = Task<Employee>.Run(() =>
            {
                Thread.Sleep(100);
                return  ProjectListServices.p_ProjectList(userid);
            });
            return await tsk;
        }
        public void ViewTask(Project data) {

            //  System.Windows.MessageBox.Show("Project ID " + data.project_id);
            if (data !=  null) {
                App.Current.Properties["project"] = (Project)data;
                TrackMainViewModel tsd = (TrackMainViewModel)this.view_parent;
                tsd.setScreen(new TaskViewModel(this.parent, this.view_parent));
            }
           
        }
        #endregion
    }
}
