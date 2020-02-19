using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackpuls.Models
{
    public class LoginResp
    {
        public int result { get; set; }
        public string msg { get; set; }
        public List<UserData> data { get; set; }
        public string __invalid_name__0 { get; set; }
    }
}
