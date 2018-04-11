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
        
        public MainWindow()
        {
            InitializeComponent();
            ClientViewModel.mainWindow = ThisWindow;
        }

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

        private void RoomTaskDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.SelectedItem == null)
                ClientViewModel.RoomTaskSelectedItem = null;
            else
            {
                ClientViewModel.RoomTaskSelectedItem = (dataGrid.SelectedItem as DataRowView).Row;
            }
        }
        
    }
}
