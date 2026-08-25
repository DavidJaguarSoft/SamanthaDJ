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
    public class SamanthaVoiceController : ApiController {

        #region GetUser

        [System.Web.Http.Route("api/SamanthaVoice/GetUser")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(SamanthaVoiceStruct))]
        public IHttpActionResult GetUser(SamanthaVoiceStruct svSt) {

            var result = new SamanthaVoiceStruct();

            try {
                SamanthaVoiceEn recognizedWordEn =
                    Service
                    .SamanthaVoiceService.GetUser(
                        svSt.SamanthaVoice.UserId,
                        svSt.SamanthaVoice.LanguageId
                    );
                result.StatusOk = true;
                result.StackTrace = String.Empty;
                result.SamanthaVoiceList = null;
                result.Message = recognizedWordEn == null ? "Item NO found" : "Item found !";
                result.ItemsFound = recognizedWordEn == null ? 0 : 1;
                result.SamanthaVoice = recognizedWordEn;
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound= 0;
                result.StackTrace = ex.StackTrace;
                result.SamanthaVoice = null;
                result.SamanthaVoiceList = null;
                Log.WriteToFile(
                    svSt.SamanthaVoice.UserId,
                    "api/SamanthaVoice/GetUser",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetUser

        #region Save

        [System.Web.Http.Route("api/SamanthaVoice/Save")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(SamanthaVoiceStruct))]
        [BasicAuthentication]
        public IHttpActionResult Save(SamanthaVoiceStruct svSt) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new SamanthaVoiceStruct();

            try {
                SamanthaVoiceEn smEn = 
                    Service
                    .SamanthaVoiceService
                    .Save(svSt.SamanthaVoice);
                result.StatusOk = true;
                if (smEn != null) {
                    result.Message = "Item saved!";
                    result.ItemsFound = 1;
                } else {
                    result.Message = "Item could not have been saved !";
                    result.ItemsFound = 0;
                }
                result.SamanthaVoice = smEn;
                result.SamanthaVoiceList = null;
            } catch (Exception ex) {
                //  log
                //  string json = Newtonsoft.Json.JsonConvert.SerializeObject(objectRequest);
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.SamanthaVoice = null;
                result.SamanthaVoiceList = null;
                Log.WriteToFile(
                    svSt.SamanthaVoice.UserId,
                    "api/SamanthaVoice/Save",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion Save

        #region GetAllxUser

        [System.Web.Http.Route("api/SamanthaVoice/GetAllxUser")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(SamanthaVoiceStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetAllxUser(SamanthaVoiceStruct svSt) {
            //
            string username = Thread.CurrentPrincipal.Identity.Name;
            UserEn user = new UserEn();
            var result = new SamanthaVoiceStruct();
            try {
                //  Validating user
                user = Service
                   .UserService
                   .UserGetNameToken(svSt.Username, svSt.Token);
                if (user == null) {
                    throw new Exception("Invalid User");
                }
                //
                SamanthaVoiceEn recognizedWordEn =
                    Service
                    .SamanthaVoiceService.GetUser(
                        user.UserId,
                        svSt.SamanthaVoice.LanguageId
                    );
                result.StatusOk = true;
                result.StackTrace = String.Empty;
                result.SamanthaVoiceList = null;
                result.Message = recognizedWordEn == null ? "Item NO found" : "Item found !";
                result.ItemsFound = recognizedWordEn == null ? 0 : 1;
                result.SamanthaVoice = recognizedWordEn;

                Log.WriteToFile(
                   svSt.Username,
                   "api/SamanthaVoice/GetAllxUser",
                   result.Message
               );
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.SamanthaVoice = null;
                result.SamanthaVoiceList = null;
                Log.WriteToFile(
                    $"{svSt.Username}",
                    "api/SamanthaVoice/GetAllxUser",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetUser
    }
}