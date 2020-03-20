using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace trackpuls.Models
{
    public class Project :  PropertyChangedBase 
    {   
        public int ID { get; set; }
        public string project_id { get; set; }
        public string user_id { get; set; }
        public string name { get; set; }
    }
}
