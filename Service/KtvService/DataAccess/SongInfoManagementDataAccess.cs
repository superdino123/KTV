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
        public static string FIELDNAME = "musicname, singerid, singername, languagetype, category, recordnumber, mvurl, musicnameinitials, dubbingurl, releasedate";

        public static int AddSongInfoDataAccess(SongInfo songInfo)
        {
            var sql = $"insert into {TABLENAME}({FIELDNAME})" +
                $" values('{songInfo.MusicName}','{songInfo.SingerId}','{songInfo.SingerName}','{songInfo.LanguageType}','{songInfo.Category}','{songInfo.RecordNumber}','{songInfo.MVUrl}','{songInfo.MusicNameInitials}','{songInfo.DubbingUrl}','{songInfo.ReleaseDate}')";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static int DeleteSongInfoDataAccess(SongInfo songInfo)
        {
            var sql = $"delete from {TABLENAME} where id = {songInfo.Id}";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static DataTable GetAllSongInfoDataAccess()
        {
            var sql = $"select id, {FIELDNAME} from {TABLENAME}";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static DataTable GetSongInfoBySingerIdDataAccess(string singerId)
        {
            var sql = $"select id, {FIELDNAME} from {TABLENAME} where singerid = '{singerId}'";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static int UpdateSongeInfoDataAccess(SongInfo songInfo)
        {
            var sql = $"update {TABLENAME} set musicname = '{songInfo.MusicName}', singerid = '{songInfo.SingerId}', singername = '{songInfo.SingerName}', languagetype = '{songInfo.LanguageType}',category = '{songInfo.Category}',recordnumber = '{songInfo.RecordNumber}',mvurl = '{songInfo.MVUrl}',musicnameinitials = '{songInfo.MusicNameInitials}',dubbingurl = '{songInfo.DubbingUrl}',releasedate = '{songInfo.ReleaseDate}' where id = {songInfo.Id}";
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

        public static DataTable GetSingerFilterSourceByInitialDataAccess(string nationality, string sex, string initial)
        {
            var sql = $"select id, singername, singerphotourl from singerinfo where singernationality = '{nationality}' and singersex = '{sex}' and singerinitials like '{initial}%'";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static DataTable GetSingerFilterSourceByRankDataAccess(string nationality, string sex)
        {
            var sql = $"select id, singername, singerphotourl from singerinfo where singernationality = '{nationality}' and singersex = '{sex}' order by singerclicknum DESC";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }
        
        public static DataTable GetSingerFilterSourceAllNationalityByInitialDataAccess(string initial)
        {
            var sql = $"select id, singername, singerphotourl from singerinfo where singerinitials like '{initial}%'";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static DataTable GetSingerFilterSourceAllNationalityByRankDataAccess()
        {
            var sql = $"select id, singername, singerphotourl from singerinfo order by singerclicknum DESC";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }
        
        public static SingerInfo GetSingerInfoByIdDataAccess(string id)
        {
            var sql = $"select id,singername, singerenglishname,singerothername,singerinitials,singernationality,singerphotourl,singerclicknum,singersex,singerintroduce from singerinfo where id = '{id}'";
            DataTable result = SqlServerHelper.GetDataFromKtvdb(sql);
            DataRow row = result.Rows[0];
            SingerInfo resultReturn = new SingerInfo();
            resultReturn.Id = int.Parse(row["id"].ToString());
            resultReturn.SingerName = row["singername"].ToString();
            resultReturn.SingerEnglishName = row["singerenglishname"].ToString();
            resultReturn.SingerOtherName = row["singerothername"].ToString();
            resultReturn.SingerInitials = row["singerinitials"].ToString();
            resultReturn.SingerNationality = row["singernationality"].ToString();
            resultReturn.SingerPhotoUrl = row["singerphotourl"].ToString();
            resultReturn.SingerClickNum = row["singerclicknum"].ToString();
            resultReturn.SingerSex = row["singersex"].ToString();
            resultReturn.SingerIntroduce = row["singerintroduce"].ToString();
            return resultReturn;
        }
    }
}