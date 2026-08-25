using Samantha.Repository;
using SamanthaX.Api.Utils;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace SamanthaX.Api {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {

            // Web API configuration and services
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //  Microsoft.AspNet.WebApi.Cors
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            #region Read Encrypted File

            try {
                Global.MyDirectoryPath = AppContext.BaseDirectory;

                //Pass the file path and file name to the StreamReader constructor
                if (System.Environment.MachineName.ToUpper().Equals("PC-DJAGUAR")) {}

                string strPathLog = Global.MyDirectoryPath + "DataLog.txt";
                StreamReader sr = new StreamReader(strPathLog);
                //  Read the first line of text
                String lineRead = sr.ReadLine();
                //Continue to read until you reach end of file
                List<string> lineCodeList = new List<string>();
                while (lineRead != null) {
                    lineCodeList.Add(lineRead);
                    //Read the next line
                    lineRead = sr.ReadLine();
                }
                sr.Close();
                Global.APIUsername = lineCodeList[10];
                Global.APIPassword = lineCodeList[13];
                Global.Key = lineCodeList[22];
                Global.IV = lineCodeList[26];
                Global.DirectoryLog = lineCodeList[33];

                Security security = new Security();
                //  Samantha Connection string
                string samanthaStringConnection = security.Decrypt(lineCodeList[19]);
                MainProgram.gSQLconexionSamanthaX = new System.Data.SqlClient.SqlConnection(samanthaStringConnection);

                #region EnrcryptMe

                //  Server=PC-DJaguar\\SQLDJaguar;Database=SamanthaX;user id=sa;password=SQLDJaguar;MultipleActiveResultSets=True
                //  - ZwRzS+ojDYFko3YJRPh+SMzp+cYnP3meRDj+UH1U89QoVGbVZ24Tuw36A7oqfmNivMXLL1L7dl5Jcwsc4lgjQ4URL4BRXe3NEwBI5S2BJyvULPeORi0GEYq7ETQcgjP2XC1n5537XcpvQhitSkIW6Q==
                //  Server=PC-DJaguar\\SQLDJaguar;Database=SupernovaX;user id=sa;password=SQLDJaguar;MultipleActiveResultSets=True
                //  Server=65.99.205.97\MSSQLSERVER2017;Database=davidja2_SamanthaX;user id=davidja2_UserX;password=YLBqgujdahv073*?;MultipleActiveResultSets=True
                //  - 1q9ydXCqQYG4rRM7uXbqb0Y137nNTcj11w3W3hLNL0i+irTIq5HwRsxehQz2QCbIFZib4ZQQ4ZGZpztTzbiDH0B6WeBiBHrJn2E6we7WaKabS7QTHBJt2plGxCPPyc7EX+4u8p29WPJG9xweMQZ9VWAQ1erqitteJhMG01rnvp9m+UrP/6uGBcGhQbzOJjAD
                //  Server=PC-Baltec\\SQLBaltec;Database=SamanthaX;user id=sa;password=SQLBaltec;MultipleActiveResultSets=True
                //  - qEGnIJph49eHQxTrxN8bJLplDg4dlpNWbzGE6EiRI38V3lmWEEu0/h7Kw7VSL45CuXyg/VrJrFMJvKTr2V23vOct8+i2w3qlUkoPHxub8QN3MG5vhHWWa6QQAXghdq2Az6KwPTT/ZE476ubezvcAHg==
                string whatsThatEncryptMe = security.Encrypt("TitireteRo1616");
                string whatsThatDecryptMe = security.Decrypt("QeWfmEZytnlFzNJjR8MBrA==");

                #endregion EnrcryptMe

                Log.WriteToFile(
                    "Initializing",
                    "WebApiConfig.Register",
                    "Message: Load Ok"
                );

            } catch (Exception ex) {
                Log.WriteToFile(
                    "Initializing",
                    "WebApiConfig.Register",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            #endregion Read Encrypted File
        }
    }
}
