using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackpuls.Models
{
    public class AttendenceResp
    {
        public string status { get; set; }
        public string message { get; set; }
        public AttendenceData data { get; set;}
    }
}
