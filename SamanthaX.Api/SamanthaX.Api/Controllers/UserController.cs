
using SamanthaX.Api.Utils;
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
    public class UserController : ApiController {

        #region GetNamePassword

        [System.Web.Http.Route("api/User/GetNamePassword")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(UserStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetNamePassword(UserStruct userEs) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new UserStruct();

            try {
                //  Validating user
                Security security = new Security();
                string passEncrypt = security.Encrypt(userEs.Password);

                UserEn user =
                    Service
                   .UserService
                   .GetNamePassword(userEs.Username, passEncrypt);

                if (user != null) {
                    result.Message = "Item found";
                    result.ItemsFound = 1;
                    result.StatusOk = true;
                    user.Company = Service
                        
                        .CompanyService
                        .GetId(user.CompanyId);
                    user.UserType = Service
                        .UserTypeService
                        .GetId(user.UserTypeId);
                    user.Language = Service
                        
                        .LanguageService
                        .GetId(user.LanguageId);
                    result.User = user;
                } else {
                    result.Message = "Item NO found !";
                    result.ItemsFound = 0;
                    result.User = null;
                    result.StatusOk = false;
                }
                result.UserList = null;

                Log.WriteToFile(
                    userEs.Username,
                    "api/User/GetNamePassword",
                    result.Message
                );
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.User = null;
                result.UserList = null;
                Log.WriteToFile(
                    userEs.Username,
                    "api/User/GetNamePassword",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetNamePassword

        #region GetNameToken

        [System.Web.Http.Route("api/User/GetNameToken")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(UserStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetNameToken(UserStruct userSt) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new UserStruct();

            try {
                //  Validating user
                UserEn user =
                    Service
                   .UserService
                   .UserGetNameToken(userSt.Username, userSt.Token);

                if (user != null) {
                    result.Message = "Item found";
                    result.ItemsFound = 1;
                    result.StatusOk = true;
                    user.Company = Service

                        .CompanyService
                        .GetId(user.CompanyId);
                    user.UserType = Service
                        .UserTypeService
                        .GetId(user.UserTypeId);
                    user.Language = Service

                        .LanguageService
                        .GetId(user.LanguageId);
                    result.User = user;
                } else {
                    result.Message = "Item NO found !";
                    result.ItemsFound = 0;
                    result.User = null;
                    result.StatusOk = false;
                }
                result.UserList = null;

                Log.WriteToFile(
                    userSt.Username,
                    "api/User/GetNameToken",
                    result.Message
                );
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.User = null;
                result.UserList = null;
                Log.WriteToFile(
                    userSt.Username,
                    "api/User/GetNameToken",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetNameToken

        #region Save

        [System.Web.Http.Route("api/User/Save")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(UserStruct))]
        [BasicAuthentication]
        public IHttpActionResult Save(UserStruct userEs) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new UserStruct();

            try {
                UserEn user = Service.UserService.Save(userEs.User);

                result.StatusOk = true;
                result.Message = "Item saved !";
                result.ItemsFound = 1;
                user.UserType =
                    Service
                    .UserTypeService
                    .GetId(user.UserTypeId);
                user.Language =
                            Service.LanguageService.GetId(user.LanguageId);
                result.User = user;
                result.UserList = null;
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.User = null;
                result.UserList = null;
                Log.WriteToFile(
                    userEs.User.UserId,
                    "api/User/Save",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion Save
    }
}