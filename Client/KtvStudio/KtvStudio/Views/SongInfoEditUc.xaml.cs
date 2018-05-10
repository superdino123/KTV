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
using System.Windows.Threading;

namespace KtvStudio.Views
{
    /// <summary>
    /// SongInfoEditUc.xaml 的交互逻辑
    /// </summary>
    public partial class SongInfoEditUc : UserControl, INotifyPropertyChanged
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

        #region CategoryEditViewModel

        private CategoryEditViewModel _CategoryEditViewModel;

        public CategoryEditViewModel CategoryEditViewModel
        {
            get { return _CategoryEditViewModel; }
            set
            {
                if (_CategoryEditViewModel != null && _CategoryEditViewModel.Equals(value)) return;
                _CategoryEditViewModel = value;
                RaisePropertyChanged("CategoryEditViewModel");

            }
        }

        #endregion

        public SongInfoEditUc()
        {
            InitializeComponent();

            DataContextChanged += delegate
            {
                ClientViewModel = (ClientViewModel)DataContext;
                ClientViewModel.songInfoEditUc = this;
                CategoryEditViewModel = new CategoryEditViewModel(ClientViewModel);
                CategoryEditViewModel.songInfoEditUc = this;
            };
        }

        #region Category

        private void FirstCategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedValue != null)
            {
                string code = comboBox.SelectedValue.ToString();
                ClientViewModel.SecondCategorySourceView.RowFilter = $"fatherid = '{code}'";
            }
        }

        private void SecondCategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedValue != null)
            {
                string code = comboBox.SelectedValue.ToString();
                CategoryEditViewModel.selectedCategoryId = code;
                CategoryEditViewModel.selectedCategoryName = ((DataRowView)comboBox.SelectedItem)["categoryname"].ToString();
            }
        }

        private void MusicNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //根据已选择的分类代码获取其中文名称
            CategoryEditViewModel.SelectedCategorySource = CategoryEditViewModel.GetCategorySourceNameByCode();
        }

        public void AddSelectedCategory()
        {
            CategoryEditViewModel.AddSelectedCategory(SelectedCategorysTabControl);
        }

        private void SelectedCategorysTabControl_TabItemClosingEvent(object sender, MahApps.Metro.Controls.BaseMetroTabControl.TabItemClosingEventArgs e)
        {
            CategoryEditViewModel.DeleteSelectedCategory(e.ClosingTabItem.Tag.ToString());
        }

        #endregion

        #region Vedio
        
        /// <summary>
        /// 播放/暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayOrPause_Click(object sender, RoutedEventArgs e)
        {
            if (vedio.Source == null || string.IsNullOrEmpty(vedio.Source.ToString())) return;
            if (ClientViewModel.VedioIsPlay)
                vedio.Pause();
            else
                vedio.Play();
            ClientViewModel.VedioIsPlay = !ClientViewModel.VedioIsPlay;
        }

        /// <summary>
        /// 重播
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Replay_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(vedio.Source.ToString())) return;
            vedio.Stop();
            vedio.Play();
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            vedio.Stop();
        }

        #region 进度条

        private DispatcherTimer timer = null;
        private void vedio_MediaOpened(object sender, RoutedEventArgs e)
        {
            sliderPosition.Maximum = vedio.NaturalDuration.TimeSpan.TotalSeconds;
            vedioAllTime.Text = vedio.NaturalDuration.TimeSpan.Minutes + ":" + vedio.NaturalDuration.TimeSpan.Seconds;

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            sliderPosition.Value = vedio.Position.TotalSeconds;
        }

        private void sliderPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            vedio.Position = TimeSpan.FromSeconds(sliderPosition.Value);
        }

        #endregion
        
        private void PreviewType_IsCheckedChanged(object sender, EventArgs e)
        {
            ToggleSwitch item = sender as ToggleSwitch;
            if (item.IsChecked.Value && !string.IsNullOrEmpty(ClientViewModel.SongLocationMVUrl))
                vedio.Source = new Uri(ClientViewModel.SongLocationMVUrl, UriKind.Absolute);
            else if(!item.IsChecked.Value && !string.IsNullOrEmpty(ClientViewModel.SongInfoEditItem.MVUrl))
                vedio.Source = new Uri(ClientViewModel.SongInfoEditItem.MVUrl, UriKind.Absolute);
        }

        #endregion


        #region Test
        
        #endregion
    }
}