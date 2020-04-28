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
        public TimeTrackViewModel(IConductor parent)
        {
            this.parent = parent;
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

            }

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
        #endregion

    }
}
