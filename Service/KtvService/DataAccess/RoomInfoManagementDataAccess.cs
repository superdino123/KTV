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
            //插入房间的同时插入任务表
            var tasksql = $"insert into taskinfo(roomid, roomstate) values('{roomInfo.RoomId}','0')";
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
    }
}
