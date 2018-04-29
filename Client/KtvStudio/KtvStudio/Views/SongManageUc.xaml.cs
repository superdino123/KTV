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

        #region FilterType

        public string singerNationality = string.Empty;
        public string singerSex = string.Empty;
        public string singerInitial = string.Empty;

        #endregion

        public SongManageUc()
        {
            InitializeComponent();

            DataContextChanged += delegate
            {
                ClientViewModel = (ClientViewModel)DataContext;
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
                ClientViewModel.SongInfoSource.DefaultView.RowFilter = $"category like '{key}' or " +
                    $" category like '{key},%' or " +
                    $" category like '%,{key},%' or " +
                    $" category like '%,{key}' ";
        }

        private void MusicSingerAreaTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == null) return;
            singerNationality = string.Empty;
            singerSex = string.Empty;
            TabControl tabControl = sender as TabControl;
            TabItem selectedItem = tabControl.SelectedItem as TabItem;
            if (selectedItem.Header.ToString().Contains("华语"))
                singerNationality = "中国";
            else if (selectedItem.Header.ToString().Contains("日本"))
                singerNationality = "日本";
            else if (selectedItem.Header.ToString().Contains("韩国"))
                singerNationality = "韩国";
            else if (selectedItem.Header.ToString().Contains("欧美"))
                singerNationality = "美国";
            else if (selectedItem.Header.ToString().Contains("全部"))
            {
                singerNationality = "全部";
                singerSex = "0";
            }

            if (selectedItem.Header.ToString().Contains("男"))
                singerSex = "0";
            else if (selectedItem.Header.ToString().Contains("女"))
                singerSex = "1";
            else if (selectedItem.Header.ToString().Contains("组合"))
                singerSex = "2";

            if (!string.IsNullOrEmpty(singerInitial)) {
                if (singerInitial.Equals("热门") && singerNationality.Equals("全部"))
                    ClientViewModel.SingerFilterSource = ClientViewModel.SongInfoManagementServiceCaller.GetSingerFilterSourceAllNationalityByRank();
                else if (singerInitial.Equals("热门") && !singerNationality.Equals("全部"))
                    ClientViewModel.SingerFilterSource = ClientViewModel.SongInfoManagementServiceCaller.GetSingerFilterSourceByRank(singerNationality, singerSex);
                else if(!singerInitial.Equals("热门") && !singerNationality.Equals("全部"))
                    ClientViewModel.SingerFilterSource = ClientViewModel.SongInfoManagementServiceCaller.GetSingerFilterSourceByInitial(singerNationality, singerSex, singerInitial);
                else
                    ClientViewModel.SingerFilterSource = ClientViewModel.SongInfoManagementServiceCaller.GetSingerFilterSourceAllNationalityByInitial(singerInitial);

                AddSingerInfoFilter();
            }
        }

        private void MusicSingerNameTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == null) return;
            singerInitial = string.Empty;
            TabControl tabControl = sender as TabControl;
            TabItem selectedItem = tabControl.SelectedItem as TabItem;
            singerInitial = selectedItem.Header.ToString();

            if (!string.IsNullOrEmpty(singerNationality) && !string.IsNullOrEmpty(singerSex))
            {
                if (singerInitial.Equals("热门") && singerNationality.Equals("全部"))
                    ClientViewModel.SingerFilterSource = ClientViewModel.SongInfoManagementServiceCaller.GetSingerFilterSourceAllNationalityByRank();
                else if (singerInitial.Equals("热门") && !singerNationality.Equals("全部"))
                    ClientViewModel.SingerFilterSource = ClientViewModel.SongInfoManagementServiceCaller.GetSingerFilterSourceByRank(singerNationality, singerSex);
                else if (!singerInitial.Equals("热门") && !singerNationality.Equals("全部"))
                    ClientViewModel.SingerFilterSource = ClientViewModel.SongInfoManagementServiceCaller.GetSingerFilterSourceByInitial(singerNationality, singerSex, singerInitial);
                else
                    ClientViewModel.SingerFilterSource = ClientViewModel.SongInfoManagementServiceCaller.GetSingerFilterSourceAllNationalityByInitial(singerInitial);

                AddSingerInfoFilter();
            }
        }

        #region Method

        public void AddSingerInfoFilter()
        {
            SingerInfoFilterWrapPanel.Children.Clear();
            for (int i = 0; i < ClientViewModel.SingerFilterSource.Rows.Count; i++)
            {
                Button item = new Button();
                item.Width = 100;
                item.FontSize = 15;
                item.BorderThickness = new Thickness(0);
                item.Margin = new Thickness(50, 0, 50, 0);
                item.Content = ClientViewModel.SingerFilterSource.Rows[i]["singername"].ToString();
                item.Tag = ClientViewModel.SingerFilterSource.Rows[i]["id"].ToString();
                item.Click += SingerMusicInfoItem_Click;
                SingerInfoFilterWrapPanel.Children.Add(item);
            }
        }

        private void SingerMusicInfoItem_Click(object sender, RoutedEventArgs e)
        {
            Button item = sender as Button;
            string singerid = item.Tag.ToString();
            ClientViewModel.SongInfoSource.DefaultView.RowFilter = $"singerid = '{singerid}'";
            ClientViewModel.SingerSelectedInfoVisibility = !ClientViewModel.SingerSelectedInfoVisibility;

            ClientViewModel.SingerInfoSelectedSource = ClientViewModel.SongInfoManagementServiceCaller.GetSingerInfoById(singerid);
        }

        #endregion
    }
}