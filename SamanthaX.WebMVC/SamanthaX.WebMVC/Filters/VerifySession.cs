using SamanthaX.WebMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamanthaX.WebMVC.Filters {

    public class VerifySession : ActionFilterAttribute {
        //private SupernovaX.Model.Entity.Core.UserSXEn oUser;
        private string oUser;
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            try {
                base.OnActionExecuting(filterContext);
                //oUser = (UserSXEn)HttpContext.Current.Session["SystemUserEMail"];
                var myObject = HttpContext.Current.Session["SystemUserEMail"];

                if (myObject == null) {
                    if (filterContext.Controller is HomeController==false) {
                        filterContext.HttpContext.Response.Redirect("/Home/Login");
                    }
                } else {
                    oUser = (string)myObject;
                }
            } catch(Exception ex) {
                filterContext.Result = new RedirectResult("~/Home/Login");
            }
        }
    }
}