using Helpers;
using Helpers.Commands;
using Helpers.FtpHelper;
using KtvMusic.SingerInfoService;
using KtvMusic.SongInfoService;
using KtvStudio.Helpers;
using KtvStudio.Helpers.QiniuService;
using KtvStudio.RoomInfoService;
using KtvStudio.RoomTaskService;
using KtvStudio.SingerInfoService;
using KtvStudio.SongInfoService;
using KtvStudio.Views;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;

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
        public SongManageUc songManageUc;
        public SongInfoEditUc songInfoEditUc;
        public LoginWnd loginWnd;

        #endregion

        #endregion

        #region Login

        #region NotifyProperty

        #region CurrentStaffInfo

        private StaffInfo _CurrentStaffInfo = new StaffInfo() { UserAddress = "田林东路店", };

        public StaffInfo CurrentStaffInfo
        {
            get { return _CurrentStaffInfo; }
            set
            {
                if (_CurrentStaffInfo != null && _CurrentStaffInfo.Equals(value)) return;
                _CurrentStaffInfo = value;
                RaisePropertyChanged("CurrentStaffInfo");
            }
        }

        #endregion

        #region LoginState

        private bool _LoginState = false;

        public bool LoginState
        {
            get { return _LoginState; }
            set
            {
                if (_LoginState.Equals(value)) return;
                _LoginState = value;
                RaisePropertyChanged("LoginState");
            }
        }

        #endregion

        #endregion

        #region Command

        #region LoginCmd

        private RelayCommand _LoginCmd;

        public ICommand LoginCmd
        {
            get { return _LoginCmd ?? (_LoginCmd = new RelayCommand(param => OnLogin(), param => CanLogin())); }
        }

        public void OnLogin()
        {
            if (string.IsNullOrEmpty(CurrentStaffInfo.UserName) || string.IsNullOrEmpty(CurrentStaffInfo.UserPassword)) {
                MessageBox.Show("请填写用户名或密码！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (RoomInfoManagementServiceCaller.Login(CurrentStaffInfo) != 0) {
                LoginState = true;
                CurrentStaffInfo.Authority = GetAuthority();
                loginWnd.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public bool CanLogin()
        {
            return true;
        }

        #endregion


        #endregion

        #region Method

        private int GetAuthority() {

            return RoomInfoManagementServiceCaller.GetAuthority(CurrentStaffInfo.UserName);
        }

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
            SingerInfoSource = GetSingerInfoSource();
            SingerInfoShowSource = GetSingerInfoShowSource(SingerInfoNationality, SingerInfoSex, SingerInfoInitial);
            SongSingerInfoShowSource = GetSingerInfoShowSource(SongSingerInfoNationality, SongSingerInfoSex, SongSingerInfoInitial);
            CountryInfoSource = SingerInfoManagementServiceCaller.GetNationalityInfoSource();
            CategorySource = SongInfoManagementServiceCaller.GetAllCategorySource();
            CategorySourceDict = getCategorySourceDict();
            FirstCategorySource = SongInfoManagementServiceCaller.GetAllCategorySource();
            SecondCategorySource = SongInfoManagementServiceCaller.GetAllCategorySource();
            FirstCategorySourceView.RowFilter = $"fatherid = '0'";
            SongInfoSource = GetSongInfoSource();

            LoadSongHotConfigFile();
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
                SongInfoSource.DefaultView.RowFilter = SongInfoFilter;
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

        #region 分页筛选

        #region SongSingerInfoShowSource

        private DataTable _SongSingerInfoShowSource = new DataTable();

        public DataTable SongSingerInfoShowSource
        {
            get { return _SongSingerInfoShowSource; }
            set
            {
                if (_SongSingerInfoShowSource != null && _SongSingerInfoShowSource.Equals(value)) return;
                _SongSingerInfoShowSource = value;
                RaisePropertyChanged("SongSingerInfoShowSource");
                SongSingerInfoShowSelectLastIndex = GetSongSingerInfoShowSelectLastIndex();
                if (songManageUc != null)
                    songManageUc.AddSingerInfo();
            }
        }

        #endregion

        #region SongSingerInfoShowSelectIndex

        private int _SongSingerInfoShowSelectIndex = 1;

        public int SongSingerInfoShowSelectIndex
        {
            get { return _SongSingerInfoShowSelectIndex; }
            set
            {
                if (_SongSingerInfoShowSelectIndex.Equals(value)) return;
                _SongSingerInfoShowSelectIndex = value;
                RaisePropertyChanged("SongSingerInfoShowSelectIndex");
                if (songManageUc != null)
                    songManageUc.AddSingerInfo();
            }
        }

        #endregion

        #region SongSingerInfoShowGoToIndex

        private int _SongSingerInfoShowGoToIndex = 1;

        public int SongSingerInfoShowGoToIndex
        {
            get { return _SongSingerInfoShowGoToIndex; }
            set
            {
                if (_SongSingerInfoShowGoToIndex.Equals(value)) return;
                _SongSingerInfoShowGoToIndex = value;
                RaisePropertyChanged("SongSingerInfoShowGoToIndex");
            }
        }

        #endregion

        #region SongSingerInfoShowSelectLastIndex

        private int _SongSingerInfoShowSelectLastIndex = 1;

        public int SongSingerInfoShowSelectLastIndex
        {
            get { return _SongSingerInfoShowSelectLastIndex; }
            set
            {
                if (_SongSingerInfoShowSelectLastIndex.Equals(value)) return;
                _SongSingerInfoShowSelectLastIndex = value;
                RaisePropertyChanged("SongSingerInfoShowSelectLastIndex");
            }
        }

        #endregion

        #region SongSingerInfoShowPageSize

        private int _SongSingerInfoShowPageSize = 20;

        public int SongSingerInfoShowPageSize
        {
            get { return _SongSingerInfoShowPageSize; }
            set
            {
                if (_SongSingerInfoShowPageSize.Equals(value)) return;
                _SongSingerInfoShowPageSize = value;
                RaisePropertyChanged("SongSingerInfoShowPageSize");
                SongSingerInfoShowSelectLastIndex = GetSongSingerInfoShowSelectLastIndex();
                if (songManageUc != null)
                    songManageUc.AddSingerInfo();
            }
        }

        #endregion

        #region SongSingerInfoNationality

        private string _SongSingerInfoNationality = string.Empty;

        public string SongSingerInfoNationality
        {
            get { return _SongSingerInfoNationality; }
            set
            {
                if (_SongSingerInfoNationality.Equals(value)) return;
                _SongSingerInfoNationality = value;
                RaisePropertyChanged("SongSingerInfoNationality");
                SongSingerInfoShowSelectIndex = 1;
                SongSingerInfoShowSource = GetSingerInfoShowSource(SongSingerInfoNationality, SongSingerInfoSex, SongSingerInfoInitial);
            }
        }

        #endregion

        #region SongSingerInfoSex

        private string _SongSingerInfoSex = string.Empty;

        public string SongSingerInfoSex
        {
            get { return _SongSingerInfoSex; }
            set
            {
                if (_SongSingerInfoSex.Equals(value)) return;
                _SongSingerInfoSex = value;
                RaisePropertyChanged("SongSingerInfoSex");
                SongSingerInfoShowSelectIndex = 1;
                SongSingerInfoShowSource = GetSingerInfoShowSource(SongSingerInfoNationality, SongSingerInfoSex, SongSingerInfoInitial);
            }
        }

        #endregion

        #region SongSingerInfoInitial

        private string _SongSingerInfoInitial = string.Empty;

        public string SongSingerInfoInitial
        {
            get { return _SongSingerInfoInitial; }
            set
            {
                if (_SongSingerInfoInitial.Equals(value)) return;
                _SongSingerInfoInitial = value;
                RaisePropertyChanged("SongSingerInfoInitial");
                SongSingerInfoShowSelectIndex = 1;
                SongSingerInfoShowSource = GetSingerInfoShowSource(SongSingerInfoNationality, SongSingerInfoSex, SongSingerInfoInitial);
            }
        }

        #endregion

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

        private SingerInfo _SingerInfoSelectedSource = new SingerInfo();

        public SingerInfo SingerInfoSelectedSource
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

        #region Vedio

        #region SongLocationMVUrl

        private string _SongLocationMVUrl = string.Empty;

        public string SongLocationMVUrl
        {
            get { return _SongLocationMVUrl; }
            set
            {
                if (_SongLocationMVUrl != null && _SongLocationMVUrl.Equals(value)) return;
                _SongLocationMVUrl = value;
                RaisePropertyChanged("SongLocationMVUrl");
            }
        }

        #endregion

        #region VedioIsPlay

        private bool _VedioIsPlay = false;

        public bool VedioIsPlay
        {
            get { return _VedioIsPlay; }
            set
            {
                if (_VedioIsPlay.Equals(value)) return;
                _VedioIsPlay = value;
                RaisePropertyChanged("VedioIsPlay");

            }
        }

        #endregion

        #endregion

        #region SongInfoFilter

        private string _SongInfoFilter = string.Empty;

        public string SongInfoFilter
        {
            get { return _SongInfoFilter; }
            set
            {
                if (_SongInfoFilter != null && _SongInfoFilter.Equals(value)) return;
                _SongInfoFilter = value;
                RaisePropertyChanged("SongInfoFilter");
                if (SongInfoSource != null)
                    SongInfoSource.DefaultView.RowFilter = SongInfoFilter;
            }
        }

        #endregion

        #region SongInfoSort

        private string _SongInfoSort = string.Empty;

        public string SongInfoSort
        {
            get { return _SongInfoSort; }
            set
            {
                if (_SongInfoSort != null && _SongInfoSort.Equals(value)) return;
                _SongInfoSort = value;
                RaisePropertyChanged("SongInfoSort");
                if (SongInfoSource != null)
                    SongInfoSource.DefaultView.Sort = SongInfoSort;
            }
        }

        #endregion

        #region IsSelectedSingerSearch

        private bool _IsSelectedSingerSearch = true;

        public bool IsSelectedSingerSearch
        {
            get { return _IsSelectedSingerSearch; }
            set
            {
                if (_IsSelectedSingerSearch.Equals(value)) return;
                _IsSelectedSingerSearch = value;
                RaisePropertyChanged("IsSelectedSingerSearch");
            }
        }

        #endregion

        #endregion

        #region Command

        #region SongInfoRefreshCmd

        private RelayCommand _SongInfoRefreshCmd;

        public ICommand SongInfoRefreshCmd
        {
            get { return _SongInfoRefreshCmd ?? (_SongInfoRefreshCmd = new RelayCommand(param => OnSongInfoRefresh(), param => CanSongInfoRefresh())); }
        }

        public void OnSongInfoRefresh()
        {
            SongInfoSource = GetSongInfoSource();
            SongSingerInfoShowSource = GetSingerInfoShowSource(SongSingerInfoNationality, SongSingerInfoSex, SongSingerInfoInitial);
        }

        public bool CanSongInfoRefresh()
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

            SongInfoEditItem.SingerId = SingerInfoSelectedSource.Id;
            SongInfoEditItem.SingerName = SingerInfoSelectedSource.SingerName;
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
            SongInfoEditItem.MVUrl = SongInfoSelectedItem[MUSICINFO.MVURL].ToString();
            SongInfoEditItem.MusicNameInitials = SongInfoSelectedItem[MUSICINFO.MUSICNAMEINITIALS].ToString();
            SongInfoEditItem.SingRail = SongInfoSelectedItem[MUSICINFO.SINGRAIL].ToString();
            SongInfoEditItem.NewSongHot = double.Parse(string.IsNullOrEmpty(SongInfoSelectedItem[MUSICINFO.NEWSONGHOT].ToString()) ? "0" : SongInfoSelectedItem[MUSICINFO.NEWSONGHOT].ToString());
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

        #region SongInfoDeleteCmd

        private RelayCommand _SongInfoDeleteCmd;

        public ICommand SongInfoDeleteCmd
        {
            get { return _SongInfoDeleteCmd ?? (_SongInfoDeleteCmd = new RelayCommand(param => OnSongInfoDelete(), param => CanSongInfoDelete())); }
        }

        public void OnSongInfoDelete()
        {
            if (SongInfoSelectedItem == null || !SongInfoSelectedItem.Table.Columns.Contains(MUSICINFO.ID))
                return;

            string id = SongInfoSelectedItem[MUSICINFO.ID].ToString();
            string songName = SongInfoSelectedItem[MUSICINFO.MUSICNAME].ToString();
            if (MessageBox.Show($"是否删除歌曲{songName}?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                SongInfoManagementServiceCaller.DeleteSongInfo(id);
                SongInfoSource = GetSongInfoSource();
            }
        }

        public bool CanSongInfoDelete()
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
            songInfoEditUc.vedio.Close();
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
            /*
             * 批量添加所有音乐的首字母
            for (int i = 0; i < SongInfoSource.Rows.Count; i++)
            {
                SongInfoSelectedItem = SongInfoSource.Rows[i];
                SongInfoEditItem.Category = SongInfoSelectedItem[MUSICINFO.CATEGORY].ToString();//Category，SingerId需要在MusicName之前赋值
                SongInfoEditItem.Id = int.Parse(SongInfoSelectedItem[MUSICINFO.ID].ToString());
                SongInfoEditItem.SingerId = int.Parse(SongInfoSelectedItem[MUSICINFO.SINGERID].ToString());
                SongInfoEditItem.SingerName = SongInfoSelectedItem[MUSICINFO.SINGERNAME].ToString();
                SongInfoEditItem.LanguageType = SongInfoSelectedItem[MUSICINFO.LANGUAGETYPE].ToString();
                SongInfoEditItem.MVUrl = SongInfoSelectedItem[MUSICINFO.MVURL].ToString();
                SongInfoEditItem.MusicNameInitials = SongInfoSelectedItem[MUSICINFO.MUSICNAMEINITIALS].ToString();
                SongInfoEditItem.SingRail = SongInfoSelectedItem[MUSICINFO.SINGRAIL].ToString();
                if (string.IsNullOrEmpty(SongInfoSelectedItem[MUSICINFO.RELEASEDATE].ToString()))
                    SongInfoEditItem.ReleaseDate = null;
                else
                    SongInfoEditItem.ReleaseDate = DateTime.Parse(SongInfoSelectedItem[MUSICINFO.RELEASEDATE].ToString());

                SongInfoEditItem.MusicName = SongInfoSelectedItem[MUSICINFO.MUSICNAME].ToString();
                //添加首字母列内容
                if (string.IsNullOrEmpty(SongInfoEditItem.MusicNameInitials))
                    SongInfoEditItem.MusicNameInitials = GetStrInitials(SongInfoEditItem.MusicName);

                if (SongInfoManagementServiceCaller.UpdateSongeInfo(SongInfoEditItem) == 0)
                {
                    MessageBox.Show($"{SongInfoEditItem.MusicName}失败");
                }
            }
             */
            if (SongInfoEditItem == null)
                return;

            //添加首字母列内容
            if (string.IsNullOrEmpty(SongInfoEditItem.MusicNameInitials))
                SongInfoEditItem.MusicNameInitials = GetStrInitials(SongInfoEditItem.MusicName);

            if ((SongInfoAddOrUpdate && SongInfoManagementServiceCaller.AddSongInfo(SongInfoEditItem) != 0) ||
                !SongInfoAddOrUpdate && SongInfoManagementServiceCaller.UpdateSongeInfo(SongInfoEditItem) != 0)
            {
                SongInfoEditVisibility = false;
                SongInfoSource = GetSongInfoSource();
                songInfoEditUc.vedio.Close();
            }
        }

        public bool CanSongInfoSave()
        {
            return true;
        }

        #endregion

        #region SongMVUrlSelectCmd

        private RelayCommand _SongMVUrlSelectCmd;

        public ICommand SongMVUrlSelectCmd
        {
            get { return _SongMVUrlSelectCmd ?? (_SongMVUrlSelectCmd = new RelayCommand(param => OnSongMVUrlSelect(), param => CanSongMVUrlSelectCmd())); }
        }

        public void OnSongMVUrlSelect()
        {
            //选择文件
            OpenFileDialog op = new OpenFileDialog
            {
                InitialDirectory = null,//默认的打开路径
                RestoreDirectory = true,
                Filter = "视频文件(*.mpg)|*.mpg|所有文件(*.*)|*.*"
            };
            op.ShowDialog();

            if (string.IsNullOrEmpty(op.FileName))
                return;
            SongLocationMVUrl = op.FileName;
        }

        public bool CanSongMVUrlSelectCmd()
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
            if (string.IsNullOrEmpty(SongLocationMVUrl))
            {
                MessageBox.Show($"请选择本地文件?", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show($"确认上传文件{SongLocationMVUrl}?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                Uri tempSource = songInfoEditUc.vedio.Source;
                songInfoEditUc.vedio.Close();

                string saveKey = SongInfoEditItem.MusicName + "-" + SongInfoEditItem.SingerName + ".mpg";
                //上传文件到七牛云
                QiniuService.UploadImage(saveKey, SongLocationMVUrl);
                SongInfoEditItem.MVUrl = QiniuService.QINIU_URL + "/" + saveKey;
                songInfoEditUc.vedio.Source = null;
                songInfoEditUc.vedio.Source = tempSource;
            }
        }

        public bool CanSongMVUpLoad()
        {
            return true;
        }

        #endregion

        #region SongSingerShowLeftIndexCmd

        private RelayCommand _SongSingerShowLeftIndexCmd;

        public ICommand SongSingerShowLeftIndexCmd
        {
            get { return _SongSingerShowLeftIndexCmd ?? (_SongSingerShowLeftIndexCmd = new RelayCommand(param => OnSongSingerShowLeftIndex(), param => CanSongSingerShowLeftIndex())); }
        }

        public void OnSongSingerShowLeftIndex()
        {
            if (SongSingerInfoShowSelectIndex == 1)
                return;
            SongSingerInfoShowSelectIndex--;
        }

        public bool CanSongSingerShowLeftIndex()
        {
            return true;
        }

        #endregion

        #region SongSingerShowRightIndexCmd

        private RelayCommand _SongSingerShowRightIndexCmd;

        public ICommand SongSingerShowRightIndexCmd
        {
            get { return _SongSingerShowRightIndexCmd ?? (_SongSingerShowRightIndexCmd = new RelayCommand(param => OnSongSingerShowRightIndex(), param => CanSongSingerShowRightIndex())); }
        }

        public void OnSongSingerShowRightIndex()
        {
            if (SongSingerInfoShowSelectIndex >= SongSingerInfoShowSelectLastIndex)
                return;
            SongSingerInfoShowSelectIndex++;
        }

        public bool CanSongSingerShowRightIndex()
        {
            return true;
        }

        #endregion

        #region SongSingerShowGoToIndexCmd

        private RelayCommand _SongSingerShowGoToIndexCmd;

        public ICommand SongSingerShowGoToIndexCmd
        {
            get { return _SongSingerShowGoToIndexCmd ?? (_SongSingerShowGoToIndexCmd = new RelayCommand(param => OnSongSingerShowGoToIndex(), param => CanSongSingerShowGoToIndex())); }
        }

        public void OnSongSingerShowGoToIndex()
        {
            if (SongSingerInfoShowGoToIndex < 1 || SongSingerInfoShowGoToIndex > SongSingerInfoShowSelectLastIndex)
                return;
            SongSingerInfoShowSelectIndex = SongSingerInfoShowGoToIndex;
        }

        public bool CanSongSingerShowGoToIndex()
        {
            return true;
        }

        #endregion

        #region SongDownloadCmd

        private RelayCommand _SongDownloadCmd;

        public ICommand SongDownloadCmd
        {
            get { return _SongDownloadCmd ?? (_SongDownloadCmd = new RelayCommand(param => OnSongDownload(), param => CanSongDownload())); }
        }

        public void OnSongDownload()
        {
            if (SongInfoSelectedItem == null || !SongInfoSelectedItem.Table.Columns.Contains("musicname"))
                return;
            if (SongInfoSelectedItem["mvurl"] == null || string.IsNullOrEmpty(SongInfoSelectedItem["mvurl"].ToString()))
                return;
            string url = SongInfoSelectedItem["mvurl"].ToString();
            DownloadFile(url);
        }

        public bool CanSongDownload()
        {
            return true;
        }

        #endregion

        #endregion

        #region Method

        private DataTable GetSongInfoSource() {
            return SongInfoManagementServiceCaller.GetAllSongInfo();
        }

        private Dictionary<string, string> getCategorySourceDict() {
            Dictionary<string, string> result = new Dictionary<string, string>();
            for (int i = 0; i < CategorySource.Rows.Count; i++)
            {
                result.Add(CategorySource.Rows[i]["categoryname"].ToString(), CategorySource.Rows[i]["id"].ToString());
            }
            return result;
        }

        public void AddSingerInfo(WrapPanel SingerInfoFilterWrapPanel) {
            int startIndex = (SongSingerInfoShowSelectIndex - 1) * SongSingerInfoShowPageSize + 1;
            int endIndex = SongSingerInfoShowSelectIndex * SongSingerInfoShowPageSize;
            SingerInfoFilterWrapPanel.Children.Clear();
            int i = 0;
            if (startIndex == 1) {
                for (i = 0; i < SongSingerInfoShowSource.Rows.Count; i++)
                {
                    if (i < startIndex - 1) continue;
                    if (i > 31 || i > SongSingerInfoShowPageSize - 1) break;
                    WrapPanel itemWrap = new WrapPanel() {
                        Orientation = Orientation.Vertical,
                    };
                    Image itemImage = new Image
                    {
                        Width = 120,
                        Height = 120,
                        Stretch = System.Windows.Media.Stretch.UniformToFill,
                        StretchDirection = StretchDirection.Both,
                    };
                    if (SongSingerInfoShowSource.Rows[i]["singerphotourl"] != null && !string.IsNullOrEmpty(SongSingerInfoShowSource.Rows[i]["singerphotourl"].ToString()))
                        itemImage.Source = new BitmapImage(new Uri(SongSingerInfoShowSource.Rows[i]["singerphotourl"].ToString(), UriKind.Absolute));

                    Button item = new Button
                    {
                        Width = 120,
                        FontSize = 13,
                        Background = new SolidColorBrush(Colors.Transparent),
                        BorderThickness = new Thickness(0),
                        Margin = new Thickness(20, 5, 20, 5),
                        Content = SongSingerInfoShowSource.Rows[i]["singername"].ToString(),
                        Tag = SongSingerInfoShowSource.Rows[i]["id"].ToString(),
                    };
                    item.Click += SingerMusicInfoItem_Click;

                    itemWrap.Children.Add(itemImage);
                    itemWrap.Children.Add(item);
                    SingerInfoFilterWrapPanel.Children.Add(itemWrap);
                }

            }

            for (; i < SongSingerInfoShowSource.Rows.Count; i++)
            {
                if (i < startIndex - 1) continue;
                if (i > endIndex - 1) break;
                Button item = new Button
                {
                    Width = 120,
                    FontSize = 13,
                    Background = new SolidColorBrush(Colors.Transparent),
                    BorderThickness = new Thickness(0),
                    Margin = new Thickness(20, 5, 20, 5),
                    Content = SongSingerInfoShowSource.Rows[i]["singername"].ToString(),
                    Tag = SongSingerInfoShowSource.Rows[i]["id"].ToString(),
                };
                item.Click += SingerMusicInfoItem_Click;
                SingerInfoFilterWrapPanel.Children.Add(item);
            }
        }

        private void SingerMusicInfoItem_Click(object sender, RoutedEventArgs e)
        {
            Button item = sender as Button;
            string singerid = item.Tag.ToString();
            SongInfoFilter = $"singerid = '{singerid}'";
            SingerSelectedInfoVisibility = !SingerSelectedInfoVisibility;

            SingerInfoSelectedSource = SingerInfoManagementServiceCaller.GetSingerInfoById(singerid);
        }

        private int GetSongSingerInfoShowSelectLastIndex() {
            if (SongSingerInfoShowSource == null)
                return 0;
            return SongSingerInfoShowSource.Rows.Count % SongSingerInfoShowPageSize == 0 ? SongSingerInfoShowSource.Rows.Count / SongSingerInfoShowPageSize : SongSingerInfoShowSource.Rows.Count / SongSingerInfoShowPageSize + 1;
        }

        public void AddNewSongButtonInGrid(Grid newSongGrid) {
            if (newSongGrid.Children.Count != 0) return;
            for (int i = 0; i < 9; i++)
            {
                Button item = new Button
                {
                    Padding = new Thickness(25, 0, 25, 0),
                    Content = string.Format("{0:yyyy-MM}", DateTime.Parse(DateTimeNow).AddMonths(-1 * i)),
                };
                item.Click += NewSongButtonInGridItem_Click;
                Grid.SetColumn(item, 2 * i);
                ControlsHelper.SetCornerRadius(item, new CornerRadius(20));
                newSongGrid.Children.Add(item);

                Border itemLine = new Border
                {
                    Width = 25,
                    Height = 1,
                    BorderBrush = new SolidColorBrush(Colors.Gray),
                    BorderThickness = new Thickness(1)
                };
                Grid.SetColumn(itemLine, 2 * i + 1);
                newSongGrid.Children.Add(itemLine);
            }

            Button itemLast = new Button
            {
                Padding = new Thickness(30, 0, 30, 0),
                Content = string.Format("{0:yyyy-MM}", DateTime.Parse(DateTimeNow).AddMonths(-1 * 9)),
            };
            itemLast.Click += NewSongButtonInGridItem_Click;
            Grid.SetColumn(itemLast, 18);
            ControlsHelper.SetCornerRadius(itemLast, new CornerRadius(20));
            newSongGrid.Children.Add(itemLast);
        }

        private void NewSongButtonInGridItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            Button item = sender as Button;
            if (item == null) return;
            string dateStr = item.Content.ToString();
            if (string.IsNullOrEmpty(dateStr)) return;
            DateTime date = DateTime.Parse(dateStr);
            SongInfoFilter = $"releasedate > ('{date}')";
        }

        #region 下载文件

        private void DownloadFile(string url)
        {
            string fileName = url.Substring(url.LastIndexOf('/') + 1);
            SaveFileDialog saveFileDialog = new SaveFileDialog();


            //设置文件类型
            //书写规则例如：txt files(*.txt)|*.txt
            saveFileDialog.Filter = "mpg files(*.mpg)|*.mpg|All files(*.*)|*.*";
            //设置默认文件名（可以不设置）
            saveFileDialog.FileName = fileName;
            //主设置默认文件extension（可以不设置）
            saveFileDialog.DefaultExt = "mpg";
            //获取或设置一个值，该值指示如果用户省略扩展名，文件对话框是否自动在文件名中添加扩展名。（可以不设置）
            saveFileDialog.AddExtension = true;

            //设置默认文件类型显示顺序（可以不设置）
            saveFileDialog.FilterIndex = 2;

            //保存对话框是否记忆上次打开的目录
            saveFileDialog.RestoreDirectory = true;

            // Show save file dialog box
            bool? result = saveFileDialog.ShowDialog();
            //点了保存按钮进入
            if (result != null && result.Value)
            {
                //获得文件路径
                string localFilePath = saveFileDialog.FileName.ToString();

                //获取文件名，不带路径
                //string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);

                //获取文件路径，不带文件名
                //string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));

                //给文件名前加上时间
                //string newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt;

                //在文件名里加字符
                //saveFileDialog.FileName.Insert(1,"dameng");
                //为用户使用 SaveFileDialog 选定的文件名创建读/写文件流。
                //System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile();//输出文件

                //fs可以用于其他要写入的操作

                if (HttpFileExist(url))
                    DownloadHttpFile(url, localFilePath);
                else
                    MessageBox.Show($"{url}文件不存在", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }


        }

        public void DownloadHttpFile(String http_url, String save_url)
        {
            WebResponse response = null;
            //获取远程文件
            WebRequest request = WebRequest.Create(http_url);
            response = request.GetResponse();
            if (response == null) return;
            //读远程文件的大小
            mainWindow.DownloadRatioProgressBar.Maximum = response.ContentLength;
            //下载远程文件
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                Stream netStream = response.GetResponseStream();
                Stream fileStream = new FileStream(save_url, FileMode.Create);
                byte[] read = new byte[1024];
                long progressBarValue = 0;
                int realReadLen = netStream.Read(read, 0, read.Length);
                while (realReadLen > 0)
                {
                    fileStream.Write(read, 0, realReadLen);
                    progressBarValue += realReadLen;
                    mainWindow.DownloadRatioProgressBar.Dispatcher.BeginInvoke(new ProgressBarSetter(SetProgressBar), progressBarValue);
                    realReadLen = netStream.Read(read, 0, read.Length);
                }
                netStream.Close();
                fileStream.Close();

            }, null);
        }

        /// <summary>
        ///  判断远程文件是否存在
        /// </summary>
        /// <param name="fileUrl">文件URL</param>
        /// <returns>存在-true，不存在-false</returns>
        private bool HttpFileExist(string http_file_url)
        {
            WebResponse response = null;
            bool result = false;//下载结果
            try
            {
                response = WebRequest.Create(http_file_url).GetResponse();
                result = response == null ? false : true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        public delegate void ProgressBarSetter(double value);

        public void SetProgressBar(double value)
        {
            //显示进度条
            mainWindow.DownloadRatioProgressBar.Value = value;
            //显示百分比
            mainWindow.DownloadRatioLabel.Content = (int)((value / mainWindow.DownloadRatioProgressBar.Maximum) * 100) + "%";

            if (mainWindow.DownloadRatioLabel.Content.Equals("100%"))
            {
                if (MessageBox.Show("文件下载成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK) {
                    mainWindow.DownloadRatioLabel.Content = "0%";
                    mainWindow.DownloadRatioProgressBar.Value = 0.0;
                }
            }
        }

        #endregion

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

        #region 分页筛选

        #region SingerInfoShowSource

        private DataTable _SingerInfoShowSource = new DataTable();

        public DataTable SingerInfoShowSource
        {
            get { return _SingerInfoShowSource; }
            set
            {
                if (_SingerInfoShowSource != null && _SingerInfoShowSource.Equals(value)) return;
                _SingerInfoShowSource = value;
                RaisePropertyChanged("SingerInfoShowSource");
                SingerInfoShowSelectLastIndex = GetSingerInfoShowSelectLastIndex();
                SingerInfoShowSource.DefaultView.RowFilter = GetSingerInfoShowSourceFilter();
            }
        }

        #endregion

        #region SingerInfoShowSelectIndex

        private int _SingerInfoShowSelectIndex = 1;

        public int SingerInfoShowSelectIndex
        {
            get { return _SingerInfoShowSelectIndex; }
            set
            {
                if (_SingerInfoShowSelectIndex.Equals(value)) return;
                _SingerInfoShowSelectIndex = value;
                RaisePropertyChanged("SingerInfoShowSelectIndex");
                SingerInfoShowSource.DefaultView.RowFilter = GetSingerInfoShowSourceFilter();
            }
        }

        #endregion

        #region SingerInfoShowGoToIndex

        private int _SingerInfoShowGoToIndex = 1;

        public int SingerInfoShowGoToIndex
        {
            get { return _SingerInfoShowGoToIndex; }
            set
            {
                if (_SingerInfoShowGoToIndex.Equals(value)) return;
                _SingerInfoShowGoToIndex = value;
                RaisePropertyChanged("SingerInfoShowGoToIndex");
            }
        }

        #endregion

        #region SingerInfoShowSelectLastIndex

        private int _SingerInfoShowSelectLastIndex = 1;

        public int SingerInfoShowSelectLastIndex
        {
            get { return _SingerInfoShowSelectLastIndex; }
            set
            {
                if (_SingerInfoShowSelectLastIndex.Equals(value)) return;
                _SingerInfoShowSelectLastIndex = value;
                RaisePropertyChanged("SingerInfoShowSelectLastIndex");
            }
        }

        #endregion

        #region SingerInfoShowPageSize

        private int _SingerInfoShowPageSize = 20;

        public int SingerInfoShowPageSize
        {
            get { return _SingerInfoShowPageSize; }
            set
            {
                if (_SingerInfoShowPageSize.Equals(value)) return;
                _SingerInfoShowPageSize = value;
                RaisePropertyChanged("SingerInfoShowPageSize");
                SingerInfoShowSelectIndex = 1;
                SingerInfoShowSelectLastIndex = GetSingerInfoShowSelectLastIndex();
                SingerInfoShowSource.DefaultView.RowFilter = GetSingerInfoShowSourceFilter();
            }
        }

        #endregion

        #region SingerInfoNationality

        private string _SingerInfoNationality = string.Empty;

        public string SingerInfoNationality
        {
            get { return _SingerInfoNationality; }
            set
            {
                if (_SingerInfoNationality.Equals(value)) return;
                _SingerInfoNationality = value;
                RaisePropertyChanged("SingerInfoNationality");
                SingerFilterChanged(SingerInfoNationality, SingerInfoSex, SingerInfoInitial);
            }
        }

        #endregion

        #region SingerInfoSex

        private string _SingerInfoSex = string.Empty;

        public string SingerInfoSex
        {
            get { return _SingerInfoSex; }
            set
            {
                if (_SingerInfoSex.Equals(value)) return;
                _SingerInfoSex = value;
                RaisePropertyChanged("SingerInfoSex");
                SingerFilterChanged(SingerInfoNationality, SingerInfoSex, SingerInfoInitial);
            }
        }

        #endregion

        #region SingerInfoInitial

        private string _SingerInfoInitial = string.Empty;

        public string SingerInfoInitial
        {
            get { return _SingerInfoInitial; }
            set
            {
                if (_SingerInfoInitial.Equals(value)) return;
                _SingerInfoInitial = value;
                RaisePropertyChanged("SingerInfoInitial");
                SingerFilterChanged(SingerInfoNationality, SingerInfoSex, SingerInfoInitial);
            }
        }

        #endregion

        #endregion

        #region CountryInfoSource

        private DataTable _CountryInfoSource = new DataTable();

        public DataTable CountryInfoSource
        {
            get { return _CountryInfoSource; }
            set
            {
                if (_CountryInfoSource != null && _CountryInfoSource.Equals(value)) return;
                _CountryInfoSource = value;
                RaisePropertyChanged("CountryInfoSource");
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

        private SingerInfo _SingerInfoEditItem = new SingerInfo();

        public SingerInfo SingerInfoEditItem
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
            SingerInfoShowSource = GetSingerInfoShowSource(SingerInfoNationality, SingerInfoSex, SingerInfoInitial);
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
            SingerInfoEditItem = new SingerInfo();
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
            if (SingerInfoEditItem == null || !SingerInfoRobustnessVerification())
                return;

            //添加首字母列内容
            if (string.IsNullOrEmpty(SingerInfoEditItem.SingerInitials))
                SingerInfoEditItem.SingerInitials = GetStrInitials(SingerInfoEditItem.SingerName);

            if ((SingerInfoAddOrUpdate && SingerInfoManagementServiceCaller.AddSingerInfo(SingerInfoEditItem) != 0) ||
                !SingerInfoAddOrUpdate && SingerInfoManagementServiceCaller.UpdateSingerInfo(SingerInfoEditItem) != 0)
            {
                SingerInfoEditVisibility = false;
                SingerInfoSource = GetSingerInfoSource();
                SingerInfoShowSource = GetSingerInfoShowSource(SingerInfoNationality, SingerInfoSex, SingerInfoInitial);
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
            string saveKey = (Guid.NewGuid().ToString() + op.FileName.Substring(op.FileName.LastIndexOf('.'))).Replace("-", "");

            //string saveKey = SingerInfoEditItem.SingerName + "-" + SingerInfoEditItem.SingerSex + "-" + SingerInfoEditItem.SingerNationality;
            //上传图片到七牛云
            QiniuService.UploadImage(saveKey, op.FileName);
            SingerInfoEditItem.SingerPhotoUrl = QiniuService.QINIU_URL + "/" + saveKey;
        }

        public bool CanSingerImageUpLoad()
        {
            return true;
        }

        #endregion

        #region SingerShowLeftIndexCmd

        private RelayCommand _SingerShowLeftIndexCmd;

        public ICommand SingerShowLeftIndexCmd
        {
            get { return _SingerShowLeftIndexCmd ?? (_SingerShowLeftIndexCmd = new RelayCommand(param => OnSingerShowLeftIndex(), param => CanSingerShowLeftIndex())); }
        }

        public void OnSingerShowLeftIndex()
        {
            if (SingerInfoShowSelectIndex == 1)
                return;
            SingerInfoShowSelectIndex--;
        }

        public bool CanSingerShowLeftIndex()
        {
            return true;
        }

        #endregion

        #region SingerShowRightIndexCmd

        private RelayCommand _SingerShowRightIndexCmd;

        public ICommand SingerShowRightIndexCmd
        {
            get { return _SingerShowRightIndexCmd ?? (_SingerShowRightIndexCmd = new RelayCommand(param => OnSingerShowRightIndex(), param => CanSingerShowRightIndex())); }
        }

        public void OnSingerShowRightIndex()
        {
            if (SingerInfoShowSelectIndex >= SingerInfoShowSelectLastIndex)
                return;
            SingerInfoShowSelectIndex++;
        }

        public bool CanSingerShowRightIndex()
        {
            return true;
        }

        #endregion

        #region SingerShowGoToIndexCmd

        private RelayCommand _SingerShowGoToIndexCmd;

        public ICommand SingerShowGoToIndexCmd
        {
            get { return _SingerShowGoToIndexCmd ?? (_SingerShowGoToIndexCmd = new RelayCommand(param => OnSingerShowGoToIndex(), param => CanSingerShowGoToIndex())); }
        }

        public void OnSingerShowGoToIndex()
        {
            if (SingerInfoShowGoToIndex < 1 || SingerInfoShowGoToIndex > SingerInfoShowSelectLastIndex)
                return;
            SingerInfoShowSelectIndex = SingerInfoShowGoToIndex;
        }

        public bool CanSingerShowGoToIndex()
        {
            return true;
        }

        #endregion

        #region SingerInfoDeleteCmd

        private RelayCommand _SingerInfoDeleteCmdd;

        public ICommand SingerInfoDeleteCmd
        {
            get { return _SingerInfoDeleteCmdd ?? (_SingerInfoDeleteCmdd = new RelayCommand(param => OnSingerInfoDelete(), param => CanSingerInfoDelete())); }
        }

        public void OnSingerInfoDelete()
        {
            if (SingerInfoSelectedItem == null || !SingerInfoSelectedItem.Table.Columns.Contains(SINGERINFO.ID))
                return;

            string singerId = SingerInfoSelectedItem[SINGERINFO.ID].ToString();
            string singerName = SingerInfoSelectedItem[SINGERINFO.SINGERNAME].ToString();
            if (MessageBox.Show($"是否删除歌手{singerName}的基本信息?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                SingerInfoManagementServiceCaller.DeleteSingerInfo(singerId);
                SingerInfoSource = GetSingerInfoSource();
                SingerInfoShowSource = GetSingerInfoShowSource(SingerInfoNationality, SingerInfoSex, SingerInfoInitial);
            }
        }

        public bool CanSingerInfoDelete()
        {
            return true;
        }

        #endregion

        #endregion

        #region Method

        private DataTable GetSingerInfoSource()
        {
            return SingerInfoManagementServiceCaller.GetAllSingerInfo();
        }

        private DataTable GetSingerInfoShowSource(string nationality, string sex, string initial) {
            return SingerInfoManagementServiceCaller.GetSingerInfoPaging(nationality, sex, initial);
        }

        private int GetSingerInfoShowSelectLastIndex() {
            if (SingerInfoShowSource == null)
                return 0;
            return SingerInfoShowSource.Rows.Count % SingerInfoShowPageSize == 0 ? SingerInfoShowSource.Rows.Count / SingerInfoShowPageSize : SingerInfoShowSource.Rows.Count / SingerInfoShowPageSize + 1;
        }

        public void SingerFilterChanged(string nationality, string sex, string initial) {
            if ((string.IsNullOrEmpty(nationality) && !string.IsNullOrEmpty(sex)) ||
                (!string.IsNullOrEmpty(nationality) && string.IsNullOrEmpty(sex)))
                return;
            //当前页置1，页大小不变
            SingerInfoShowSelectIndex = 1;
            //更新ShowSource
            SingerInfoShowSource = GetSingerInfoShowSource(nationality, sex, initial);
        }

        private string GetSingerInfoShowSourceFilter() {
            int startIndex = SingerInfoShowPageSize * (SingerInfoShowSelectIndex - 1) + 1;
            int endIndex = SingerInfoShowPageSize * SingerInfoShowSelectIndex;
            return $"rownum >= {startIndex} and rownum <= {endIndex}";
        }

        private bool SingerInfoRobustnessVerification()
        {
            StringBuilder message = new StringBuilder();
            if (string.IsNullOrEmpty(SingerInfoEditItem.SingerName) || string.IsNullOrWhiteSpace(SingerInfoEditItem.SingerName))
                message.Append(" 歌手名称 ");
            if (string.IsNullOrEmpty(SingerInfoEditItem.SingerNationality) || string.IsNullOrWhiteSpace(SingerInfoEditItem.SingerNationality))
                message.Append(" 歌手国籍 ");
            if (string.IsNullOrEmpty(SingerInfoEditItem.SingerSex) || string.IsNullOrWhiteSpace(SingerInfoEditItem.SingerSex))
                message.Append(" 歌手性别 ");
            if (string.IsNullOrEmpty(SingerInfoEditItem.SingerClickNum) || string.IsNullOrWhiteSpace(SingerInfoEditItem.SingerClickNum))
                message.Append(" 歌手热度 ");
            if (string.IsNullOrEmpty(SingerInfoEditItem.SingerPhotoUrl) || string.IsNullOrWhiteSpace(SingerInfoEditItem.SingerPhotoUrl))
                message.Append(" 歌手照片 ");
            if (string.IsNullOrEmpty(message.ToString()))
                return true;
            else
                MessageBox.Show($"{message.ToString()}不得为空！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        /// <summary>
        /// 获取字符串的首字母序列
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string GetStrInitials(string str) {
            return GetInitialsHelper.GetChineseSpell(str);
        }

        #endregion

        #endregion

        #region RoomTask

        #region NotifyProperty

        private DateTime defaultTime = new DateTime(1900, 1, 1);
        public Dictionary<string, DateTime> RoomTaskHintDict = new Dictionary<string, DateTime>();

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

                RoomTaskHintDict = GetRoomTaskHintDict();
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

        #region RoomTaskIsAddRemark

        private bool _RoomTaskIsAddRemark = false;

        public bool RoomTaskIsAddRemark
        {
            get { return _RoomTaskIsAddRemark; }
            set
            {
                if (_RoomTaskIsAddRemark.Equals(value)) return;
                _RoomTaskIsAddRemark = value;
                RaisePropertyChanged("RoomTaskIsAddRemark");
            }
        }

        #endregion

        #region RoomTaskAddRemarkString

        private string _RoomTaskAddRemarkString;

        public string RoomTaskAddRemarkString
        {
            get { return _RoomTaskAddRemarkString; }
            set
            {
                if (_RoomTaskAddRemarkString != null && _RoomTaskAddRemarkString.Equals(value)) return;
                _RoomTaskAddRemarkString = value;
                RaisePropertyChanged("RoomTaskAddRemarkString");
            }
        }

        #endregion

        #region RoomEndTimeHintStr

        private string _RoomEndTimeHintStr;

        public string RoomEndTimeHintStr
        {
            get { return _RoomEndTimeHintStr; }
            set
            {
                if (_RoomEndTimeHintStr != null && _RoomEndTimeHintStr.Equals(value)) return;
                _RoomEndTimeHintStr = value;
                RaisePropertyChanged("RoomEndTimeHintStr");
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
            if (!RoomTaskRobustnessVerification())
                return;
            if (!UserInfoWhenRenewNoVisibility) //订购
            {
                //修改使用状态
                RoomTaskEditItem.RoomState = "1";
                //insert用户表——如果电话相同不插入，update数据
                if (RoomTaskManagementServiceCaller.HasExistUser(UserInfo.CustomerTel) == 0)
                    RoomTaskManagementServiceCaller.InsertUserInfo(UserInfo);
                if (UserInfo.CustomerId == null)
                {
                    UserInfo.CustomerId = RoomTaskManagementServiceCaller.GetCustomerIdByTel(UserInfo.CustomerTel);
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
            if (MessageBox.Show($"此次消费{RoomTaskSelectedItem["roomconsume"].ToString()}元，是否提交本次订单?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                //添加日志流水
                SelectedItemToEditItem();
                RoomTaskManagementServiceCaller.AddConsumeLog(RoomTaskEditItem);
                //清空当前包间任务状态
                RoomTaskEditItem.RoomState = "0";
                RoomTaskEditItem.RoomConsume = null;
                RoomTaskEditItem.StartTime = defaultTime;
                RoomTaskEditItem.EndTime = defaultTime;
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
            if (user == null || user.Rows.Count != 1)
            {
                string count = user == null ? "stirng.Empty" : user.Rows.Count.ToString();
                LogHelper.LogError($"用户ID:{RoomTaskEditItem.CustomerId},读取数据库用户信息数应为1，实际为{count}");
            }
            else
            {
                UserInfo.CustomerId = user.Rows[0][CUSTOMERINFO.CUSTOMERID].ToString();
                UserInfo.CustomerName = user.Rows[0][CUSTOMERINFO.CUSTOMERNAME].ToString();
                UserInfo.CustomerSex = user.Rows[0][CUSTOMERINFO.CUSTOMERSEX].ToString();
                UserInfo.CustomerTel = user.Rows[0][CUSTOMERINFO.CUSTOMERTEL].ToString();
            }
            //隐藏人员信息栏目
            UserInfoWhenRenewNoVisibility = true;
            if (roomTaskEditWnd != null)
                roomTaskEditWnd.Close();

            //打开界面
            roomTaskEditWnd = new RoomTaskEditWnd(mainWindow.ClientViewModel);
            //更新消费时长
            roomTaskEditWnd.ConsumeTimeValue.Value = (RoomTaskEditItem.EndTime - RoomTaskEditItem.StartTime).TotalHours;
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
            if (RoomTaskSelectedItem != null && RoomTaskSelectedItem["roomid"] != null && !string.IsNullOrEmpty(RoomTaskSelectedItem["roomid"].ToString()))
                RoomTaskIsAddRemark = true;
        }

        public bool CanRoomTaskRemark()
        {
            return true;
        }

        #endregion

        #region RoomTaskRemarkCommitCmd

        private RelayCommand _RoomTaskRemarkCommitCmd;

        public ICommand RoomTaskRemarkCommitCmd
        {
            get { return _RoomTaskRemarkCommitCmd ?? (_RoomTaskRemarkCommitCmd = new RelayCommand(param => OnRoomTaskRemarkCommit(), param => CanRoomTaskRemarkCommit())); }
        }

        public void OnRoomTaskRemarkCommit()
        {
            if (!string.IsNullOrEmpty(RoomTaskAddRemarkString))
            {
                RoomInfoManagementServiceCaller.AddRoomTaskRemark(RoomTaskSelectedItem["roomid"].ToString(), RoomTaskAddRemarkString, "操作员");
                RoomInfoSource = GetRoomInfoSource();
                RoomTaskSource = GetRoomTaskSource();
            }
            RoomTaskIsAddRemark = false;

        }

        public bool CanRoomTaskRemarkCommit()
        {
            return true;
        }

        #endregion

        #region RoomTaskRemarkCancelCmd

        private RelayCommand _RoomTaskRemarkCancelCmd;

        public ICommand RoomTaskRemarkCancelCmd
        {
            get { return _RoomTaskRemarkCancelCmd ?? (_RoomTaskRemarkCancelCmd = new RelayCommand(param => OnRoomTaskRemarkCancel(), param => CanRoomTaskRemarkCancel())); }
        }

        public void OnRoomTaskRemarkCancel()
        {
            RoomTaskIsAddRemark = false;
            RoomTaskAddRemarkString = string.Empty;
        }

        public bool CanRoomTaskRemarkCancel()
        {
            return true;
        }

        #endregion

        #region StartMusicSystemCmd

        private RelayCommand _StartMusicSystemCmd;

        public ICommand StartMusicSystemCmd
        {
            get { return _StartMusicSystemCmd ?? (_StartMusicSystemCmd = new RelayCommand(param => OnStartMusicSystem(), param => CanStartMusicSystem())); }
        }

        public void OnStartMusicSystem()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"KtvMusic.exe";
            var startinfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = path,
            };
            Process.Start(startinfo);
            Task.Run(() =>
            {
                Thread.Sleep(2000);
            });
        }

        public bool CanStartMusicSystem()
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
            if (roomTaskEditItem.EndTime <= roomTaskEditItem.StartTime.AddMinutes(30))
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
                RoomTaskEditItem.StartTime = defaultTime;
            else
                RoomTaskEditItem.StartTime = DateTime.Parse(RoomTaskSelectedItem[ROOMTASK.STARTTIME].ToString());
            if (string.IsNullOrEmpty(RoomTaskSelectedItem[ROOMTASK.ENDTIME].ToString()))
                RoomTaskEditItem.EndTime = defaultTime;
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

        private bool RoomTaskRobustnessVerification() {
            StringBuilder message = new StringBuilder();
            if (RoomTaskEditItem.EndTime <= RoomTaskEditItem.StartTime)
            {
                message.Append($" 请调整消费时间 ");
                MessageBox.Show($"{message.ToString()}！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(UserInfo.CustomerName) || string.IsNullOrWhiteSpace(UserInfo.CustomerName))
            {
                MessageBox.Show($"姓氏/姓名不得为空！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(UserInfo.CustomerTel) || string.IsNullOrWhiteSpace(UserInfo.CustomerTel) || !CheckMobliePhone(UserInfo.CustomerTel))
            {
                MessageBox.Show($"请输入合法电话号码！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public bool CheckMobliePhone(string phone)
        {
            //电信手机号码正则        
            string dianxin = @"^1[3578][01379]\d{8}$";
            Regex dReg = new Regex(dianxin);
            //联通手机号正则        
            string liantong = @"^1[34578][01256]\d{8}$";
            Regex tReg = new Regex(liantong);
            //移动手机号正则        
            string yidong = @"^(134[012345678]\d{7}|1[34578][012356789]\d{8})$";
            Regex yReg = new Regex(yidong);

            if (dReg.IsMatch(phone) || tReg.IsMatch(phone) || yReg.IsMatch(phone))
            {
                return true;
            }
            return false;
        }

        public void RoomEndTimeHintTimer() {
            var timer = new DispatcherTimer();
            timer.Tick += RoomEndTimeHintTimerTick;
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Start();
        }

        private void RoomEndTimeHintTimerTick(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder();
            DateTime date = DateTime.Now;
            foreach (KeyValuePair<string, DateTime> item in RoomTaskHintDict)
            {
                if (date.AddMinutes(10) >= item.Value) {
                    result.Append(" [" + item.Key + "] ");
                }
            }
            if (!string.IsNullOrEmpty(result.ToString()))
                result.Append("包间将在十分钟内到期，请及时询问是否续费！");
            RoomEndTimeHintStr = result.ToString();
        }

        private Dictionary<string, DateTime> GetRoomTaskHintDict() {
            Dictionary<string, DateTime> result = new Dictionary<string, DateTime>();
            for (int i = 0; i < RoomTaskSource.Rows.Count; i++)
            {
                if (RoomTaskSource.Rows[i]["roomstate"] == null || int.Parse(RoomTaskSource.Rows[i]["roomstate"].ToString()) == 0) continue;
                if (int.Parse(RoomTaskSource.Rows[i]["roomstate"].ToString()) == 1) {
                    result.Add(RoomTaskSource.Rows[i]["roomid"].ToString(), DateTime.Parse(RoomTaskSource.Rows[i]["endtime"].ToString()));
                }
            }
            return result;
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
        private void GetRoomInfoEditItemFromSelectedItem() {
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

        #region DataAnalysis

        #region NotifyProperty

        #region DataAnalysisSelectedIndex

        private int _DataAnalysisSelectedIndex = 0;

        public int DataAnalysisSelectedIndex
        {
            get { return _DataAnalysisSelectedIndex; }
            set
            {
                if (_DataAnalysisSelectedIndex.Equals(value)) return;
                _DataAnalysisSelectedIndex = value;
                RaisePropertyChanged("DataAnalysisSelectedIndex");
            }
        }

        #endregion

        #region ConsumeLogSource

        private DataTable _ConsumeLogSource = new DataTable();

        public DataTable ConsumeLogSource
        {
            get { return _ConsumeLogSource; }
            set
            {
                if (_ConsumeLogSource != null && _ConsumeLogSource.Equals(value)) return;
                _ConsumeLogSource = value;
                RaisePropertyChanged("ConsumeLogSource");
            }
        }

        #endregion

        #region 营业额-时间

        #region ConsumeSeriesCollection

        private SeriesCollection _ConsumeSeriesCollection = new SeriesCollection();

        public SeriesCollection ConsumeSeriesCollection
        {
            get { return _ConsumeSeriesCollection; }
            set
            {
                if (_ConsumeSeriesCollection != null && _ConsumeSeriesCollection.Equals(value)) return;
                _ConsumeSeriesCollection = value;
                RaisePropertyChanged("ConsumeSeriesCollection");
            }
        }

        #endregion

        #region ConsumeLabels

        private string[] _ConsumeLabels;

        public string[] ConsumeLabels
        {
            get { return _ConsumeLabels; }
            set
            {
                if (_ConsumeLabels != null && _ConsumeLabels.Equals(value)) return;
                _ConsumeLabels = value;
                RaisePropertyChanged("ConsumeLabels");
            }
        }

        #endregion

        #region ConsumeYFormatter

        private Func<double, string> _ConsumeYFormatter;

        public Func<double, string> ConsumeYFormatter
        {
            get { return _ConsumeYFormatter; }
            set
            {
                if (_ConsumeYFormatter != null && _ConsumeYFormatter.Equals(value)) return;
                _ConsumeYFormatter = value;
                RaisePropertyChanged("ConsumeYFormatter");
            }
        }

        #endregion

        #region ConsumeTimeSpanType

        private int _ConsumeTimeSpanType = 1;// 0-近10年; 1-年; 2-月; 3-日

        public int ConsumeTimeSpanType
        {
            get { return _ConsumeTimeSpanType; }
            set
            {
                if (_ConsumeTimeSpanType.Equals(value)) return;
                _ConsumeTimeSpanType = value;
                RaisePropertyChanged("ConsumeTimeSpanType");
            }
        }

        #endregion

        #region ConsumeYearValue

        private int _ConsumeYearValue = DateTime.Now.Year;

        public int ConsumeYearValue
        {
            get { return _ConsumeYearValue; }
            set
            {
                if (_ConsumeYearValue.Equals(value)) return;
                _ConsumeYearValue = value;
                RaisePropertyChanged("ConsumeYearValue");
            }
        }

        #endregion

        #region ConsumeMonthValue

        private int _ConsumeMonthValue = DateTime.Now.Month;

        public int ConsumeMonthValue
        {
            get { return _ConsumeMonthValue; }
            set
            {
                if (_ConsumeMonthValue.Equals(value)) return;
                _ConsumeMonthValue = value;
                RaisePropertyChanged("ConsumeMonthValue");
            }
        }

        #endregion

        #endregion

        #region 营业额-包间类型

        #region ConsumeRoomTypeSeriesCollection

        private SeriesCollection _ConsumeRoomTypeSeriesCollection = new SeriesCollection();

        public SeriesCollection ConsumeRoomTypeSeriesCollection
        {
            get { return _ConsumeRoomTypeSeriesCollection; }
            set
            {
                if (_ConsumeRoomTypeSeriesCollection != null && _ConsumeRoomTypeSeriesCollection.Equals(value)) return;
                _ConsumeRoomTypeSeriesCollection = value;
                RaisePropertyChanged("ConsumeRoomTypeSeriesCollection");
            }
        }

        #endregion


        #endregion

        #region 包间使用率-时间

        #region RoomTimeSeriesCollection

        private SeriesCollection _RoomTimeSeriesCollection = new SeriesCollection();

        public SeriesCollection RoomTimeSeriesCollection
        {
            get { return _RoomTimeSeriesCollection; }
            set
            {
                if (_RoomTimeSeriesCollection != null && _RoomTimeSeriesCollection.Equals(value)) return;
                _RoomTimeSeriesCollection = value;
                RaisePropertyChanged("RoomTimeSeriesCollection");
            }
        }

        #endregion

        #region RoomTimeLabels

        private string[] _RoomTimeLabels;

        public string[] RoomTimeLabels
        {
            get { return _RoomTimeLabels; }
            set
            {
                if (_RoomTimeLabels != null && _RoomTimeLabels.Equals(value)) return;
                _RoomTimeLabels = value;
                RaisePropertyChanged("RoomTimeLabels");
            }
        }

        #endregion

        #region RoomTimeYFormatter

        private Func<double, string> _RoomTimeYFormatter;

        public Func<double, string> RoomTimeYFormatter
        {
            get { return _RoomTimeYFormatter; }
            set
            {
                if (_RoomTimeYFormatter != null && _RoomTimeYFormatter.Equals(value)) return;
                _RoomTimeYFormatter = value;
                RaisePropertyChanged("RoomTimeYFormatter");
            }
        }

        #endregion

        #endregion

        #region 包间使用率-包间类型

        #region UserRateRoomTypeSeriesCollection

        private SeriesCollection _UserRateRoomTypeSeriesCollection = new SeriesCollection();

        public SeriesCollection UserRateRoomTypeSeriesCollection
        {
            get { return _UserRateRoomTypeSeriesCollection; }
            set
            {
                if (_UserRateRoomTypeSeriesCollection != null && _UserRateRoomTypeSeriesCollection.Equals(value)) return;
                _UserRateRoomTypeSeriesCollection = value;
                RaisePropertyChanged("UserRateRoomTypeSeriesCollection");
            }
        }

        #endregion

        #endregion

        #region SongRecordSource

        private DataTable _SongRecordSource = new DataTable();

        public DataTable SongRecordSource
        {
            get { return _SongRecordSource; }
            set
            {
                if (_SongRecordSource != null && _SongRecordSource.Equals(value)) return;
                _SongRecordSource = value;
                RaisePropertyChanged("SongRecordSource");
            }
        }

        #endregion

        #region 歌曲热度

        #region MinClickDate

        private DateTime _MinClickDate = DateTime.Today.AddMonths(-90);

        public DateTime MinClickDate
        {
            get { return _MinClickDate; }
            set
            {
                if (_MinClickDate != null && _MinClickDate.Equals(value)) return;
                _MinClickDate = value;
                RaisePropertyChanged("MinClickDate");
            }
        }

        #endregion

        #region MinRecordNumber

        private int _MinRecordNumber = 1;

        public int MinRecordNumber
        {
            get { return _MinRecordNumber; }
            set
            {
                if (_MinRecordNumber.Equals(value)) return;
                _MinRecordNumber = value;
                RaisePropertyChanged("MinRecordNumber");
            }
        }

        #endregion

        #region HalfLifeValue

        private int _HalfLifeValue = 1000;

        public int HalfLifeValue
        {
            get { return _HalfLifeValue; }
            set
            {
                if (_HalfLifeValue.Equals(value)) return;
                _HalfLifeValue = value;
                RaisePropertyChanged("HalfLifeValue");
            }
        }

        #endregion

        #region SongRecordTempResultSource

        private DataTable _SongRecordTempResultSource = new DataTable();

        public DataTable SongRecordTempResultSource
        {
            get { return _SongRecordTempResultSource; }
            set
            {
                if (_SongRecordTempResultSource != null && _SongRecordTempResultSource.Equals(value)) return;
                _SongRecordTempResultSource = value;
                RaisePropertyChanged("SongRecordTempResultSource");
            }
        }

        #endregion

        #region SongTimeSeriesCollection

        private SeriesCollection _SongTimeSeriesCollection = new SeriesCollection();

        public SeriesCollection SongTimeSeriesCollection
        {
            get { return _SongTimeSeriesCollection; }
            set
            {
                if (_SongTimeSeriesCollection != null && _SongTimeSeriesCollection.Equals(value)) return;
                _SongTimeSeriesCollection = value;
                RaisePropertyChanged("SongTimeSeriesCollection");
            }
        }

        #endregion

        #region SongTimeLabels

        private string[] _SongTimeLabels;

        public string[] SongTimeLabels
        {
            get { return _SongTimeLabels; }
            set
            {
                if (_SongTimeLabels != null && _SongTimeLabels.Equals(value)) return;
                _SongTimeLabels = value;
                RaisePropertyChanged("SongTimeLabels");
            }
        }

        #endregion

        #region SongTimeYFormatter

        private Func<double, string> _SongTimeYFormatter;

        public Func<double, string> SongTimeYFormatter
        {
            get { return _SongTimeYFormatter; }
            set
            {
                if (_SongTimeYFormatter != null && _SongTimeYFormatter.Equals(value)) return;
                _SongTimeYFormatter = value;
                RaisePropertyChanged("SongTimeYFormatter");
            }
        }

        #endregion

        #region SongTimeSpanType

        private int _SongTimeSpanType = 1;// 0-近10年; 1-年; 2-月; 3-日

        public int SongTimeSpanType
        {
            get { return _SongTimeSpanType; }
            set
            {
                if (_SongTimeSpanType.Equals(value)) return;
                _SongTimeSpanType = value;
                RaisePropertyChanged("SongTimeSpanType");
            }
        }

        #endregion

        #region SongYearValue

        private int _SongYearValue = DateTime.Now.Year;

        public int SongYearValue
        {
            get { return _SongYearValue; }
            set
            {
                if (_SongYearValue.Equals(value)) return;
                _SongYearValue = value;
                RaisePropertyChanged("SongYearValue");
            }
        }

        #endregion

        #region SongMonthValue

        private int _SongMonthValue = DateTime.Now.Month;

        public int SongMonthValue
        {
            get { return _SongMonthValue; }
            set
            {
                if (_SongMonthValue.Equals(value)) return;
                _SongMonthValue = value;
                RaisePropertyChanged("SongMonthValue");
            }
        }

        #endregion

        #region SongSelectedDataRowViews

        private List<DataRowView> _SongSelectedDataRowViews = new List<DataRowView>();

        public List<DataRowView> SongSelectedDataRowViews
        {
            get { return _SongSelectedDataRowViews; }
            set
            {
                if (_SongSelectedDataRowViews != null && _SongSelectedDataRowViews.Equals(value)) return;
                _SongSelectedDataRowViews = value;
                RaisePropertyChanged("SongSelectedDataRowViews");
            }
        }

        #endregion


        #endregion

        #region 歌手热度

        #region SingerMinClickDate

        private DateTime _SingerMinClickDate = DateTime.Today.AddMonths(-90);

        public DateTime SingerMinClickDate
        {
            get { return _SingerMinClickDate; }
            set
            {
                if (_SingerMinClickDate != null && _SingerMinClickDate.Equals(value)) return;
                _SingerMinClickDate = value;
                RaisePropertyChanged("SingerMinClickDate");
            }
        }

        #endregion

        #region SingerRecordTempResultSource

        private DataTable _SingerRecordTempResultSource = new DataTable();

        public DataTable SingerRecordTempResultSource
        {
            get { return _SingerRecordTempResultSource; }
            set
            {
                if (_SingerRecordTempResultSource != null && _SingerRecordTempResultSource.Equals(value)) return;
                _SingerRecordTempResultSource = value;
                RaisePropertyChanged("SingerRecordTempResultSource");
            }
        }

        #endregion

        #endregion

        #endregion

        #region Commands

        #region TurnOverCmd

        private RelayCommand _TurnOverCmd;

        public ICommand TurnOverCmd
        {
            get { return _TurnOverCmd ?? (_TurnOverCmd = new RelayCommand(param => OnTurnOver(), param => CanTurnOver())); }
        }

        public void OnTurnOver()
        {
            if (DataAnalysisSelectedIndex != 1)
                DataAnalysisSelectedIndex = 1;

            ConsumeLogSource = GetConsumeLogSource();

            #region 营业额-时间

            ChartValues<int> consumeData = GetConsumeData(ConsumeTimeSpanType, ConsumeYearValue, ConsumeMonthValue);

            //数据集赋值
            ConsumeSeriesCollection = new SeriesCollection
            {
                new ColumnSeries//ColumnSeries-柱状图; LineSeries-曲线图 
                {
                    Title = "营业额",
                    Values = consumeData,
                },
            };

            //x,y轴
            ConsumeLabels = GetConsumeX(ConsumeTimeSpanType, ConsumeYearValue, ConsumeMonthValue);
            ConsumeYFormatter = value => value.ToString("C");

            #endregion

            #region 营业额-房间类型

            Dictionary<string, int> consumeRoomTypeRate = GetConsumeDataByRoomType(ConsumeTimeSpanType, ConsumeYearValue, ConsumeMonthValue);

            ConsumeRoomTypeSeriesCollection = new SeriesCollection();
            foreach (KeyValuePair<string, int> item in consumeRoomTypeRate)
            {
                ConsumeRoomTypeSeriesCollection.Add(new PieSeries
                {
                    Title = GetRoomTypeNameFromCode(item.Key),
                    Values = new ChartValues<ObservableValue> { new ObservableValue(item.Value) },
                    DataLabels = true
                });
            }

            #endregion
        }

        public bool CanTurnOver()
        {
            return true;
        }

        #endregion

        #region RoomUseRateCmd

        private RelayCommand _RoomUseRateCmd;

        public ICommand RoomUseRateCmd
        {
            get { return _RoomUseRateCmd ?? (_RoomUseRateCmd = new RelayCommand(param => OnRoomUseRate(), param => CanRoomUseRate())); }
        }

        public void OnRoomUseRate()
        {
            if (DataAnalysisSelectedIndex != 2)
                DataAnalysisSelectedIndex = 2;

            ConsumeLogSource = GetConsumeLogSource();

            #region 房间使用率-时间

            ChartValues<double> consumeData = GetRoomUseRateData(ConsumeTimeSpanType, ConsumeYearValue, ConsumeMonthValue);

            //数据集赋值
            RoomTimeSeriesCollection = new SeriesCollection
            {
                new LineSeries//ColumnSeries-柱状图; LineSeries-曲线图 
                {
                    Title = "房间使用率",
                    Values = consumeData,
                },
            };

            //x,y轴
            RoomTimeLabels = GetConsumeX(ConsumeTimeSpanType, ConsumeYearValue, ConsumeMonthValue);
            RoomTimeYFormatter = value => value.ToString("p");

            #endregion

            #region 房间使用率-房间类型

            Dictionary<string, double> useRateRoomTypeRate = GetuseRateRoomTypeDataByRoomType(ConsumeTimeSpanType, ConsumeYearValue, ConsumeMonthValue);

            UserRateRoomTypeSeriesCollection = new SeriesCollection();
            foreach (KeyValuePair<string, double> item in useRateRoomTypeRate)
            {
                UserRateRoomTypeSeriesCollection.Add(new PieSeries
                {
                    Title = GetRoomTypeNameFromCode(item.Key),
                    Values = new ChartValues<ObservableValue> { new ObservableValue(item.Value) },
                    DataLabels = true
                });
            }

            #endregion
        }

        public bool CanRoomUseRate()
        {
            return true;
        }

        #endregion

        #region SongHotCmd

        private RelayCommand _SongHotCmd;

        public ICommand SongHotCmd
        {
            get { return _SongHotCmd ?? (_SongHotCmd = new RelayCommand(param => OnSongHot(), param => CanSongHot())); }
        }

        public void OnSongHot()
        {
            if (DataAnalysisSelectedIndex != 3)
                DataAnalysisSelectedIndex = 3;

            SongRecordSource = GetSongRecordSource();

            SongRecordSource.DefaultView.RowFilter = $"clickdate >= '{MinClickDate}'";

            //计算发行时间内所有歌曲的总点击量，过滤掉点击量不够的，计算出热度
            SongRecordTempResultSource = GetSongRecordTimeResultSource(SongRecordSource);
            SongRecordTempResultSource.DefaultView.Sort = "hotnumber desc";
        }

        public bool CanSongHot()
        {
            return true;
        }

        #endregion

        #region SongTimeHotCmd

        private RelayCommand _SongTimeHotCmd;

        public ICommand SongTimeHotCmd
        {
            get { return _SongTimeHotCmd ?? (_SongTimeHotCmd = new RelayCommand(param => OnSongTimeHot(), param => CanSongTimeHot())); }
        }

        public void OnSongTimeHot()
        {
            if (SongSelectedDataRowViews == null || SongSelectedDataRowViews.Count == 0)
            {
                MessageBox.Show("请选择需要展示的歌曲", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SongRecordSource = GetSongRecordSource();

            SongTimeSeriesCollection = new SeriesCollection();
            //获取几个歌曲的热度走势
            foreach (DataRowView item in SongSelectedDataRowViews)
            {
                ChartValues<int> songData = GetSongData(SongTimeSpanType, SongYearValue, SongMonthValue, item["musicid"].ToString());

                //数据集赋值
                SongTimeSeriesCollection.Add(
                    new LineSeries//ColumnSeries-柱状图; LineSeries-曲线图 
                {
                        Title = item["musicname"].ToString(),
                        Values = songData,
                    }
                );
            }

            //x,y轴
            SongTimeLabels = GetConsumeX(SongTimeSpanType, SongYearValue, SongMonthValue);
            SongTimeYFormatter = value => value.ToString();

            #endregion

        }

        public bool CanSongTimeHot()
        {
            return true;
        }

        #endregion

        #region UpdateSongHotCmd

        private RelayCommand _UpdateSongHotCmd;

        public ICommand UpdateSongHotCmd
        {
            get { return _UpdateSongHotCmd ?? (_UpdateSongHotCmd = new RelayCommand(param => OnUpdateSongHot(), param => CanUpdateSongHot())); }
        }

        public void OnUpdateSongHot()
        {
            //确认是否更新现有排行榜
            if (MessageBox.Show($"是否更新现有排行榜?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                //赋值歌曲信息表-新歌排行字段为0,将SongRecordTempResultSource记录的分值存入该字段，更新歌曲数据源
                Dictionary<string, double> result = new Dictionary<string, double>();
                for (int i = 0; i < SongRecordTempResultSource.DefaultView.Count; i++)
                {
                    result.Add(SongRecordTempResultSource.DefaultView[i]["musicid"].ToString(), double.Parse(SongRecordTempResultSource.DefaultView[i]["hotnumber"].ToString()));
                }
                SongInfoManagementServiceCaller.UpdateNewSongRank(result);
                OnSongInfoRefresh();
                //更新排行后将点唱次数统计起始时间，最低点击量，半衰期写入XML中
                UpdateSongHotConfigFile();
                MessageBox.Show("更新成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool CanUpdateSongHot()
        {
            return true;
        }

        #endregion

        #region SingerHotCmd

        private RelayCommand _SingerHotCmd;

        public ICommand SingerHotCmd
        {
            get { return _SingerHotCmd ?? (_SingerHotCmd = new RelayCommand(param => OnSingerHot(), param => CanSingerHot())); }
        }

        public void OnSingerHot()
        {
            if (DataAnalysisSelectedIndex != 4)
                DataAnalysisSelectedIndex = 4;

            SongRecordSource = GetSongRecordSource();

            SongRecordSource.DefaultView.RowFilter = $"clickdate >= '{SingerMinClickDate}'";

            //计算发行时间内所有歌手的总点击量
            SingerRecordTempResultSource = GetSingerRecordTimeResultSource(SongRecordSource);
            SingerRecordTempResultSource.DefaultView.Sort = "clicknum desc";
        }

        public bool CanSingerHot()
        {
            return true;
        }

        #endregion

        #region UpdateSingerHotCmd

        private RelayCommand _UpdateSingerHotCmd;

        public ICommand UpdateSingerHotCmd
        {
            get { return _UpdateSingerHotCmd ?? (_UpdateSingerHotCmd = new RelayCommand(param => OnUpdateSingerHot(), param => CanUpdateSingerHot())); }
        }

        public void OnUpdateSingerHot()
        {
            //确认是否更新现有排行榜
            if (MessageBox.Show($"是否更新现有歌手排名?", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
            {
                //赋值歌首信息表-singerclicknum字段为0,将clicknum记录的分值存入该字段，更新歌手数据源
                Dictionary<string, int> result = new Dictionary<string, int>();
                for (int i = 0; i < SingerRecordTempResultSource.DefaultView.Count; i++)
                {
                    result.Add(SingerRecordTempResultSource.DefaultView[i]["singerid"].ToString(), int.Parse(SingerRecordTempResultSource.DefaultView[i]["clicknum"].ToString()));
                }
                SingerInfoManagementServiceCaller.UpdateNewSingerRank(result);
                OnGetAllSingerInfo();
                //更新排行后将点唱次数统计起始时间写入XML中
                UpdateSingerHotConfigFile();
                MessageBox.Show("更新成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public bool CanUpdateSingerHot()
        {
            return true;
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// 获取经营流水数据源
        /// </summary>
        /// <returns></returns>
        private DataTable GetConsumeLogSource() {
            return RoomTaskManagementServiceCaller.GetConsumeLog();
        }

        /// <summary>
        /// 获取经营流水数据集合
        /// </summary>
        /// <param name="timeSpanType"></param>
        /// <param name="yearValue"></param>
        /// <param name="monthValue"></param>
        /// <returns></returns>
        private ChartValues<int> GetConsumeData(int timeSpanType, int yearValue, int monthValue)
        {
            ChartValues<int> result = new ChartValues<int>();

            Dictionary<int, int> tempResult = GetTempResultDictionary(timeSpanType, yearValue, monthValue);

            ConsumeLogSource.DefaultView.RowFilter = GetRowFilterByTime(timeSpanType, yearValue, monthValue);

            for (int i = 0; i < ConsumeLogSource.DefaultView.Count; i++)
            {
                DateTime date = DateTime.Parse(ConsumeLogSource.DefaultView[i]["starttime"].ToString());
                if (timeSpanType == 0)
                    tempResult[date.Year] = tempResult[date.Year] + int.Parse(ConsumeLogSource.DefaultView[i]["roomconsume"].ToString());
                if (timeSpanType == 1)
                    tempResult[date.Month] = tempResult[date.Month] + int.Parse(ConsumeLogSource.DefaultView[i]["roomconsume"].ToString());
                if (timeSpanType == 2)
                    tempResult[date.Day] = tempResult[date.Day] + int.Parse(ConsumeLogSource.DefaultView[i]["roomconsume"].ToString());
            }

            foreach (KeyValuePair<int, int> item in tempResult)
            {
                result.Add(item.Value);
            }

            return result;
        }

        private Dictionary<int, int> GetTempResultDictionary(int timeSpanType, int yearValue, int monthValue)
        {
            Dictionary<int, int> tempResult = new Dictionary<int, int>();

            if (timeSpanType == 0)//近10年
            {
                for (int i = 0; i < 10; i++)
                {
                    tempResult.Add(DateTime.Now.Year - i, 0);
                }
            }
            else if (timeSpanType == 1)//某一年
            {
                for (int i = 1; i <= 12; i++)
                {
                    tempResult.Add(i, 0);
                }
            }
            else if (timeSpanType == 2)//某个月
            {
                DateTime date = new DateTime(yearValue, monthValue, 1);
                int days = DateTime.DaysInMonth(yearValue, monthValue);
                for (int i = 1; i <= days; i++)
                {
                    tempResult.Add(i, 0);
                }
            }
            return tempResult;
        }

        private Dictionary<int, double> GetTempResultDictionaryReturnDouble(int timeSpanType, int yearValue, int monthValue)
        {
            Dictionary<int, double> tempResult = new Dictionary<int, double>();

            if (timeSpanType == 0)//近10年
            {
                for (int i = 0; i < 10; i++)
                {
                    tempResult.Add(DateTime.Now.Year - i, 0);
                }
            }
            else if (timeSpanType == 1)//某一年
            {
                for (int i = 1; i <= 12; i++)
                {
                    tempResult.Add(i, 0);
                }
            }
            else if (timeSpanType == 2)//某个月
            {
                DateTime date = new DateTime(yearValue, monthValue, 1);
                int days = DateTime.DaysInMonth(yearValue, monthValue);
                for (int i = 1; i <= days; i++)
                {
                    tempResult.Add(i, 0);
                }
            }
            return tempResult;
        }

        private string GetRowFilterByTime(int timeSpanType, int yearValue, int monthValue) {
            if (timeSpanType == 0)
            {
                DateTime startDate = DateTime.Now.AddYears(-10);
                return $"starttime >= ('{startDate}')";
            }
            if (timeSpanType == 1)
            {
                DateTime startDate = new DateTime(yearValue, 1, 1);
                DateTime endDate = new DateTime(yearValue + 1, 1, 1).AddDays(-1);
                return $"starttime >= ('{startDate}') and starttime <= ('{endDate}')";
            }
            if (timeSpanType == 2)
            {
                DateTime startDate = new DateTime(yearValue, monthValue, 1);
                DateTime endDate = new DateTime(yearValue, monthValue, 1).AddDays(DateTime.DaysInMonth(yearValue, monthValue) - 1);
                return $"starttime >= ('{startDate}') and starttime <= ('{endDate}')";
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// 根据时间获取X轴时间段值
        /// </summary>
        /// <param name="timeSpanType"></param>
        /// <param name="yearValue"></param>
        /// <param name="monthValue"></param>
        /// <returns></returns>
        private string[] GetConsumeX(int timeSpanType, int yearValue, int monthValue)
        {
            List<string> result = new List<string>();
            if (timeSpanType == 0)
                for (int i = 0; i < 10; i++)
                {
                    result.Add((yearValue - i).ToString());
                }
            if (timeSpanType == 1)
                return new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            if (timeSpanType == 2)
            {
                int days = DateTime.DaysInMonth(yearValue, monthValue);
                for (int i = 1; i <= days; i++)
                {
                    result.Add(i.ToString());
                }
            }
            string[] resultStr = new string[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                resultStr[i] = result[i];
            }
            return resultStr;
        }

        /// <summary>
        /// 获取经营流水(房间类型)数据集合
        /// </summary>
        /// <param name="timeSpanType"></param>
        /// <param name="yearValue"></param>
        /// <param name="monthValue"></param>
        /// <returns></returns>
        private Dictionary<string, int> GetConsumeDataByRoomType(int timeSpanType, int yearValue, int monthValue) {

            Dictionary<string, int> result = new Dictionary<string, int>() {
                { "0", 0},//小型包间
                { "1", 0},//中型包间
                { "2", 0},//大型包间
                { "3", 0},//情侣包间
                { "4", 0},//豪华包间
                { "5", 0},//商务包间
            };

            ConsumeLogSource.DefaultView.RowFilter = GetRowFilterByTime(timeSpanType, yearValue, monthValue);

            for (int i = 0; i < ConsumeLogSource.DefaultView.Count; i++)
            {
                string roomType = ConsumeLogSource.DefaultView[i]["roomtype"].ToString();
                if (result.ContainsKey(roomType))
                    result[roomType] += int.Parse(ConsumeLogSource.DefaultView[i]["roomconsume"].ToString());
            }

            return result;
        }
        
        /// <summary>
        /// 获取房间使用率(房间类型)数据集合
        /// </summary>
        /// <param name="timeSpanType"></param>
        /// <param name="yearValue"></param>
        /// <param name="monthValue"></param>
        /// <returns></returns>
        private Dictionary<string, double> GetuseRateRoomTypeDataByRoomType(int timeSpanType, int yearValue, int monthValue)
        {

            Dictionary<string, double> result = new Dictionary<string, double>() {
                { "0", 0},//小型包间
                { "1", 0},//中型包间
                { "2", 0},//大型包间
                { "3", 0},//情侣包间
                { "4", 0},//豪华包间
                { "5", 0},//商务包间
            };

            ConsumeLogSource.DefaultView.RowFilter = GetRowFilterByTime(timeSpanType, yearValue, monthValue);

            for (int i = 0; i < ConsumeLogSource.DefaultView.Count; i++)
            {
                string roomType = ConsumeLogSource.DefaultView[i]["roomtype"].ToString();
                if (result.ContainsKey(roomType))
                    result[roomType] += (DateTime.Parse(ConsumeLogSource.DefaultView[i]["endtime"].ToString())
                        - DateTime.Parse(ConsumeLogSource.DefaultView[i]["starttime"].ToString())).TotalHours;
            }
            
            return result;
        }




        /// <summary>
        /// 获取房间使用率数据集合
        /// </summary>
        /// <param name="timeSpanType"></param>
        /// <param name="yearValue"></param>
        /// <param name="monthValue"></param>
        /// <returns></returns>
        private ChartValues<double> GetRoomUseRateData(int timeSpanType, int yearValue, int monthValue)
        {
            ChartValues<double> result = new ChartValues<double>();

            Dictionary<int, double> tempResult = GetTempResultDictionaryReturnDouble(timeSpanType, yearValue, monthValue);

            ConsumeLogSource.DefaultView.RowFilter = GetRowFilterByTime(timeSpanType, yearValue, monthValue);

            int allHours = GetAllHours(timeSpanType);

            for (int i = 0; i < ConsumeLogSource.DefaultView.Count; i++)
            {
                DateTime date = DateTime.Parse(ConsumeLogSource.DefaultView[i]["starttime"].ToString());
                if (timeSpanType == 0)
                {
                    tempResult[date.Year] += (DateTime.Parse(ConsumeLogSource.DefaultView[i]["endtime"].ToString()) - DateTime.Parse(ConsumeLogSource.DefaultView[i]["starttime"].ToString())).TotalHours;
                }
                if (timeSpanType == 1)
                {
                    tempResult[date.Month] += (DateTime.Parse(ConsumeLogSource.DefaultView[i]["endtime"].ToString()) - DateTime.Parse(ConsumeLogSource.DefaultView[i]["starttime"].ToString())).TotalHours;
                }
                if (timeSpanType == 2)
                {
                    tempResult[date.Day] += ((DateTime.Parse(ConsumeLogSource.DefaultView[i]["endtime"].ToString()) - DateTime.Parse(ConsumeLogSource.DefaultView[i]["starttime"].ToString())).TotalHours);
                }
            }

            foreach (KeyValuePair<int, double> item in tempResult)
            {
                result.Add(item.Value / allHours);
            }

            return result;
        }

        private int GetAllHours(int timeSpanType) {
            if (ConsumeLogSource.DefaultView.Count == 0)
                return 0;
            int result = 1;

            DateTime date = DateTime.Parse(ConsumeLogSource.DefaultView[0]["starttime"].ToString());
            if (timeSpanType == 0)
                result = DateTime.IsLeapYear(date.Year) ? 366 * 24 : 365 * 24;
            if (timeSpanType == 1)
                result = DateTime.DaysInMonth(date.Year, date.Month) * 24;
            if (timeSpanType == 2)
                result = 24;
            return result * RoomInfoSource.Rows.Count;
        }

        private DataTable GetSongRecordSource() {
            return SongInfoManagementServiceCaller.GetAllSongRecord();
        }

        private DataTable GetSongRecordTimeResultSource(DataTable songRecordSource) {
            DataTable result = InitSongRecordSource(new DataTable());

            //将歌曲的点击量汇总起来[以musicId为key]
            Dictionary<string, RecordNumberAndReleaseDate> tempAllSongNum = new Dictionary<string, RecordNumberAndReleaseDate>();
            for (int i = 0; i < songRecordSource.DefaultView.Count; i++)
            {
                if (tempAllSongNum.ContainsKey(songRecordSource.DefaultView[i]["musicid"].ToString()))
                    tempAllSongNum[songRecordSource.DefaultView[i]["musicid"].ToString()].recordNumber += int.Parse(songRecordSource.DefaultView[i]["clicknum"].ToString());
                else
                {
                    tempAllSongNum.Add(songRecordSource.DefaultView[i]["musicid"].ToString(),
                        new RecordNumberAndReleaseDate(int.Parse(songRecordSource.DefaultView[i]["clicknum"].ToString()), DateTime.Parse(songRecordSource.DefaultView[i]["releasedate"].ToString())));
                }
            }
            //过滤掉点击量不够的，且算出热度[点击量 * (0.5)^([点击量最低时限与出版时间的最大值]与现在时间的时间差/半衰期)]
            for (int i = 0; i < songRecordSource.DefaultView.Count; i++)
            {
                if (tempAllSongNum.Count == 0) break;
                if (tempAllSongNum.ContainsKey(songRecordSource.DefaultView[i]["musicid"].ToString()))
                {
                    if (tempAllSongNum[songRecordSource.DefaultView[i]["musicid"].ToString()].recordNumber < MinRecordNumber)
                    {
                        tempAllSongNum.Remove(songRecordSource.DefaultView[i]["musicid"].ToString());
                        continue;
                    }
                    DataRow row = result.NewRow();
                    row["musicid"] = songRecordSource.DefaultView[i]["musicid"].ToString();
                    row["musicname"] = songRecordSource.DefaultView[i]["musicname"].ToString();
                    row["singername"] = songRecordSource.DefaultView[i]["singername"].ToString();
                    row["releasedate"] = DateTime.Parse(songRecordSource.DefaultView[i]["releasedate"].ToString());
                    row["clicknum"] = tempAllSongNum[songRecordSource.DefaultView[i]["musicid"].ToString()].recordNumber.ToString();

                    DateTime date = DateTime.Parse(songRecordSource.DefaultView[i]["releasedate"].ToString()) > MinClickDate ? DateTime.Parse(songRecordSource.DefaultView[i]["releasedate"].ToString()) : MinClickDate;
                    row["hotnumber"] = (tempAllSongNum[songRecordSource.DefaultView[i]["musicid"].ToString()].recordNumber *
                        Math.Pow(0.5, (DateTime.Today - date).TotalDays / HalfLifeValue)).ToString();
                    result.Rows.Add(row);
                    tempAllSongNum.Remove(songRecordSource.DefaultView[i]["musicid"].ToString());
                }
            }
            return result;
        }

        private DataTable InitSongRecordSource(DataTable dataTable) {
            if (!dataTable.Columns.Contains("musicid"))
                dataTable.Columns.Add("musicid");
            if (!dataTable.Columns.Contains("musicname"))
                dataTable.Columns.Add("musicname");
            if (!dataTable.Columns.Contains("singername"))
                dataTable.Columns.Add("singername");
            if (!dataTable.Columns.Contains("clicknum"))
                dataTable.Columns.Add("clicknum");
            if (!dataTable.Columns.Contains("releasedate"))
                dataTable.Columns.Add("releasedate").DataType = typeof(DateTime);
            if (!dataTable.Columns.Contains("hotnumber"))
                dataTable.Columns.Add("hotnumber").DataType = typeof(double);
            return dataTable;
        }

        #region XML

        private void LoadSongHotConfigFile() {
            string ConfigFileName = AppDomain.CurrentDomain.BaseDirectory + @"Configs\SongHotConfig\SongHotConfig.xml";

            if (!File.Exists(ConfigFileName))
            {
                CreateSongHotConfigXMLFile();
            }
            else
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(ConfigFileName);

                XmlElement SongHotConfigNode = xmlDoc.DocumentElement;
                if (!string.IsNullOrEmpty(SongHotConfigNode.GetElementsByTagName("MinClickDate").Item(0).InnerText))
                    MinClickDate = DateTime.Parse(SongHotConfigNode.GetElementsByTagName("MinClickDate").Item(0).InnerText);
                if (!string.IsNullOrEmpty(SongHotConfigNode.GetElementsByTagName("MinClickNum").Item(0).InnerText))
                    MinRecordNumber = int.Parse(SongHotConfigNode.GetElementsByTagName("MinClickNum").Item(0).InnerText);
                if (!string.IsNullOrEmpty(SongHotConfigNode.GetElementsByTagName("HalfLifeValue").Item(0).InnerText))
                    HalfLifeValue = int.Parse(SongHotConfigNode.GetElementsByTagName("HalfLifeValue").Item(0).InnerText);
                if (!string.IsNullOrEmpty(SongHotConfigNode.GetElementsByTagName("SingerMinClickDate").Item(0).InnerText))
                    SingerMinClickDate = DateTime.Parse(SongHotConfigNode.GetElementsByTagName("SingerMinClickDate").Item(0).InnerText);
            }
        }

        private void UpdateSongHotConfigFile() {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Configs\SongHotConfig\SongHotConfig.xml")) {
                CreateSongHotConfigXMLFile();
            }
            UpdateSongHotConfig(MinClickDate.ToString(), MinRecordNumber.ToString(), HalfLifeValue.ToString());
        }

        private void CreateSongHotConfigXMLFile() {
            XmlDocument xmlDoc = new XmlDocument();

            //添加配置行
            XmlDeclaration xmlSM = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlSM);

            //添加根节点
            XmlElement xml = xmlDoc.CreateElement("", "SongHotConfig", "");
            xmlDoc.AppendChild(xml);

            XmlNode SongHotConfig = xmlDoc.SelectSingleNode("SongHotConfig");

            //添加 最低发行日期 ， 最低点击量 ， 半衰期 节点
            //NewSongHotConfig.SetAttribute("name", "XXX");
            XmlElement MinClickDate = xmlDoc.CreateElement("MinClickDate");
            //InnerText:获取或设置节点及其所有子节点的串连值
            SongHotConfig.AppendChild(MinClickDate);
            XmlElement MinClickNum = xmlDoc.CreateElement("MinClickNum");
            SongHotConfig.AppendChild(MinClickNum);
            XmlElement HalfLifeValue = xmlDoc.CreateElement("HalfLifeValue");
            SongHotConfig.AppendChild(HalfLifeValue);

            // 添加 歌手点击量统计起始时间
            XmlElement SingerMinClickDate = xmlDoc.CreateElement("SingerMinClickDate");
            SongHotConfig.AppendChild(SingerMinClickDate);

            //保存好创建的XML文档
            xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory + @"Configs\SongHotConfig\SongHotConfig.xml");
        }

        private void UpdateSongHotConfig(string MinClickDate, string MinClickNum, string HalfLifeValue)
        {
            var ConfigFileName = AppDomain.CurrentDomain.BaseDirectory + @"Configs\SongHotConfig\SongHotConfig.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigFileName);

            XmlElement SongHotConfigNode = xmlDoc.DocumentElement;
            SongHotConfigNode.GetElementsByTagName("MinClickDate").Item(0).InnerText = MinClickDate;
            SongHotConfigNode.GetElementsByTagName("MinClickNum").Item(0).InnerText = MinClickNum;
            SongHotConfigNode.GetElementsByTagName("HalfLifeValue").Item(0).InnerText = HalfLifeValue;

            xmlDoc.Save(ConfigFileName);
        }

        private void UpdateSingerHotConfigFile() {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Configs\SongHotConfig\SongHotConfig.xml"))
            {
                CreateSongHotConfigXMLFile();
            }
            UpdateSingerHotConfig(SingerMinClickDate.ToString());
        }
        
        private void UpdateSingerHotConfig(string SingerMinClickDate)
        {
            var ConfigFileName = AppDomain.CurrentDomain.BaseDirectory + @"Configs\SongHotConfig\SongHotConfig.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(ConfigFileName);

            XmlElement SongHotConfigNode = xmlDoc.DocumentElement;
            SongHotConfigNode.GetElementsByTagName("SingerMinClickDate").Item(0).InnerText = SingerMinClickDate;

            xmlDoc.Save(ConfigFileName);
        }

        #endregion


        private DataTable GetSingerRecordTimeResultSource(DataTable songRecordSource)
        {
            DataTable result = InitSingerRecordSource(new DataTable());

            //将歌手的点击量汇总起来[以singerId为key]
            Dictionary<string, SingerNameAndClickNum> tempAllSingerNum = new Dictionary<string, SingerNameAndClickNum>();
            for (int i = 0; i < songRecordSource.DefaultView.Count; i++)
            {
                if (tempAllSingerNum.ContainsKey(songRecordSource.DefaultView[i]["singerid"].ToString()))
                    tempAllSingerNum[songRecordSource.DefaultView[i]["singerid"].ToString()].clickNum += int.Parse(songRecordSource.DefaultView[i]["clicknum"].ToString());
                else
                {
                    tempAllSingerNum.Add(songRecordSource.DefaultView[i]["singerid"].ToString(),
                        new SingerNameAndClickNum(songRecordSource.DefaultView[i]["singername"].ToString(), int.Parse(songRecordSource.DefaultView[i]["clicknum"].ToString())));
                }
            }
            //输出
            foreach (KeyValuePair<string, SingerNameAndClickNum> item in tempAllSingerNum)
            {
                DataRow row = result.NewRow();
                row["singerid"] = item.Key;
                row["singername"] = item.Value.singerName;
                row["clicknum"] = item.Value.clickNum;
                result.Rows.Add(row);
            }
            return result;
        }

        private DataTable InitSingerRecordSource(DataTable dataTable)
        {
            if (!dataTable.Columns.Contains("singerId"))
                dataTable.Columns.Add("singerId");
            if (!dataTable.Columns.Contains("singername"))
                dataTable.Columns.Add("singername");
            if (!dataTable.Columns.Contains("clicknum"))
                dataTable.Columns.Add("clicknum").DataType = typeof(int);
            return dataTable;
        }


        /// <summary>
        /// 获取某一歌曲数据集合
        /// </summary>
        /// <param name="timeSpanType"></param>
        /// <param name="yearValue"></param>
        /// <param name="monthValue"></param>
        /// <returns></returns>
        private ChartValues<int> GetSongData(int timeSpanType, int yearValue, int monthValue, string musicId)
        {
            ChartValues<int> result = new ChartValues<int>();

            Dictionary<int, int> tempResult = GetTempResultDictionary(timeSpanType, yearValue, monthValue);

            SongRecordSource.DefaultView.RowFilter = $"{GetSongRowFilterByTime(timeSpanType, yearValue, monthValue)} and musicid = {musicId}";

            for (int i = 0; i < SongRecordSource.DefaultView.Count; i++)
            {
                DateTime date = DateTime.Parse(SongRecordSource.DefaultView[i]["clickdate"].ToString());
                if (timeSpanType == 0)
                    tempResult[date.Year] = tempResult[date.Year] + int.Parse(SongRecordSource.DefaultView[i]["clicknum"].ToString());
                if (timeSpanType == 1)
                    tempResult[date.Month] = tempResult[date.Month] + int.Parse(SongRecordSource.DefaultView[i]["clicknum"].ToString());
                if (timeSpanType == 2)
                    tempResult[date.Day] = tempResult[date.Day] + int.Parse(SongRecordSource.DefaultView[i]["clicknum"].ToString());
            }

            foreach (KeyValuePair<int, int> item in tempResult)
            {
                result.Add(item.Value);
            }

            return result;
        }

        private string GetSongRowFilterByTime(int timeSpanType, int yearValue, int monthValue)
        {
            if (timeSpanType == 0)
            {
                DateTime startDate = DateTime.Now.AddYears(-10);
                return $"clickdate >= ('{startDate}')";
            }
            if (timeSpanType == 1)
            {
                DateTime startDate = new DateTime(yearValue, 1, 1);
                DateTime endDate = new DateTime(yearValue + 1, 1, 1).AddDays(-1);
                return $"clickdate >= ('{startDate}') and clickdate <= ('{endDate}')";
            }
            if (timeSpanType == 2)
            {
                DateTime startDate = new DateTime(yearValue, monthValue, 1);
                DateTime endDate = new DateTime(yearValue, monthValue, 1).AddDays(DateTime.DaysInMonth(yearValue, monthValue) - 1);
                return $"clickdate >= ('{startDate}') and clickdate <= ('{endDate}')";
            }
            else
                return string.Empty;
        }

        #endregion      
    }

    internal class RecordNumberAndReleaseDate {
        public int recordNumber;
        public DateTime releaseDate;
        public RecordNumberAndReleaseDate(int recordNumber, DateTime releaseDate) {
            this.recordNumber = recordNumber;
            this.releaseDate = releaseDate;
        }
    }

    internal class SingerNameAndClickNum {
        public string singerName;
        public int clickNum;
        public SingerNameAndClickNum(string singerName, int clickNum)
        {
            this.singerName = singerName;
            this.clickNum = clickNum;
        }
    }
}