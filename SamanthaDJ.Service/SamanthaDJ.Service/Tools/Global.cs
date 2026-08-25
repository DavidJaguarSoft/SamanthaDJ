using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaDJ.ServiceWPF.Tools {

    public class Global {
        public static string PathFileDataTxt = "C:\\DavidJaguarSoft\\SamanthaDJ";
        public static string PathLogOut = "C:\\DavidJaguarSoft\\SamanthaDJ\\LogFile";
        public static string SamanthaDJServiceVersion = "20260516.01";
        // URL that returns JSON with update info: { "version":"20260325.02", "installerUrl":"https://.../SamanthaDJ_Setup.exe" }
        public static string UpdateInfoUrl = ""; // set this to your update metadata endpoint or release JSON URL
        public static string AllRighsReserved = "Copyright © 2013 David Jaguar Soft. All rights reserved.";
    }
}