using SamanthaX.Library.Global;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SamanthaX.WebMVC {

    public class RouteConfig {

        public static void RegisterRoutes(RouteCollection routes) {

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            #region Read Encrypted File
            
            try {
                Variable.MyDirectoryPath = AppContext.BaseDirectory;
                Variable.MyDirectoryPathTest1 = AppDomain.CurrentDomain.BaseDirectory;
                Variable.MyDirectoryPathTest2 = Environment.CurrentDirectory;

                //  Pass the file path and file name to the StreamReader constructor
                //  AppContext.BaseDirectory => "D:\Empresas\202401 SupernovaSX\SamanthaX\SamanthaX.WebMVC\SamanthaX.WebMVC\"
                string strPathLog = Variable.MyDirectoryPath + "DataLog.txt";
                StreamReader sr = new StreamReader(strPathLog);
                //Read the first line of text
                String lineRead = sr.ReadLine();
                //Continue to read until you reach end of file
                List<string> lineCodeList = new List<string>();
                while (lineRead != null) {
                    lineCodeList.Add(lineRead);
                    //Read the next line
                    lineRead = sr.ReadLine();
                }
                sr.Close();
                
                Variable.APIUsername = lineCodeList[10];
                Variable.APIPassword = lineCodeList[13];
                Variable.Key = lineCodeList[22];
                Variable.IV = lineCodeList[26];
                Variable.DirectoryLog = lineCodeList[33];
                Variable.UrlApi = lineCodeList[32];

                Log.WriteToFile(
                    "Initializing",
                    "RouteConfig.RegisterRoutes",
                    $"Load Ok. DirectoryLog: {Variable.DirectoryLog}"
                );
            } catch (Exception ex) {
                Log.WriteToFile(
                    "Initializing",
                    "RouteConfig.RegisterRoutes",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            #endregion Read Encrypted File
        }
    }
}
