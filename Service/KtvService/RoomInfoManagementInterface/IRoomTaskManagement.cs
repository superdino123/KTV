using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RoomInfoManagementInterface
{
    [ServiceContract]
    public interface IRoomTaskManagement
    {
        [OperationContract]
        DataTable GetAllRoomTask();

        [OperationContract]
        int UpdateRoomInfo(RoomTask roomTask);

        [OperationContract]
        int GetAccentRoomConsume(RoomTask roomTask);


        [OperationContract]
        DataTable GetUserInfoById(string id);

        [OperationContract]
        int UpdateUserInfo(CustomerInfo customerInfo);

        [OperationContract]
        int InsertUserInfo(CustomerInfo customerInfo);
        
        [OperationContract]
        int HasExistUser(string customerIdCard);

        [OperationContract]
        string GetCustomerIdByIdCard(string customerIdCard);

        [OperationContract]
        int AddConsumeLog(RoomTask roomTask);

        [OperationContract]
        DataTable GetRoomPriceSource();

        [OperationContract]
        DataTable GetConsumeLog();
    }
}
