using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackpuls.Models
{
    public class LoginResp
    {
        public string status { get; set; }
        public string msg { get; set; }
        public IList<UserData> data {   get; set; }
    }
}
