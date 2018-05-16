using KtvStudio.Helpers;
using KtvStudio.ViewModels;
using KtvStudio.Views;
using System.ComponentModel;
using System.Data;
using System.Windows.Controls;

namespace KtvStudio
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow: INotifyPropertyChanged
    {
        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region ClientViewModel

        private ClientViewModel _ClientViewModel = new ClientViewModel();

        public ClientViewModel ClientViewModel
        {
            get { return _ClientViewModel; }
            set
            {
                if (_ClientViewModel != null && _ClientViewModel.Equals(value)) return;
                _ClientViewModel = value;
                RaisePropertyChanged("ClientViewModel");

            }
        }

        #endregion
        
        public MainWindow(SplashScreen splashScreen)
        {
            InitializeComponent();
            ClientViewModel.mainWindow = ThisWindow;
            ClientViewModel.songManageUc = SongManageUc;
            splashScreen?.close();

            new LoginWnd(ClientViewModel).ShowDialog();

            if (ClientViewModel.CurrentStaffInfo.Authority == 1)
                Menu.SelectedTabIndex = 1;
            else {
                //操作员和开发人员定期扫描房间状态，快到时间的包间弹出提示
                ClientViewModel.RoomEndTimeHintTimer();
            }
        }

        #region RoomInfo

        private void RoomInfoSourceDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ClientViewModel.OnRoomInfoEdit();
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.SelectedItem == null)
                ClientViewModel.RoomInfoSelectedItem = null;
            else
            {
                ClientViewModel.RoomInfoSelectedItem = (dataGrid.SelectedItem as DataRowView).Row;
            }
        }

        private void RoomTypeTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            TabItem selectedItem = tabControl.SelectedItem as TabItem;
            string roomType = selectedItem.Header.ToString();
            if (roomType.Equals("全部"))
                ClientViewModel.RoomInfoSource.DefaultView.RowFilter = string.Empty;
            else
                ClientViewModel.RoomInfoSource.DefaultView.RowFilter = $"roomtype = '{roomType}'";
        }

        #region 快捷键

        private void RoomInfoNew_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RoomInfoNew_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoAdd();
        }

        private void RoomInfoEdit_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RoomInfoEdit_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoEdit();
        }

        private void RoomInfoEditCancel_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RoomInfoEditCancel_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoCancel();
        }

        private void RoomInfoRemove_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RoomInfoRemove_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoDelete();
        }

        private void RoomInfoSave_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RoomInfoSave_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoSave();
        }

        private void RoomInfoFresh_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RoomInfoFresh_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoSave();
        }

        #endregion

        #endregion

        #region RoomTask

        private void RoomTaskDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.SelectedItem == null)
                ClientViewModel.RoomTaskSelectedItem = null;
            else
            {
                ClientViewModel.RoomTaskSelectedItem = (dataGrid.SelectedItem as DataRowView).Row;
                if (ClientViewModel.RoomTaskSelectedItem["roomstate"].ToString().Equals("0"))
                    ClientViewModel.RoomTaskSelectedItemStateIsUsing = false;
                else if (ClientViewModel.RoomTaskSelectedItem["roomstate"].ToString().Equals("1"))
                    ClientViewModel.RoomTaskSelectedItemStateIsUsing = true;
            }
        }

        private void RoomTaskRoomTypeTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            TabItem selectedItem = tabControl.SelectedItem as TabItem;
            string roomType = selectedItem.Header.ToString();
            if (roomType.Equals("全部"))
                ClientViewModel.RoomTaskSource.DefaultView.RowFilter = string.Empty;
            else
                ClientViewModel.RoomTaskSource.DefaultView.RowFilter = $"roomtype = '{roomType}'";
        }

        private void RoomTaskDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (ClientViewModel.RoomTaskSelectedItemStateIsUsing)
                ClientViewModel.OnRoomTaskRenew();
            else
                ClientViewModel.OnRoomTaskAdd();
        }

        #endregion

        #region SingerInfo
        
        private void SingerInfoDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.SelectedItem == null)
                ClientViewModel.SingerInfoSelectedItem = null;
            else
            {
                ClientViewModel.SingerInfoSelectedItem = (dataGrid.SelectedItem as DataRowView).Row;
            }
        }

        private void SingerInfoSourceDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ClientViewModel.OnSingerInfoEdit();
        }
        
        private void SingerAreaFilterTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == null) return;
            TabControl tabControl = sender as TabControl;
            TabItem selectedItem = tabControl.SelectedItem as TabItem;
            if (selectedItem.Header.ToString().Contains("华语"))
                ClientViewModel.SingerInfoNationality = "中国";
            else if (selectedItem.Header.ToString().Contains("日本"))
                ClientViewModel.SingerInfoNationality = "日本";
            else if (selectedItem.Header.ToString().Contains("韩国"))
                ClientViewModel.SingerInfoNationality = "韩国";
            else if (selectedItem.Header.ToString().Contains("欧美"))
                ClientViewModel.SingerInfoNationality = "美国";
            else if (selectedItem.Header.ToString().Contains("全部"))
            {
                ClientViewModel.SingerInfoNationality = string.Empty;
                ClientViewModel.SingerInfoSex = string.Empty;
            }

            if (selectedItem.Header.ToString().Contains("男"))
                ClientViewModel.SingerInfoSex = "0";
            else if (selectedItem.Header.ToString().Contains("女"))
                ClientViewModel.SingerInfoSex = "1";
            else if (selectedItem.Header.ToString().Contains("组合"))
                ClientViewModel.SingerInfoSex = "2";
        }

        private void SingerInitialFilterTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == null) return;
            TabControl tabControl = sender as TabControl;
            TabItem selectedItem = tabControl.SelectedItem as TabItem;
            ClientViewModel.SingerInfoInitial = selectedItem.Header.ToString();
            if (string.IsNullOrEmpty(ClientViewModel.SingerInfoInitial) || ClientViewModel.SingerInfoInitial.Equals("热门"))
                 ClientViewModel.SingerInfoInitial = string.Empty;
        }

        #region 快捷键

        private void SingerInfoNew_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SingerInfoNew_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnSingerInfoAdd();
        }

        private void SingerInfoEdit_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SingerInfoEdit_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnSingerInfoEdit();
        }

        private void SingerInfoEditCancel_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;;
        }

        private void SingerInfoEditCancel_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnSingerInfoCancel();
        }

        private void SingerInfoRemove_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SingerInfoRemove_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnSingerInfoDelete();
        }

        private void SingerInfoSave_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SingerInfoSave_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnSingerInfoSave();
        }

        private void SingerInfoFresh_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SingerInfoFresh_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnGetAllSingerInfo();
        }

        #endregion

        #endregion
    }
}