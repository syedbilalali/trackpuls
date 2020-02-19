using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackpuls.Models
{
    public class UserData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string group_id { get; set; }
        public string office_time { get; set; }
        public string status { get; set; }
        public string invitation { get; set; }
        public object gu_id { get; set; }
        public object pay_rate { get; set; }
        public object overtime_pay_rate { get; set; }
        public object bill_rate { get; set; }
        public object overtime_bill_rate { get; set; }
        public string created_at { get; set; }
        public override string ToString()
        {
            return "ID " + this.id + " Name : " + this.name;
        }
    }
}
