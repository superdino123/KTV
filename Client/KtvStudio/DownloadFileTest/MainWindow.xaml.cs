using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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

namespace DownloadFileTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (HttpFileExist("http://p6axlbvba.bkt.clouddn.com/演员-薛之谦.mpg"))
            {
                DownloadHttpFile("http://p6axlbvba.bkt.clouddn.com/演员-薛之谦.mpg", @"g:\f1.mpg");
            }
            else
            {
                MessageBox.Show("文件不存在", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void DownloadHttpFile(String http_url, String save_url)
        {
            WebResponse response = null;
            //获取远程文件
            WebRequest request = WebRequest.Create(http_url);
            response = request.GetResponse();
            if (response == null) return;
            //读远程文件的大小
            pbDown.Maximum = response.ContentLength;
            //下载远程文件
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                Stream netStream = response.GetResponseStream();
                Stream fileStream = new FileStream(save_url, FileMode.Create);
                byte[] read = new byte[1024];
                long progressBarValue = 0;
                int realReadLen = netStream.Read(read, 0, read.Length);
                while (realReadLen > 0)
                {
                    fileStream.Write(read, 0, realReadLen);
                    progressBarValue += realReadLen;
                    pbDown.Dispatcher.BeginInvoke(new ProgressBarSetter(SetProgressBar), progressBarValue);
                    realReadLen = netStream.Read(read, 0, read.Length);
                }
                netStream.Close();
                fileStream.Close();

            }, null);
        }
        /// <summary>
        ///  判断远程文件是否存在
        /// </summary>
        /// <param name="fileUrl">文件URL</param>
        /// <returns>存在-true，不存在-false</returns>
        private bool HttpFileExist(string http_file_url)
        {
            WebResponse response = null;
            bool result = false;//下载结果
            try
            {
                response = WebRequest.Create(http_file_url).GetResponse();
                result = response == null ? false : true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }
        public delegate void ProgressBarSetter(double value);
        public void SetProgressBar(double value)
        {
            //显示进度条
            pbDown.Value = value;
            //显示百分比
            label1.Content = (value / pbDown.Maximum) * 100 + "%";
        }
    }
}
