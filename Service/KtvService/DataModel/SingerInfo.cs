using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [DataContract]
    public class SingerInfo
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string SingerName { get; set; }
        [DataMember]
        public string SingerEnglishName { get; set; }
        [DataMember]
        public string SingerOtherName { get; set; }
        [DataMember]
        public string SingerInitials { get; set; }
        [DataMember]
        public string SingerNationality { get; set; }
        [DataMember]
        public string SingerPhotoUrl { get; set; }
        [DataMember]
        public string SingerClickNum { get; set; }
        [DataMember]
        public string SingerSex { get; set; }
        [DataMember]
        public string SingerIntroduce { get; set; }

    }
}
