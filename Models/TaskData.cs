using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackpuls.Models
{
    public class TaskData
    {  
        public string user_name { get; set;  }
        public string project { get; set; }
        public string id { get; set;  }
        public string name { get; set;  }
        public string description { get; set;  }
        public string status { get; set;  }
        public string priority { get; set;  }
        public string due_date { get; set; }
        public string due_time { get; set;  }
        public string project_id { get; set;  }
        public string user_id { get; set;  }
        public string created_at { get; set;  }
    }
}
