using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace trackpuls.Models
{
    public class Timeslab : PropertyChangedBase
    {   
        public int ID { get; set; }
        public string ClockIn { get; set; }
        public string ClockOut { get; set; }
        public string Duration { get; set; }
    }
}
