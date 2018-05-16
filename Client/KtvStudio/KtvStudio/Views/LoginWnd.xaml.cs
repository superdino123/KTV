using KtvStudio.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace KtvStudio.Views
{
    /// <summary>
    /// LoginWnd.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWnd : INotifyPropertyChanged
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

        public LoginWnd(ClientViewModel clientViewModel)
        {
            InitializeComponent();
            ClientViewModel = clientViewModel;
            ClientViewModel.loginWnd = this;

            Closing += LoginWnd_Closing;
        }

        private void LoginWnd_Closing(object sender, CancelEventArgs e)
        {
            if (ClientViewModel.LoginState)
                return;
            Process.GetCurrentProcess().Kill();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox item = sender as PasswordBox;
            ClientViewModel.CurrentStaffInfo.UserPassword = item.Password;
        }

        private void UpdatePasswordButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(ClientViewModel.CurrentStaffInfo.UserName) ||
                string.IsNullOrEmpty(OldPasswordTextBox.Text) ||
                string.IsNullOrEmpty(NewPasswordTextBox.Password) ||
                string.IsNullOrEmpty(NewPasswordAgainTextBox.Password))
            {
                MessageBox.Show("请将信息填写完整！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!NewPasswordTextBox.Password.Equals(NewPasswordAgainTextBox.Password)) {
                MessageBox.Show("两次输入新密码不一致！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ClientViewModel.CurrentStaffInfo.UserPassword = OldPasswordTextBox.Text;
            if (ClientViewModel.RoomInfoManagementServiceCaller.Login(ClientViewModel.CurrentStaffInfo) == 0)
            {
                MessageBox.Show("原用户名或密码不正确！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ClientViewModel.CurrentStaffInfo.UserPassword = NewPasswordTextBox.Password;
            ClientViewModel.RoomInfoManagementServiceCaller.UpdatePassword(ClientViewModel.CurrentStaffInfo);
            LoginTabControl.SelectedIndex = 0;
        }

        private void ReturnLogin_Click(object sender, RoutedEventArgs e)
        {
            ClientViewModel.CurrentStaffInfo.UserPassword = string.Empty;
            OldPasswordTextBox.Text = string.Empty;
            NewPasswordTextBox.Password = string.Empty;
            NewPasswordAgainTextBox.Password = string.Empty;
            LoginTabControl.SelectedIndex = 0;
        }

        private void ReturnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            ClientViewModel.CurrentStaffInfo.UserPassword = string.Empty;
            LoginTabControl.SelectedIndex = 1;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }
    }
}
