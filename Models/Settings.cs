using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackpuls.Models
{
    public class Settings
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string name { get; set; }
        public string computer_type { get; set; }
        public string visibility { get; set; }
        public string screenshot_frequency { get; set; }
        public string break_time { get; set; }
        public string idle_time { get; set; }
        public string tracking_scenario { get; set; }
        public string days { get; set; }
        public string shift_start { get; set; }
        public string shift_end { get; set; }
        public string created_at { get; set;   }
    }
}
