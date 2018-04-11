using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataModel
{
    [DataContract]
    public class RoomTask
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string RoomId { get; set; }

        [DataMember]
        public string RoomState { get; set; }

        [DataMember]
        public string RoomConsume { get; set; }

        [DataMember]
        public DateTime? StartTime { get; set; }

        [DataMember]
        public DateTime? EndTime { get; set; }

        [DataMember]
        public int? CustomerId { get; set; }
    }
}