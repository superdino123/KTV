using System;
using Qiniu.Util;
using Qiniu.Storage;
using System.IO;
using Qiniu.Http;
using Helpers;
using System.Windows;

namespace KtvStudio.Helpers.QiniuService
{
    public class QiniuService
    {
        public static string QINIU_URL = "http://p6axlbvba.bkt.clouddn.com";

        private static string AK = "vbFEYpJnosfn4SGoowq3eTQH3dDrv-w5gmZyxpyr";
        private static string SK = "Tzg_CaKHxXuNgDrke8hfIYWMb9MbLExWFCJr1jXF";
        // 目标空间名
        private static string bucket = "diplant";

        /// <summary>
        /// 上传文件到七牛云
        /// </summary>
        /// <param name="saveKey">目标文件名</param>
        /// <param name="localFile">本地文件</param>
        public static void UploadImage(string saveKey, string localFile)
        {
            // 上传策略
            PutPolicy putPolicy = new PutPolicy();
            // 设置要上传的目标空间
            putPolicy.Scope = bucket;
            // 上传策略的过期时间(单位:秒)
            putPolicy.SetExpires(3600);
            // 文件上传完毕后，在多少天后自动被删除
            putPolicy.DeleteAfterDays = 360;

            // 请注意这里的Zone设置(如果不设置，就默认为华东机房)
            // var zoneId = Qiniu.Common.AutoZone.Query(AK,BUCKET);
            // Qiniu.Common.Config.ConfigZone(zoneId);

            Mac mac = new Mac(AK, SK); // Use AK & SK here
                                       // 生成上传凭证
            string uploadToken = Auth.createUploadToken(putPolicy, mac);

            UploadOptions uploadOptions = null;

            // 上传完毕事件处理
            UpCompletionHandler uploadCompleted = new UpCompletionHandler(OnUploadCompleted);

            // 方式1：使用UploadManager
            Qiniu.Common.Config.PUT_THRESHOLD = 1024*1024;
            //可以适当修改,UploadManager会根据这个阈值自动选择是否使用分片(Resumable)上传    
            UploadManager um = new UploadManager();
            um.uploadFile(localFile, saveKey, uploadToken, uploadOptions, uploadCompleted);

            // 方式2：使用FormManager
            //FormUploader fm = new FormUploader();
            //fm.uploadFile(localFile, saveKey, token, uploadOptions, uploadCompleted);
            
        }

        private static void OnUploadCompleted(string key, ResponseInfo respInfo, string respJson)
        {
            // respInfo.StatusCode
            // respJson是返回的json消息，示例: { "key":"FILE","hash":"HASH","fsize":FILE_SIZE }
            MessageBox.Show($"文件上传成功！{respJson}", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            LogHelper.LogInfo($"{respJson}");
        }
    }
}
