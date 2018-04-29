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

        public int DeleteSongInfo(SongInfo songInfo)
        {
            return SongInfoManagementDataAccess.DeleteSongInfoDataAccess(songInfo);
        }

        public DataTable GetAllCategorySource()
        {
            return SongInfoManagementDataAccess.GetAllCategorySourceDataAccess();
        }

        public DataTable GetAllSongInfo()
        {
            return SongInfoManagementDataAccess.GetAllSongInfoDataAccess();
        }
        
        public DataTable GetCategorySourceByFatherId(string fatherId)
        {
            return SongInfoManagementDataAccess.GetCategorySourceByFatherIdDataAccess(fatherId);
        }

        public DataTable GetSingerFilterSourceAllNationalityByInitial(string initial)
        {
            return SongInfoManagementDataAccess.GetSingerFilterSourceAllNationalityByInitialDataAccess(initial);
        }

        public DataTable GetSingerFilterSourceAllNationalityByRank()
        {
            return SongInfoManagementDataAccess.GetSingerFilterSourceAllNationalityByRankDataAccess();
        }

        public DataTable GetSingerFilterSourceByInitial(string nationality, string sex, string initial)
        {
            return SongInfoManagementDataAccess.GetSingerFilterSourceByInitialDataAccess(nationality, sex, initial);
        }

        public DataTable GetSingerFilterSourceByRank(string nationality, string sex)
        {
            return SongInfoManagementDataAccess.GetSingerFilterSourceByRankDataAccess(nationality, sex);
        }

        public SingerInfo GetSingerInfoById(string id)
        {
            return SongInfoManagementDataAccess.GetSingerInfoByIdDataAccess(id);
        }

        public DataTable GetSongInfoBySingerId(string singerId)
        {
            return SongInfoManagementDataAccess.GetSongInfoBySingerIdDataAccess(singerId);
        }

        public int UpdateSongeInfo(SongInfo songInfo)
        {
            return SongInfoManagementDataAccess.UpdateSongeInfoDataAccess(songInfo);
        }
    }
}