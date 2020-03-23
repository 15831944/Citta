﻿using System;
using System.Windows.Forms;

using Citta_T1.Utils;

namespace Citta_T1
{
    static class Program
    {
        // 数据预览字典
        public static BCPBuffer GlobalBCPBuffer = new BCPBuffer();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
