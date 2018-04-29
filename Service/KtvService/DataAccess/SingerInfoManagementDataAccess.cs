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
    public class SingerInfoManagementDataAccess
    {
        public static int AddSingerInfoDataAccess(SingerInfo singerInfo)
        {
            var sql = $"insert into singerinfo(singername, singerenglishname,singerothername,singerinitials,singernationality,singerphotourl,singerclicknum,singersex,singerintroduce)" +
                $" values('{singerInfo.SingerName}','{singerInfo.SingerEnglishName}','{singerInfo.SingerOtherName}','{singerInfo.SingerInitials}','{singerInfo.SingerNationality}','{singerInfo.SingerPhotoUrl}','{singerInfo.SingerClickNum}','{singerInfo.SingerSex}','{singerInfo.SingerIntroduce}')";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static int DeleteSingerInfoDataAccess(SingerInfo singerInfo)
        {
            var sql = $"delete from singerinfo where id = {singerInfo.Id}";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static DataTable GetAllSingerInfoDataAccess()
        {
            var sql = $"select id,singername, singerenglishname,singerothername,singerinitials,singernationality,singerphotourl,singerclicknum,singersex,singerintroduce from singerinfo";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static DataTable GetSingerInfoByNameDataAccess(string singerName)
        {
            var sql = $"select id,singername, singerenglishname,singerothername,singerinitials,singernationality,singerphotourl,singerclicknum,singersex,singerintroduce from singerinfo where singername = '{singerName}'";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static int UpdateSingerInfoDataAccess(SingerInfo singerInfo)
        {
            var sql = $"update singerinfo set singername = '{singerInfo.SingerName}', singerenglishname = '{singerInfo.SingerEnglishName}',singerothername = '{singerInfo.SingerOtherName}',singerinitials = '{singerInfo.SingerInitials}',singernationality = '{singerInfo.SingerNationality}',singerphotourl = '{singerInfo.SingerPhotoUrl}',singerclicknum = '{singerInfo.SingerClickNum}',singersex = '{singerInfo.SingerSex}',singerintroduce = '{singerInfo.SingerIntroduce}' where id = {singerInfo.Id}";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }
    }
}