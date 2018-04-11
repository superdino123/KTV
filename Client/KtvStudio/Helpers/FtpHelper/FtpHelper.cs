using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Helpers.FtpHelper
{
    public class FtpHelper
    {
        //暂时没用了,改用七牛云
        public static FtpHelper instance = new FtpHelper();

        #region 构造函数
        /// <summary>
        /// 创建FTP工具
        /// <para>
        /// 默认不使用SSL,使用二进制传输方式,使用被动模式FTP有两种使用模式：主动和被动。
        /// 主动模式要求客户端和服务器端同时打开并且监听一个端口以建立连接。
        /// 在这种情况下，客户端由于安装了防火墙会产生一些问题。
        /// 所以，创立了被动模式。
        /// 被动模式只要求服务器端产生一个监听相应端口的进程，这样就可以绕过客户端安装了防火墙的问题。
        /// </para>
        /// </summary>
        public FtpHelper()
            : this(21, null, false, true, true)
        {
        }

        /// <summary>
        /// 创建FTP工具
        /// </summary>
        /// <param name="port">端口</param>
        /// <param name="enableSsl">允许Ssl</param>
        /// <param name="proxy">代理</param>
        /// <param name="useBinary">允许二进制</param>
        /// <param name="usePassive">允许被动模式</param>
        public FtpHelper(int port, IWebProxy proxy, bool enableSsl, bool useBinary, bool usePassive)
        {
            this.port = port;
            this.proxy = proxy;
            this.enableSsl = enableSsl;
            this.useBinary = useBinary;
            this.usePassive = usePassive;
        }
        #endregion

        #region 变量

        #region 主机
        private string host = ConfigurationManager.AppSettings["FtpUrl"];
        /// <summary>
        /// 主机
        /// </summary>
        public string Host
        {
            get
            {
                return this.host ?? string.Empty;
            }
        }
        #endregion

        #region 登录用户名
        private string userId = ConfigurationManager.AppSettings["FtpUser"];
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string UserId
        {
            get
            {
                return this.userId;
            }
        }
        #endregion

        #region 密码
        private string password = ConfigurationManager.AppSettings["FtpPwd"];
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }
        }
        #endregion

        #region 代理
        IWebProxy proxy = null;
        /// <summary>
        /// 代理
        /// </summary>
        public IWebProxy Proxy
        {
            get
            {
                return this.proxy;
            }
            set
            {
                this.proxy = value;
            }
        }
        #endregion

        #region 端口
        private int port = 21;
        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get
            {
                return port;
            }
            set
            {
                this.port = value;
            }
        }
        #endregion

        #region 设置是否允许Ssl
        private bool enableSsl = false;
        /// <summary>
        /// EnableSsl 
        /// </summary>
        public bool EnableSsl
        {
            get
            {
                return enableSsl;
            }
        }
        #endregion

        #region 使用被动模式
        private bool usePassive = true;
        /// <summary>
        /// 被动模式
        /// </summary>
        public bool UsePassive
        {
            get
            {
                return usePassive;
            }
            set
            {
                this.usePassive = value;
            }
        }
        #endregion

        #region 二进制方式
        private bool useBinary = true;
        /// <summary>
        /// 二进制方式
        /// </summary>
        public bool UseBinary
        {
            get
            {
                return useBinary;
            }
            set
            {
                this.useBinary = value;
            }
        }
        #endregion

        #region 远端路径
        private string remotePath = "/";
        /// <summary>
        /// 远端路径
        /// <para>
        ///     返回FTP服务器上的当前路径(可以是 / 或 /a/../ 的形式)
        /// </para>
        /// </summary>
        public string RemotePath
        {
            get
            {
                return remotePath;
            }
            set
            {
                string result = "/";
                if (!string.IsNullOrEmpty(value) && value != "/")
                {
                    result = "/" + value.TrimStart('/').TrimEnd('/') + "/";
                }
                this.remotePath = result;
            }
        }
        #endregion
        #endregion

        #region 创建一个FTP连接
        /// <summary>
        /// 创建一个FTP请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求方法</param>
        /// <returns>FTP请求</returns>
        private FtpWebRequest CreateRequest(string url, string method)
        {
            //建立连接
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
            request.Credentials = new NetworkCredential(this.userId, this.password);
            request.Proxy = this.proxy;
            request.KeepAlive = false;//命令执行完毕之后关闭连接
            request.UseBinary = useBinary;
            request.UsePassive = usePassive;
            request.EnableSsl = enableSsl;
            request.Method = method;
            return request;

        }
        #endregion

        #region 上传一个文件到远端路径下
        /// <summary>
        /// 把文件上传到FTP服务器的RemotePath下
        /// </summary>
        /// <param name="localFile">本地文件信息</param>
        /// <param name="remoteFileName">要保存到FTP文件服务器上的文件名称包含扩展名</param>
        public bool Upload(FileInfo localFile, string remoteFileName)
        {
            bool result = false;
            if (localFile.Exists)
            {
                string url = Host.TrimEnd('/') + RemotePath + remoteFileName;
                FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.UploadFile);

                //上传数据
                using (Stream rs = request.GetRequestStream())
                {
                    using (FileStream fs = localFile.OpenRead())
                    {
                        byte[] buffer = new byte[4096];//4K
                        int count = fs.Read(buffer, 0, buffer.Length);//每次从流中读4个字节再写入缓冲区
                        while (count > 0)
                        {
                            rs.Write(buffer, 0, count);
                            count = fs.Read(buffer, 0, buffer.Length);
                        }
                        fs.Close();
                        result = true;
                    }
                    rs.Close();
                }
                return result;
            }
            throw new Exception(string.Format("本地文件不存在,文件路径:{0}", localFile.FullName));
        }
        #endregion

        #region 从FTP服务器上下载文件
        /// <summary>
        /// 从当前目录下下载文件
        /// <para>
        /// 如果本地文件存在,则从本地文件结束的位置开始下载.
        /// </para>
        /// </summary>
        /// <param name="serverName">服务器上的文件名称</param>
        /// <param name="localName">本地文件名称</param>
        /// <returns>返回一个值,指示是否下载成功</returns>
        public bool Download(string serverName, string localName)
        {
            bool result = false;
            using (FileStream fs = new FileStream(localName, FileMode.Create)) //创建或打开本地文件
            {
                //建立连接
                string url = Host.TrimEnd('/') + RemotePath + serverName;
                FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.DownloadFile);
                request.ContentOffset = fs.Length;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    fs.Position = fs.Length;
                    byte[] buffer = new byte[4096];//4K
                    int count = response.GetResponseStream().Read(buffer, 0, buffer.Length);
                    while (count > 0)
                    {
                        fs.Write(buffer, 0, count);
                        count = response.GetResponseStream().Read(buffer, 0, buffer.Length);
                    }
                    response.GetResponseStream().Close();
                }
                result = true;
                fs.Close();
            }
            return result;
        }
        #endregion

        #region 重命名FTP服务器上的文件
        /// <summary>
        /// 文件更名
        /// </summary>
        /// <param name="oldFileName">原文件名</param>
        /// <param name="newFileName">新文件名</param>
        /// <returns>返回一个值,指示更名是否成功</returns>
        public bool Rename(string oldFileName, string newFileName)
        {
            bool result = false;
            //建立连接
            string url = Host.TrimEnd('/') + RemotePath + oldFileName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.Rename);
            request.RenameTo = newFileName;
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region 从当前目录下获取文件列表
        /// <summary>
        /// 获取当前目录下文件列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetFileList()
        {
            List<string> result = new List<string>();
            //建立连接
            string url = Host.TrimEnd('/') + RemotePath;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.ListDirectory);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);//中文文件名
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Add(line);
                    line = reader.ReadLine();
                }
            }
            return result;
        }
        #endregion

        #region 从FTP服务器上获取文件和文件夹列表
        /// <summary>
        /// 获取详细列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetFileDetails()
        {
            List<string> result = new List<string>();
            //建立连接
            string url = Host.TrimEnd('/') + RemotePath;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.ListDirectoryDetails);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);//中文文件名
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Add(line);
                    line = reader.ReadLine();
                }
            }
            return result;
        }
        #endregion

        #region 从FTP服务器上删除文件
        /// <summary>
        /// 删除FTP服务器上的文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>返回一个值,指示是否删除成功</returns>
        public bool DeleteFile(string fileName)
        {
            bool result = false;
            //建立连接
            string url = Host.TrimEnd('/') + RemotePath + fileName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.DeleteFile);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                result = true;
            }

            return result;
        }
        #endregion

        #region 在FTP服务器上创建目录
        /// <summary>
        /// 在当前目录下创建文件夹
        /// </summary>
        /// <param name="dirName">文件夹名称</param>
        /// <returns>返回一个值,指示是否创建成功</returns>
        public bool MakeDirectory(string dirName)
        {
            bool result = false;
            //建立连接
            string url = Host.TrimEnd('/') + RemotePath + dirName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.MakeDirectory);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region 从FTP服务器上删除目录
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="dirName">文件夹名称</param>
        /// <returns>返回一个值,指示是否删除成功</returns>
        public bool DeleteDirectory(string dirName)
        {
            bool result = false;
            //建立连接
            string url = Host.TrimEnd('/') + RemotePath + dirName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.RemoveDirectory);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region 从FTP服务器上获取文件大小
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public long GetFileSize(string fileName)
        {
            long result = 0;
            //建立连接
            string url = Host.TrimEnd('/') + RemotePath + fileName;
            FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.GetFileSize);
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                result = response.ContentLength;
            }

            return result;
        }
        #endregion

        #region 给FTP服务器上的文件追加内容
        /// <summary>
        /// 给FTP服务器上的文件追加内容
        /// </summary>
        /// <param name="localFile">本地文件</param>
        /// <param name="remoteFileName">FTP服务器上的文件</param>
        /// <returns>返回一个值,指示是否追加成功</returns>
        public bool Append(FileInfo localFile, string remoteFileName)
        {
            if (localFile.Exists)
            {
                using (FileStream fs = new FileStream(localFile.FullName, FileMode.Open))
                {
                    return Append(fs, remoteFileName);
                }
            }
            throw new Exception(string.Format("本地文件不存在,文件路径:{0}", localFile.FullName));
        }

        /// <summary>
        /// 给FTP服务器上的文件追加内容
        /// </summary>
        /// <param name="stream">数据流(可通过设置偏移来实现从特定位置开始上传)</param>
        /// <param name="remoteFileName">FTP服务器上的文件</param>
        /// <returns>返回一个值,指示是否追加成功</returns>
        public bool Append(Stream stream, string remoteFileName)
        {
            bool result = false;
            if (stream != null && stream.CanRead)
            {
                //建立连接
                string url = Host.TrimEnd('/') + RemotePath + remoteFileName;
                FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.AppendFile);
                using (Stream rs = request.GetRequestStream())
                {
                    //上传数据
                    byte[] buffer = new byte[4096];//4K
                    int count = stream.Read(buffer, 0, buffer.Length);
                    while (count > 0)
                    {
                        rs.Write(buffer, 0, count);
                        count = stream.Read(buffer, 0, buffer.Length);
                    }
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region 获取FTP服务器上的当前路径
        /// <summary>
        /// 获取FTP服务器上的当前路径
        /// </summary>
        public string CurrentDirectory
        {
            get
            {
                string result = string.Empty;
                string url = Host.TrimEnd('/') + RemotePath;
                FtpWebRequest request = CreateRequest(url, WebRequestMethods.Ftp.PrintWorkingDirectory);
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    string temp = response.StatusDescription;
                    int start = temp.IndexOf('"') + 1;
                    int end = temp.LastIndexOf('"');
                    if (end >= start)
                    {
                        result = temp.Substring(start, end - start);
                    }
                }
                return result;

            }
        }
        #endregion

        #region 检查当前路径上是否存在某个文件
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="fileName">要检查的文件名</param>
        /// <returns>返回一个值,指示要检查的文件是否存在</returns>
        public bool CheckFileExist(string fileName)
        {
            bool result = false;
            if (fileName != null && fileName.Trim().Length > 0)
            {
                fileName = fileName.Trim();
                List<string> files = GetFileList();
                if (files != null && files.Count > 0)
                {
                    foreach (string file in files)
                    {
                        if (file.ToLower() == fileName.ToLower())
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        #endregion

    }
}
