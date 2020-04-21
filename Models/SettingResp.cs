using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackpuls.Models
{
    public class SettingResp
    { 
        public string status { get; set; }
        public List<Settings> data { get; set; }
    }
}
