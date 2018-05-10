using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [DataContract]
    public class SongInfo
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string MusicName { get; set; }
        [DataMember]
        public int SingerId { get; set; }
        [DataMember]
        public string SingerName { get; set; }
        [DataMember]
        public string LanguageType { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string RecordNumber { get; set; }
        [DataMember]
        public string MVUrl { get; set; }
        [DataMember]
        public string MusicNameInitials { get; set; }
        [DataMember]
        public DateTime? ReleaseDate { get; set; }
        [DataMember]
        public string SingRail { get; set; }
    }
}
