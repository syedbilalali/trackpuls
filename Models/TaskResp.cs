using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackpuls.Models
{
    class TaskResp
    {
        public string status { get; set; }
        public string message { get; set; }
        public TaskData[] data { get; set; }
    }
}
