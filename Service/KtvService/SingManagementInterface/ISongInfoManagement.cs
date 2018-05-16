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
        int DeleteSongInfo(string id);

        [OperationContract]
        DataTable GetSongInfoBySingerId(string singerId);

        [OperationContract]
        DataTable GetAllCategorySource();

        [OperationContract]
        DataTable GetCategorySourceByFatherId(string fatherId);


        [OperationContract]
        int AddSongRecord(List<SongRecord> records);

        [OperationContract]
        DataTable GetAllSongRecord();

        [OperationContract]
        int UpdateNewSongRank(Dictionary<string, double> newRank);

    }
}