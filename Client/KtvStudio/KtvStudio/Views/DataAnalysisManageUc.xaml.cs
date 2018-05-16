using KtvStudio.ViewModels;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KtvStudio.Views
{
    /// <summary>
    /// DataAnalysisManageUc.xaml 的交互逻辑
    /// </summary>
    public partial class DataAnalysisManageUc : UserControl, INotifyPropertyChanged
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

        public DataAnalysisManageUc()
        {
            InitializeComponent();

            DataContextChanged += delegate
            {
                ClientViewModel = (ClientViewModel)DataContext;
            };
        }

        private void SongTimeHot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClientViewModel.SongSelectedDataRowViews.Clear();
            DataGrid dataGrid = sender as DataGrid;
            IList rows = dataGrid.SelectedItems;
            for (int i = 0; i < rows.Count; i++)
            {
                DataRowView item = rows[i] as DataRowView;
                ClientViewModel.SongSelectedDataRowViews.Add(item);
            }
        }
    }
}
