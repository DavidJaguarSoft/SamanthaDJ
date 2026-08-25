using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SamanthaX.Api.Utils {

    public class Log {

        #region WriteToFile

        /// <summary>
        ///     Save log to file
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="origin">Origin log</param>
        /// <param name="message">Message log</param>
        public static void WriteToFile(int userId, string origin, string message) {
            string userIdSt = $"User_{userId.ToString("####")}";
            WriteData(userIdSt, origin, message);
        }

        public static void WriteToFile(string id, string origin, string message) {
            WriteData(id, origin, message);
        }

        #endregion WriteToFile

        #region WriteData

        private static void WriteData(string id, string origin, string message) {
            bool tryWrite = true;
            int tryCount = 0;
            while (tryWrite) {
                try {
                    string nameDirectoryYear = $"{Global.MyDirectoryPath}{Global.DirectoryLog}\\{DateTime.Now.ToString("yyyy")}";
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