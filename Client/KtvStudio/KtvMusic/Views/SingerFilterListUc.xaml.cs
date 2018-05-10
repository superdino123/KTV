using KtvMusic.ViewModels;
using MahApps.Metro.Controls;
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
    /// SingerFilterListUc.xaml 的交互逻辑
    /// </summary>
    public partial class SingerFilterListUc : UserControl, INotifyPropertyChanged
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

        public SingerFilterListUc()
        {
            InitializeComponent();

            DataContextChanged += delegate
            {
                MusicViewModel = (MusicViewModel)DataContext;
                MusicViewModel.singerFilterListUc = this;
                MusicViewModel.AddSongTypeButton(MusicStyleWrapPanel, MusicTitleWrapPanel, MusicMoodWrapPanel);
            };
        }

        private void LanguageTypeButton_Click(object sender, RoutedEventArgs e)
        {
            Button item = sender as Button;
            string languageType = item.Content.ToString();
            languageType = MusicViewModel.GetLanguageTypeCode(languageType);
            MusicViewModel.SongInfoFilter = $"languagetype in ({languageType})";        
        }

        private void ATOZButton_Click(object sender, RoutedEventArgs e)
        {
            Button item = sender as Button;
            if (MusicViewModel.SongNameSearchStr.Length < 20)
                MusicViewModel.SongNameSearchStr = MusicViewModel.SongNameSearchStr + item.Content.ToString();
        }

        private void SongRankTile_Click(object sender, RoutedEventArgs e)
        {
            Tile item = sender as Tile;
            string title = item.Title.ToString();
            MusicViewModel.GetRankFilter(title);
        }
    }
}