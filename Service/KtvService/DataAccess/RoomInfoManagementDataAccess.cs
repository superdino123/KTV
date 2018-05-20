using DataHelper;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RoomInfoManagementDataAccess
    {
        public static string TABLENAME = "roominfo";
        public static string FIELDNAME = "roomid, roomtype, roomsize, imageurl, microphonenumber, airconditionernumber, poweramplifiernumber, soundnumber, effectornumber, songdesknumber, lcdtvnumber, roomremark";
        
        public static DataTable GetAllRoomInfoDataAccess()
        {
            string sql = $"select {FIELDNAME} from {TABLENAME}";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static int InSertRoomInfoDataAccess(RoomInfo roomInfo)
        {
            DateTime date = new DateTime(1900, 1, 1);
            //插入房间的同时插入任务表
            var tasksql = $"insert into taskinfo(roomid, roomstate, starttime, endtime) values('{roomInfo.RoomId}','0','{date}','{date}')";
            SqlServerHelper.ExecuteNonQuery(CommandType.Text, tasksql, 30, null);
            var sql = $"insert into {TABLENAME}({FIELDNAME}) values('{roomInfo.RoomId}','{roomInfo.RoomType}','{roomInfo.RoomSize}','{roomInfo.ImageUrl}','{roomInfo.MicroPhoneNumber}','{roomInfo.AirConditionerNumber}','{roomInfo.PowerAmplifierNumber}','{roomInfo.SoundNumber}','{roomInfo.EffectorNumber}','{roomInfo.SongDeskNumber}','{roomInfo.LCDTVNumber}','{roomInfo.RoomRemark}')";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static int UpdateRoomInfoDataAccess(RoomInfo roomInfo)
        {
            var sql = $"update {TABLENAME} set roomtype = '{roomInfo.RoomType}',roomSize = '{roomInfo.RoomSize}',imageurl = '{roomInfo.ImageUrl}',microphonenumber = '{roomInfo.MicroPhoneNumber}', airconditionernumber = '{roomInfo.AirConditionerNumber}', poweramplifiernumber = '{roomInfo.PowerAmplifierNumber}', soundnumber = '{roomInfo.SoundNumber}', effectornumber = '{roomInfo.EffectorNumber}', songdesknumber = '{roomInfo.SongDeskNumber}', lcdtvnumber = '{roomInfo.LCDTVNumber}', roomremark = '{roomInfo.RoomRemark}' where roomid = '{roomInfo.RoomId}'";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }
        
        public static int RemoveRoomInfoDataAccess(string roomId)
        {
            var tasksql = $"delete from taskinfo where roomid = '{roomId}'";
            SqlServerHelper.ExecuteNonQuery(CommandType.Text, tasksql, 30, null);
            var sql = $"delete from {TABLENAME} where roomid = '{roomId}'";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static DataTable GetActionSourceDataAccess(string groupCode)
        {
            var sql = $"select actioncode, actionname from actioninfo where groupcode = {groupCode}";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static int AddRoomTaskRemarkDataAccess(string roomId, string remark, string name)
        {
            string getRemarkSql = $"select roomremark from {TABLENAME} where roomid = {roomId}";
            DataTable resultRemark = SqlServerHelper.GetDataFromKtvdb(getRemarkSql);
            string remarkHas = resultRemark.Rows[0]["roomremark"].ToString();
            string remarkAdd = remarkHas + $"[{name}]{remark}";
            var sql = $"update {TABLENAME} set roomremark = '{remarkAdd}' where roomid = '{roomId}'";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        #region User
        
        public static string STAFFTABLENAME = "staffcount";
        public static string STAFFFIELDNAME = "userrecord, username, userpassword, adress";

        public static int LoginDataAccess(StaffInfo staffInfo)
        {
            string hasUserSql = $"select salt from {STAFFTABLENAME} where userrecord = '{staffInfo.UserRecord}'";
            DataTable result = SqlServerHelper.GetDataFromKtvdb(hasUserSql);
            if (result.Rows.Count == 0) { 
                return 0;
            }else{
                string getSalt = result.Rows[0][0].ToString();
                string psd = MD5Encoding(staffInfo.UserPassword + getSalt);
                string sql = $"select count(*) from {STAFFTABLENAME} where userrecord = '{staffInfo.UserRecord}' and " +
                    $" userpassword in ('{staffInfo.UserPassword}', '{psd}') ";
                return int.Parse(SqlServerHelper.GetDataFromKtvdb(sql).Rows[0][0].ToString());
            }
        }

        public static int UpdatePasswordDataAccess(StaffInfo staffInfo)
        {
            string salt = Guid.NewGuid().ToString();
            string psd = MD5Encoding(staffInfo.UserPassword + salt);

            var sql = $"update {STAFFTABLENAME} set userpassword = '{psd}', salt = '{salt}' where userrecord = '{staffInfo.UserRecord}'";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static StaffInfo GetStaffInfoByRecordDataAccess(string userRecord)
        {
            var sql = $"select username from {STAFFTABLENAME} where userrecord = '{userRecord}'";
            string usernName = SqlServerHelper.GetDataFromKtvdb(sql).Rows[0][0].ToString();
            return new StaffInfo() { UserName = usernName, };
        }

        /// <summary>  
        /// MD5 加密字符串  
        /// </summary>  
        /// <param name="rawPass">源字符串</param>  
        /// <returns>加密后字符串</returns>  
        public static string MD5Encoding(string rawPass)
        {
            // 创建MD5类的默认实例：MD5CryptoServiceProvider  
            MD5 md5 = MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(rawPass);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                // 以十六进制格式格式化  
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

        #endregion
    }
}