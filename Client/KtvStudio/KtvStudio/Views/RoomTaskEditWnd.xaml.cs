using KtvStudio.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KtvStudio.Views
{
    /// <summary>
    /// RoomTaskEditWnd.xaml 的交互逻辑
    /// </summary>
    public partial class RoomTaskEditWnd : INotifyPropertyChanged
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

        private ClientViewModel _ClientViewModel;

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
        
        public RoomTaskEditWnd(ClientViewModel clientViewModel)
        {
            InitializeComponent();
            ClientViewModel = clientViewModel;
        }
        
        private void DateTimePicker_SelectedTimeChanged(object sender, MahApps.Metro.Controls.TimePickerBaseSelectionChangedEventArgs<TimeSpan?> e)
        {
            ClientViewModel.getRoomConsume(ClientViewModel.RoomTaskEditItem);
        }

        private void DateTimePicker_SelectedDateChanged(object sender, MahApps.Metro.Controls.TimePickerBaseSelectionChangedEventArgs<DateTime?> e)
        {
            ClientViewModel.getRoomConsume(ClientViewModel.RoomTaskEditItem);
        }
    }
}
