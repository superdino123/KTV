using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [DataContract]
    public class SongRecord
    {
        [DataMember]
        public string SongId { get; set; }

        [DataMember]
        public int ClickNum { get; set; }
    }
}
