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
        private static string getAllRoomInfoSql = "select roomid,roomtype,roomsize,imageurl,microphonenumber from roominfo";

        public static DataTable GetAllRoomInfoDataAccess()
        {
            return SqlServerHelper.GetDataFromKtvdb(getAllRoomInfoSql);
        }

        public static int InSertRoomInfoDataAccess(RoomInfo roomInfo)
        {
            var sql = $"insert into roominfo(roomid,roomtype,roomsize,imageurl,microphonenumber) values('{roomInfo.RoomId}','{roomInfo.RoomType}','{roomInfo.RoomSize}','{roomInfo.ImageUrl}','{roomInfo.MicroPhoneNumber}')";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static int UpdateRoomInfoDataAccess(RoomInfo roomInfo)
        {
            var sql = $"update roominfo set roomtype = '{roomInfo.RoomType}',roomSize = '{roomInfo.RoomSize}',imageurl = '{roomInfo.ImageUrl}',microphonenumber = '{roomInfo.MicroPhoneNumber}' where roomid = '{roomInfo.RoomId}'";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static DataTable GetActionSourceDataAccess(string groupCode)
        {
            var sql = $"select actioncode, actionname from actioninfo where groupcode = {groupCode}";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }
    }
}
