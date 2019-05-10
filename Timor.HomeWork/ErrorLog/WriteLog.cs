using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorLog
{
    public static class WriteLog
    {
        private static string SaveDirectoryPath = "Logs";
        public static void Write(string message)
        {
            var saveFilePath = SaveDirectoryPath + "/" + DateTime.Now.ToString("yyyyMMdd") + "ErrorLog.txt";
            if (!Directory.Exists(SaveDirectoryPath))
            {
                Directory.CreateDirectory(SaveDirectoryPath);
            }
            using (StreamWriter sw = new StreamWriter(saveFilePath, true))
            {
                sw.WriteLine(DateTime.Now + ":" + message);
            }
        }
    }

}
