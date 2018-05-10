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

namespace KtvMusic.Views
{
    /// <summary>
    /// SingerSearchUc.xaml 的交互逻辑
    /// </summary>
    public partial class SingerSearchUc : UserControl, INotifyPropertyChanged
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
        
        public SingerSearchUc()
        {
            InitializeComponent();

            DataContextChanged += delegate
            {
                MusicViewModel = (MusicViewModel)DataContext;
            };
        }
        
        private void ATOZButton_Click(object sender, RoutedEventArgs e)
        {
            Button item = sender as Button;
            if(MusicViewModel.SingerNameSearchStr.Length < 20)
                MusicViewModel.SingerNameSearchStr = MusicViewModel.SingerNameSearchStr + item.Content.ToString();
        }

        private void SingerAreaSearchTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl item = sender as TabControl;
            if (item.SelectedIndex == 0)//全部
                MusicViewModel.SingerInfoFilter = string.Empty;
            else if (item.SelectedIndex == 1)//华语男歌星
                MusicViewModel.SingerInfoFilter = $"singernationality = '中国' and singersex = 0";
            else if (item.SelectedIndex == 2)//华语女歌星
                MusicViewModel.SingerInfoFilter = $"singernationality = '中国' and singersex = 1";
            else if (item.SelectedIndex == 3)//华语组合歌星
                MusicViewModel.SingerInfoFilter = $"singernationality = '中国' and singersex = 2";
            else if (item.SelectedIndex == 4)//外国男歌星
                MusicViewModel.SingerInfoFilter = $"singernationality not in ('中国') and singersex = 0";
            else if (item.SelectedIndex == 5)//外国女歌星
                MusicViewModel.SingerInfoFilter = $"singernationality not in ('中国') and singersex = 1";
            else if (item.SelectedIndex == 6)//外国组合歌星
                MusicViewModel.SingerInfoFilter = $"singernationality not in ('中国') and singersex = 2";

        }
    }
}
