using DataAccess;
using DataModel;
using SingManagementInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingManagementService
{
    public class SongInfoManagementImplementation : ISongInfoManagement
    {
        public int AddSongInfo(SongInfo songInfo)
        {
            return SongInfoManagementDataAccess.AddSongInfoDataAccess(songInfo);
        }

        public int AddSongRecord(List<SongRecord> records)
        {
            return SongInfoManagementDataAccess.AddSongRecordDataAccess(records);
        }

        public int DeleteSongInfo(string id)
        {
            return SongInfoManagementDataAccess.DeleteSongInfoDataAccess(id);
        }

        public DataTable GetAllCategorySource()
        {
            return SongInfoManagementDataAccess.GetAllCategorySourceDataAccess();
        }

        public DataTable GetAllSongInfo()
        {
            return SongInfoManagementDataAccess.GetAllSongInfoDataAccess();
        }

        public DataTable GetAllSongRecord()
        {
            return SongInfoManagementDataAccess.GetAllSongRecordDataAccess();
        }

        public DataTable GetCategorySourceByFatherId(string fatherId)
        {
            return SongInfoManagementDataAccess.GetCategorySourceByFatherIdDataAccess(fatherId);
        }

        public DataTable GetSongInfoBySingerId(string singerId)
        {
            return SongInfoManagementDataAccess.GetSongInfoBySingerIdDataAccess(singerId);
        }

        public int UpdateNewSongRank(Dictionary<string, double> newRank)
        {
            return SongInfoManagementDataAccess.UpdateNewSongRankDataAccess(newRank);
        }

        public int UpdateSongeInfo(SongInfo songInfo)
        {
            return SongInfoManagementDataAccess.UpdateSongeInfoDataAccess(songInfo);
        }
    }
}