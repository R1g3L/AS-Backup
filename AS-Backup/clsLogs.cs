using System;
using System.IO;

namespace AS_Backup
{
    class clsLogs
    {
        public void addLogMessage(string path, string filename, string message)
        {
            using (StreamWriter w = File.AppendText(path + "\\" + filename + ".csv"))
            {
                w.WriteLine("{0};{1}", DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss"), message);
            }
        }        
    }
}
