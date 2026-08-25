using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamanthaX.WebMVC.Filters {

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute {

        private UserEn oUser;
        private int idOperacion;

        public AuthorizeUser(int idOperacion = 0) {
            this.idOperacion = idOperacion;
        }

        public override void OnAuthorization(AuthorizationContext filterContext) {

            string nombreOperacion = string.Empty;
            string nombreModulo = string.Empty;

            try {

            } catch (Exception ex) {
                filterContext.Result =
                    new RedirectResult("~/Errir/UnauthorizedOperation?operacion=" + nombreOperacion);
            }

            base.OnAuthorization(filterContext);
        }
    }
}