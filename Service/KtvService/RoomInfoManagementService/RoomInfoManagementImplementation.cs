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
    public class RoomInfoManagementImplementation : IRoomInfoManagement
    {
        public DataTable GetAllRoomInfo()
        {
            return RoomInfoManagementDataAccess.GetAllRoomInfoDataAccess();
        }

        public int InsertRoomInfo(RoomInfo roomInfo)
        {
            return RoomInfoManagementDataAccess.InSertRoomInfoDataAccess(roomInfo);
        }

        public int RemoveRoomInfo(String roomId)
        {
            return RoomInfoManagementDataAccess.RemoveRoomInfoDataAccess(roomId);
        }

        public int UpdateRoomInfo(RoomInfo roomInfo)
        {
            return RoomInfoManagementDataAccess.UpdateRoomInfoDataAccess(roomInfo);
        }

        public DataTable GetActionSource(string groupCode)
        {
            return RoomInfoManagementDataAccess.GetActionSourceDataAccess(groupCode);
        }
    }
}
