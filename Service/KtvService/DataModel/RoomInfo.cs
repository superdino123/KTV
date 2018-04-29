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

        /// <summary>
        /// 空调的数量
        /// </summary>
        [DataMember]
        public string AirConditionerNumber { get; set; }

        /// <summary>
        /// 功放机的数量
        /// </summary>
        [DataMember]
        public string PowerAmplifierNumber { get; set; }

        /// <summary>
        /// 音响的数量
        /// </summary>
        [DataMember]
        public string SoundNumber { get; set; }

        /// <summary>
        /// 效果器的数量
        /// </summary>
        [DataMember]
        public string EffectorNumber { get; set; }

        /// <summary>
        /// 点歌台的数量
        /// </summary>
        [DataMember]
        public string SongDeskNumber { get; set; }

        /// <summary>
        /// 液晶电视的数量
        /// </summary>
        [DataMember]
        public string LCDTVNumber { get; set; }

        /// <summary>
        /// 房间备注
        /// </summary>
        [DataMember]
        public string RoomRemark { get; set; }
    }
}
