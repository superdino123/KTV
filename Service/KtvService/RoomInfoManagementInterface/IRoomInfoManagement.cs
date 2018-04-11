﻿using DataModel;
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
    public interface IRoomInfoManagement
    {
        [OperationContract]
        DataTable GetAllRoomInfo();
        [OperationContract]
        int InsertRoomInfo(RoomInfo roomInfo);
        [OperationContract]
        int RemoveRoomInfo(RoomInfo roomInfo);
        [OperationContract]
        int UpdateRoomInfo(RoomInfo roomInfo);
        [OperationContract]
        DataTable GetActionSource(string groupCode);
    }
}