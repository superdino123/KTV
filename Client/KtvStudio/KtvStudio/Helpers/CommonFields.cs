using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtvStudio.Helpers
{
    public class CommonFields
    {
        public const string ROOMINFO = "ROOMINFO";
        public const string ROOMTASK = "ROOMTASK";
        public const string CUSTOMERINFO = "CUSTOMERINFO";
    }

    public class ROOMTASK
    {
        public const string ID = "ID";
        public const string ROOMID = "roomid";
        public const string ROOMSTATE = "roomstate";
        public const string ROOMCONSUME = "roomconsume";
        public const string STARTTIME = "starttime";
        public const string ENDTIME = "endtime";
        public const string CUSTOMERID = "customerid";
    }

    public class ROOMINFO
    {
        public const string ID = "ID";
        public const string ROOMID = "roomid";
        public const string ROOMTYPE = "roomtype";
        public const string ROOMSIZE = "roomsize";
        public const string IMAGEURL = "imageurl";
        public const string MICROPHONENUMBER = "microphonenumber";
    }

    public class CUSTOMERINFO
    {
        public const string CUSTOMERID = "customerid";
        public const string CUSTOMERNAME = "customername";
        public const string CUSTOMERSEX = "customersex";
        public const string CUSTOMERTEL = "customertel";
        public const string CUSTOMERAGE = "customerage";
        public const string CUSTOMERIDCARD = "customeridcard";
    }
}
