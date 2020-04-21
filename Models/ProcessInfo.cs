using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trackpuls.Models
{
    public class ProcessInfo : IEquatable<ProcessInfo>
    {  
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string StartTime { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            ProcessInfo objAsPart = obj as ProcessInfo;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return ID;
        }
        public bool Equals(ProcessInfo other)
        {
            if (other == null) return false;
            return (this.ID.Equals(other.ID));
        }
    }
}
