using System;
using System.IO;
using System.Configuration;

namespace AS_Backup
{
    class Program
    {
        public static clsLogs log = new clsLogs();
        public static string pathLogs;
        public static string filenameLogs;
        public static string tempFolderOpodo;
        public static string tempFolderOpodo_toDelete;
            
        static void Main(string[] args)
        {
            Console.WriteLine("AS-Backup 0.0.3");
            // Leggo le variabili dal file di configurazione
            string sourceFoldereDreams = ConfigurationManager.AppSettings["sourceFoldereDreams"];
            string destinationFoldereDreams = ConfigurationManager.AppSettings["destinationFoldereDreams"];
            string filtereDreams = ConfigurationManager.AppSettings["filtereDreams"];
            string prefixeDreams = ConfigurationManager.AppSettings["prefixeDreams"];
            string sourceFolderOpodo = ConfigurationManager.AppSettings["sourceFolderOpodo"];
            string destinationFolderOpodo = ConfigurationManager.AppSettings["destinationFolderOpodo"];
            string filterOpodo = ConfigurationManager.AppSettings["filterOpodo"];
            string prefixOpodo = ConfigurationManager.AppSettings["prefixOpodo"];

            // Variabili per eventuali logs
            pathLogs = ConfigurationManager.AppSettings["pathLogs"];
            filenameLogs = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
            tempFolderOpodo = sourceFolderOpodo + "\\" + DateTime.Now.ToString("yyyy_MM_dd");
            tempFolderOpodo_toDelete = sourceFolderOpodo + "\\" + DateTime.Now.AddDays(-1).ToString("yyyy_MM_dd");
            
            // Controllo file eDreams
            Console.WriteLine("EDREAMS");
            if ((checkFolder(sourceFoldereDreams)) && (checkFolder(destinationFoldereDreams)))
                getSourceFile(sourceFoldereDreams, destinationFoldereDreams, filtereDreams, prefixeDreams);
            
            // Controllo file Opodo
            Console.WriteLine("OPODO");
            if ((checkFolder(sourceFolderOpodo)) && (checkFolder(destinationFolderOpodo)))
            {
                try
                {
                    if (checkFolder(tempFolderOpodo_toDelete))
                    {
                        DirectoryInfo oDir = new DirectoryInfo(tempFolderOpodo_toDelete);
                        FileInfo[] aFile = oDir.GetFiles(filterOpodo);
                        if (aFile.Length==0)
                            Directory.Delete(tempFolderOpodo_toDelete);
                        else
                            getSourceFile(tempFolderOpodo_toDelete, destinationFolderOpodo, filterOpodo, prefixOpodo);
                    }
                    else
                    {
                        if (checkFolder(tempFolderOpodo))
                            getSourceFile(tempFolderOpodo, destinationFolderOpodo, filterOpodo, prefixOpodo);
                    }                    
                }
                catch
                {
                    log.addLogMessage(pathLogs, filenameLogs, "copy function - Opodo");
                }
            }
                
            
            // Fine
            System.Threading.Thread.Sleep(2000);
        }

        static bool checkFolder(string folder)
        {
            try
            {
                if ((folder.Substring(folder.Length - 1, 1) == "\\") || (!Directory.Exists(folder)))
                {
                    log.addLogMessage(pathLogs, filenameLogs, "checkFolder - Impossibile trovare la cartella ("+folder.ToString()+")");
                    return false;
                }
                else
                    return true;                
            }
            catch
            {
                log.addLogMessage(pathLogs, filenameLogs, "checkFolder in errore!!!");
                return false;
            }            
        }

        static void getSourceFile(string sourceFolder, string destinationFolder, string filter, string prefix)
        {
            try
            {
                DirectoryInfo oDir = new DirectoryInfo(sourceFolder);
                FileInfo[] aFile = oDir.GetFiles(filter);
                foreach (FileInfo oFile in aFile)
                {
                    string filename;
                    if (oFile.Name.Contains("DGEN"))
                        filename = oFile.Name.Substring(5, oFile.Name.Length - 5).Replace(Path.GetExtension(oFile.Name), ".txt");
                    else
                        filename = oFile.Name.Replace(Path.GetExtension(oFile.Name), ".txt");

                    filename = tempo() + "_" + filename;
                    File.Move(oFile.FullName, destinationFolder + "\\" + filename);
                    Console.WriteLine("{0} Move File: {1} to: {2}", prefix, oFile.Name, filename);
                    System.Threading.Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                log.addLogMessage(pathLogs, filenameLogs, "getSourceFile " + prefix + " - " + ex.Message);
            }            
        }

        static string tempo()
        {
            DateTime now = DateTime.Now;
            string year = now.Year.ToString("0000");
            string month = now.Month.ToString("00");
            string day = now.Day.ToString("00");
            string hour = now.Hour.ToString("00");
            string minute = now.Minute.ToString("00");
            string second = now.Second.ToString("00");
            string mil = now.Millisecond.ToString("00");
            return year + month + day + hour + minute + second;
        }
    }
}
