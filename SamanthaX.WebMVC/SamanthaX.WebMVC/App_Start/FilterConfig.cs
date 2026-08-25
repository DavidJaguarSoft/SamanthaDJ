using System.Web;
using System.Web.Mvc;

namespace SamanthaX.WebMVC {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SamanthaX.WebMVC.Filters.VerifySession());
        }
    }
}
