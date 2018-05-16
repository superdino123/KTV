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

            if (!selectedItem.Header.Equals("歌 星"))
            {
                ClientViewModel.IsSelectedSingerSearch = false;
                if (ClientViewModel.SingerSelectedInfoVisibility)
                    ClientViewModel.SingerSelectedInfoVisibility = false;

                if (selectedItem.Header.Equals("分 类"))
                {
                    string key = string.Empty;
                    ClientViewModel.CategorySourceDict.TryGetValue("流行", out key);
                    if (!string.IsNullOrEmpty(key))
                        ClientViewModel.SongInfoFilter = $"category like '{key}' or " +
                            $" category like '{key},%' or " +
                            $" category like '%,{key},%' or " +
                            $" category like '%,{key}' ";
                }
                else if (selectedItem.Header.Equals("排 行"))
                {
                    ClientViewModel.SongInfoSort = $"newsonghot desc";
                    ClientViewModel.SongInfoFilter = $"languagetype in ('0001', '0002')";
                    if (ClientViewModel.SongInfoSource.DefaultView.Count != 0)
                        if (ClientViewModel.SongInfoSource.DefaultView.Count <= 100)
                            ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[ClientViewModel.SongInfoSource.DefaultView.Count - 1]["newsonghot"].ToString()} and {ClientViewModel.SongInfoFilter}";
                        else
                            ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[99]["newsonghot"].ToString()} and {ClientViewModel.SongInfoFilter}";
                }
                else if (selectedItem.Header.Equals("新 歌"))
                {
                    DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    ClientViewModel.SongInfoFilter = $"releasedate > ('{date}')";
                }

            }
            else if (selectedItem.Header.Equals("歌 星")) {
                ClientViewModel.IsSelectedSingerSearch = true;
                if (!ClientViewModel.SingerSelectedInfoVisibility)
                    ClientViewModel.SingerSelectedInfoVisibility = true;
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
                /*
                 * 华语榜排序规则
                 * 1.按热度字段排序
                 * 2.去掉非国语和粤语类别的
                 * 3.最多选取100首
                 */
                ClientViewModel.SongInfoSort = $"newsonghot desc";
                ClientViewModel.SongInfoFilter = $"languagetype in ('0001', '0002')";
                if (ClientViewModel.SongInfoSource.DefaultView.Count != 0)
                    if (ClientViewModel.SongInfoSource.DefaultView.Count <= 100)
                        ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[ClientViewModel.SongInfoSource.DefaultView.Count - 1]["newsonghot"].ToString()} and {ClientViewModel.SongInfoFilter}";
                    else
                        ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[99]["newsonghot"].ToString()} and {ClientViewModel.SongInfoFilter}";
            }
            else if (selectedItem.Header.Equals("情歌对唱"))
            {
                /*
                 * 情歌对唱排序规则
                 * 1.按热度字段排序
                 * 2.去掉非情歌对唱类别的
                 * 3.最多选取100首
                 */
                ClientViewModel.SongInfoSort = $"newsonghot desc";
                string key = string.Empty;
                ClientViewModel.CategorySourceDict.TryGetValue("情歌对唱", out key);
                if (!string.IsNullOrEmpty(key))
                {
                    string categoryFilter = $" category like '{key}' or " +
                                            $" category like '{key},%' or " +
                                            $" category like '%,{key},%' or " +
                                            $" category like '%,{key}' ";
                    ClientViewModel.SongInfoFilter = categoryFilter;
                    if (ClientViewModel.SongInfoSource.DefaultView.Count != 0)
                        if (ClientViewModel.SongInfoSource.DefaultView.Count <= 100)
                            ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[ClientViewModel.SongInfoSource.DefaultView.Count - 1]["newsonghot"].ToString()} and {categoryFilter}";
                        else
                            ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[99]["newsonghot"].ToString()} and {categoryFilter}";
                }
            }
            else if (selectedItem.Header.Equals("影视金曲"))
            {
                /*
                 * 影视金曲排序规则
                 * 1.按热度字段排序
                 * 2.去掉非影视金曲类别的
                 * 3.最多选取100首
                 */
                ClientViewModel.SongInfoSort = $"newsonghot desc";
                string key = string.Empty;
                ClientViewModel.CategorySourceDict.TryGetValue("影视", out key);
                if (!string.IsNullOrEmpty(key))
                {
                    string categoryFilter = $" category like '{key}' or " +
                                            $" category like '{key},%' or " +
                                            $" category like '%,{key},%' or " +
                                            $" category like '%,{key}' ";
                    ClientViewModel.SongInfoFilter = categoryFilter;
                    if (ClientViewModel.SongInfoSource.DefaultView.Count != 0)
                        if (ClientViewModel.SongInfoSource.DefaultView.Count <= 100)
                            ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[ClientViewModel.SongInfoSource.DefaultView.Count - 1]["newsonghot"].ToString()} and {categoryFilter}";
                        else
                            ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[99]["newsonghot"].ToString()} and {categoryFilter}";
                }
            }
            else if (selectedItem.Header.Equals("经典老歌"))
            {
                /*
                 * 经典老歌排序规则
                 * 1.按热度字段排序
                 * 2.去掉发行日期晚于新老歌分割日期的
                 * 3.最多选取100首
                 */
                ClientViewModel.SongInfoSort = $"newsonghot desc";
                ClientViewModel.SongInfoFilter = $"releasedate < '{ClientViewModel.MinClickDate}'";
                if (ClientViewModel.SongInfoSource.DefaultView.Count != 0)
                    if (ClientViewModel.SongInfoSource.DefaultView.Count <= 100)
                        ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[ClientViewModel.SongInfoSource.DefaultView.Count - 1]["newsonghot"].ToString()} and releasedate < '{ClientViewModel.MinClickDate}'";
                    else
                        ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[99]["newsonghot"].ToString()} and releasedate < '{ClientViewModel.MinClickDate}'";
            }
            else if (selectedItem.Header.Equals("新歌榜"))
            {
                /*
                 * 新歌榜排序规则
                 * 1.按热度字段排序
                 * 2.去掉发行日期早于新老歌分割日期的
                 * 3.最多选取100首
                 */
                ClientViewModel.SongInfoSort = $"newsonghot desc";
                ClientViewModel.SongInfoFilter = $"releasedate >= '{ClientViewModel.MinClickDate}'";
                if (ClientViewModel.SongInfoSource.DefaultView.Count != 0)
                    if (ClientViewModel.SongInfoSource.DefaultView.Count <= 100)
                        ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[ClientViewModel.SongInfoSource.DefaultView.Count - 1]["newsonghot"].ToString()} and releasedate >= '{ClientViewModel.MinClickDate}'";
                    else
                        ClientViewModel.SongInfoFilter = $"newsonghot >= {ClientViewModel.SongInfoSource.DefaultView[99]["newsonghot"].ToString()} and releasedate >= '{ClientViewModel.MinClickDate}'";
            }
        }
    }
}