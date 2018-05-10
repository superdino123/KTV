using KtvMusic.Helpers;
using KtvMusic.SingerInfoService;
using KtvMusic.SongInfoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtvMusic.ViewModels
{
    public class ViewModelBase : NotifyPropertyHelper
    {
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
