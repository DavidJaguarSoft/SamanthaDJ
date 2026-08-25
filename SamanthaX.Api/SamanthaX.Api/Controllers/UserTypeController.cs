using SamanthaX.API.Utils;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace SamanthaX.API.Controllers {

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserSXTypeController : ApiController {

        #region "GetId"

        [System.Web.Http.Route("api/UserType/GetId")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(UserTypeStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetId(UserTypeStruct userTypeEs) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new UserTypeStruct();

            try {
                UserTypeEn userType = Service.UserTypeService.GetId(userTypeEs.UserType.UserTypeId);

                result.StatusOk = true;
                result.Message = userType == null ? "Item NO found" : "Item found !";
                result.ItemsFound = userType == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.UserType = userType;
                result.UserTypeList = null;
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.UserType = null;
                result.UserTypeList = null;
            }

            return Ok(result);
        }

        #endregion

        #region Save

        [System.Web.Http.Route("api/UserType/Save")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(UserTypeStruct))]
        [BasicAuthentication]
        public IHttpActionResult Save(UserTypeStruct userTypeEs) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new UserTypeStruct();

            try {
                UserTypeEn userType = Service.UserTypeService.Save(userTypeEs.UserType);

                result.StatusOk = true;
                result.Message = userType == null ? "Item could not have been saved !" : "Item saved !";
                result.ItemsFound = userType == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.UserType = userType;
                result.UserTypeList = null;
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.UserType = null;
                result.UserTypeList = null;
            }

            return Ok(result);
        }

        #endregion Save

        #region Delete

        [System.Web.Http.Route("api/UserType/Delete")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(UserTypeStruct))]
        [BasicAuthentication]
        public IHttpActionResult Delete(UserTypeStruct userTypeEs) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new UserTypeStruct();
            
            try {
                UserTypeEn userType = Service.UserTypeService.Delete(userTypeEs.UserType);
                //
                result.StatusOk = true;
                result.Message = userType == null ? "item could not have been affected !" : "Item deleted !";
                result.ItemsFound = userType == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.UserType = userType;
                result.UserTypeList = null;
                //
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.UserType = null;
                result.UserTypeList = null;
            }

            return Ok(result);
        }

        #endregion Delete

    }
}