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
        DataTable GetSingerInfoPaging(string nationality, string sex, string initial);

        [OperationContract]
        int AddSingerInfo(SingerInfo singerInfo);

        [OperationContract]
        int UpdateSingerInfo(SingerInfo singerInfo);

        [OperationContract]
        int DeleteSingerInfo(string singerId);

        [OperationContract]
        DataTable GetSingerInfoByName(string singerName);

        [OperationContract]
        SingerInfo GetSingerInfoById(string id);
        
        [OperationContract]
        DataTable GetNationalityInfoSource();
    }
}
