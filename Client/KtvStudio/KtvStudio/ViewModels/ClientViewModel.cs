using Helpers;
using Helpers.Commands;
using Helpers.FtpHelper;
using KtvStudio.Helpers;
using KtvStudio.Helpers.QiniuService;
using KtvStudio.RoomInfoService;
using KtvStudio.RoomTaskService;
using KtvStudio.SingerInfoService;
using KtvStudio.SongInfoService;
using KtvStudio.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace KtvStudio.ViewModels
{
    public class ClientViewModel : ViewModelBase
    {
        #region Common

        #region NotifyProperty

        #region SIMPLECLOCK (INotifyPropertyChanged Property)

        public readonly SimpleClock SimpleClock = new SimpleClock(1);

        public string DateTimeNow
        {
            get
            {
                return SimpleClock.Clock.ToString(CultureInfo.InstalledUICulture.DateTimeFormat.FullDateTimePattern);
            }
        }

        #endregion SIMPLECLOCK

        #region 下拉源

        #region 房间种类下拉源

        private DataTable _RoomTypeSource = new DataTable();

        public DataTable RoomTypeSource
        {
            get { return _RoomTypeSource; }
            set
            {
                if (_RoomTypeSource != null && _RoomTypeSource.Equals(value)) return;
                _RoomTypeSource = value;
                RaisePropertyChanged("RoomTypeSource");
            }
        }

        #endregion

        #region 房间种类下拉源

        private DataTable _LanguageTypeSource = new DataTable();

        public DataTable LanguageTypeSource
        {
            get { return _LanguageTypeSource; }
            set
            {
                if (_LanguageTypeSource != null && _LanguageTypeSource.Equals(value)) return;
                _LanguageTypeSource = value;
                RaisePropertyChanged("LanguageTypeSource");
            }
        }

        #endregion

        #endregion

        public MainWindow mainWindow;
        public RoomTaskEditWnd roomTaskEditWnd;

        #endregion

        #endregion

        #region Constructor

        public ClientViewModel()
        {
            SimpleClock.ClockChanged += delegate { RaisePropertyChanged("DateTimeNow"); };
            RoomInfoSource = GetRoomInfoSource();
            RoomTaskSource = GetRoomTaskSource();
            RoomPriceSource = GetRoomPriceSource();
            RoomTypeSource = RoomInfoManagementServiceCaller.GetActionSource("0001");
            LanguageTypeSource = RoomInfoManagementServiceCaller.GetActionSource("0002");
            SingerInfoSource = SingerInfoManagementServiceCaller.GetAllSingerInfo();
            CategorySource = SongInfoManagementServiceCaller.GetAllCategorySource();
            CategorySourceDict = getCategorySourceDict();
            FirstCategorySource = SongInfoManagementServiceCaller.GetAllCategorySource();
            SecondCategorySource = SongInfoManagementServiceCaller.GetAllCategorySource();
            FirstCategorySourceView.RowFilter = $"fatherid = '0'";
            SongInfoSource = SongInfoManagementServiceCaller.GetAllSongInfo();
        }

        #endregion

        #region SongInfo

        #region NotifyProperty

        #region SongInfoSource

        private DataTable _SongInfoSource = new DataTable();

        public DataTable SongInfoSource
        {
            get { return _SongInfoSource; }
            set
            {
                if (_SongInfoSource != null && _SongInfoSource.Equals(value)) return;
                _SongInfoSource = value;
                RaisePropertyChanged("SongInfoSource");
            }
        }

        #endregion

        #region SongInfoSelectedItem

        private DataRow _SongInfoSelectedItem;

        public DataRow SongInfoSelectedItem
        {
            get { return _SongInfoSelectedItem; }
            set
            {
                if (_SongInfoSelectedItem != null && _SongInfoSelectedItem.Equals(value)) return;
                _SongInfoSelectedItem = value;
                RaisePropertyChanged("SongInfoSelectedItem");
            }
        }

        #endregion

        #region SongInfoEditItem

        private SongInfo _SongInfoEditItem = new SongInfo();

        public SongInfo SongInfoEditItem
        {
            get { return _SongInfoEditItem; }
            set
            {
                if (_SongInfoEditItem != null && _SongInfoEditItem.Equals(value)) return;
                _SongInfoEditItem = value;
                RaisePropertyChanged("SongInfoEditItem");
            }
        }

        #endregion

        #region SongInfoEditVisibility

        private bool _SongInfoEditVisibility = false;

        public bool SongInfoEditVisibility
        {
            get { return _SongInfoEditVisibility; }
            set
            {
                if (_SongInfoEditVisibility.Equals(value)) return;
                _SongInfoEditVisibility = value;
                RaisePropertyChanged("SongInfoEditVisibility");
            }
        }

        #endregion

        #region SongInfoAddOrUpdate(Add-true;Update-false)

        private bool _SongInfoAddOrUpdate;

        public bool SongInfoAddOrUpdate
        {
            get { return _SongInfoAddOrUpdate; }
            set
            {
                if (_SongInfoAddOrUpdate.Equals(value)) return;
                _SongInfoAddOrUpdate = value;
                RaisePropertyChanged("SongInfoAddOrUpdate");
            }
        }

        #endregion

        #region CategorySource

        private DataTable _CategorySource = new DataTable();

        public DataTable CategorySource
        {
            get { return _CategorySource; }
            set
            {
                if (_CategorySource != null && _CategorySource.Equals(value)) return;
                _CategorySource = value;
                RaisePropertyChanged("CategorySource");
            }
        }

        public Dictionary<string, string> CategorySourceDict = new Dictionary<string, string>();

        #endregion

        #region FirstCategorySource

        private DataTable _FirstCategorySource = new DataTable();

        public DataTable FirstCategorySource
        {
            get { return _FirstCategorySource; }
            set
            {
                if (_FirstCategorySource != null && _FirstCategorySource.Equals(value)) return;
                _FirstCategorySource = value;
                RaisePropertyChanged("FirstCategorySource");
                RaisePropertyChanged("FirstCategorySourceView");
            }
        }

        public DataView FirstCategorySourceView => FirstCategorySource?.DefaultView;

        #endregion

        #region SecondCategorySource

        private DataTable _SecondCategorySource = new DataTable();

        public DataTable SecondCategorySource
        {
            get { return _SecondCategorySource; }
            set
            {
                if (_SecondCategorySource != null && _SecondCategorySource.Equals(value)) return;
                _SecondCategorySource = value;
                RaisePropertyChanged("SecondCategorySource");
                RaisePropertyChanged("SecondCategorySourceView");
            }
        }

        public DataView SecondCategorySourceView => SecondCategorySource?.DefaultView;

        #endregion

        #region SingerFilterSource

        private DataTable _SingerFilterSource = new DataTable();

        public DataTable SingerFilterSource
        {
            get { return _SingerFilterSource; }
            set
            {
                if (_SingerFilterSource != null && _SingerFilterSource.Equals(value)) return;
                _SingerFilterSource = value;
                RaisePropertyChanged("SingerFilterSource");
            }
        }

        #endregion

        #region SingerSelectedInfoVisibility

        private bool _SingerSelectedInfoVisibility = true;

        public bool SingerSelectedInfoVisibility
        {
            get { return _SingerSelectedInfoVisibility; }
            set
            {
                if (_SingerSelectedInfoVisibility.Equals(value)) return;
                _SingerSelectedInfoVisibility = value;
                RaisePropertyChanged("SingerSelectedInfoVisibility");
            }
        }

        #endregion

        #region SingerInfoSelectedSource

        private SongInfoService.SingerInfo _SingerInfoSelectedSource = new SongInfoService.SingerInfo();

        public SongInfoService.SingerInfo SingerInfoSelectedSource
        {
            get { return _SingerInfoSelectedSource; }
            set
            {
                if (_SingerInfoSelectedSource != null && _SingerInfoSelectedSource.Equals(value)) return;
                _SingerInfoSelectedSource = value;
                RaisePropertyChanged("SingerInfoSelectedSource");
            }
        }

        #endregion

        #endregion

        #region Command

        #region GetAllSongInfoCmd

        private RelayCommand _GetAllSongInfoCmd;

        public ICommand GetAllSongInfoCmd
        {
            get { return _GetAllSongInfoCmd ?? (_GetAllSongInfoCmd = new RelayCommand(param => OnGetAllSongInfo(), param => CanGetAllSongInfo())); }
        }

        public void OnGetAllSongInfo()
        {
            SongInfoSource = SongInfoManagementServiceCaller.GetAllSongInfo();
        }

        public bool CanGetAllSongInfo()
        {
            return true;
        }

        #endregion

        #region SongInfoAddCmd

        private RelayCommand _SongInfoAddCmd;

        public ICommand SongInfoAddCmd
        {
            get { return _SongInfoAddCmd ?? (_SongInfoAddCmd = new RelayCommand(param => OnSongInfoAdd(), param => CanSongInfoAdd())); }
        }

        public void OnSongInfoAdd()
        {
            SongInfoEditVisibility = true;
            SongInfoEditItem = new SongInfo();
            SongInfoAddOrUpdate = true;
        }

        public bool CanSongInfoAdd()
        {
            return true;
        }

        #endregion

        #region SongInfoEditCmd

        private RelayCommand _SongInfoEditCmd;

        public ICommand SongInfoEditCmd
        {
            get { return _SongInfoEditCmd ?? (_SongInfoEditCmd = new RelayCommand(param => OnSongInfoEdit(), param => CanSongInfoEdit())); }
        }

        public void OnSongInfoEdit()
        {
            if (SongInfoSelectedItem == null || !SongInfoSelectedItem.Table.Columns.Contains(MUSICINFO.MUSICNAME))
                return;
            SongInfoEditVisibility = true;

            SongInfoEditItem.Category = SongInfoSelectedItem[MUSICINFO.CATEGORY].ToString();//Category，SingerId需要在MusicName之前赋值
            SongInfoEditItem.Id = int.Parse(SongInfoSelectedItem[MUSICINFO.ID].ToString());
            SongInfoEditItem.SingerId = int.Parse(SongInfoSelectedItem[MUSICINFO.SINGERID].ToString());
            SongInfoEditItem.SingerName = SongInfoSelectedItem[MUSICINFO.SINGERNAME].ToString();
            SongInfoEditItem.LanguageType = SongInfoSelectedItem[MUSICINFO.LANGUAGETYPE].ToString();
            SongInfoEditItem.RecordNumber = SongInfoSelectedItem[MUSICINFO.RECORDNUMBER].ToString();
            SongInfoEditItem.MVUrl = SongInfoSelectedItem[MUSICINFO.MVURL].ToString();
            SongInfoEditItem.MusicNameInitials = SongInfoSelectedItem[MUSICINFO.MUSICNAMEINITIALS].ToString();
            SongInfoEditItem.DubbingUrl = SongInfoSelectedItem[MUSICINFO.DUBBINGURL].ToString();
            if (string.IsNullOrEmpty(SongInfoSelectedItem[MUSICINFO.RELEASEDATE].ToString()))
                SongInfoEditItem.ReleaseDate = null;
            else
                SongInfoEditItem.ReleaseDate = DateTime.Parse(SongInfoSelectedItem[MUSICINFO.RELEASEDATE].ToString());

            SongInfoEditItem.MusicName = SongInfoSelectedItem[MUSICINFO.MUSICNAME].ToString();
            SongInfoAddOrUpdate = false;
        }

        public bool CanSongInfoEdit()
        {
            return true;
        }

        #endregion

        #region SongInfoCancelCmd

        private RelayCommand _SongInfoCancelCmd;

        public ICommand SongInfoCancelCmd
        {
            get { return _SongInfoCancelCmd ?? (_SongInfoCancelCmd = new RelayCommand(param => OnSongInfoCancel(), param => CanSongInfoCancel())); }
        }

        public void OnSongInfoCancel()
        {
            SongInfoEditVisibility = false;
        }

        public bool CanSongInfoCancel()
        {
            return true;
        }

        #endregion

        #region SongInfoReturnSingerTypeCmd

        private RelayCommand _SongInfoReturnSingerTypeCmd;

        public ICommand SongInfoReturnSingerTypeCmd
        {
            get { return _SongInfoReturnSingerTypeCmd ?? (_SongInfoReturnSingerTypeCmd = new RelayCommand(param => OnSongInfoReturnSingerType(), param => CanSongInfoReturnSingerType())); }
        }

        public void OnSongInfoReturnSingerType()
        {
            SingerSelectedInfoVisibility = true;
        }

        public bool CanSongInfoReturnSingerType()
        {
            return true;
        }

        #endregion

        #region SongInfoSaveCmd

        private RelayCommand _SongInfoSaveCmd;

        public ICommand SongInfoSaveCmd
        {
            get { return _SongInfoSaveCmd ?? (_SongInfoSaveCmd = new RelayCommand(param => OnSongInfoSave(), param => CanSongInfoSave())); }
        }

        public void OnSongInfoSave()
        {
            if (SongInfoEditItem == null)
                return;

            if ((SongInfoAddOrUpdate && SongInfoManagementServiceCaller.AddSongInfo(SongInfoEditItem) != 0) ||
                !SongInfoAddOrUpdate && SongInfoManagementServiceCaller.UpdateSongeInfo(SongInfoEditItem) != 0)
            {
                SongInfoEditVisibility = false;
                SongInfoSource = SongInfoManagementServiceCaller.GetAllSongInfo();
            }
        }

        public bool CanSongInfoSave()
        {
            return true;
        }

        #endregion

        #region SongMVUpLoadCmd

        private RelayCommand _SongMVUpLoadCmd;

        public ICommand SongMVUpLoadCmd
        {
            get { return _SongMVUpLoadCmd ?? (_SongMVUpLoadCmd = new RelayCommand(param => OnSongMVUpLoad(), param => CanSongMVUpLoad())); }
        }

        public void OnSongMVUpLoad()
        {
            //选择图片
            OpenFileDialog op = new OpenFileDialog
            {
                InitialDirectory = null,//默认的打开路径
                RestoreDirectory = true,
                Filter = "视频文件(*.mpg)|*.mpg|所有文件(*.*)|*.*"
            };
            op.ShowDialog();

            if (string.IsNullOrEmpty(op.FileName))
                return;

            string saveKey = SongInfoEditItem.MusicName + "-" + SongInfoEditItem.SingerName + ".mpg";
            //上传图片到七牛云
            QiniuService.UploadImage(saveKey, op.FileName);
            SongInfoEditItem.MVUrl = QiniuService.QINIU_URL + "/" + saveKey;
        }

        public bool CanSongMVUpLoad()
        {
            return true;
        }

        #endregion

        #endregion

        #region Method

        private Dictionary<string, string> getCategorySourceDict() {
            Dictionary<string, string> result = new Dictionary<string, string>();
            for (int i = 0; i < CategorySource.Rows.Count; i++)
            {
                result.Add(CategorySource.Rows[i]["categoryname"].ToString(), CategorySource.Rows[i]["id"].ToString());
            }
            return result;
        }

        #endregion

        #endregion

        #region SingerInfo

        #region NotifyProperty

        #region SingerInfoSource

        private DataTable _SingerInfoSource = new DataTable();

        public DataTable SingerInfoSource
        {
            get { return _SingerInfoSource; }
            set
            {
                if (_SingerInfoSource != null && _SingerInfoSource.Equals(value)) return;
                _SingerInfoSource = value;
                RaisePropertyChanged("SingerInfoSource");
            }
        }

        #endregion

        #region SingerInfoSelectedItem

        private DataRow _SingerInfoSelectedItem;

        public DataRow SingerInfoSelectedItem
        {
            get { return _SingerInfoSelectedItem; }
            set
            {
                if (_SingerInfoSelectedItem != null && _SingerInfoSelectedItem.Equals(value)) return;
                _SingerInfoSelectedItem = value;
                RaisePropertyChanged("SingerInfoSelectedItem");
            }
        }

        #endregion

        #region SingerInfoEditItem

        private SingerInfoService.SingerInfo _SingerInfoEditItem = new SingerInfoService.SingerInfo();

        public SingerInfoService.SingerInfo SingerInfoEditItem
        {
            get { return _SingerInfoEditItem; }
            set
            {
                if (_SingerInfoEditItem != null && _SingerInfoEditItem.Equals(value)) return;
                _SingerInfoEditItem = value;
                RaisePropertyChanged("SingerInfoEditItem");
            }
        }

        #endregion

        #region SingerInfoEditVisibility

        private bool _SingerInfoEditVisibility = false;

        public bool SingerInfoEditVisibility
        {
            get { return _SingerInfoEditVisibility; }
            set
            {
                if (_SingerInfoEditVisibility.Equals(value)) return;
                _SingerInfoEditVisibility = value;
                RaisePropertyChanged("SingerInfoEditVisibility");
            }
        }

        #endregion

        #region SingerInfoAddOrUpdate(Add-true;Update-false)

        private bool _SingerInfoAddOrUpdate;

        public bool SingerInfoAddOrUpdate
        {
            get { return _SingerInfoAddOrUpdate; }
            set
            {
                if (_SingerInfoAddOrUpdate.Equals(value)) return;
                _SingerInfoAddOrUpdate = value;
                RaisePropertyChanged("SingerInfoAddOrUpdate");
            }
        }

        #endregion

        #endregion

        #region Command

        #region GetAllSingerInfoCmd

        private RelayCommand _GetAllSingerInfoCmd;

        public ICommand GetAllSingerInfoCmd
        {
            get { return _GetAllSingerInfoCmd ?? (_GetAllSingerInfoCmd = new RelayCommand(param => OnGetAllSingerInfo(), param => CanGetAllSingerInfo())); }
        }

        public void OnGetAllSingerInfo()
        {
            SingerInfoSource = SingerInfoManagementServiceCaller.GetAllSingerInfo();
        }

        public bool CanGetAllSingerInfo()
        {
            return true;
        }

        #endregion

        #region SingerInfoAddCmd

        private RelayCommand _SingerInfoAddCmd;

        public ICommand SingerInfoAddCmd
        {
            get { return _SingerInfoAddCmd ?? (_SingerInfoAddCmd = new RelayCommand(param => OnSingerInfoAdd(), param => CanSingerInfoAdd())); }
        }

        public void OnSingerInfoAdd()
        {
            SingerInfoEditVisibility = true;
            SingerInfoEditItem = new SingerInfoService.SingerInfo();
            SingerInfoAddOrUpdate = true;
        }

        public bool CanSingerInfoAdd()
        {
            return true;
        }

        #endregion

        #region SingerInfoEditCmd

        private RelayCommand _SingerInfoEditCmd;

        public ICommand SingerInfoEditCmd
        {
            get { return _SingerInfoEditCmd ?? (_SingerInfoEditCmd = new RelayCommand(param => OnSingerInfoEdit(), param => CanSingerInfoEdit())); }
        }

        public void OnSingerInfoEdit()
        {
            if (SingerInfoSelectedItem == null || !SingerInfoSelectedItem.Table.Columns.Contains(SINGERINFO.SINGERNAME))
                return;
            SingerInfoEditVisibility = true;
            SingerInfoEditItem.Id = int.Parse(SingerInfoSelectedItem[SINGERINFO.ID].ToString());
            SingerInfoEditItem.SingerName = SingerInfoSelectedItem[SINGERINFO.SINGERNAME].ToString();
            SingerInfoEditItem.SingerEnglishName = SingerInfoSelectedItem[SINGERINFO.SINGERENGLISHNAME].ToString();
            SingerInfoEditItem.SingerOtherName = SingerInfoSelectedItem[SINGERINFO.SINGEROTHERNAME].ToString();
            SingerInfoEditItem.SingerInitials = SingerInfoSelectedItem[SINGERINFO.SINGERINITIALS].ToString();
            SingerInfoEditItem.SingerNationality = SingerInfoSelectedItem[SINGERINFO.SINGERNATIONALITY].ToString();
            SingerInfoEditItem.SingerPhotoUrl = SingerInfoSelectedItem[SINGERINFO.SINGERPHOTOURL].ToString();
            SingerInfoEditItem.SingerClickNum = SingerInfoSelectedItem[SINGERINFO.SINGERCLICKNUM].ToString();
            SingerInfoEditItem.SingerSex = SingerInfoSelectedItem[SINGERINFO.SINGERSEX].ToString();
            SingerInfoEditItem.SingerIntroduce = SingerInfoSelectedItem[SINGERINFO.SINGERINTRODUCE].ToString();
            SingerInfoAddOrUpdate = false;
        }

        public bool CanSingerInfoEdit()
        {
            return true;
        }

        #endregion

        #region SingerInfoCancelCmd

        private RelayCommand _SingerInfoCancelCmd;

        public ICommand SingerInfoCancelCmd
        {
            get { return _SingerInfoCancelCmd ?? (_SingerInfoCancelCmd = new RelayCommand(param => OnSingerInfoCancel(), param => CanSingerInfoCancel())); }
        }

        public void OnSingerInfoCancel()
        {
            SingerInfoEditVisibility = false;
        }

        public bool CanSingerInfoCancel()
        {
            return true;
        }

        #endregion

        #region SingerInfoSaveCmd

        private RelayCommand _SingerInfoSaveCmd;

        public ICommand SingerInfoSaveCmd
        {
            get { return _SingerInfoSaveCmd ?? (_SingerInfoSaveCmd = new RelayCommand(param => OnSingerInfoSave(), param => CanSingerInfoSave())); }
        }

        public void OnSingerInfoSave()
        {
            if (SingerInfoEditItem == null)
                return;

            if ((SingerInfoAddOrUpdate && SingerInfoManagementServiceCaller.AddSingerInfo(SingerInfoEditItem) != 0) ||
                !SingerInfoAddOrUpdate && SingerInfoManagementServiceCaller.UpdateSingerInfo(SingerInfoEditItem) != 0)
            {
                SingerInfoEditVisibility = false;
                SingerInfoSource = SingerInfoManagementServiceCaller.GetAllSingerInfo();
            }
        }

        public bool CanSingerInfoSave()
        {
            return true;
        }

        #endregion

        #region SingerImageUpLoadCmd

        private RelayCommand _SingerImageUpLoadCmd;

        public ICommand SingerImageUpLoadCmd
        {
            get { return _SingerImageUpLoadCmd ?? (_SingerImageUpLoadCmd = new RelayCommand(param => OnSingerImageUpLoad(), param => CanSingerImageUpLoad())); }
        }

        public void OnSingerImageUpLoad()
        {
            //选择图片
            OpenFileDialog op = new OpenFileDialog
            {
                InitialDirectory = null,//默认的打开路径
                RestoreDirectory = true,
                Filter = "图像文件(*.gif;*.jpg;*.jpeg;*.png;*.psd)|*.gif;*.jpg;*.jpeg;*.png;*.psd|所有文件(*.*)|*.*"
            };
            op.ShowDialog();

            if (string.IsNullOrEmpty(op.FileName))
                return;

            string saveKey = SingerInfoEditItem.SingerName + "-" + SingerInfoEditItem.SingerSex + "-" + SingerInfoEditItem.SingerNationality;
            //上传图片到七牛云
            QiniuService.UploadImage(saveKey, op.FileName);
            SingerInfoEditItem.SingerPhotoUrl = QiniuService.QINIU_URL + "/" + saveKey;
        }

        public bool CanSingerImageUpLoad()
        {
            return true;
        }

        #endregion


        #region Test

        private RelayCommand _Test;

        public ICommand Test
        {
            get { return _Test ?? (_Test = new RelayCommand(param => OnTest(), param => CanTest())); }
        }

        public void OnTest()
        {
            //填写首字母
            for (int i = 0; i < SingerInfoSource.Rows.Count; i++)
            {
                string name = SingerInfoSource.Rows[i][SINGERINFO.SINGERNAME].ToString();
                string initials = GetInitialsHelper.GetChineseSpell(name);
                SingerInfoSource.Rows[i][SINGERINFO.SINGERINITIALS] = initials;
            }
            SingerInfoService.SingerInfo info = new SingerInfoService.SingerInfo();
            for (int i = 0; i < SingerInfoSource.Rows.Count; i++)
            {
                info.Id = int.Parse(SingerInfoSource.Rows[i][SINGERINFO.ID].ToString());
                info.SingerName = SingerInfoSource.Rows[i][SINGERINFO.SINGERNAME].ToString();
                info.SingerEnglishName = SingerInfoSource.Rows[i][SINGERINFO.SINGERENGLISHNAME].ToString();
                info.SingerOtherName = SingerInfoSource.Rows[i][SINGERINFO.SINGEROTHERNAME].ToString();
                info.SingerInitials = SingerInfoSource.Rows[i][SINGERINFO.SINGERINITIALS].ToString();
                info.SingerNationality = SingerInfoSource.Rows[i][SINGERINFO.SINGERNATIONALITY].ToString();
                info.SingerPhotoUrl = SingerInfoSource.Rows[i][SINGERINFO.SINGERPHOTOURL].ToString();
                info.SingerClickNum = SingerInfoSource.Rows[i][SINGERINFO.SINGERCLICKNUM].ToString();
                info.SingerSex = SingerInfoSource.Rows[i][SINGERINFO.SINGERSEX].ToString();
                info.SingerIntroduce = SingerInfoSource.Rows[i][SINGERINFO.SINGERINTRODUCE].ToString();
                SingerInfoManagementServiceCaller.UpdateSingerInfo(info);
            }
        }

        public bool CanTest()
        {
            return true;
        }

        #endregion



        #endregion

        #endregion

        #region RoomTask

        #region NotifyProperty

        #region RoomTaskSource

        private DataTable _RoomTaskSource = new DataTable();

        public DataTable RoomTaskSource
        {
            get { return _RoomTaskSource; }
            set
            {
                if (_RoomTaskSource != null && _RoomTaskSource.Equals(value)) return;
                _RoomTaskSource = value;
                RaisePropertyChanged("RoomTaskSource");
                GetRoomTaskShowSourceFromRoomTaskSource();
            }
        }

        #endregion
        
        #region RoomTaskSelectedItem

        private DataRow _RoomTaskSelectedItem;

        public DataRow RoomTaskSelectedItem
        {
            get { return _RoomTaskSelectedItem; }
            set
            {
                if (_RoomTaskSelectedItem != null && _RoomTaskSelectedItem.Equals(value)) return;
                _RoomTaskSelectedItem = value;
                RaisePropertyChanged("RoomTaskSelectedItem");
            }
        }

        #endregion

        #region RoomTaskEditItem

        private RoomTask _RoomTaskEditItem = new RoomTask();

        public RoomTask RoomTaskEditItem
        {
            get { return _RoomTaskEditItem; }
            set
            {
                if (_RoomTaskEditItem != null && _RoomTaskEditItem.Equals(value)) return;
                _RoomTaskEditItem = value;
                RaisePropertyChanged("RoomTaskEditItem");
            }
        }

        #endregion

        #region UserItem

        private CustomerInfo _userInfo = new CustomerInfo();

        public CustomerInfo UserInfo
        {
            get { return _userInfo; }
            set
            {
                if (_userInfo != null && _userInfo.Equals(value)) return;
                _userInfo = value;
                RaisePropertyChanged("UserInfo");
            }
        }

        #endregion

        #region UserInfoWhenRenewNoVisibility

        private bool _UserInfoWhenRenewNoVisibility = false;

        public bool UserInfoWhenRenewNoVisibility
        {
            get { return _UserInfoWhenRenewNoVisibility; }
            set
            {
                if (_UserInfoWhenRenewNoVisibility.Equals(value)) return;
                _UserInfoWhenRenewNoVisibility = value;
                RaisePropertyChanged("UserInfoWhenRenewNoVisibility");
            }
        }

        #endregion

        #region RoomPriceSource

        private DataTable _RoomPriceSource = new DataTable();

        public DataTable RoomPriceSource
        {
            get { return _RoomPriceSource; }
            set
            {
                if (_RoomPriceSource != null && _RoomPriceSource.Equals(value)) return;
                _RoomPriceSource = value;
                RaisePropertyChanged("RoomPriceSource");
            }
        }

        #endregion
        
        #region RoomTaskSelectedItemStateIsUsing

        private bool _RoomTaskSelectedItemStateIsUsing = false;

        public bool RoomTaskSelectedItemStateIsUsing
        {
            get { return _RoomTaskSelectedItemStateIsUsing; }
            set
            {
                if (_RoomTaskSelectedItemStateIsUsing.Equals(value)) return;
                _RoomTaskSelectedItemStateIsUsing = value;
                RaisePropertyChanged("RoomTaskSelectedItemStateIsUsing");
            }
        }

        #endregion
        
        #endregion

        #region Commands

        #region RoomTaskAddCmd

        private RelayCommand _RoomTaskAddCmd;

        public ICommand RoomTaskAddCmd
        {
            get { return _RoomTaskAddCmd ?? (_RoomTaskAddCmd = new RelayCommand(param => OnRoomTaskAdd(), param => CanRoomTaskAdd())); }
        }

        public void OnRoomTaskAdd()
        {
            SelectedItemToEditItem();
            RoomTaskEditItem.StartTime = DateTime.Now;
            RoomTaskEditItem.EndTime = DateTime.Now;
            if (RoomTaskEditItem.CustomerId != null && RoomTaskEditItem.CustomerId != 0)
            {
                //CustomerId不为空，读取用户具体信息
                DataTable user = RoomTaskManagementServiceCaller.GetUserInfoById(RoomTaskEditItem.CustomerId.ToString());
                if (user.Rows.Count != 1)
                    LogHelper.LogError($"用户ID:{RoomTaskEditItem.CustomerId},读取数据库用户信息数应为1，实际为{user.Rows.Count}");
                else
                {
                    UserInfo.CustomerId = user.Rows[0][CUSTOMERINFO.CUSTOMERID].ToString();
                    UserInfo.CustomerName = user.Rows[0][CUSTOMERINFO.CUSTOMERNAME].ToString();
                    UserInfo.CustomerSex = user.Rows[0][CUSTOMERINFO.CUSTOMERSEX].ToString();
                    UserInfo.CustomerTel = user.Rows[0][CUSTOMERINFO.CUSTOMERTEL].ToString();
                    UserInfo.CustomerAge = user.Rows[0][CUSTOMERINFO.CUSTOMERAGE].ToString();
                    UserInfo.CustomerIdCard = user.Rows[0][CUSTOMERINFO.CUSTOMERIDCARD].ToString();
                }
            }
            else
            {
                UserInfo = new CustomerInfo();
            }
            if (UserInfoWhenRenewNoVisibility)
                UserInfoWhenRenewNoVisibility = false;
            if (roomTaskEditWnd != null)
                roomTaskEditWnd.Close();
            roomTaskEditWnd = new RoomTaskEditWnd(mainWindow.ClientViewModel);
            roomTaskEditWnd.Show();

        }

        public bool CanRoomTaskAdd()
        {
            return true;
        }

        #endregion

        #region RoomTaskSaveCmd

        private RelayCommand _RoomTaskSaveCmd;

        public ICommand RoomTaskSaveCmd
        {
            get { return _RoomTaskSaveCmd ?? (_RoomTaskSaveCmd = new RelayCommand(param => OnRoomTaskSave(), param => CanRoomTaskSave())); }
        }

        public void OnRoomTaskSave()
        {
            if (!UserInfoWhenRenewNoVisibility) //订购
            {
                //修改使用状态
                RoomTaskEditItem.RoomState = "1";
                //insert用户表——如果电话相同不插入，update数据
                if (RoomTaskManagementServiceCaller.HasExistUser(UserInfo.CustomerIdCard) == 0)
                    RoomTaskManagementServiceCaller.InsertUserInfo(UserInfo);
                if (UserInfo.CustomerId == null)
                {
                    UserInfo.CustomerId = RoomTaskManagementServiceCaller.GetCustomerIdByIdCard(UserInfo.CustomerIdCard);
                    RoomTaskEditItem.CustomerId = int.Parse(UserInfo.CustomerId);
                }
            }
            //update任务表
            RoomTaskManagementServiceCaller.UpdateRoomInfo(RoomTaskEditItem);

            roomTaskEditWnd.Close();
            //更新列表显示
            RoomTaskSource = GetRoomTaskSource();
        }

        public bool CanRoomTaskSave()
        {
            return true;
        }

        #endregion

        #region RoomTaskEditWndCloseCmd

        private RelayCommand _RoomTaskEditWndCloseCmd;

        public ICommand RoomTaskEditWndCloseCmd
        {
            get { return _RoomTaskEditWndCloseCmd ?? (_RoomTaskEditWndCloseCmd = new RelayCommand(param => OnRoomTaskEditWndClose(), param => CanRoomTaskEditWndClose())); }
        }

        public void OnRoomTaskEditWndClose()
        {
            roomTaskEditWnd.Close();
        }

        public bool CanRoomTaskEditWndClose()
        {
            return true;
        }

        #endregion

        #region RoomTaskBalanceCmd

        private RelayCommand _RoomTaskBalanceCmd;

        public ICommand RoomTaskBalanceCmd
        {
            get { return _RoomTaskBalanceCmd ?? (_RoomTaskBalanceCmd = new RelayCommand(param => OnRoomTaskBalance(), param => CanRoomTaskBalance())); }
        }

        public void OnRoomTaskBalance()
        {
            if (MessageBox.Show($"此次消费{RoomTaskEditItem.RoomConsume}元，是否提交本次订单?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                //添加日志流水
                SelectedItemToEditItem();
                RoomTaskManagementServiceCaller.AddConsumeLog(RoomTaskEditItem);
                //清空当前包间任务状态
                RoomTaskEditItem.RoomState = "0";
                RoomTaskEditItem.RoomConsume = null;
                RoomTaskEditItem.StartTime = null;
                RoomTaskEditItem.EndTime = null;
                RoomTaskEditItem.CustomerId = null;
                RoomTaskManagementServiceCaller.UpdateRoomInfo(RoomTaskEditItem);
                //刷新数据源
                RoomTaskSource = GetRoomTaskSource();
            }
        }

        public bool CanRoomTaskBalance()
        {
            return true;
        }

        #endregion

        #region RoomTaskRenewCmd

        private RelayCommand _RoomTaskRenewCmd;

        public ICommand RoomTaskRenewCmd
        {
            get { return _RoomTaskRenewCmd ?? (_RoomTaskRenewCmd = new RelayCommand(param => OnRoomTaskRenew(), param => CanRoomTaskRenew())); }
        }

        public void OnRoomTaskRenew()
        {
            SelectedItemToEditItem();
            //CustomerId不为空，读取用户具体信息
            DataTable user = RoomTaskManagementServiceCaller.GetUserInfoById(RoomTaskEditItem.CustomerId.ToString());
            if (user.Rows.Count != 1)
                LogHelper.LogError($"用户ID:{RoomTaskEditItem.CustomerId},读取数据库用户信息数应为1，实际为{user.Rows.Count}");
            else
            {
                UserInfo.CustomerId = user.Rows[0][CUSTOMERINFO.CUSTOMERID].ToString();
                UserInfo.CustomerName = user.Rows[0][CUSTOMERINFO.CUSTOMERNAME].ToString();
                UserInfo.CustomerSex = user.Rows[0][CUSTOMERINFO.CUSTOMERSEX].ToString();
                UserInfo.CustomerTel = user.Rows[0][CUSTOMERINFO.CUSTOMERTEL].ToString();
                UserInfo.CustomerAge = user.Rows[0][CUSTOMERINFO.CUSTOMERAGE].ToString();
                UserInfo.CustomerIdCard = user.Rows[0][CUSTOMERINFO.CUSTOMERIDCARD].ToString();
            }
            //隐藏人员信息栏目
            UserInfoWhenRenewNoVisibility = true;
            if (roomTaskEditWnd != null)
                roomTaskEditWnd.Close();
            //打开界面
            roomTaskEditWnd = new RoomTaskEditWnd(mainWindow.ClientViewModel);
            roomTaskEditWnd.Show();
        }

        public bool CanRoomTaskRenew()
        {
            return true;
        }

        #endregion

        #region RoomTaskRefreshCmd

        private RelayCommand _RoomTaskRefreshCmd;

        public ICommand RoomTaskRefreshCmd
        {
            get { return _RoomTaskRefreshCmd ?? (_RoomTaskRefreshCmd = new RelayCommand(param => OnRoomTaskRefresh(), param => CanRoomTaskRefresh())); }
        }

        public void OnRoomTaskRefresh()
        {
            RoomTaskSource = GetRoomTaskSource();
        }

        public bool CanRoomTaskRefresh()
        {
            return true;
        }

        #endregion

        #region RoomTaskRemarkCmd

        private RelayCommand _RoomTaskRemarkCmd;

        public ICommand RoomTaskRemarkCmd
        {
            get { return _RoomTaskRemarkCmd ?? (_RoomTaskRemarkCmd = new RelayCommand(param => OnRoomTaskRemark(), param => CanRoomTaskRemark())); }
        }

        public void OnRoomTaskRemark()
        {
            if (MessageBox.Show($"此次消费{RoomTaskEditItem.RoomConsume}元，是否提交本次订单?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
            }
        }

        public bool CanRoomTaskRemark()
        {
            return true;
        }

        #endregion

        #endregion

        #region Method

        /// <summary>
        /// 获取房间任务数据源
        /// </summary>
        /// <returns></returns>
        private DataTable GetRoomTaskSource()
        {
            return RoomTaskManagementServiceCaller.GetAllRoomTask();
        }

        private void GetRoomTaskShowSourceFromRoomTaskSource() {
            RoomInfoSource.PrimaryKey = new DataColumn[] { RoomInfoSource.Columns["roomid"] };
            RoomTaskSource.PrimaryKey = new DataColumn[] { RoomTaskSource.Columns["roomid"] };
            RoomTaskSource.Merge(RoomInfoSource, false);
        }

        /// <summary>
        /// 获取当前预计消费
        /// </summary>
        /// <param name="roomTaskEditItem"></param>
        /// <returns></returns>
        public void getRoomConsume(RoomTask roomTaskEditItem) {
            if (roomTaskEditItem.EndTime == null || roomTaskEditItem.StartTime == null)
                return;
            if (roomTaskEditItem.EndTime.Value <= roomTaskEditItem.StartTime.Value)
                RoomTaskEditItem.RoomConsume = "0";
            else
                RoomTaskEditItem.RoomConsume = RoomTaskManagementServiceCaller.GetAccentRoomConsume(roomTaskEditItem).ToString();
        }

        /// <summary>
        /// 将RoomTaskSelectedItem赋值给RoomTaskEditItem
        /// </summary>
        public void SelectedItemToEditItem()
        {
            if (RoomTaskSelectedItem == null || !RoomTaskSelectedItem.Table.Columns.Contains(ROOMINFO.ROOMID))
                return;
            RoomTaskEditItem.RoomId = RoomTaskSelectedItem[ROOMTASK.ROOMID].ToString();
            RoomTaskEditItem.RoomState = RoomTaskSelectedItem[ROOMTASK.ROOMSTATE].ToString();
            RoomTaskEditItem.RoomConsume = RoomTaskSelectedItem[ROOMTASK.ROOMCONSUME].ToString();
            if (string.IsNullOrEmpty(RoomTaskSelectedItem[ROOMTASK.STARTTIME].ToString()))
                RoomTaskEditItem.StartTime = null;
            else
                RoomTaskEditItem.StartTime = DateTime.Parse(RoomTaskSelectedItem[ROOMTASK.STARTTIME].ToString());
            if (string.IsNullOrEmpty(RoomTaskSelectedItem[ROOMTASK.ENDTIME].ToString()))
                RoomTaskEditItem.EndTime = null;
            else
                RoomTaskEditItem.EndTime = DateTime.Parse(RoomTaskSelectedItem[ROOMTASK.ENDTIME].ToString());
            if (string.IsNullOrEmpty(RoomTaskSelectedItem[ROOMTASK.CUSTOMERID].ToString()))
                RoomTaskEditItem.CustomerId = null;
            else
            {
                RoomTaskEditItem.CustomerId = int.Parse(RoomTaskSelectedItem[ROOMTASK.CUSTOMERID].ToString());
            }
        }

        private DataTable GetRoomPriceSource() {
            DataTable result = RoomTaskManagementServiceCaller.GetRoomPriceSource();
            DataTable resultShow = new DataTable();
            resultShow.Columns.Add("roomtype");
            resultShow.Columns.Add("firsttime");
            resultShow.Columns.Add("secondtime");
            resultShow.Columns.Add("thirdtime");
            resultShow.Columns.Add("fourthtime");

            DataRow row = resultShow.NewRow();
            row["roomtype"] = string.Empty;
            row["firsttime"] = "0:00 - 6:00";
            row["secondtime"] = "6:00 - 15:00";
            row["thirdtime"] = "15:00 - 18:00";
            row["fourthtime"] = "18:00 - 24:00";
            resultShow.Rows.Add(row);
            for (int i = 0; i < 6; i++)
            {
                DataRow tempRow = resultShow.NewRow();
                tempRow["roomtype"] = GetRoomTypeNameFromCode(i.ToString());
                foreach (DataRow item in result.Rows)
                {
                    if (item["roomtype"].ToString().Equals(i.ToString()) &&
                        item["starttime"].ToString().Equals("0"))
                        tempRow["firsttime"] = item["roomprice"].ToString();
                    else if (item["roomtype"].ToString().Equals(i.ToString()) &&
                             item["starttime"].ToString().Equals("6"))
                        tempRow["secondtime"] = item["roomprice"].ToString();
                    else if (item["roomtype"].ToString().Equals(i.ToString()) &&
                             item["starttime"].ToString().Equals("15"))
                        tempRow["thirdtime"] = item["roomprice"].ToString();
                    else if (item["roomtype"].ToString().Equals(i.ToString()) &&
                             item["starttime"].ToString().Equals("18"))
                        tempRow["fourthtime"] = item["roomprice"].ToString();
                }
                resultShow.Rows.Add(tempRow);
            }
            return resultShow;
        }

        #endregion

        #endregion

        #region RoomInfo

        #region NotifyProperty

        #region RoomInfoSource

        private DataTable _RoomInfoSource = new DataTable();

        public DataTable RoomInfoSource
        {
            get { return _RoomInfoSource; }
            set
            {
                if (_RoomInfoSource != null && _RoomInfoSource.Equals(value)) return;
                _RoomInfoSource = value;
                RaisePropertyChanged("RoomInfoSource");
            }
        }

        #endregion

        #region RoomInfoEditVisibility

        private bool _RoomInfoEditVisibility = false;

        public bool RoomInfoEditVisibility
        {
            get { return _RoomInfoEditVisibility; }
            set
            {
                if (_RoomInfoEditVisibility.Equals(value)) return;
                _RoomInfoEditVisibility = value;
                RaisePropertyChanged("RoomInfoEditVisibility");
            }
        }

        #endregion

        #region RoomInfoSelectedItem

        private DataRow _RoomInfoSelectedItem;

        public DataRow RoomInfoSelectedItem
        {
            get { return _RoomInfoSelectedItem; }
            set
            {
                if (_RoomInfoSelectedItem != null && _RoomInfoSelectedItem.Equals(value)) return;
                _RoomInfoSelectedItem = value;
                RaisePropertyChanged("RoomInfoSelectedItem");
            }
        }

        #endregion

        #region RoomInfoEditItem

        private RoomInfo _RoomInfoEditItem = new RoomInfo();

        public RoomInfo RoomInfoEditItem
        {
            get { return _RoomInfoEditItem; }
            set
            {
                if (_RoomInfoEditItem != null && _RoomInfoEditItem.Equals(value)) return;
                _RoomInfoEditItem = value;
                RaisePropertyChanged("RoomInfoEditItem");
            }
        }

        #endregion

        #region RoomInfoAddOrUpdate(Add-true;Update-false)

        private bool _RoomInfoAddOrUpdate;

        public bool RoomInfoAddOrUpdate
        {
            get { return _RoomInfoAddOrUpdate; }
            set
            {
                if (_RoomInfoAddOrUpdate.Equals(value)) return;
                _RoomInfoAddOrUpdate = value;
                RaisePropertyChanged("RoomInfoAddOrUpdate");
            }
        }

        #endregion

        #endregion

        #region Commands

        #region RoomInfoAddCmd

        private RelayCommand _RoomInfoAddCmd;

        public ICommand RoomInfoAddCmd
        {
            get { return _RoomInfoAddCmd ?? (_RoomInfoAddCmd = new RelayCommand(param => OnRoomInfoAdd(), param => CanRoomInfoAdd())); }
        }

        public void OnRoomInfoAdd()
        {
            RoomInfoEditVisibility = true;
            RoomInfoEditItem = new RoomInfo();
            RoomInfoAddOrUpdate = true;
        }

        public bool CanRoomInfoAdd()
        {
            return true;
        }

        #endregion

        #region RoomInfoEditCmd

        private RelayCommand _RoomInfoEditCmd;

        public ICommand RoomInfoEditCmd
        {
            get { return _RoomInfoEditCmd ?? (_RoomInfoEditCmd = new RelayCommand(param => OnRoomInfoEdit(), param => CanRoomInfoEdit())); }
        }

        public void OnRoomInfoEdit()
        {
            if (RoomInfoSelectedItem == null || !RoomInfoSelectedItem.Table.Columns.Contains(ROOMINFO.ROOMID))
                return;
            RoomInfoEditVisibility = true;
            GetRoomInfoEditItemFromSelectedItem();
            RoomInfoAddOrUpdate = false;
        }

        public bool CanRoomInfoEdit()
        {
            return true;
        }

        #endregion

        #region RoomInfoCancelCmd

        private RelayCommand _RoomInfoCancelCmd;

        public ICommand RoomInfoCancelCmd
        {
            get { return _RoomInfoCancelCmd ?? (_RoomInfoCancelCmd = new RelayCommand(param => OnRoomInfoCancel(), param => CanRoomInfoCancel())); }
        }

        public void OnRoomInfoCancel()
        {
            RoomInfoEditVisibility = false;
        }

        public bool CanRoomInfoCancel()
        {
            return true;
        }

        #endregion

        #region RoomInfoDeleteCmd

        private RelayCommand _RoomInfoDeleteCmd;

        public ICommand RoomInfoDeleteCmd
        {
            get { return _RoomInfoDeleteCmd ?? (_RoomInfoDeleteCmd = new RelayCommand(param => OnRoomInfoDelete(), param => CanRoomInfoDelete())); }
        }

        public void OnRoomInfoDelete()
        {
            if (RoomInfoSelectedItem == null || !RoomInfoSelectedItem.Table.Columns.Contains(ROOMINFO.ROOMID))
                return;

            string roomId = RoomInfoSelectedItem[ROOMINFO.ROOMID].ToString();
            if (MessageBox.Show($"是否删除房间{roomId}?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                RoomInfoManagementServiceCaller.RemoveRoomInfo(roomId);
                RoomInfoSource = GetRoomInfoSource();
                RoomTaskSource = GetRoomTaskSource();
            }
        }

        public bool CanRoomInfoDelete()
        {
            return true;
        }

        #endregion

        #region RoomInfoSaveCmd

        private RelayCommand _RoomInfoSaveCmd;

        public ICommand RoomInfoSaveCmd
        {
            get { return _RoomInfoSaveCmd ?? (_RoomInfoSaveCmd = new RelayCommand(param => OnRoomInfoSave(), param => CanRoomInfoSave())); }
        }

        public void OnRoomInfoSave()
        {
            if (RoomInfoEditItem == null || !RoomInfoRobustnessVerification())
                return;

            //还原房间类型
            RoomInfoEditItem.RoomType = GetRoomTypeCodeFromName(RoomInfoEditItem.RoomType);

            if ((RoomInfoAddOrUpdate && RoomInfoManagementServiceCaller.InsertRoomInfo(RoomInfoEditItem) != 0) ||
                !RoomInfoAddOrUpdate && RoomInfoManagementServiceCaller.UpdateRoomInfo(RoomInfoEditItem) != 0)
            {
                RoomInfoEditVisibility = false;
                RoomInfoSource = GetRoomInfoSource();
                RoomTaskSource = GetRoomTaskSource();
            }

        }

        public bool CanRoomInfoSave()
        {
            return true;
        }

        #endregion

        #region RoomImageUpLoadCmd

        private RelayCommand _RoomImageUpLoadCmd;

        public ICommand RoomImageUpLoadCmd
        {
            get { return _RoomImageUpLoadCmd ?? (_RoomImageUpLoadCmd = new RelayCommand(param => OnRoomImageUpLoad(), param => CanRoomImageUpLoad())); }
        }

        public void OnRoomImageUpLoad()
        {
            //选择图片
            OpenFileDialog op = new OpenFileDialog
            {
                InitialDirectory = null,//默认的打开路径
                RestoreDirectory = true,
                Filter = "图像文件(*.gif;*.jpg;*.jpeg;*.png;*.psd)|*.gif;*.jpg;*.jpeg;*.png;*.psd|所有文件(*.*)|*.*"
            };
            op.ShowDialog();

            if (string.IsNullOrEmpty(op.FileName))
                return;

            string saveKey = (Guid.NewGuid().ToString() + op.FileName.Substring(op.FileName.LastIndexOf('.'))).Replace("-", "");
            //上传图片到七牛云
            QiniuService.UploadImage(saveKey, op.FileName);
            RoomInfoEditItem.ImageUrl = QiniuService.QINIU_URL + "/" + saveKey;
        }

        public bool CanRoomImageUpLoad()
        {
            return true;
        }

        #endregion

        #region RoomInfoRefreshCmd

        private RelayCommand _RoomInfoRefreshCmd;

        public ICommand RoomInfoRefreshCmd
        {
            get { return _RoomInfoRefreshCmd ?? (_RoomInfoRefreshCmd = new RelayCommand(param => OnRoomInfoRefresh(), param => CanRoomInfoRefresh())); }
        }

        public void OnRoomInfoRefresh()
        {
            RoomInfoSource = GetRoomInfoSource();
        }

        public bool CanRoomInfoRefresh()
        {
            return true;
        }

        #endregion
        
        #endregion

        #region Methods

        /// <summary>
        /// 获取房间信息数据源
        /// </summary>
        /// <returns></returns>
        private DataTable GetRoomInfoSource()
        {
            return TableDataTranslate(RoomInfoManagementServiceCaller.GetAllRoomInfo());
        }

        /// <summary>
        /// 转换部分列的值
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private DataTable TableDataTranslate(DataTable dataTable)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                //房间类型
                dataTable.Rows[i]["ROOMTYPE"] = GetRoomTypeNameFromCode(dataTable.Rows[i]["ROOMTYPE"].ToString());
            }

            return dataTable;
        }

        /// <summary>
        /// 根据RoomInfoSelctedItem获取RoomInfoEditItem
        /// </summary>
        private void GetRoomInfoEditItemFromSelectedItem(){
            if (RoomInfoSelectedItem == null || !RoomInfoSelectedItem.Table.Columns.Contains(ROOMINFO.ROOMID))
                return;
            RoomInfoEditItem.RoomId = RoomInfoSelectedItem[ROOMINFO.ROOMID].ToString();
            RoomInfoEditItem.RoomType = RoomInfoSelectedItem[ROOMINFO.ROOMTYPE].ToString();
            RoomInfoEditItem.RoomSize = RoomInfoSelectedItem[ROOMINFO.ROOMSIZE].ToString();
            RoomInfoEditItem.MicroPhoneNumber = RoomInfoSelectedItem[ROOMINFO.MICROPHONENUMBER].ToString();
            RoomInfoEditItem.AirConditionerNumber = RoomInfoSelectedItem[ROOMINFO.AIRCONDITIONERNUMBER].ToString();
            RoomInfoEditItem.PowerAmplifierNumber = RoomInfoSelectedItem[ROOMINFO.POWERAMPLIFIERNUMBER].ToString();
            RoomInfoEditItem.SoundNumber = RoomInfoSelectedItem[ROOMINFO.SOUNDNUMBER].ToString();
            RoomInfoEditItem.EffectorNumber = RoomInfoSelectedItem[ROOMINFO.EFFECTORNUMBER].ToString();
            RoomInfoEditItem.SongDeskNumber = RoomInfoSelectedItem[ROOMINFO.SONGDESKNUMBER].ToString();
            RoomInfoEditItem.LCDTVNumber = RoomInfoSelectedItem[ROOMINFO.LCDTVNUMBER].ToString();
            RoomInfoEditItem.RoomRemark = RoomInfoSelectedItem[ROOMINFO.ROOMREMARK].ToString();
            RoomInfoEditItem.ImageUrl = RoomInfoSelectedItem[ROOMINFO.IMAGEURL].ToString();
        }

        public string GetRoomTypeCodeFromName(string roomTypeName) {
            string result = roomTypeName;
            if (roomTypeName.Equals("小型包间"))
                result = "0";
            if (roomTypeName.Equals("中型包间"))
                result = "1";
            if (roomTypeName.Equals("大型包间"))
                result = "2";
            if (roomTypeName.Equals("情侣包间"))
                result = "3";
            if (roomTypeName.Equals("豪华包间"))
                result = "4";
            if (roomTypeName.Equals("商务包间"))
                result = "5";
            return result;
        }

        public string GetRoomTypeNameFromCode(string roomTypeCode)
        {
            string result = roomTypeCode;
            if (roomTypeCode.Equals("0"))
                result = "小型包间";
            else if (roomTypeCode.Equals("1"))
                result = "中型包间";
            else if (roomTypeCode.Equals("2"))
                result = "大型包间";
            else if (roomTypeCode.Equals("3"))
                result = "情侣包间";
            else if (roomTypeCode.Equals("4"))
                result = "豪华包间";
            else if (roomTypeCode.Equals("5"))
                result = "商务包间";
            return result;

        }

        /// <summary>
        /// 房间信息保存操作的鲁棒性验证
        /// </summary>
        /// <returns></returns>
        private bool RoomInfoRobustnessVerification() {
            StringBuilder message = new StringBuilder();
            if (string.IsNullOrEmpty(RoomInfoEditItem.RoomId) || string.IsNullOrWhiteSpace(RoomInfoEditItem.RoomId))
                message.Append(" 房间号 ");
            if (string.IsNullOrEmpty(RoomInfoEditItem.RoomType) || string.IsNullOrWhiteSpace(RoomInfoEditItem.RoomType))
                message.Append(" 房间类型 ");
            if (string.IsNullOrEmpty(RoomInfoEditItem.RoomSize) || string.IsNullOrWhiteSpace(RoomInfoEditItem.RoomSize))
                message.Append(" 房间面积 ");
            if (string.IsNullOrEmpty(RoomInfoEditItem.MicroPhoneNumber) || string.IsNullOrWhiteSpace(RoomInfoEditItem.MicroPhoneNumber))
                message.Append(" 麦克数量 ");
            if (string.IsNullOrEmpty(RoomInfoEditItem.AirConditionerNumber) || string.IsNullOrWhiteSpace(RoomInfoEditItem.AirConditionerNumber))
                message.Append(" 空调数量 ");
            if (string.IsNullOrEmpty(RoomInfoEditItem.PowerAmplifierNumber) || string.IsNullOrWhiteSpace(RoomInfoEditItem.PowerAmplifierNumber))
                message.Append(" 功放机数量 ");
            if (string.IsNullOrEmpty(RoomInfoEditItem.SoundNumber) || string.IsNullOrWhiteSpace(RoomInfoEditItem.SoundNumber))
                message.Append(" 音响数量 ");
            if (string.IsNullOrEmpty(RoomInfoEditItem.EffectorNumber) || string.IsNullOrWhiteSpace(RoomInfoEditItem.EffectorNumber))
                message.Append(" 效果器数量 ");
            if (string.IsNullOrEmpty(RoomInfoEditItem.SongDeskNumber) || string.IsNullOrWhiteSpace(RoomInfoEditItem.SongDeskNumber))
                message.Append(" 点歌台数量 ");
            if (string.IsNullOrEmpty(RoomInfoEditItem.LCDTVNumber) || string.IsNullOrWhiteSpace(RoomInfoEditItem.LCDTVNumber))
                message.Append(" 液晶电视数量 ");
            if (string.IsNullOrEmpty(message.ToString()))
                return true;
            else
                MessageBox.Show($"{message.ToString()}不得为空！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        #endregion

        #endregion
    }
}