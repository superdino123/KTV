using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [DataContract]
    public class StaffInfo
    {
        [DataMember]
        public string UserRecord { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string UserPassword { get; set; }
        
        [DataMember]
        public string UserAddress { get; set; }

        [DataMember]
        public string Salt { get; set; }

    }
}