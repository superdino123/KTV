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
        public static string TABLENAME = "singerinfo";
        public static string FIELDNAME = "singername, singerenglishname,singerothername,singerinitials,singernationality,singerphotourl,singerclicknum,singersex,singerintroduce"; 
        public static string INVERTFIELDNAME = "singername, singerenglishname,singerothername,singerinitials,singernationality,singerphotourl,convert(int, singerclicknum) singerclicknum,singersex,singerintroduce";

        public static int AddSingerInfoDataAccess(SingerInfo singerInfo)
        {
            var sql = $"insert into {TABLENAME}({FIELDNAME})" +
                $" values('{singerInfo.SingerName}','{singerInfo.SingerEnglishName}','{singerInfo.SingerOtherName}','{singerInfo.SingerInitials}','{singerInfo.SingerNationality}','{singerInfo.SingerPhotoUrl}','{singerInfo.SingerClickNum}','{singerInfo.SingerSex}','{singerInfo.SingerIntroduce}')";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static int DeleteSingerInfoDataAccess(string singerId)
        {
            var sql = $"delete from {TABLENAME} where id = {singerId}";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static DataTable GetAllSingerInfoDataAccess()
        {
            var sql = $"select id, {INVERTFIELDNAME} from {TABLENAME}";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static DataTable GetSingerInfoByNameDataAccess(string singerName)
        {
            var sql = $"select id, {FIELDNAME} from {TABLENAME} where singername = '{singerName}'";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static int UpdateSingerInfoDataAccess(SingerInfo singerInfo)
        {
            var sql = $"update {TABLENAME} set singername = '{singerInfo.SingerName}', singerenglishname = '{singerInfo.SingerEnglishName}',singerothername = '{singerInfo.SingerOtherName}',singerinitials = '{singerInfo.SingerInitials}',singernationality = '{singerInfo.SingerNationality}',singerphotourl = '{singerInfo.SingerPhotoUrl}',singerclicknum = '{singerInfo.SingerClickNum}',singersex = '{singerInfo.SingerSex}',singerintroduce = '{singerInfo.SingerIntroduce}' where id = {singerInfo.Id}";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static SingerInfo GetSingerInfoByIdDataAccess(string id)
        {
            var sql = $"select id, {FIELDNAME} from {TABLENAME} where id = '{id}'";
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
        
        public static DataTable GetSingerInfoPagingDataAccess(string nationality, string sex, string initial)
        {
            StringBuilder whereSql = new StringBuilder();
            string OrderBySql = string.Empty;
            if (!string.IsNullOrEmpty(nationality) && !string.IsNullOrEmpty(sex))
                whereSql.Append($" where singernationality = '{nationality}' and singersex = '{sex}' ");
            if (!string.IsNullOrEmpty(initial))
            {
                string joinStr = string.IsNullOrEmpty(whereSql.ToString()) ? " where " : " and ";
                whereSql.Append($" {joinStr} singerinitials like '{initial}%'");
                OrderBySql = $" order by id";
            }
            else
                OrderBySql = $" order by convert(int, singerclicknum) desc ";
            
            string sql = $"select  id, {FIELDNAME},row_number() over( {OrderBySql} ) as rownum from {TABLENAME} {whereSql.ToString()}";
            DataTable result = SqlServerHelper.GetDataFromKtvdb(sql);
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static DataTable GetNationalityInfoSourceDataAccess()
        {
            var sql = $"select actionname from actioninfo where groupcode = '0003'";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }
    }
}