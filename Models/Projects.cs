﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Newtonsoft.Json;

namespace trackpuls.Models
{   
    
    public class Projects 
    {
        public string status { get; set; }
        public Project[] data { get; set; }
        public TaskCount[] task { get; set; }

    }
}
