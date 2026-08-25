using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SamanthaX.Library.Global {

    public class Log {

        #region WriteToFile

        public static void WriteToFile(int userId, string origin, string message) {
            string userIdSt = $"User_{userId.ToString("####")}";
            WriteData(userIdSt, origin, message);
        }

        public static void WriteToFile(string module, string method, string message) {
            WriteData(module, method, message);
        }

        public static void WriteToFile(string fileName, string strInfo, bool hour = false) {
            bool tryWrite = true;
            int tryCount = 0;
            while (tryWrite) {
                try {
                    string nameDirectoryMonth = DateTime.Now.ToString("yyyy-MM");
                    if (!Directory.Exists(nameDirectoryMonth))
                        Directory.CreateDirectory(nameDirectoryMonth);
                    string nameDirectoryDay = DateTime.Now.ToString("yyyy-MM-dd");
                    if (!Directory.Exists($"{nameDirectoryMonth}\\{nameDirectoryDay}"))
                        Directory.CreateDirectory($"{nameDirectoryMonth}\\{nameDirectoryDay}");
                    //
                    string hourSt = hour ? "HH" : "";
                    string nameFileComplement = DateTime.Now.ToString($"yyyyMMdd{hourSt}");
                    string sNombreTXTSession = $"{nameDirectoryMonth}\\{nameDirectoryDay}\\{fileName}_{nameFileComplement}.txt";
                    StringBuilder sb = new StringBuilder();

                    string tryCountSt = tryCount > 0 ? $"({tryCount})" : "";
                    sb.AppendLine($"{tryCountSt}{DateTime.Now.ToString("yyy-MM-dd HH:mm:ss.ffff ")} {strInfo}");
                    using (StreamWriter archivo = new StreamWriter(sNombreTXTSession, true)) {
                        archivo.Write(sb.ToString());
                    }
                    tryWrite = false;
                } catch (Exception ex) {
                    Console.WriteLine("Exception: " + ex.Message);
                    tryCount++;
                    if (tryCount > 15) {
                        tryWrite = false;
                        return;
                    }
                    Thread.Sleep(10);
                }
            }
        }

        #endregion WriteToFile

        #region WriteData

        private static void WriteData(string id, string origin, string message) {
            bool tryWrite = true;
            int tryCount = 0;
            while (tryWrite) {
                try {
                    string nameDirectoryYear = $"{Global.Variable.MyDirectoryPath}{Global.Variable.DirectoryLog}\\{DateTime.Now.ToString("yyyy")}";
                    if (!Directory.Exists(nameDirectoryYear))
                        Directory.CreateDirectory(nameDirectoryYear);

                    string nameDirectoryYearMonth = $"{nameDirectoryYear}\\{DateTime.Now.ToString("yyyy-MM")}";
                    if (!Directory.Exists(nameDirectoryYearMonth))
                        Directory.CreateDirectory(nameDirectoryYearMonth);
                    string nameDirectoryDay = DateTime.Now.ToString("yyyy-MM-dd");
                    if (!Directory.Exists($"{nameDirectoryYearMonth}\\{nameDirectoryDay}"))
                        Directory.CreateDirectory($"{nameDirectoryYearMonth}\\{nameDirectoryDay}");
                    //
                    string nameFileComplement = DateTime.Now.ToString($"yyyyMMdd");
                    string sNombreTXTSession = $"{nameDirectoryYearMonth}\\{nameDirectoryDay}\\{id}_{nameFileComplement}.txt";

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"{DateTime.Now.ToString("yyy-MM-dd HH:mm:ss.ffff")} *{origin}* {message}");
                    using (StreamWriter archivo = new StreamWriter(sNombreTXTSession, true)) {
                        archivo.Write(sb.ToString());
                    }
                    tryWrite = false;
                } catch (Exception ex) {
                    Console.WriteLine("Exception: " + ex.Message);
                    tryCount++;
                    if (tryCount > 2) {
                        tryWrite = false;
                        return;
                    }
                }
            }
        }

        #endregion WriteData
    }
}