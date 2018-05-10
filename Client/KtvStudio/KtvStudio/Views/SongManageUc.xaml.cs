using KtvStudio.ViewModels;
using MahApps.Metro.Controls;
using System;
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
    /// SongManageUc.xaml 的交互逻辑
    /// </summary>
    public partial class SongManageUc : UserControl, INotifyPropertyChanged
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

        public SongManageUc()
        {
            InitializeComponent();

            DataContextChanged += delegate
            {
                ClientViewModel = (ClientViewModel)DataContext;
                AddNewSongButtonInGrid();
            };
        }
        
        private void SongInfoDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.SelectedItem == null)
                ClientViewModel.SongInfoSelectedItem = null;
            else
            {
                ClientViewModel.SongInfoSelectedItem = (dataGrid.SelectedItem as DataRowView).Row;
            }
        }

        private void MusicTypeTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == null) return;
            string type = string.Empty;
            string key = string.Empty;
            TabControl tabControl = sender as TabControl;
            TabItem selectedItem = tabControl.SelectedItem as TabItem;
            if (selectedItem.Header.Equals("主 题") && MusicTypeTopic.SelectedItem != null)
                type = (MusicTypeTopic.SelectedItem as TabItem).Header.ToString();
            if (selectedItem.Header.Equals("心 情") && MusicTypeMood.SelectedItem != null)
                type = (MusicTypeMood.SelectedItem as TabItem).Header.ToString();
            if (selectedItem.Header.Equals("音乐风格") && MusicTypeStyle.SelectedItem != null)
                type = (MusicTypeStyle.SelectedItem as TabItem).Header.ToString();
            ClientViewModel.CategorySourceDict.TryGetValue(type, out key);
            if(!string.IsNullOrEmpty(key))
                ClientViewModel.SongInfoFilter = $"category like '{key}' or " +
                    $" category like '{key},%' or " +
                    $" category like '%,{key},%' or " +
                    $" category like '%,{key}' ";
        }

        private void MusicSingerAreaTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == null) return;
            TabControl tabControl = sender as TabControl;
            TabItem selectedItem = tabControl.SelectedItem as TabItem;
            if (selectedItem.Header.ToString().Contains("华语"))
                ClientViewModel.SongSingerInfoNationality = "中国";
            else if (selectedItem.Header.ToString().Contains("日本"))
                ClientViewModel.SongSingerInfoNationality = "日本";
            else if (selectedItem.Header.ToString().Contains("韩国"))
                ClientViewModel.SongSingerInfoNationality = "韩国";
            else if (selectedItem.Header.ToString().Contains("欧美"))
                ClientViewModel.SongSingerInfoNationality = "美国";
            else if (selectedItem.Header.ToString().Contains("全部"))
            {
                ClientViewModel.SongSingerInfoNationality = string.Empty;
                ClientViewModel.SongSingerInfoSex = string.Empty;
            }

            if (selectedItem.Header.ToString().Contains("男"))
                ClientViewModel.SongSingerInfoSex = "0";
            else if (selectedItem.Header.ToString().Contains("女"))
                ClientViewModel.SongSingerInfoSex = "1";
            else if (selectedItem.Header.ToString().Contains("组合"))
                ClientViewModel.SongSingerInfoSex = "2";
        }

        private void MusicSingerNameTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == null) return;
            TabControl tabControl = sender as TabControl;
            TabItem selectedItem = tabControl.SelectedItem as TabItem;
            ClientViewModel.SongSingerInfoInitial = selectedItem.Header.ToString();
            if (string.IsNullOrEmpty(ClientViewModel.SongSingerInfoInitial) || ClientViewModel.SongSingerInfoInitial.Equals("热门"))
                ClientViewModel.SongSingerInfoInitial = string.Empty;
        }

        #region Method

        public void AddSingerInfo() {
            ClientViewModel.AddSingerInfo(SingerInfoFilterWrapPanel);
        }
        
        private void SongInfoDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClientViewModel.OnSongInfoEdit();
        }

        public void AddNewSongButtonInGrid() {
            ClientViewModel.AddNewSongButtonInGrid(NewSongGrid);
        }

        #endregion

        private void SongBasicTypeTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == null) return;
            TabControl tabControl = sender as TabControl;
            TabItem selectedItem = tabControl.SelectedItem as TabItem;
            
            if (!selectedItem.Header.Equals("歌 星") && ClientViewModel.SingerSelectedInfoVisibility)
                ClientViewModel.SingerSelectedInfoVisibility = false;
            else if (selectedItem.Header.Equals("歌 星") && !ClientViewModel.SingerSelectedInfoVisibility)
                ClientViewModel.SingerSelectedInfoVisibility = true;
            else if(selectedItem.Header.Equals("分 类"))
            {
                string key = string.Empty;
                ClientViewModel.CategorySourceDict.TryGetValue("流行", out key);
                if (!string.IsNullOrEmpty(key))
                    ClientViewModel.SongInfoFilter = $"category like '{key}' or " +
                        $" category like '{key},%' or " +
                        $" category like '%,{key},%' or " +
                        $" category like '%,{key}' ";
            }
            else if (selectedItem.Header.Equals("新 歌"))
            {
                DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                ClientViewModel.SongInfoFilter = $"releasedate > ('{date}')";
            }
            //取消编辑状态
            ClientViewModel.SongInfoEditVisibility = false;
            ClientViewModel.songInfoEditUc.vedio.Close();
        }

        private void SongRankTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == null) return;
            TabControl tabControl = sender as TabControl;
            TabItem selectedItem = tabControl.SelectedItem as TabItem;

            if (selectedItem.Header.Equals("华语榜"))
            {
                DateTime time = DateTime.Now.AddMonths(-100);//-6
                ClientViewModel.SongInfoFilter = $"(languagetype = '0001' or languagetype = '0002') and releasedate > ('{time}')";
                ClientViewModel.SongInfoSort = $"recordnumber desc";
            }
            else if (selectedItem.Header.Equals("情歌对唱"))
            {
                DateTime time = DateTime.Now.AddYears(-5);
                string key = string.Empty;
                ClientViewModel.CategorySourceDict.TryGetValue("情歌对唱", out key);
                if (!string.IsNullOrEmpty(key))
                    ClientViewModel.SongInfoFilter = $"category like '{key}' or " +
                        $" category like '{key},%' or " +
                        $" category like '%,{key},%' or " +
                        $" category like '%,{key}' and releasedate > ('{time}')";
                ClientViewModel.SongInfoSort = $"recordnumber desc";
            }
            else if (selectedItem.Header.Equals("影视金曲"))
            {
                DateTime time = DateTime.Now.AddMonths(-100);//-6
                string key = string.Empty;
                ClientViewModel.CategorySourceDict.TryGetValue("影视", out key);
                if (!string.IsNullOrEmpty(key))
                    ClientViewModel.SongInfoFilter = $"category like '{key}' or " +
                        $" category like '{key},%' or " +
                        $" category like '%,{key},%' or " +
                        $" category like '%,{key}' and releasedate > ('{time}')";
                ClientViewModel.SongInfoSort = $"recordnumber desc";
            }
            else if (selectedItem.Header.Equals("经典老歌"))
            {
                ClientViewModel.SongInfoFilter = $"";
                ClientViewModel.SongInfoSort = $"recordnumber desc";
            }
            else if (selectedItem.Header.Equals("新歌榜"))
            {
                DateTime time = DateTime.Now.AddDays(-100);
                ClientViewModel.SongInfoFilter = $"releasedate > ('{time}')";
                ClientViewModel.SongInfoSort = $"recordnumber desc";
            }
        }

    }
}