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
    public class RoomTaskManagementDataAccess
    {
        public static string TABLENAME = "taskinfo";
        public static string FIELDNAME = "roomid, roomstate, roomconsume, starttime, endtime, customerid";

        public static DataTable GetAllRoomTaskDataAccess()
        {
            var sql = $"select {FIELDNAME} from {TABLENAME}";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static int UpdateRoomInfoDataAccess(RoomTask roomTask) {
            var sql = $"update {TABLENAME} set roomstate = '{roomTask.RoomState}', roomconsume = '{roomTask.RoomConsume}'," +
                $" starttime = '{roomTask.StartTime}', endtime = '{roomTask.EndTime}', customerid = '{roomTask.CustomerId}'" +
                $" where roomid = '{roomTask.RoomId}'";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }
        
        public static DataTable GetRoomPriceSourceDataAccess()
        {
            var sql = $"select roomtype, roomprice, starttime, endtime from roompriceinfo";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        #region User

        public static string USERTABLENAME = "customerinfo";
        public static string USERFIELDNAME = "customername, customersex, customertel";
        
        /// <summary>
        /// 获取某一用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetUserInfoByIdDataAccess(string id)
        {
            var sql = $"select customerid, {USERFIELDNAME} from {USERTABLENAME} where customerid = {id}";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        /// <summary>
        /// 更新某一用户信息
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        public static int UpdateUserInfoDataAccess(CustomerInfo customerInfo)
        {
            var sql = $"update {USERTABLENAME} set customername = '{customerInfo.CustomerName}', customersex = '{customerInfo.CustomerSex}'," +
                $" customertel = '{customerInfo.CustomerTel}' where customerid = '{customerInfo.CustomerId}'";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        /// <summary>
        /// 插入某一用户信息
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        public static int InsertUserInfoDataAccess(CustomerInfo customerInfo)
        {
            var sql = $"insert into {USERTABLENAME}({USERFIELDNAME}) values ('{customerInfo.CustomerName}','{customerInfo.CustomerSex}','{customerInfo.CustomerTel}')";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        /// <summary>
        /// 判断是否有此用户
        /// </summary>
        /// <param name="customerTel"></param>
        /// <returns></returns>
        public static int HasExistUserDataAccess(string customerTel) {
            var sql = $"select count(customertel) from {USERTABLENAME} where customertel = '{customerTel}'";
            return int.Parse(SqlServerHelper.GetDataFromKtvdb(sql).Rows[0][0].ToString());
        }

        /// <summary>
        /// 根据用Tel获取用户Id
        /// </summary>
        /// <param name="customerIdCard"></param>
        /// <returns></returns>
        public static string GetCustomerIdByTelDataAccess(string customerTel)
        {
            var sql = $"select customerid from {USERTABLENAME} where customertel = '{customerTel}'";
            return SqlServerHelper.GetDataFromKtvdb(sql).Rows[0][0].ToString();
        }

        #endregion

        /// <summary>
        /// 获取当前时间房间单价
        /// </summary>
        /// <param name="roomTask"></param>
        /// <returns></returns>
        public static int GetAccentRoomConsumeDataAccess(RoomTask roomTask)
        {
            if (roomTask.StartTime == null || roomTask.EndTime == null)
                return 0;
            //获取单价
            var sql = $"select roomprice from roompriceinfo where roomtype in (select roomtype from roominfo where roomid = '{roomTask.RoomId}' and starttime <= {roomTask.StartTime.Hour} and endtime > {roomTask.StartTime.Hour})";
            int price = int.Parse(SqlServerHelper.GetDataFromKtvdb(sql).Rows[0][0].ToString());
            int hours = (int)((roomTask.EndTime - roomTask.StartTime).TotalHours + 0.1);
            return hours * price;
        }

        #region ConsumeLog

        public static string CONSUMELOGTABLENAME = "consumelog";
        public static string CONSUMELOGFIELDNAME = "roomid, customerid, roomconsume, starttime, endtime";

        /// <summary>
        /// 添加流水日志
        /// </summary>
        /// <param name="roomTask"></param>
        /// <returns></returns>
        public static int AddConsumeLogDataAccess(RoomTask roomTask)
        {
            var sql = $"insert into {CONSUMELOGTABLENAME}({CONSUMELOGFIELDNAME}) values('{roomTask.RoomId}', '{roomTask.CustomerId}', '{roomTask.RoomConsume}', '{roomTask.StartTime}', '{roomTask.EndTime}')";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        public static DataTable GetConsumeLogDataAccess()
        {
            var sql = $"select {CONSUMELOGTABLENAME}.roomid, customerid, roomconsume, starttime, endtime, roomtype from {CONSUMELOGTABLENAME},roominfo where {CONSUMELOGTABLENAME}.roomid = roominfo.roomid";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }
        
        #endregion
    }
}
