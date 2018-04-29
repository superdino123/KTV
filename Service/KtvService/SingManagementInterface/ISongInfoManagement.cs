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
    public interface ISongInfoManagement
    {
        [OperationContract]
        DataTable GetAllSongInfo();

        [OperationContract]
        int AddSongInfo(SongInfo songInfo);

        [OperationContract]
        int UpdateSongeInfo(SongInfo songInfo);

        [OperationContract]
        int DeleteSongInfo(SongInfo songInfo);

        [OperationContract]
        DataTable GetSongInfoBySingerId(string singerId);

        [OperationContract]
        DataTable GetAllCategorySource();

        [OperationContract]
        DataTable GetCategorySourceByFatherId(string fatherId);
        
        [OperationContract]
        DataTable GetSingerFilterSourceByInitial(string nationality, string sex, string initial);

        [OperationContract]
        DataTable GetSingerFilterSourceByRank(string nationality, string sex);

        [OperationContract]
        DataTable GetSingerFilterSourceAllNationalityByInitial(string initial);

        [OperationContract]
        DataTable GetSingerFilterSourceAllNationalityByRank();

        [OperationContract]
        SingerInfo GetSingerInfoById(string id);
    }
}
