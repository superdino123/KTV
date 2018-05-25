using Helpers.Commands;
using KtvMusic.Helpers;
using KtvMusic.Views;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace KtvMusic.ViewModels
{
    public class MusicViewModel: ViewModelBase
    {
        #region Constructs

        public MusicViewModel() {
            MusicInfoSource = GetSongInfoSource();
            SingerInfoSource = GetSingerInfoSource();
            InitSelectedSongSour();
            CategorySource = SongInfoManagementServiceCaller.GetAllCategorySource();
            CategorySourceDict = getCategorySourceDict();
            InitSpeech();
        }

        #endregion

        #region Common

        #region NotifyProperty

        public MainWindow mainWindow;
        public SingerSearchUc singerSearchUc;
        public SingerFilterListUc singerFilterListUc;
        public SelectedSongListUc selectedSongListUc;
        
        #region SIMPLECLOCK (INotifyPropertyChanged Property)

        public readonly SimpleClock SimpleClock = new SimpleClock(1);

        public string DateTimeNow
        {
            get
            {
                return SimpleClock.Clock.ToString(CultureInfo.InstalledUICulture.DateTimeFormat.FullDateTimePattern);
            }
        }

        #endregion SIMPLECLOCK
        
        #region CategorySource

        private DataTable _CategorySource = new DataTable();

        public DataTable CategorySource
        {
            get { return _CategorySource; }
            set
            {
                if (_CategorySource != null && _CategorySource.Equals(value)) return;
                _CategorySource = value;
                RaisePropertyChanged("CategorySource");
            }
        }

        public Dictionary<string, string> CategorySourceDict = new Dictionary<string, string>();

        #endregion
        
        /// <summary>
        /// 程序关闭后提交所有被点歌曲的点击量<songId, clicknum>
        /// </summary>
        public Dictionary<string, int> TempSongRecordNumberDict = new Dictionary<string, int>();

        #region PageSelectedIndex

        private int _PageSelectedIndex = 0;

        public int PageSelectedIndex
        {
            get { return _PageSelectedIndex; }
            set
            {
                if (_PageSelectedIndex.Equals(value)) return;
                if (PageSelectedIndex == 3) {
                }
                else if (PageSelectedIndex != 2)
                    PagePreIndex = PageSelectedIndex;
                else if (ShowLanguageTypeFilter)
                    PagePreIndex = 2;
                else if (ShowSongNameFilter)
                    PagePreIndex = 4;
                else if (ShowSongRankFilter)
                    PagePreIndex = 5;
                else if (ShowSongTypeFilter)
                    PagePreIndex = 6;
                else
                    PagePreIndex = 7;

                _PageSelectedIndex = value;
                RaisePropertyChanged("PageSelectedIndex");
                /*
                 * PageSelectedIndex
                 * 0 首页
                 * 1 歌手
                 * 2 歌曲(ShowLanguageTypeFilter/ShowSongNameFilter/ShowSongRankFilter/ShowSongTypeFilter)
                 * 3 已选
                 * PagePreIndex
                 * 0 首页
                 * 1 歌手
                 * 2 歌曲 ShowLanguageTypeFilter
                 * 3 已选
                 * 4 歌曲 ShowSongNameFilter
                 * 5 歌曲 ShowSongRankFilter
                 * 6 歌曲 ShowSongTypeFilter
                 * 7 歌曲
                 */

                if (PageSelectedIndex == 1 && singerSearchUc != null)//为歌手时，没有添加过歌手信息就添加歌手信息
                {
                    AddSingerInfo(singerSearchUc.SingerInfoWrapPanel);
                }
                //清空状态
                if (ShowLanguageTypeFilter)
                    ShowLanguageTypeFilter = false;
                if (ShowSongNameFilter)
                    ShowSongNameFilter = false;
                if (ShowSongRankFilter)
                    ShowSongRankFilter = false;
                if (ShowSongTypeFilter)
                    ShowSongTypeFilter = false;
            }
        }

        #endregion

        #region PagePreIndex

        private int _PagePreIndex = 0;

        public int PagePreIndex
        {
            get { return _PagePreIndex; }
            set
            {
                if (_PagePreIndex.Equals(value)) return;
                _PagePreIndex = value;
                RaisePropertyChanged("PagePreIndex");
            }
        }

        #endregion

        #region PlayingSongName

        private string _PlayingSongName = string.Empty;

        public string PlayingSongName
        {
            get { return _PlayingSongName; }
            set
            {
                if (_PlayingSongName != null && _PlayingSongName.Equals(value)) return;
                _PlayingSongName = value;
                RaisePropertyChanged("PlayingSongName");
                if (string.IsNullOrEmpty(PlayingSongName) && string.IsNullOrEmpty(PlayingSongUrl))
                {
                    UpdatePlayingInfo();
                }
                if (!string.IsNullOrEmpty(PlayingSongName) && !string.IsNullOrEmpty(PlayingSongUrl))
                {
                    selectedSongListUc.vedio.Play();
                }
            }
        }

        #endregion

        #region PlayingSongUrl

        private string _PlayingSongUrl = string.Empty;

        public string PlayingSongUrl
        {
            get { return _PlayingSongUrl; }
            set
            {
                if (_PlayingSongUrl != null && _PlayingSongUrl.Equals(value)) return;
                _PlayingSongUrl = value;
                RaisePropertyChanged("PlayingSongUrl");
                if (string.IsNullOrEmpty(PlayingSongName) && string.IsNullOrEmpty(PlayingSongUrl))
                {
                    UpdatePlayingInfo();
                }
                if (!string.IsNullOrEmpty(PlayingSongName) && !string.IsNullOrEmpty(PlayingSongUrl))
                {
                    selectedSongListUc.vedio.Play();
                }
            }
        }

        #endregion
        
        #region PlayingSongRail

        private string _PlayingSongRail = "0";

        public string PlayingSongRail
        {
            get { return _PlayingSongRail; }
            set
            {
                if (_PlayingSongRail != null && _PlayingSongRail.Equals(value)) return;
                _PlayingSongRail = value;
                RaisePropertyChanged("PlayingSongRail");
            }
        }

        #endregion
        
        #region NextPlaySongName

        private string _NextPlaySongName = string.Empty;

        public string NextPlaySongName
        {
            get { return _NextPlaySongName; }
            set
            {
                if (_NextPlaySongName != null && _NextPlaySongName.Equals(value)) return;
                _NextPlaySongName = value;
                RaisePropertyChanged("NextPlaySongName");
                if (string.IsNullOrEmpty(NextPlaySongName) && string.IsNullOrEmpty(NextPlaySongUrl))
                {
                    UpdateNextPlayInfo();
                }
                if (string.IsNullOrEmpty(PlayingSongName) && string.IsNullOrEmpty(PlayingSongUrl) && !string.IsNullOrEmpty(NextPlaySongName) && !string.IsNullOrEmpty(NextPlaySongUrl))
                {
                    UpdatePlayingInfo();
                }
            }
        }

        #endregion

        #region NextPlaySongUrl

        private string _NextPlaySongUrl = string.Empty;

        public string NextPlaySongUrl
        {
            get { return _NextPlaySongUrl; }
            set
            {
                if (_NextPlaySongUrl != null && _NextPlaySongUrl.Equals(value)) return;
                _NextPlaySongUrl = value;
                RaisePropertyChanged("NextPlaySongUrl");
                if (string.IsNullOrEmpty(NextPlaySongName) && string.IsNullOrEmpty(NextPlaySongUrl))
                {
                    UpdateNextPlayInfo();
                }
                if (string.IsNullOrEmpty(PlayingSongName) && string.IsNullOrEmpty(PlayingSongUrl) && !string.IsNullOrEmpty(NextPlaySongName) && !string.IsNullOrEmpty(NextPlaySongUrl))
                {
                    UpdatePlayingInfo();
                }
            }
        }

        #endregion

        #region IsPlayNoPause

        private bool _IsPlayNoPause = true;

        public bool IsPlayNoPause
        {
            get { return _IsPlayNoPause; }
            set
            {
                if (_IsPlayNoPause.Equals(value)) return;
                _IsPlayNoPause = value;
                RaisePropertyChanged("IsPlayNoPause");
            }
        }

        #endregion

        #region IsSingNoAccompany

        private bool _IsSingNoAccompany = true;

        public bool IsSingNoAccompany
        {
            get { return _IsSingNoAccompany; }
            set
            {
                if (_IsSingNoAccompany.Equals(value)) return;
                _IsSingNoAccompany = value;
                RaisePropertyChanged("IsSingNoAccompany");
            }
        }

        #endregion

        #region NoShowMv

        private bool _NoShowMv = true;

        public bool NoShowMv
        {
            get { return _NoShowMv; }
            set
            {
                if (_NoShowMv.Equals(value)) return;
                _NoShowMv = value;
                RaisePropertyChanged("NoShowMv");
            }
        }

        #endregion

        #endregion

        #region Command

        #region TurnToSelectedSongCmd

        private RelayCommand _TurnToSelectedSongCmd;

        public ICommand TurnToSelectedSongCmd
        {
            get { return _TurnToSelectedSongCmd ?? (_TurnToSelectedSongCmd = new RelayCommand(param => OnTurnToSelectedSong(), param => CanTurnToSelectedSong())); }
        }

        public void OnTurnToSelectedSong()
        {
            PageSelectedIndex = 3;
            if (!NoShowMv)
                NoShowMv = true;
        }

        public bool CanTurnToSelectedSong()
        {
            return true;
        }

        #endregion

        #region SoundDownCmd

        private RelayCommand _SoundDownCmd;

        public ICommand SoundDownCmd
        {
            get { return _SoundDownCmd ?? (_SoundDownCmd = new RelayCommand(param => OnSoundDown(), param => CanSoundDown())); }
        }

        public void OnSoundDown()
        {
            selectedSongListUc.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, (Action)delegate
            {
                selectedSongListUc.vedio.Volume = selectedSongListUc.vedio.Volume >= 0.1 ? selectedSongListUc.vedio.Volume - 0.1 : 0;
            });
        }

        public bool CanSoundDown()
        {
            return true;
        }

        #endregion

        #region SoundUpCmd

        private RelayCommand _SoundUpCmd;

        public ICommand SoundUpCmd
        {
            get { return _SoundUpCmd ?? (_SoundUpCmd = new RelayCommand(param => OnSoundUp(), param => CanSoundUp())); }
        }

        public void OnSoundUp()
        {
            selectedSongListUc.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, (Action)delegate
            {
                selectedSongListUc.vedio.Volume = selectedSongListUc.vedio.Volume <= 0.9 ? selectedSongListUc.vedio.Volume + 0.1 : 1;
            });
        }

        public bool CanSoundUp()
        {
            return true;
        }

        #endregion
        
        #region SoundCancelCmd

        private RelayCommand _SoundCancelCmd;

        public ICommand SoundCancelCmd
        {
            get { return _SoundCancelCmd ?? (_SoundCancelCmd = new RelayCommand(param => OnSoundCancel(), param => CanSoundCancel())); }
        }

        public void OnSoundCancel()
        {
            selectedSongListUc.vedio.Volume = 0;
        }

        public bool CanSoundCancel()
        {
            return true;
        }

        #endregion
        
        #region SongToNextCmd

        private RelayCommand _SongToNextCmd;

        public ICommand SongToNextCmd
        {
            get { return _SongToNextCmd ?? (_SongToNextCmd = new RelayCommand(param => OnSongToNext(), param => CanSongToNext())); }
        }

        public void OnSongToNext()
        {
            selectedSongListUc.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, (Action)delegate
            {
                UpdatePlayingInfo();
            });
        }

        public bool CanSongToNext()
        {
            return true;
        }

        #endregion

        #region SongPauseAndPlayCmd

        private RelayCommand _SongPauseAndPlayCmd;

        public ICommand SongPauseAndPlayCmd
        {
            get { return _SongPauseAndPlayCmd ?? (_SongPauseAndPlayCmd = new RelayCommand(param => OnSongPauseAndPlay(), param => CanSongPauseAndPlay())); }
        }

        public void OnSongPauseAndPlay()
        {
            if (IsPlayNoPause)
            {
                selectedSongListUc.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, (Action)delegate
                {
                    selectedSongListUc.vedio.Pause();
                });
            }
            else
            {
                selectedSongListUc.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, (Action)delegate
                {
                    selectedSongListUc.vedio.Play();
                });
            }
            IsPlayNoPause = !IsPlayNoPause;
        }

        public bool CanSongPauseAndPlay()
        {
            return true;
        }

        #endregion
        
        #region SongAgainCmd

        private RelayCommand _SongAgainCmd;

        public ICommand SongAgainCmd
        {
            get { return _SongAgainCmd ?? (_SongAgainCmd = new RelayCommand(param => OnSongAgain(), param => CanSongAgain())); }
        }

        public void OnSongAgain()
        {
            selectedSongListUc.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, (Action)delegate
            {
                selectedSongListUc.vedio.Stop();
                selectedSongListUc.vedio.Play();
            });
        }

        public bool CanSongAgain()
        {
            return true;
        }

        #endregion

        #region SongAccompanyOrSingCmd

        private RelayCommand _SongAccompanyOrSingCmd;

        public ICommand SongAccompanyOrSingCmd
        {
            get { return _SongAccompanyOrSingCmd ?? (_SongAccompanyOrSingCmd = new RelayCommand(param => OnSongAccompanyOrSing(), param => CanSongAccompanyOrSing())); }
        }

        public void OnSongAccompanyOrSing()
        {
            if (IsSingNoAccompany)
            {
                selectedSongListUc.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, (Action)delegate
                {
                    selectedSongListUc.vedio.Balance = -1 * int.Parse(PlayingSongRail);
                });
            }
            else {
                selectedSongListUc.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, (Action)delegate
                {
                    selectedSongListUc.vedio.Balance = 0;
                });
            }
            IsSingNoAccompany = !IsSingNoAccompany;
        }

        public bool CanSongAccompanyOrSing()
        {
            return true;
        }

        #endregion
        
        #region ShowMVCmd

        private RelayCommand _ShowMVCmd;

        public ICommand ShowMVCmd
        {
            get { return _ShowMVCmd ?? (_ShowMVCmd = new RelayCommand(param => OnShowMV(), param => CanShowMV())); }
        }

        public void OnShowMV()
        {
            PageSelectedIndex = 3;
            if(NoShowMv)
                NoShowMv = false;
        }

        public bool CanShowMV()
        {
            return true;
        }

        #endregion
        
        #region ReturnHomeCmd

        private RelayCommand _ReturnHomeCmd;

        public ICommand ReturnHomeCmd
        {
            get { return _ReturnHomeCmd ?? (_ReturnHomeCmd = new RelayCommand(param => OnReturnHome(), param => CanReturnHome())); }
        }

        public void OnReturnHome()
        {
            PageSelectedIndex = 0;
        }

        public bool CanReturnHome()
        {
            return true;
        }

        #endregion

        #region ReturnPreCmd

        private RelayCommand _ReturnPreCmd;

        public ICommand ReturnPreCmd
        {
            get { return _ReturnPreCmd ?? (_ReturnPreCmd = new RelayCommand(param => OnReturnPre(), param => CanReturnPre())); }
        }

        public void OnReturnPre()
        {
            if(PageSelectedIndex == 2 && SongInfoFilter.Contains("singerid"))
                PageSelectedIndex = 1;
            else if(PageSelectedIndex == 2)
                PageSelectedIndex = 0;
            else if (PagePreIndex == 0 || PagePreIndex == 1)
                PageSelectedIndex = PagePreIndex;
            else if (PagePreIndex == 2)
            {
                PageSelectedIndex = 2;
                ShowLanguageTypeFilter = true;
            }
            else if (PagePreIndex == 4)
            {
                PageSelectedIndex = 2;
                ShowSongNameFilter = true;
            }
            else if (PagePreIndex == 5)
            {
                PageSelectedIndex = 2;
                ShowSongRankFilter = true;
            }
            else if (PagePreIndex == 6)
            {
                PageSelectedIndex = 2;
                ShowSongTypeFilter = true;
            }
            else
                PageSelectedIndex = 2;
        }

        public bool CanReturnPre()
        {
            return true;
        }

        #endregion

        #endregion

        #region Method

        private DataTable GetSongInfoSource() {
            return SongInfoManagementServiceCaller.GetAllSongInfo();
        }

        private void UpdateNextPlayInfo() {
            if(SelectedSongSource != null && SelectedSongSource.DefaultView.Count != 0)
            {
                NextPlaySongName = SelectedSongSource.DefaultView[0]["songname"].ToString();
                NextPlaySongUrl = SelectedSongSource.DefaultView[0]["mvurl"].ToString();
            }
            else
            {
                NextPlaySongName = string.Empty;
                NextPlaySongUrl = string.Empty;
            }
        }

        public void UpdatePlayingInfo()
        {
            PlayingSongName = NextPlaySongName;
            PlayingSongUrl = NextPlaySongUrl;
            if (string.IsNullOrEmpty(NextPlaySongName) || string.IsNullOrEmpty(NextPlaySongUrl))
            {
                UpdateNextPlayInfo();
            }
            if (string.IsNullOrEmpty(NextPlaySongName) || string.IsNullOrEmpty(NextPlaySongUrl))
            {
                return;
            }
            PlayingSongRail = SelectedSongSource.Rows[0]["singrail"].ToString();
            //更改正在播放歌曲时，添加临时点击量记录
            string songId = SelectedSongSource.Rows[0]["songid"].ToString();
            if (TempSongRecordNumberDict.ContainsKey(songId))
                TempSongRecordNumberDict[songId]++;
            else
                TempSongRecordNumberDict.Add(songId, 1);

            SelectedSongSource.Rows.RemoveAt(0);
            AddSelectedSongInfo();
        }

        private Dictionary<string, string> getCategorySourceDict()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            for (int i = 0; i < CategorySource.Rows.Count; i++)
            {
                result.Add(CategorySource.Rows[i]["categoryname"].ToString(), CategorySource.Rows[i]["id"].ToString());
            }
            return result;
        }

        #endregion

        #endregion

        #region SingerSearch

        #region NotifyProperty

        #region SingerInfoSource

        private DataTable _SingerInfoSource = new DataTable();

        public DataTable SingerInfoSource
        {
            get { return _SingerInfoSource; }
            set
            {
                if (_SingerInfoSource != null && _SingerInfoSource.Equals(value)) return;
                _SingerInfoSource = value;
                RaisePropertyChanged("SingerInfoSource");
                RaisePropertyChanged("SingerInfoSourceView");
                SingerInfoSource.DefaultView.RowFilter = SingerInfoFilter;
                SingerInfoSource.DefaultView.Sort = SingerInfoSort;
                SingerInfoAllPage = GetSingerInfoAllPage();
            }
        }

        public DataView SingerInfoSourceView => SingerInfoSource?.DefaultView;

        #endregion

        #region SingerInfoFilter

        private string _SingerInfoFilter = string.Empty;

        public string SingerInfoFilter
        {
            get { return _SingerInfoFilter; }
            set
            {
                if (_SingerInfoFilter != null && _SingerInfoFilter.Equals(value)) return;
                _SingerInfoFilter = value;
                RaisePropertyChanged("SingerInfoFilter");
                if (SingerInfoSource != null)
                    SingerInfoSource.DefaultView.RowFilter = SingerInfoFilter;
                SingerInfoAllPage = GetSingerInfoAllPage();
                AddSingerInfo(singerSearchUc.SingerInfoWrapPanel);
            }
        }

        #endregion

        #region SingerInfoSort

        private string _SingerInfoSort = "singerclicknum desc";

        public string SingerInfoSort
        {
            get { return _SingerInfoSort; }
            set
            {
                if (_SingerInfoSort != null && _SingerInfoSort.Equals(value)) return;
                _SingerInfoSort = value;
                RaisePropertyChanged("SingerInfoSort");
                if (SingerInfoSource != null)
                    SingerInfoSource.DefaultView.Sort = SingerInfoSort;
            }
        }

        #endregion

        #region SingerInfoAccentPage

        private int _SingerInfoAccentPage = 1;

        public int SingerInfoAccentPage
        {
            get { return _SingerInfoAccentPage; }
            set
            {
                if (_SingerInfoAccentPage.Equals(value)) return;
                _SingerInfoAccentPage = value;
                RaisePropertyChanged("SingerInfoAccentPage");
                if(singerSearchUc != null)
                    AddSingerInfo(singerSearchUc.SingerInfoWrapPanel);
            }
        }

        #endregion

        #region SingerInfoAllPage

        private int _SingerInfoAllPage = 0;

        public int SingerInfoAllPage
        {
            get { return _SingerInfoAllPage; }
            set
            {
                if (_SingerInfoAllPage.Equals(value)) return;
                _SingerInfoAllPage = value;
                RaisePropertyChanged("SingerInfoAllPage");
            }
        }

        #endregion

        #region SingerNameSearchStr

        private string _SingerNameSearchStr = string.Empty;

        public string SingerNameSearchStr
        {
            get { return _SingerNameSearchStr; }
            set
            {
                if (_SingerNameSearchStr != null && _SingerNameSearchStr.Equals(value)) return;
                _SingerNameSearchStr = value;
                RaisePropertyChanged("SingerNameSearchStr");
                SingerInfoFilter = $"singerinitials like '{SingerNameSearchStr}%'";
            }
        }

        #endregion

        #endregion

        #region Command

        #region SingerSearchOpenCmd

        private RelayCommand _SingerSearchOpenCmd;

        public ICommand SingerSearchOpenCmd
        {
            get { return _SingerSearchOpenCmd ?? (_SingerSearchOpenCmd = new RelayCommand(param => OnSingerSearchOpen(), param => CanSingerSearchOpen())); }
        }

        public void OnSingerSearchOpen()
        {
            PageSelectedIndex = 1;
        }

        public bool CanSingerSearchOpen()
        {
            return true;
        }

        #endregion

        #region PrePageCmd

        private RelayCommand _PrePageCmd;

        public ICommand PrePageCmd
        {
            get { return _PrePageCmd ?? (_PrePageCmd = new RelayCommand(param => OnPrePage(), param => CanPrePage())); }
        }

        public void OnPrePage()
        {
            if (SingerInfoAccentPage > 1)
                SingerInfoAccentPage--;
        }

        public bool CanPrePage()
        {
            return true;
        }

        #endregion

        #region NextPageCmd

        private RelayCommand _NextPageCmd;

        public ICommand NextPageCmd
        {
            get { return _NextPageCmd ?? (_NextPageCmd = new RelayCommand(param => OnNextPage(), param => CanNextPage())); }
        }

        public void OnNextPage()
        {
            if (SingerInfoAccentPage < SingerInfoAllPage)
                SingerInfoAccentPage++;
        }

        public bool CanNextPage()
        {
            return true;
        }

        #endregion
        
        #region SingerSearchStrDeleteCmd

        private RelayCommand _SingerSearchStrDeleteCmd;

        public ICommand SingerSearchStrDeleteCmd
        {
            get { return _SingerSearchStrDeleteCmd ?? (_SingerSearchStrDeleteCmd = new RelayCommand(param => OnSingerSearchStrDelete(), param => CanSingerSearchStrDelete())); }
        }

        public void OnSingerSearchStrDelete()
        {
            if (!string.IsNullOrEmpty(SingerNameSearchStr))
                SingerNameSearchStr = SingerNameSearchStr.Remove(SingerNameSearchStr.Length - 1);
        }

        public bool CanSingerSearchStrDelete()
        {
            return true;
        }

        #endregion
        
        #region SingerSearchStrClearCmd

        private RelayCommand _SingerSearchStrClearCmd;

        public ICommand SingerSearchStrClearCmd
        {
            get { return _SingerSearchStrClearCmd ?? (_SingerSearchStrClearCmd = new RelayCommand(param => OnSingerSearchStrClear(), param => CanSingerSearchStrClear())); }
        }

        public void OnSingerSearchStrClear()
        {
            if (!string.IsNullOrEmpty(SingerNameSearchStr))
                SingerNameSearchStr = string.Empty;
        }

        public bool CanSingerSearchStrClear()
        {
            return true;
        }

        #endregion

        #endregion

        #region Methods

        private DataTable GetSingerInfoSource()
        {
            return SingerInfoManagementServiceCaller.GetAllSingerInfo();
        }
        
        private int GetSingerInfoAllPage()
        {
            return SingerInfoSourceView.Count % 18 == 0 ? SingerInfoSourceView.Count / 18 : SingerInfoSourceView.Count / 18 + 1;
        }
        
        public void AddSingerInfo(WrapPanel wrap) {
            wrap.Children.Clear();
            int firstIndex = (SingerInfoAccentPage - 1) * 18;
            for (int i = firstIndex; i < firstIndex + 18; i++)
            {
                if (i >= SingerInfoSourceView.Count) break;
                string singerId = SingerInfoSourceView[i]["id"].ToString();
                string singerPhotoUrl = SingerInfoSourceView[i]["singerphotourl"].ToString();
                string singerName = SingerInfoSourceView[i]["singername"].ToString();
                wrap.Children.Add(AddOneSingerInfo(singerId, singerPhotoUrl, singerName));
            }
        }

        private Grid AddOneSingerInfo(string singerId, string singerPhotoUrl, string singerName)
        {
            /*
             <Grid Margin="8 5">
                <Grid.RowDefinitions>
                    <RowDefinition Height="Auto"/>
                    <RowDefinition Height="Auto"/>
                </Grid.RowDefinitions>
                <Border Grid.Row="0" Grid.RowSpan="2" Background="White" Opacity="0.2"/>
                <Image Grid.Row="0" Source="{StaticResource icon}" Width="150" Height="150"/>
                <TextBlock Grid.Row="1" Text="XXX" Foreground="White" FontSize="18" Padding="0 5" TextAlignment="Center"/>
            </Grid>
             */
            Grid itemGrid = new Grid { Margin = new Thickness(5, 0, 5, 0) };
            itemGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            itemGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            itemGrid.MouseLeftButtonDown += SingerInfoItemGrid_MouseLeftButtonDown;
            itemGrid.Tag = singerId;

            Border itemBorder = new Border()
            {
                Background = new SolidColorBrush(Colors.White),
                Opacity = 0.2,
            };
            Grid.SetRow(itemBorder, 0);
            Grid.SetRowSpan(itemBorder, 2);
            itemGrid.Children.Add(itemBorder);

            Image itemImage = new Image()
            {
                Width = 150,
                Height = 150,
                Source = string.IsNullOrEmpty(singerPhotoUrl) ? null : new BitmapImage(new Uri(singerPhotoUrl, UriKind.Absolute)),
                Stretch = Stretch.UniformToFill,
                StretchDirection = StretchDirection.Both,
            };
            Grid.SetRow(itemImage, 0);
            itemGrid.Children.Add(itemImage);

            TextBlock itemText = new TextBlock()
            {
                Text = singerName,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 18,
                Padding = new Thickness(0, 5, 0, 5),
                TextAlignment = TextAlignment.Center,
                MaxWidth = 150,
            };
            Grid.SetRow(itemText, 1);
            itemGrid.Children.Add(itemText);
            return itemGrid;
        }

        private void SingerInfoItemGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid item = sender as Grid;
            string singerId = item.Tag.ToString();
            SongInfoFilter = $"singerid = {singerId}";
            PageSelectedIndex = 2;
        }

        #endregion

        #endregion

        #region SongList

        #region NotifyProperty
        
        #region MusicInfoSource

        private DataTable _MusicInfoSource = new DataTable();

        public DataTable MusicInfoSource
        {
            get { return _MusicInfoSource; }
            set
            {
                if (_MusicInfoSource != null && _MusicInfoSource.Equals(value)) return;
                _MusicInfoSource = value;
                RaisePropertyChanged("MusicInfoSource");
                MusicInfoSource.DefaultView.RowFilter = SongInfoFilter;
                if(singerFilterListUc != null)
                    AddFilterSongInfo(singerFilterListUc.SongFilterStackPanel);
            }
        }

        #endregion

        #region SongInfoFilter

        private string _SongInfoFilter = string.Empty;

        public string SongInfoFilter
        {
            get { return _SongInfoFilter; }
            set
            {
                if (_SongInfoFilter != null && _SongInfoFilter.Equals(value)) return;
                _SongInfoFilter = value;
                RaisePropertyChanged("SongInfoFilter");
                if (MusicInfoSource != null)
                    MusicInfoSource.DefaultView.RowFilter = SongInfoFilter;
                SongInfoAccentPage = 1;
                SongInfoAllPage = GetSongInfoAllPage();
                if (singerFilterListUc != null)
                    AddFilterSongInfo(singerFilterListUc.SongFilterStackPanel);
            }
        }

        #endregion

        #region SongInfoSort

        private string _SongInfoSort = string.Empty;

        public string SongInfoSort
        {
            get { return _SongInfoSort; }
            set
            {
                if (_SongInfoSort != null && _SongInfoSort.Equals(value)) return;
                _SongInfoSort = value;
                RaisePropertyChanged("SongInfoSort");
                if (MusicInfoSource != null)
                {
                    MusicInfoSource.DefaultView.Sort = SongInfoSort;
                }
                SongInfoAccentPage = 1;
                if (singerFilterListUc != null)
                    AddFilterSongInfo(singerFilterListUc.SongFilterStackPanel);
            }
        }

        #endregion

        #region SongInfoAccentPage

        private int _SongInfoAccentPage = 1;

        public int SongInfoAccentPage
        {
            get { return _SongInfoAccentPage; }
            set
            {
                if (_SongInfoAccentPage.Equals(value)) return;
                _SongInfoAccentPage = value;
                RaisePropertyChanged("SongInfoAccentPage");
                if (singerFilterListUc != null)
                    AddFilterSongInfo(singerFilterListUc.SongFilterStackPanel);
            }
        }

        #endregion

        #region SongInfoAllPage

        private int _SongInfoAllPage = 0;

        public int SongInfoAllPage
        {
            get { return _SongInfoAllPage; }
            set
            {
                if (_SongInfoAllPage.Equals(value)) return;
                _SongInfoAllPage = value;
                RaisePropertyChanged("SongInfoAllPage");
            }
        }

        #endregion

        #endregion

        #region Command
        
        #region SongInfoPrePageCmd

        private RelayCommand _SongInfoPrePageCmd;

        public ICommand SongInfoPrePageCmd
        {
            get { return _SongInfoPrePageCmd ?? (_SongInfoPrePageCmd = new RelayCommand(param => OnSongInfoPrePage(), param => CanSongInfoPrePage())); }
        }

        public void OnSongInfoPrePage()
        {
            if (SongInfoAccentPage > 1)
                SongInfoAccentPage--;
        }

        public bool CanSongInfoPrePage()
        {
            return true;
        }

        #endregion

        #region SongInfoNextPageCmd

        private RelayCommand _SongInfoNextPageCmd;

        public ICommand SongInfoNextPageCmd
        {
            get { return _SongInfoNextPageCmd ?? (_SongInfoNextPageCmd = new RelayCommand(param => OnSongInfoNextPage(), param => CanSongInfoNextPage())); }
        }

        public void OnSongInfoNextPage()
        {
            if (SongInfoAccentPage < SongInfoAllPage)
                SongInfoAccentPage++;
        }

        public bool CanSongInfoNextPage()
        {
            return true;
        }

        #endregion


        #endregion

        #region Method

        public void AddFilterSongInfo(StackPanel stackPanel)
        {
            stackPanel.Children.Clear();
            int startIndex = (SongInfoAccentPage - 1) * 7;
            string songId = string.Empty;
            string songName = string.Empty;
            string singerName = string.Empty;
            string mvUrl = string.Empty;
            string singrail = string.Empty;
            for (int i = startIndex; i < startIndex + 7; i++)
            {
                if (i >= MusicInfoSource.DefaultView.Count) break;
                songId = MusicInfoSource.DefaultView[i]["id"].ToString();
                songName = MusicInfoSource.DefaultView[i]["musicname"].ToString();
                singerName = MusicInfoSource.DefaultView[i]["singername"].ToString();
                mvUrl = MusicInfoSource.DefaultView[i]["mvurl"].ToString();
                singrail = MusicInfoSource.DefaultView[i]["singrail"].ToString();
                stackPanel.Children.Add(AddOneFilterSongInfo(songId, songName, singerName, mvUrl, singrail));
            }
        }

        private Border AddOneFilterSongInfo(string songId, string songName, string singerName, string mvUrl, string singrail)
        {
        /*
             <Border Background = "#F16F54" CornerRadius="30" Margin="0 10" Padding="0 5">
                <Grid>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width = "*" />
                        < ColumnDefinition Width="*"/>
                        <ColumnDefinition Width = "Auto" />
                    </ Grid.ColumnDefinitions >
                    < TextBlock Grid.Column="0" Margin="30 0" VerticalAlignment="Center" Foreground="White" FontSize="18" Text="明明白白我的心"/>
                    <TextBlock Grid.Column="1" Margin= "30 0" VerticalAlignment= "Center" Foreground= "White" FontSize= "18" Text= "刘德华" />
                    < Button Grid.Column= "2" Margin= "30 0" Width= "50" Height= "50" BorderBrush= "White" Style= "{StaticResource AccentCircleButtonStyle}" >
                        < iconPacks:PackIconMaterial Kind = "Plus" Width= "26" Height= "26" />
                    </ Button >
                </ Grid >
            </ Border >
        */
            Border itemBorder = new Border() {
                Background = new SolidColorBrush(Color.FromRgb(241, 111, 84)),
                CornerRadius = new CornerRadius(30),
                Margin = new Thickness(0, 10, 0, 10),
                Padding = new Thickness(0, 5, 0, 5),
            };

            Grid itemGrid = new Grid();
            itemGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            itemGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            itemGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            itemBorder.Child = itemGrid;

            TextBlock itemTextSongName = new TextBlock() {
                Margin = new Thickness(30, 0, 30, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 18,
                Text = songName,
            };
            Grid.SetColumn(itemTextSongName, 0);
            itemGrid.Children.Add(itemTextSongName);

            TextBlock itemTextSingerName = new TextBlock()
            {
                Margin = new Thickness(30, 0, 30, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 18,
                Text = singerName,
            };
            Grid.SetColumn(itemTextSingerName, 1);
            itemGrid.Children.Add(itemTextSingerName);

            Button itemButton = new Button()
            {
                Tag = new string[5] { songId, songName, singerName, mvUrl, singrail },
                Margin = new Thickness(30, 0, 30, 0),
                Width = 50,
                Height = 50,
                BorderBrush = new SolidColorBrush(Colors.White),
                Style = singerFilterListUc.FindResource("AccentCircleButtonStyle") as Style,
                Content = new PackIconMaterial()
                {
                    Kind = PackIconMaterialKind.Plus,
                    Width = 26,
                    Height = 26,
                },
            };
            Grid.SetColumn(itemButton, 2);
            itemButton.Click += SongFilterInfoItemButton_Click;
            itemGrid.Children.Add(itemButton);
            
            return itemBorder;
        }

        private void SongFilterInfoItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button item = sender as Button;
            string[] infos = item.Tag as string[];
            string songId = infos[0];
            string songName = infos[1];
            string singerName = infos[2];
            string mvUrl = infos[3];
            string singRail = infos[4];
            DataRow row = SelectedSongSource.NewRow();
            row["songid"] = songId;
            row["songname"] = songName;
            row["singername"] = singerName;
            row["mvurl"] = mvUrl;
            row["singrail"] = singRail;
            SelectedSongSource.Rows.Add(row);

            AddSelectedSongInfo();
        }

        private int GetSongInfoAllPage() {
            return MusicInfoSource.DefaultView.Count % 7 == 0 ? MusicInfoSource.DefaultView.Count / 7 : MusicInfoSource.DefaultView.Count / 7 + 1;
        }

        #endregion

        #endregion

        #region SelectedSongList
        
        #region NotifyProperty

        #region SelectedSongSource

        private DataTable _SelectedSongSource = new DataTable() { };

        public DataTable SelectedSongSource
        {
            get { return _SelectedSongSource; }
            set
            {
                if (_SelectedSongSource != null && _SelectedSongSource.Equals(value)) return;
                _SelectedSongSource = value;
                RaisePropertyChanged("SelectedSongSource");
            }
        }

        private void InitSelectedSongSour() {
            SelectedSongSource.Columns.Add("songid");
            SelectedSongSource.Columns.Add("songname");
            SelectedSongSource.Columns.Add("singername");
            SelectedSongSource.Columns.Add("mvurl");
            SelectedSongSource.Columns.Add("singrail");
        }

        #endregion

        #endregion

        #region Methods
        
        public void AddSelectedSongInfo()
        {
            if (selectedSongListUc == null) return;
            selectedSongListUc.SongSelectedStackPanel.Children.Clear();
            string songId = string.Empty;
            string songName = string.Empty;
            string singerName = string.Empty;
            string mvUrl = string.Empty;
            string singRail = string.Empty;
            for (int i = 0; i < SelectedSongSource.Rows.Count; i++)
            {
                if (i >= SelectedSongSource.DefaultView.Count) break;
                songId = SelectedSongSource.DefaultView[i]["songid"].ToString();
                songName = SelectedSongSource.DefaultView[i]["songname"].ToString();
                singerName = SelectedSongSource.DefaultView[i]["singername"].ToString();
                mvUrl = SelectedSongSource.DefaultView[i]["mvurl"].ToString();
                singRail = SelectedSongSource.DefaultView[i]["singrail"].ToString();
                selectedSongListUc.SongSelectedStackPanel.Children.Add(AddOneSelectedSongInfo(songId, songName, singerName, mvUrl, singRail));
            }

            UpdateNextPlayInfo();
        }

        private Border AddOneSelectedSongInfo(string songId, string songName, string singerName, string mvUrl, string singRail)
        {
            /*
                 <Border Background="#F16F54" CornerRadius="30" Margin="0 10" Padding="0 5">
                    <Grid>
                        <Grid.ColumnDefinitions>
                            <ColumnDefinition Width="*"/>
                            <ColumnDefinition Width="*"/>
                            <ColumnDefinition Width="Auto"/>
                            <ColumnDefinition Width="Auto"/>
                        </Grid.ColumnDefinitions>
                        <TextBlock Grid.Column="0" Margin="30 0" VerticalAlignment="Center" Foreground="White" FontSize="18" Text="明明白白我的心"/>
                        <TextBlock Grid.Column="1" Margin="30 0" VerticalAlignment="Center" Foreground="White" FontSize="18" Text="刘德华"/>
                        <Button Grid.Column="2" Margin="30 0 15 0" Width="50" Height="50" BorderBrush="White" Style="{StaticResource AccentCircleButtonStyle}">
                            <iconPacks:PackIconMaterial Kind="FormatVerticalAlignTop" Width="26" Height="26" />
                        </Button>
                        <Button Grid.Column="3" Margin="0 0 30 0" Width="50" Height="50" BorderBrush="White" Style="{StaticResource AccentCircleButtonStyle}">
                            <iconPacks:PackIconModern Kind="Delete" Width="26" Height="26" />
                        </Button>
                    </Grid>
                </Border>
            */
            Border itemBorder = new Border()
            {
                Background = new SolidColorBrush(Color.FromRgb(241, 111, 84)),
                CornerRadius = new CornerRadius(30),
                Margin = new Thickness(0, 10, 0, 10),
                Padding = new Thickness(0, 5, 0, 5),
            };

            Grid itemGrid = new Grid();
            itemGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            itemGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            itemGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            itemGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            itemBorder.Child = itemGrid;

            TextBlock itemTextSongName = new TextBlock()
            {
                Margin = new Thickness(30, 0, 30, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 18,
                Text = songName,
            };
            Grid.SetColumn(itemTextSongName, 0);
            itemGrid.Children.Add(itemTextSongName);

            TextBlock itemTextSingerName = new TextBlock()
            {
                Margin = new Thickness(30, 0, 30, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 18,
                Text = singerName,
            };
            Grid.SetColumn(itemTextSingerName, 1);
            itemGrid.Children.Add(itemTextSingerName);

            Button itemButton = new Button()
            {
                Tag = new string[5] { songId, songName, singerName, mvUrl, singRail },
                Margin = new Thickness(30, 0, 30, 0),
                Width = 50,
                Height = 50,
                BorderBrush = new SolidColorBrush(Colors.White),
                Style = singerFilterListUc.FindResource("AccentCircleButtonStyle") as Style,
                Content = new PackIconMaterial()
                {
                    Kind = PackIconMaterialKind.FormatVerticalAlignTop,
                    Width = 26,
                    Height = 26,
                },
            };
            Grid.SetColumn(itemButton, 2);
            itemButton.Click += SongInfoToTopItemButton_Click;
            itemGrid.Children.Add(itemButton);

            Button itemButtonDelete = new Button()
            {
                Tag = new string[5] { songId, songName, singerName, mvUrl, singRail },
                Margin = new Thickness(30, 0, 30, 0),
                Width = 50,
                Height = 50,
                BorderBrush = new SolidColorBrush(Colors.White),
                Style = singerFilterListUc.FindResource("AccentCircleButtonStyle") as Style,
                Content = new PackIconModern()
                {
                    Kind = PackIconModernKind.Delete,
                    Width = 26,
                    Height = 26,
                },
            };
            Grid.SetColumn(itemButtonDelete, 3);
            itemButtonDelete.Click += SongInfoDeleteItemButton_Click;
            itemGrid.Children.Add(itemButtonDelete);

            return itemBorder;
        }

        private void SongInfoDeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button item = sender as Button;
            string[] infos = item.Tag as string[];
            string songId = infos[0];

            int index = 0;
            for (int i = 0; i < SelectedSongSource.Rows.Count; i++)
            {
                if (SelectedSongSource.Rows[i]["songid"].ToString().Equals(songId))
                {
                    index = i;
                    break;
                }
            }
            SelectedSongSource.Rows.RemoveAt(index);

            AddSelectedSongInfo();
        }

        private void SongInfoToTopItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button item = sender as Button;
            string[] infos = item.Tag as string[];
            string songId = infos[0];
            string songName = infos[1];
            string singerName = infos[2];
            string mvUrl = infos[3];
            string singRail = infos[4];
            DataRow row = SelectedSongSource.NewRow();
            row["songid"] = songId;
            row["songname"] = songName;
            row["singername"] = singerName;
            row["mvurl"] = mvUrl;
            row["singrail"] = singRail;

            int index = 0;
            for (int i = 0; i < SelectedSongSource.Rows.Count; i++)
            {
                if(SelectedSongSource.Rows[i]["songid"].ToString().Equals(songId))
                {
                    index = i;
                    break;
                }
            }
            if (index == 0)
                return;
            SelectedSongSource.Rows.RemoveAt(index);
            SelectedSongSource.Rows.InsertAt(row, 0);

            AddSelectedSongInfo();
        }

        #endregion

        #endregion

        #region FilterSearch

        #region NotifyProperty

        #region ShowLanguageTypeFilter

        private bool _ShowLanguageTypeFilter = false;

        public bool ShowLanguageTypeFilter
        {
            get { return _ShowLanguageTypeFilter; }
            set
            {
                if (_ShowLanguageTypeFilter.Equals(value)) return;
                _ShowLanguageTypeFilter = value;
                RaisePropertyChanged("ShowLanguageTypeFilter");
            }
        }

        #endregion

        #region ShowSongNameFilter

        private bool _ShowSongNameFilter = false;

        public bool ShowSongNameFilter
        {
            get { return _ShowSongNameFilter; }
            set
            {
                if (_ShowSongNameFilter.Equals(value)) return;
                _ShowSongNameFilter = value;
                RaisePropertyChanged("ShowSongNameFilter");
            }
        }

        #endregion
        
        #region SongNameSearchStr

        private string _SongNameSearchStr = string.Empty;

        public string SongNameSearchStr
        {
            get { return _SongNameSearchStr; }
            set
            {
                if (_SongNameSearchStr != null && _SongNameSearchStr.Equals(value)) return;
                _SongNameSearchStr = value;
                RaisePropertyChanged("SongNameSearchStr");
                SongInfoFilter = $"musicnameinitials like '{SongNameSearchStr}%'";
            }
        }

        #endregion
        
        #region ShowSongRankFilter

        private bool _ShowSongRankFilter = false;

        public bool ShowSongRankFilter
        {
            get { return _ShowSongRankFilter; }
            set
            {
                if (_ShowSongRankFilter.Equals(value)) return;
                _ShowSongRankFilter = value;
                RaisePropertyChanged("ShowSongRankFilter");
            }
        }

        #endregion

        #region ShowSongTypeFilter

        private bool _ShowSongTypeFilter = false;

        public bool ShowSongTypeFilter
        {
            get { return _ShowSongTypeFilter; }
            set
            {
                if (_ShowSongTypeFilter.Equals(value)) return;
                _ShowSongTypeFilter = value;
                RaisePropertyChanged("ShowSongTypeFilter");
            }
        }

        #endregion

        #region MinClickDate

        private DateTime _MinClickDate = new DateTime(2013, 11, 11);

        public DateTime MinClickDate
        {
            get { return _MinClickDate; }
            set
            {
                if (_MinClickDate != null && _MinClickDate.Equals(value)) return;
                _MinClickDate = value;
                RaisePropertyChanged("MinClickDate");
            }
        }

        #endregion


        #endregion

        #region Command

        #region NewSongFilterCmd

        private RelayCommand _NewSongFilterCmd;

        public ICommand NewSongFilterCmd
        {
            get { return _NewSongFilterCmd ?? (_NewSongFilterCmd = new RelayCommand(param => OnNewSongFilter(), param => CanNewSongFilterCmd())); }
        }

        public void OnNewSongFilter()
        {
            PageSelectedIndex = 2;
            DateTime date = DateTime.Now.AddMonths(-48);
            SongInfoFilter = $"releasedate > ('{date}')";
        }

        public bool CanNewSongFilterCmd()
        {
            return true;
        }

        #endregion

        #region LanguageTypeCmd

        private RelayCommand _LanguageTypeCmd;

        public ICommand LanguageTypeCmd
        {
            get { return _LanguageTypeCmd ?? (_LanguageTypeCmd = new RelayCommand(param => OnLanguageType(), param => CanLanguageType())); }
        }

        public void OnLanguageType()
        {
            PageSelectedIndex = 2;
            ShowLanguageTypeFilter = true;
            SongInfoFilter = $"languagetype in ('0001')";
        }

        public bool CanLanguageType()
        {
            return true;
        }

        #endregion

        #region SongNameFilterCmd

        private RelayCommand _SongNameFilterCmd;

        public ICommand SongNameFilterCmd
        {
            get { return _SongNameFilterCmd ?? (_SongNameFilterCmd = new RelayCommand(param => OnSongNameFilter(), param => CanSongNameFilter())); }
        }

        public void OnSongNameFilter()
        {
            PageSelectedIndex = 2;
            ShowSongNameFilter = true;
            //强制刷新
            SongInfoFilter = $"languagetype in ('0001')";
            SongInfoFilter = string.Empty;
        }

        public bool CanSongNameFilter()
        {
            return true;
        }

        #endregion

        #region SongSearchStrDeleteCmd

        private RelayCommand _SongSearchStrDeleteCmd;

        public ICommand SongSearchStrDeleteCmd
        {
            get { return _SongSearchStrDeleteCmd ?? (_SongSearchStrDeleteCmd = new RelayCommand(param => OnSongSearchStrDelete(), param => CanSongSearchStrDelete())); }
        }

        public void OnSongSearchStrDelete()
        {
            if (!string.IsNullOrEmpty(SongNameSearchStr))
                SongNameSearchStr = SongNameSearchStr.Remove(SongNameSearchStr.Length - 1);
        }

        public bool CanSongSearchStrDelete()
        {
            return true;
        }

        #endregion

        #region SongSearchStrClearCmd

        private RelayCommand _SongSearchStrClearCmd;

        public ICommand SongSearchStrClearCmd
        {
            get { return _SongSearchStrClearCmd ?? (_SongSearchStrClearCmd = new RelayCommand(param => OnSongSearchStrClear(), param => CanSongSearchStrClear())); }
        }

        public void OnSongSearchStrClear()
        {
            if (!string.IsNullOrEmpty(SongNameSearchStr))
                SongNameSearchStr = string.Empty;
        }

        public bool CanSongSearchStrClear()
        {
            return true;
        }

        #endregion

        #region SongRankCmd

        private RelayCommand _SongRankCmd;

        public ICommand SongRankCmd
        {
            get { return _SongRankCmd ?? (_SongRankCmd = new RelayCommand(param => OnSongRank(), param => CanSongRank())); }
        }

        public void OnSongRank()
        {
            PageSelectedIndex = 2;
            ShowSongRankFilter = true;

            GetRankFilter("华语榜");

        }

        public bool CanSongRank()
        {
            return true;
        }

        #endregion
        
        #region SongTypeCmd

        private RelayCommand _SongTypeCmd;

        public ICommand SongTypeCmd
        {
            get { return _SongTypeCmd ?? (_SongTypeCmd = new RelayCommand(param => OnSongType(), param => CanSongType())); }
        }

        public void OnSongType()
        {
            PageSelectedIndex = 2;
            ShowSongTypeFilter = true;
            string category = string.Empty;
            CategorySourceDict.TryGetValue("流行", out category);
            if(!string.IsNullOrEmpty(category))
                SongInfoFilter = $"category like '{category}' or " +
                        $" category like '{category},%' or " +
                        $" category like '%,{category},%' or " +
                        $" category like '%,{category}' ";
        }

        public bool CanSongType()
        {
            return true;
        }

        #endregion


        #endregion

        #region Methods

        public string GetLanguageTypeCode(string languageType) {
            if (languageType.Equals("国语"))
                languageType = "'0001'";
            else if (languageType.Equals("粤语"))
                languageType = "'0002'";
            else if (languageType.Equals("韩语"))
                languageType = "'0003'";
            else if (languageType.Equals("英语"))
                languageType = "'0004'";
            else if (languageType.Equals("日语"))
                languageType = "'0005'";
            else if (languageType.Equals("俄语"))
                languageType = "'0008'";
            else if (languageType.Equals("其他"))
                languageType = "'0006','0007','0009'";
            return languageType;
        }

        public void GetRankFilter(string rankName) {
            if (rankName.Equals("华语榜"))
            {
                /*
                 * 华语榜排序规则
                 * 1.按热度字段排序
                 * 2.去掉非国语和粤语类别的
                 * 3.最多选取100首
                 */
                SongInfoSort = $"newsonghot desc";
                SongInfoFilter = $"languagetype in ('0001', '0002')";
                if (MusicInfoSource.DefaultView.Count != 0)
                    if (MusicInfoSource.DefaultView.Count <= 100)
                        SongInfoFilter = $"newsonghot >= {MusicInfoSource.DefaultView[MusicInfoSource.DefaultView.Count - 1]["newsonghot"].ToString()} and {SongInfoFilter}";
                    else
                        SongInfoFilter = $"newsonghot >= {MusicInfoSource.DefaultView[99]["newsonghot"].ToString()} and {SongInfoFilter}";
            }
            else if (rankName.Equals("情歌对唱"))
            {
                /*
                 * 情歌对唱排序规则
                 * 1.按热度字段排序
                 * 2.去掉非情歌对唱类别的
                 * 3.最多选取100首
                 */
                SongInfoSort = $"newsonghot desc";
                string key = string.Empty;
                CategorySourceDict.TryGetValue("情歌对唱", out key);
                if (!string.IsNullOrEmpty(key))
                {
                    string categoryFilter = $" category like '{key}' or " +
                                            $" category like '{key},%' or " +
                                            $" category like '%,{key},%' or " +
                                            $" category like '%,{key}' ";
                    SongInfoFilter = categoryFilter;
                    if (MusicInfoSource.DefaultView.Count != 0)
                        if (MusicInfoSource.DefaultView.Count <= 100)
                            SongInfoFilter = $"newsonghot >= {MusicInfoSource.DefaultView[MusicInfoSource.DefaultView.Count - 1]["newsonghot"].ToString()} and {categoryFilter}";
                        else
                            SongInfoFilter = $"newsonghot >= {MusicInfoSource.DefaultView[99]["newsonghot"].ToString()} and {categoryFilter}";
                }
            }
            else if (rankName.Equals("影视金曲"))
            {
                /*
                 * 影视金曲排序规则
                 * 1.按热度字段排序
                 * 2.去掉非影视金曲类别的
                 * 3.最多选取100首
                 */
                SongInfoSort = $"newsonghot desc";
                string key = string.Empty;
                CategorySourceDict.TryGetValue("影视", out key);
                if (!string.IsNullOrEmpty(key))
                {
                    string categoryFilter = $" category like '{key}' or " +
                                            $" category like '{key},%' or " +
                                            $" category like '%,{key},%' or " +
                                            $" category like '%,{key}' ";
                    SongInfoFilter = categoryFilter;
                    if (MusicInfoSource.DefaultView.Count != 0)
                        if (MusicInfoSource.DefaultView.Count <= 100)
                            SongInfoFilter = $"newsonghot >= {MusicInfoSource.DefaultView[MusicInfoSource.DefaultView.Count - 1]["newsonghot"].ToString()} and {categoryFilter}";
                        else
                            SongInfoFilter = $"newsonghot >= {MusicInfoSource.DefaultView[99]["newsonghot"].ToString()} and {categoryFilter}";
                }
            }
            else if (rankName.Equals("经典老歌"))
            {
                /*
                 * 经典老歌排序规则
                 * 1.按热度字段排序
                 * 2.去掉发行日期晚于新老歌分割日期的
                 * 3.最多选取100首
                 */
                SongInfoSort = $"newsonghot desc";
                SongInfoFilter = $"releasedate < '{MinClickDate}'";
                if (MusicInfoSource.DefaultView.Count != 0)
                    if (MusicInfoSource.DefaultView.Count <= 100)
                        SongInfoFilter = $"newsonghot >= {MusicInfoSource.DefaultView[MusicInfoSource.DefaultView.Count - 1]["newsonghot"].ToString()} and releasedate < '{MinClickDate}'";
                    else
                        SongInfoFilter = $"newsonghot >= {MusicInfoSource.DefaultView[99]["newsonghot"].ToString()} and releasedate < '{MinClickDate}'";
            }
            else if (rankName.Equals("新歌榜"))
            {
                /*
                 * 新歌榜排序规则
                 * 1.按热度字段排序
                 * 2.去掉发行日期早于新老歌分割日期的
                 * 3.最多选取100首
                 */
                SongInfoSort = $"newsonghot desc";
                SongInfoFilter = $"releasedate >= '{MinClickDate}'";
                if (MusicInfoSource.DefaultView.Count != 0)
                    if (MusicInfoSource.DefaultView.Count <= 100)
                        SongInfoFilter = $"newsonghot >= {MusicInfoSource.DefaultView[MusicInfoSource.DefaultView.Count - 1]["newsonghot"].ToString()} and releasedate >= '{MinClickDate}'";
                    else
                        SongInfoFilter = $"newsonghot >= {MusicInfoSource.DefaultView[99]["newsonghot"].ToString()} and releasedate >= '{MinClickDate}'";
            }
        }

        public void AddSongTypeButton(WrapPanel MusicStyleWrapPanel, WrapPanel MusicTitleWrapPanel, WrapPanel MusicMoodWrapPanel)
        {
            /*
                <Button Content="流行" Style="{StaticResource SongTypeButton}"/>
             */
            for (int i = 0; i < CategorySource.Rows.Count; i++)
            {
                if (CategorySource.Rows[i]["fatherid"].ToString().Equals("0"))
                    continue;

                Button itemButton = new Button()
                {
                    Style = singerFilterListUc.FindResource("SongTypeButton") as Style,
                    Content = CategorySource.Rows[i]["categoryname"].ToString(),
                    Tag = CategorySource.Rows[i]["id"].ToString(),
                };
                itemButton.Click += SongTypeItemButton_Click;

                if (CategorySource.Rows[i]["fatherid"].ToString().Equals("3"))
                    MusicStyleWrapPanel.Children.Add(itemButton);
                else if (CategorySource.Rows[i]["fatherid"].ToString().Equals("2"))
                    MusicTitleWrapPanel.Children.Add(itemButton);
                else if (CategorySource.Rows[i]["fatherid"].ToString().Equals("1"))
                    MusicMoodWrapPanel.Children.Add(itemButton);
            }

        }

        private void SongTypeItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button item = sender as Button;
            string category = item.Tag.ToString();
            SongInfoFilter = $"category like '{category}' or " +
                    $" category like '{category},%' or " +
                    $" category like '%,{category},%' or " +
                    $" category like '%,{category}' ";
        }

        #endregion

        #endregion

        #region Speech

        #region NotifyProperty
        
        #region IsUsingSpeech

        private bool _IsUsingSpeech = false;

        public bool IsUsingSpeech
        {
            get { return _IsUsingSpeech; }
            set
            {
                if (_IsUsingSpeech.Equals(value)) return;
                _IsUsingSpeech = value;
                RaisePropertyChanged("IsUsingSpeech");
            }
        }

        #endregion

        private SpeechRecognitionEngine speech = new SpeechRecognitionEngine();

        #endregion

        #region Methods

        private void InitSpeech()
        {

            speech.SetInputToDefaultAudioDevice();
            GrammarBuilder grammarBuilder = new GrammarBuilder();
            grammarBuilder.Append(new Choices(new string[] { "暂停", "播放", "伴奏", "原唱", "切歌", "调大", "调小", "重唱" }));
            Grammar grammar = new Grammar(grammarBuilder);
            grammar.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(G_SpeechRecognized);
            speech.LoadGrammar(grammar);
            speech.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void G_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (!IsUsingSpeech) return;
            switch (e.Result.Text)
            {
                case "暂停":
                    OnSongPauseAndPlay();
                    break;
                case "播放":
                    OnSongPauseAndPlay();
                    break;
                case "伴奏":
                    OnSongAccompanyOrSing();
                    break;
                case "原唱":
                    OnSongAccompanyOrSing();
                    break;
                case "切歌":
                    OnSongToNext();
                    break;
                case "调大":
                    OnSoundUp();
                    break;
                case "调小":
                    OnSoundDown();
                    break;
                case "重唱":
                    OnSongAgain();
                    break;
            }
        }

        #endregion

        #endregion
    }
}