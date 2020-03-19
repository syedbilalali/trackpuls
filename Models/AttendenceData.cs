using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackpuls.Models
{
   public class AttendenceData
    {
        public string user_id { get; set; }
        public string clock_in { get; set; }
        public string clock_out { get; set; }
        public string computer_act { get; set; }
        public string office_time { get; set; }
        public string productive { get; set; }
    }
}
