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
using System.Windows.Threading;

namespace KtvMusic.Views
{
    /// <summary>
    /// SelectedSongListUc.xaml 的交互逻辑
    /// </summary>
    public partial class SelectedSongListUc : UserControl, INotifyPropertyChanged
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

        public SelectedSongListUc()
        {
            InitializeComponent();

            DataContextChanged += delegate
            {
                MusicViewModel = (MusicViewModel)DataContext;
            };
        }
        
        #region 进度条

        private DispatcherTimer timer = null;
        private void vedio_MediaOpened(object sender, RoutedEventArgs e)
        {
            sliderPosition.Value = 0;
            sliderPosition.Maximum = vedio.NaturalDuration.TimeSpan.TotalSeconds;

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += new EventHandler(Timer_tick);
            timer.Start();
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            sliderPosition.Value = vedio.Position.TotalSeconds;
            if(vedio.Position.TotalSeconds >= sliderPosition.Maximum)
            {
                timer.Stop();
            }
        }

        private void vedio_MediaEnded(object sender, RoutedEventArgs e)
        {
            MusicViewModel.OnSongToNext();
        }
        
        private void sliderPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vedio.Position = TimeSpan.FromSeconds(sliderPosition.Value);
        }

        #endregion
    }
}
