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
        public const string SINGERINFO = "SINGERINFO";
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
        public const string AIRCONDITIONERNUMBER = "airconditionernumber";
        public const string POWERAMPLIFIERNUMBER = "poweramplifiernumber";
        public const string SOUNDNUMBER = "soundnumber";
        public const string EFFECTORNUMBER = "effectornumber";
        public const string SONGDESKNUMBER = "songdesknumber";
        public const string LCDTVNUMBER = "lcdtvnumber";
        public const string ROOMREMARK = "roomremark";

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

    public class SINGERINFO {
        public const string ID = "id";
        public const string SINGERNAME = "singername";
        public const string SINGERENGLISHNAME = "singerenglishname";
        public const string SINGEROTHERNAME = "singerothername";
        public const string SINGERINITIALS = "singerinitials";
        public const string SINGERNATIONALITY = "singernationality";
        public const string SINGERPHOTOURL = "singerphotourl";
        public const string SINGERCLICKNUM = "singerclicknum";
        public const string SINGERSEX = "singersex";
        public const string SINGERINTRODUCE = "singerintroduce";
    }

    public class MUSICINFO {
        public const string ID = "id";
        public const string MUSICNAME = "musicname";
        public const string SINGERID = "singerid";
        public const string SINGERNAME = "singername";
        public const string LANGUAGETYPE = "languagetype";
        public const string CATEGORY = "category";
        public const string RECORDNUMBER = "recordnumber";
        public const string MVURL = "mvurl";
        public const string MUSICNAMEINITIALS = "musicnameinitials";
        public const string SINGRAIL = "singrail";
        public const string RELEASEDATE = "releasedate";
    }
}