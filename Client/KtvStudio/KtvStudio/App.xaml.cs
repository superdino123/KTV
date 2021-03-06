﻿using Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KtvStudio
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CHS");
            string strLogConfig = AppDomain.CurrentDomain.BaseDirectory + @"Configs\LogSettingFile\NLog.dll.nlog";
            LogHelper.SetConfig(new FileInfo(strLogConfig));
        }
        
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var splashScreen = new SplashScreen();
            splashScreen.Show();
            Thread.Sleep(1000);
            new MainWindow(splashScreen).Show();
        }
    }
}
