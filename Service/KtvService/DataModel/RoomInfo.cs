using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [DataContract]
    public class RoomInfo
    {
        /// <summary>
        /// 房间号
        /// </summary>
        [DataMember]
        public string RoomId { get; set; }
        /// <summary>
        /// 房间类型
        /// </summary>
        [DataMember]
        public string RoomType { get; set; }
        /// <summary>
        /// 房间面积
        /// </summary>
        [DataMember]
        public string RoomSize { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        [DataMember]
        public string ImageUrl { get; set; }
        /// <summary>
        /// 麦克风的数量
        /// </summary>
        [DataMember]
        public string MicroPhoneNumber { get; set; }
    }
}
