using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Enum
{
    [DataContract]
    public enum LanguageTypeEnum
    {
        [DataMember]
        国语 = 0001,
        [DataMember]
        粤语 = 0002,
        [DataMember]
        韩语 = 0003,
        [DataMember]
        英语 = 0004,
        [DataMember]
        日语 = 0005,
        [DataMember]
        法语 = 0006,
        [DataMember]
        德语 = 0007,
        [DataMember]
        俄语 = 0008,
    }
}
