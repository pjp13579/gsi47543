using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSI47543.AD
{
    public class User
    {
        // change type to GUID
        public string objectGUID { get; set; }
        
        public string? displayName { get; set; }
        public string? distinguishedName { get; set; }
        public int? logonCount { get; set; }
        public int? userAccountControl { get; set; }
        
        //dates
        public DateTime? pwdExpires { get; set; }
        public DateTime? whenCreated { get; set; }
        public DateTime? pwdLastSet { get; set; }
        

    }
}
