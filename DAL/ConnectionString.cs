using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace trackpuls.DAL
{
    class ConnectionString
    {
        public ConnectionString()
        {
        }
        public string connect()
        {
            // local , Default
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
    }
}
