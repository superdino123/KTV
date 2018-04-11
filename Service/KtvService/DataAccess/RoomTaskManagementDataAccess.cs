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
        public static DataTable GetAllRoomTaskDataAccess()
        {
            var sql = "select roomid, roomstate, roomconsume, starttime, endtime, customerid from taskinfo";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        public static int UpdateRoomInfoDataAccess(RoomTask roomTask) {
            var sql = $"update taskinfo set roomstate = '{roomTask.RoomState}', roomconsume = '{roomTask.RoomConsume}'," +
                $" starttime = '{roomTask.StartTime}', endtime = '{roomTask.EndTime}', customerid = '{roomTask.CustomerId}'" +
                $" where roomid = '{roomTask.RoomId}'";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }
        
        #region User

        /// <summary>
        /// 获取某一用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetUserInfoByIdDataAccess(string id)
        {
            var sql = $"select customerid, customername, customersex, customertel, customerage, customeridcard from customerinfo where customerid = {id}";
            return SqlServerHelper.GetDataFromKtvdb(sql);
        }

        /// <summary>
        /// 更新某一用户信息
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        public static int UpdateUserInfoDataAccess(CustomerInfo customerInfo)
        {
            var sql = $"update customerinfo set customername = '{customerInfo.CustomerName}', customersex = '{customerInfo.CustomerSex}'," +
                $" customertel = '{customerInfo.CustomerTel}', customerage = '{customerInfo.CustomerAge}', customeridcard = '{customerInfo.CustomerIdCard}' where customerid = '{customerInfo.CustomerId}'";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        /// <summary>
        /// 插入某一用户信息
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        public static int InsertUserInfoDataAccess(CustomerInfo customerInfo)
        {
            var sql = $"insert into customerinfo(customername,customersex,customertel,customerage,customeridcard) values ('{customerInfo.CustomerName}','{customerInfo.CustomerSex}','{customerInfo.CustomerTel}'," +
                $"'{customerInfo.CustomerAge}','{customerInfo.CustomerIdCard}')";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        /// <summary>
        /// 判断是否有此用户
        /// </summary>
        /// <param name="customerIdCard"></param>
        /// <returns></returns>
        public static int HasExistUserDataAccess(string customerIdCard) {
            var sql = $"select count(customeridcard) from customerinfo where customeridcard = '{customerIdCard}'";
            return int.Parse(SqlServerHelper.GetDataFromKtvdb(sql).Rows[0][0].ToString());
        }

        /// <summary>
        /// 获取当前时间房间单价
        /// </summary>
        /// <param name="roomTask"></param>
        /// <returns></returns>
        public static int GetAccentRoomConsumeDataAccess(RoomTask roomTask) {
            if (roomTask.StartTime == null || roomTask.EndTime == null)
                return 0;
            //获取单价
            var sql = $"select roomprice from roompriceinfo where roomtype in (select roomtype from roominfo where roomid = '{roomTask.RoomId}' and starttime <= {roomTask.StartTime.Value.Hour} and endtime > {roomTask.StartTime.Value.Hour})";
            int price = int.Parse(SqlServerHelper.GetDataFromKtvdb(sql).Rows[0][0].ToString());
            return (int)(roomTask.EndTime.Value - roomTask.StartTime.Value).TotalHours * price;
        }

        /// <summary>
        /// 添加流水日志
        /// </summary>
        /// <param name="roomTask"></param>
        /// <returns></returns>
        public static int AddConsumeLogDataAccess(RoomTask roomTask) {
            var sql = $"insert into consumelog(roomid, customerid, roomconsume, starttime, endtime) values('{roomTask.RoomId}', '{roomTask.CustomerId}', '{roomTask.RoomConsume}', '{roomTask.StartTime}', '{roomTask.EndTime}')";
            return SqlServerHelper.ExecuteNonQuery(CommandType.Text, sql, 30, null);
        }

        #endregion

    }
}
