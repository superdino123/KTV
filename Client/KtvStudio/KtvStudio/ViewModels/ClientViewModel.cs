using Helpers;
using Helpers.Commands;
using Helpers.FtpHelper;
using KtvStudio.Helpers;
using KtvStudio.Helpers.QiniuService;
using KtvStudio.RoomInfoService;
using KtvStudio.RoomTaskService;
using KtvStudio.Views;
using Microsoft.Win32;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace KtvStudio.ViewModels
{
    public class ClientViewModel: ViewModelBase
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
            RoomTypeSource = RoomInfoManagementServiceCaller.GetActionSource("0001");
        }

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
            if (RoomTaskEditItem.CustomerId != null)
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
            if (!UserInfoWhenRenewNoVisibility)
                UserInfoWhenRenewNoVisibility = true;
            if (roomTaskEditWnd == null)
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
            //隐藏人员信息栏目
            UserInfoWhenRenewNoVisibility = true;
            //打开界面
            if (roomTaskEditWnd == null)
                roomTaskEditWnd = new RoomTaskEditWnd(mainWindow.ClientViewModel);
            roomTaskEditWnd.Show();
        }

        public bool CanRoomTaskRenew()
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

        /// <summary>
        /// 获取当前预计消费
        /// </summary>
        /// <param name="roomTaskEditItem"></param>
        /// <returns></returns>
        public void getRoomConsume(RoomTask roomTaskEditItem) {
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
            RoomInfoEditItem.RoomId = RoomInfoSelectedItem[ROOMINFO.ROOMID].ToString();
            RoomInfoEditItem.RoomType = RoomInfoSelectedItem[ROOMINFO.ROOMTYPE].ToString();
            RoomInfoEditItem.RoomSize = RoomInfoSelectedItem[ROOMINFO.ROOMSIZE].ToString();
            RoomInfoEditItem.MicroPhoneNumber = RoomInfoSelectedItem[ROOMINFO.MICROPHONENUMBER].ToString();
            RoomInfoEditItem.ImageUrl = RoomInfoSelectedItem[ROOMINFO.IMAGEURL].ToString();
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

        #region RoomInfoSaveCmd

        private RelayCommand _RoomInfoSaveCmd;

        public ICommand RoomInfoSaveCmd
        {
            get { return _RoomInfoSaveCmd ?? (_RoomInfoSaveCmd = new RelayCommand(param => OnRoomInfoSave(), param => CanRoomInfoSave())); }
        }

        public void OnRoomInfoSave()
        {
            if (RoomInfoEditItem == null)
                return;
            //还原房间类型
            if (RoomInfoEditItem.RoomType.Equals("小型包间"))
                RoomInfoEditItem.RoomType = "0";
            if (RoomInfoEditItem.RoomType.Equals("中型包间"))
                RoomInfoEditItem.RoomType = "1";
            if (RoomInfoEditItem.RoomType.Equals("大型包间"))
                RoomInfoEditItem.RoomType = "2";
            if (RoomInfoEditItem.RoomType.Equals("情侣包间"))
                RoomInfoEditItem.RoomType = "3";
            if (RoomInfoEditItem.RoomType.Equals("豪华包间"))
                RoomInfoEditItem.RoomType = "4";
            if (RoomInfoEditItem.RoomType.Equals("商务包间"))
                RoomInfoEditItem.RoomType = "5";

            if ((RoomInfoAddOrUpdate && RoomInfoManagementServiceCaller.InsertRoomInfo(RoomInfoEditItem) != 0) ||
                !RoomInfoAddOrUpdate && RoomInfoManagementServiceCaller.UpdateRoomInfo(RoomInfoEditItem) != 0)
            {
                RoomInfoEditVisibility = false;
                RoomInfoSource = GetRoomInfoSource();
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

            string saveKey = (Guid.NewGuid().ToString() + op.FileName.Substring(op.FileName.LastIndexOf('.'))).Replace("-","");
            //上传图片到七牛云
            QiniuService.UploadImage(saveKey, op.FileName);
            RoomInfoEditItem.ImageUrl = QiniuService.QINIU_URL + "/" + saveKey;
            //上传图片到ftp
            //RoomImageUploadToFtp(op.FileName);
        }

        public bool CanRoomImageUpLoad()
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
                if (dataTable.Rows[i]["ROOMTYPE"].Equals("0"))
                    dataTable.Rows[i]["ROOMTYPE"] = "小型包间";
                if (dataTable.Rows[i]["ROOMTYPE"].Equals("1"))
                    dataTable.Rows[i]["ROOMTYPE"] = "中型包间";
                if (dataTable.Rows[i]["ROOMTYPE"].Equals("2"))
                    dataTable.Rows[i]["ROOMTYPE"] = "大型包间";
                if (dataTable.Rows[i]["ROOMTYPE"].Equals("3"))
                    dataTable.Rows[i]["ROOMTYPE"] = "情侣包间";
                if (dataTable.Rows[i]["ROOMTYPE"].Equals("4"))
                    dataTable.Rows[i]["ROOMTYPE"] = "豪华包间";
                if (dataTable.Rows[i]["ROOMTYPE"].Equals("5"))
                    dataTable.Rows[i]["ROOMTYPE"] = "商务包间";
            }
            
            return dataTable;
        }
        
      
        #endregion

        #endregion
    }
}