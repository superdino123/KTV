using RoomInfoManagementService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HostAllServices
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        #region INotifyPropertyChanged RaisePropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region List (INotifyPropertyChanged Property)

        private List<string> _List = new List<string>();

        public List<string> List
        {
            get { return _List; }
            set
            {
                if (_List != null && _List.Equals(value)) return;
                _List = value;
                RaisePropertyChanged("List");

            }
        }

        #endregion
        
        private static ServiceHost RoomInfoManagementServiceHost;
        private static ServiceHost RoomTaskManagementServiceHost;

        public MainWindow()
        {
            InitializeComponent();
            List.Add("ServiceHost");
            try
            {
                RoomInfoManagementServiceHost = new ServiceHost(typeof(RoomInfoManagementImplementation));
                RoomInfoManagementServiceHost.Open();
                RoomTaskManagementServiceHost = new ServiceHost(typeof(RoomTaskManagementImplementation));
                RoomTaskManagementServiceHost.Open();
            }
            catch (Exception ex)
            {
                List.Add(ex.Message);
                throw;
            }
            List.Add("Started");
        }
    }
}
