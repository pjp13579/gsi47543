using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSI47543.DB
{
    public class User
    {
        public string ID { get; set; }
        public string? displayName { get; set; }
        public string? ou { get; set; }
        
        public bool? accountActive { get; set; }
        
        public int? userAccountControl { get; set; }
        public int? logonCount { get; set; }
        
        // add hour to db cell
        public DateTime? pwdExpires { get; set; }
        public DateTime? whenCreated { get; set; }
        public DateTime? pwdLastSet { get; set; }
    }
}
