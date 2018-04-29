using KtvStudio.Helpers;
using KtvStudio.ViewModels;
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

            splashScreen?.close();
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
            ClientViewModel.CanRoomInfoAdd();
        }

        private void RoomInfoNew_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoAdd();
        }

        private void RoomInfoEdit_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoEdit();
        }

        private void RoomInfoEdit_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoEdit();
        }

        private void RoomInfoEditCancel_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoCancel();
        }

        private void RoomInfoEditCancel_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoCancel();
        }

        private void RoomInfoRemove_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoDelete();
        }

        private void RoomInfoRemove_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoDelete();
        }

        private void RoomInfoSave_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)
        {
            ClientViewModel.OnRoomInfoSave();
        }

        private void RoomInfoSave_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
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

        #endregion
    }
}