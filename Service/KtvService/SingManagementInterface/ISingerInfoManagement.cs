using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SingManagementInterface
{
    [ServiceContract]
    public interface ISingerInfoManagement
    {
        [OperationContract]
        DataTable GetAllSingerInfo();

        [OperationContract]
        int AddSingerInfo(SingerInfo singerInfo);

        [OperationContract]
        int UpdateSingerInfo(SingerInfo singerInfo);

        [OperationContract]
        int DeleteSingerInfo(SingerInfo singerInfo);

        [OperationContract]
        DataTable GetSingerInfoByName(string singerName);
    }
}
