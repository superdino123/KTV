using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [DataContract]
    public class CustomerInfo
    {
        [DataMember]
        public string CustomerId { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string CustomerSex { get; set; }

        [DataMember]
        public string CustomerTel { get; set; }
    }
}
