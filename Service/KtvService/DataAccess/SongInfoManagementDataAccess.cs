using DataHelper;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SongInfoManagementDataAccess
    {
        public static string TABLENAME = "musicinfo";
        public static string FIELDNAME = "musicname, singerid, singername, languagetype, category, mvurl, musicnameinitials, singrail, releasedate, newsonghot";
        public static string INVERTFIELDNAME = "musicname, singerid, singername, languagetype, category, mvurl, musicnameinitials, singrail, releasedate, newsonghot";

        public static int AddSongInfoDataAccess(SongInfo songInfo)
        {
            var sql = $"insert into {TABLENAME}({FIELDNAME})" +
                $" values('{songInfo.MusicName}','{songInfo.SingerId}','{songInfo.SingerName}','{songInfo.LanguageType}','{songInfo.Category}','{songInfo.MVUrl}','{songInfo.MusicNameInitials}','{songInfo.SingRail}','{songInfo.ReleaseDate}','{songInfo.NewSongHot}')";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static int DeleteSongInfoDataAccess(string id)
        {
            var sql = $"delete from {TABLENAME} where id = {id}";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static DataTable GetAllSongInfoDataAccess()
        {
            var sql = $"select id, {INVERTFIELDNAME} from {TABLENAME} order by musicname";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static DataTable GetSongInfoBySingerIdDataAccess(string singerId)
        {
            var sql = $"select id, {FIELDNAME} from {TABLENAME} where singerid = '{singerId}'";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static int UpdateSongeInfoDataAccess(SongInfo songInfo)
        {
            var sql = $"update {TABLENAME} set musicname = '{songInfo.MusicName}', singerid = '{songInfo.SingerId}', singername = '{songInfo.SingerName}', languagetype = '{songInfo.LanguageType}',category = '{songInfo.Category}',mvurl = '{songInfo.MVUrl}',musicnameinitials = '{songInfo.MusicNameInitials}',singrail = '{songInfo.SingRail}',releasedate = '{songInfo.ReleaseDate}',newsonghot = '{songInfo.NewSongHot}' where id = {songInfo.Id}";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static DataTable GetAllCategorySourceDataAccess() {
            var sql = $"select id, categoryname, fatherid from musiccategory";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static DataTable GetCategorySourceByFatherIdDataAccess(string fatherId) {
            var sql = $"select id, categoryname from musiccategory where fatherid = '{fatherId}'";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        #region MusicRecord

        public static string MusicRecordTABLENAME = "musicrecord";
        public static string MusicRecordFIELDNAME = "musicid, clicknum, clickdate";
        
        public static int AddSongRecordDataAccess(List<SongRecord> records)
        {
            int returnNum = 0;
            DateTime clickDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            for (int i = 0; i < records.Count; i++)
            {
                var sql = $"insert into {MusicRecordTABLENAME}({MusicRecordFIELDNAME})" +
                          $" values('{records[i].SongId}','{records[i].ClickNum}','{clickDate}')";
                returnNum += SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
            }
            if (returnNum == records.Count) return 1;
            return 0;
        }

        public static DataTable GetAllSongRecordDataAccess() {
            var sql = $"select {MusicRecordTABLENAME}.musicid, clicknum, clickdate, musicname, singerid, singername, releasedate from {MusicRecordTABLENAME}, {TABLENAME} where {MusicRecordTABLENAME}.musicid = {TABLENAME}.id";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }
        
        public static int UpdateNewSongRankDataAccess(Dictionary<string, double> newRank)
        {
            int returnNum = 0;
            var clearSql = $"update {TABLENAME} set newsonghot = '0'";
            SqlServerHelper.ExecuteNonQuery(CommandType.Text, clearSql, 30, null);

            foreach (KeyValuePair<string, double> item in newRank)
            {
                var sql = $"update {TABLENAME} set newsonghot = '{item.Value}' where id = '{item.Key}'";
                returnNum += SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
            }
            
            if (returnNum == newRank.Count) return 1;
            return 0;
        }

        #endregion

    }
}