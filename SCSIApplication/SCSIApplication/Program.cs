using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace SCSIApplication
{
    static class Program
    {
        static Form1 myForm;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            string logString = "\r\n\r\n==============" + DateTime.Now + @" - Open Program" + "==============" + "\r\n";
            EventLog.Write(logString);
            //string value1 = ConfigurationManager.AppSettings["SettingKey1"];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            myForm = new Form1();
            Application.Run(myForm);
        }
    }
}
