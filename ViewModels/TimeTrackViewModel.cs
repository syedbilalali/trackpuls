using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private List<Project> _projectsList = new List<Project>();
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
        public TimeTrackViewModel() {

            UserData userData = (UserData)App.Current.Properties["userdata"];
            if (userData != null) {
                GetProjects(userData.id);
            }
           
        }
        public async void GetProjects(string userid) {
           Projects proj =  await  ProjectListServices.p_ProjectList(userid);
           ProjectList.Add(proj.data[0]);
        }
        #endregion
    }
}
