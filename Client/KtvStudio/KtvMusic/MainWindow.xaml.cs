using KtvMusic.SongInfoService;
using KtvMusic.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KtvMusic
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region MusicViewModel

        private MusicViewModel _MusicViewModel = new MusicViewModel();

        public MusicViewModel MusicViewModel
        {
            get { return _MusicViewModel; }
            set
            {
                if (_MusicViewModel != null && _MusicViewModel.Equals(value)) return;
                _MusicViewModel = value;
                RaisePropertyChanged("MusicViewModel");

            }
        }

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            MusicViewModel.mainWindow = this;
            MusicViewModel.singerSearchUc = singerSearchUc;
            MusicViewModel.singerFilterListUc = singerFilterListUc;
            MusicViewModel.selectedSongListUc = selectedSongListUc;

            Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            List<SongRecord> result = new List<SongRecord>();
            //提交歌曲点击量
            foreach (KeyValuePair<string, int> item in MusicViewModel.TempSongRecordNumberDict)
            {
                result.Add(new SongRecord() { SongId = item.Key, ClickNum = item.Value });
            }
            SongRecord[] resultArr = result.ToArray();
            MusicViewModel.SongInfoManagementServiceCaller.AddSongRecord(resultArr);
        }

        private void CloseWindow_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CloseWindow_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}
