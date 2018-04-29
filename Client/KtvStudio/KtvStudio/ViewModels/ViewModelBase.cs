using KtvStudio.Helpers;
using KtvStudio.RoomInfoService;
using KtvStudio.RoomTaskService;
using KtvStudio.SingerInfoService;
using KtvStudio.SongInfoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtvStudio.ViewModels
{
    public class ViewModelBase:NotifyPropertyHelper
    {
        #region RoomInfoManagementServiceCaller

        private RoomInfoManagementClient roomInfoManagementClient = new RoomInfoManagementClient();

        public RoomInfoManagementClient RoomInfoManagementServiceCaller
        {
            get
            {
                return roomInfoManagementClient;
            }
        }

        #endregion

        #region RoomTaskManagementServiceCaller

        private RoomTaskManagementClient roomtaskManagementClient = new RoomTaskManagementClient();

        public RoomTaskManagementClient RoomTaskManagementServiceCaller
        {
            get
            {
                return roomtaskManagementClient;
            }
        }

        #endregion

        #region SingerInfoManagementServiceCaller

        private SingerInfoManagementClient singerInfoManagementClient = new SingerInfoManagementClient();

        public SingerInfoManagementClient SingerInfoManagementServiceCaller
        {
            get
            {
                return singerInfoManagementClient;
            }
        }

        #endregion

        #region SongInfoManagementServiceCaller

        private SongInfoManagementClient songInfoManagementClient = new SongInfoManagementClient();

        public SongInfoManagementClient SongInfoManagementServiceCaller
        {
            get
            {
                return songInfoManagementClient;
            }
        }

        #endregion
    }
}
