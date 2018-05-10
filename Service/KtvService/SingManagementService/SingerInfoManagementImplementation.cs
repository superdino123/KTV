using DataAccess;
using DataModel;
using SingManagementInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingManagementService
{
    public class SingerInfoManagementImplementation : ISingerInfoManagement
    {
        public int AddSingerInfo(SingerInfo singerInfo)
        {
            return SingerInfoManagementDataAccess.AddSingerInfoDataAccess(singerInfo);
        }

        public int DeleteSingerInfo(string singerId)
        {
            return SingerInfoManagementDataAccess.DeleteSingerInfoDataAccess(singerId);
        }

        public DataTable GetAllSingerInfo()
        {
            return SingerInfoManagementDataAccess.GetAllSingerInfoDataAccess();
        }

        public DataTable GetSingerInfoByName(string singerName)
        {
            return SingerInfoManagementDataAccess.GetSingerInfoByNameDataAccess(singerName);
        }

        public int UpdateSingerInfo(SingerInfo singerInfo)
        {
            return SingerInfoManagementDataAccess.UpdateSingerInfoDataAccess(singerInfo);
        }

        public SingerInfo GetSingerInfoById(string id)
        {
            return SingerInfoManagementDataAccess.GetSingerInfoByIdDataAccess(id);
        }

        public DataTable GetSingerInfoPaging( string nationality, string sex, string initial)
        {
            return SingerInfoManagementDataAccess.GetSingerInfoPagingDataAccess(nationality, sex, initial);
        }

        public DataTable GetNationalityInfoSource()
        {
            return SingerInfoManagementDataAccess.GetNationalityInfoSourceDataAccess();
        }
    }
}
