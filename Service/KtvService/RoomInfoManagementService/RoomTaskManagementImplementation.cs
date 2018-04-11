using DataAccess;
using DataModel;
using RoomInfoManagementInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomInfoManagementService
{
    public class RoomTaskManagementImplementation : IRoomTaskManagement
    {
        DataTable IRoomTaskManagement.GetAllRoomTask()
        {
            return RoomTaskManagementDataAccess.GetAllRoomTaskDataAccess();
        }

        int IRoomTaskManagement.UpdateRoomInfo(RoomTask roomTask)
        {
            return RoomTaskManagementDataAccess.UpdateRoomInfoDataAccess(roomTask);
        }

        public DataTable GetUserInfoById(string id)
        {
            return RoomTaskManagementDataAccess.GetUserInfoByIdDataAccess(id);
        }

        public int UpdateUserInfo(CustomerInfo customerInfo)
        {
            return RoomTaskManagementDataAccess.UpdateUserInfoDataAccess(customerInfo);
        }

        public int InsertUserInfo(CustomerInfo customerInfo)
        {
            return RoomTaskManagementDataAccess.InsertUserInfoDataAccess(customerInfo);
        }

        public int HasExistUser(string customerIdCard)
        {
            return RoomTaskManagementDataAccess.HasExistUserDataAccess(customerIdCard);
        }

        public int GetAccentRoomConsume(RoomTask roomTask)
        {
            return RoomTaskManagementDataAccess.GetAccentRoomConsumeDataAccess(roomTask);
        }

        public int AddConsumeLog(RoomTask roomTask)
        {
            return RoomTaskManagementDataAccess.AddConsumeLogDataAccess(roomTask);
        }
    }
}
