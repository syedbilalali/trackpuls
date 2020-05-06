using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.ComponentModel;

namespace trackpuls.Models
{
    public class TaskData :  INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

     //   public string user_name { get; set; }
        private string _username;
        public string user_name {
            get {
                return _username;
            }
            set {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
      //  public string project { get; set; }
        private string _project;
        public string Project {
            get {
                return _project;
            }
            set {
                _project = value;
                OnPropertyChanged("Project");
            }
        }
       
     //   public string id { get; set; }
        private string _id;
        public string id {
            get {
                return _id;
            }
            set {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
      //  public string name { get; set; }
        private string _name;
        public string name {
            get {
                return _name;
            }
            set {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

    //    public string description { get; set; }
        private string _description;
        public string description {
            get {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
     //   public string status { get; set; }
        private string _status;
        public string status {
            get {
                return _status;
            }
            set {
                _status = value;
                OnPropertyChanged("Status");
            }
        }
    //    public string priority { get; set; }
        private string _priority;
        public string priority { get { return _priority; } set {
                _priority = value;
                OnPropertyChanged("Priority");
            }
        }
      //  public string due_date { get; set; }
        private string _due_date;
        public string due_date { get {
                return _due_date;
            }
            set {
                _due_date = value;
                OnPropertyChanged("Due_date");
            }
        }
       // public string due_time { get; set; }
        private string _due_time;
        public string due_time { get {
                return _due_time;
            } set {
                _due_time = value;
                OnPropertyChanged("Due_time");
            } }
      //  public string project_id { get; set; }
        public string _project_id;
        public string project_id { get {
                return _project_id;
            }
            set {
                _project_id = value;
                OnPropertyChanged("Project_id");
            }
        }
     //   public string user_id { get; set; }
        private string _user_id;
        public string user_id { get {
                return _user_id;
            } set {
                _user_id = value;
                OnPropertyChanged("User_id");
            } }
       // public string created_at { get; set; }
        private string _created_at;
        public string created_at { get {
                return _created_at;
            } set {
                _created_at = value;
                OnPropertyChanged("Created_at");
            }
        }
        private string _duration = "00:00:00";
        public string duration
        {
            get {
           
                    return _duration;
            }
            set {
                _duration = value;
                OnPropertyChanged("duration");
            }
        }
        private bool _IsRunning ;
        public bool isRunning {
            get {
                return _IsRunning;
            }
            set {
                _IsRunning = value;
                OnPropertyChanged("isRunning");
            }
        }
        private void OnPropertyChanged( string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
