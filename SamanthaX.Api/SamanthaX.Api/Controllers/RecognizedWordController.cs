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
    public class RecognizedWordController : ApiController {

        #region GetId

        [System.Web.Http.Route("api/RecognizedWord/GetId")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RecognizedWordStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetId(RecognizedWordStruct recognizedWordSt) {
            //
            string username = Thread.CurrentPrincipal.Identity.Name;
            //
            var result = new RecognizedWordStruct();

            try {

                RecognizedWordEn oResult =
                    Service
                    .RecognizedWordService
                    .GetId(recognizedWordSt.RecognizedWord.RecognizedWordId);

                result.StatusOk = true;
                result.StackTrace = String.Empty;

                result.StatusOk = true;
                result.Message = oResult == null ? "Item could not have been found !" : "Item found !";
                result.ItemsFound = oResult == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.RecognizedWord = oResult;
                result.RecognizedWordList = null;
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.RecognizedWord = null;
                result.RecognizedWordList = null;
                Log.WriteToFile(
                    recognizedWordSt.RecognizedWord.UserId,
                    "api/RecognizedWord/GetId",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetId

        #region GetAll

        [System.Web.Http.Route("api/RecognizedWord/GetAll")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RecognizedWordStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetAll(RecognizedWordStruct recognizedWordSt) {
            //
            string username = Thread.CurrentPrincipal.Identity.Name;
            //
            var result = new RecognizedWordStruct();

            try {

                List<RecognizedWordEn> recognizedWordList =
                    Service
                    .RecognizedWordService
                    .GetAll(recognizedWordSt.RecognizedWord.UserId);

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                result.RecognizedWord = null;
                if(recognizedWordList != null) {
                    result.Message = $"{recognizedWordList.Count} Items found !";
                    result.ItemsFound = recognizedWordList.Count;
                    result.RecognizedWordList = recognizedWordList;
                } else {
                    result.Message = $"Items NO found !";
                    result.ItemsFound = 0;
                    result.RecognizedWordList = null;
                }
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound= 0;
                result.StackTrace = ex.StackTrace;
                result.RecognizedWord = null;
                result.RecognizedWordList = null;
                Log.WriteToFile(
                    recognizedWordSt.RecognizedWord.UserId,
                    "api/RecognizedWord/GetAll",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetAll

        #region Save

        [System.Web.Http.Route("api/RecognizedWord/Save")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RecognizedWordStruct))]
        [BasicAuthentication]
        public IHttpActionResult Save(RecognizedWordStruct recognizedWordSt) {
            //
            string username = Thread.CurrentPrincipal.Identity.Name;
            //
            var result = new RecognizedWordStruct();

            try {

                RecognizedWordEn oResult =
                    Service
                    .RecognizedWordService
                    .Save(recognizedWordSt.RecognizedWord);

                result.StatusOk = true;
                result.StackTrace = String.Empty;

                result.StatusOk = true;
                result.Message = oResult == null ? "Item could not have been found !" : "Item found !";
                result.ItemsFound = oResult == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.RecognizedWord = oResult;
                result.RecognizedWordList = null;
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.RecognizedWord = null;
                result.RecognizedWordList = null;
                Log.WriteToFile(
                    recognizedWordSt.RecognizedWord.UserId,
                    "api/RecognizedWord/Save",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion Save

        #region Enable

        [System.Web.Http.Route("api/RecognizedWord/Enable")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RecognizedWordStruct))]
        [BasicAuthentication]
        public IHttpActionResult Enable(RecognizedWordStruct recognizedWordSt) {
            //
            string username = Thread.CurrentPrincipal.Identity.Name;
            //
            var result = new RecognizedWordStruct();

            try {

                RecognizedWordEn oResult =
                    Service
                    .RecognizedWordService
                    .Enable(
                        recognizedWordSt.RecognizedWord.RecognizedWordId,
                        recognizedWordSt.RecognizedWord.Enabled
                    );

                result.StatusOk = true;
                result.StackTrace = String.Empty;

                result.StatusOk = true;
                result.Message = oResult == null ? "Item could not have been found !" : "Item found !";
                result.ItemsFound = oResult == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.RecognizedWord = oResult;
                result.RecognizedWordList = null;
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.RecognizedWord = null;
                result.RecognizedWordList = null;
                Log.WriteToFile(
                    recognizedWordSt.RecognizedWord.UserId,
                    "api/RecognizedWord/Enable",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion Enable

        #region GetWordClass

        [System.Web.Http.Route("api/RecognizedWord/GetWordClass")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RecognizedWordStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetWordClass(RecognizedWordStruct recognizedWordSt) {
            //
            string username = Thread.CurrentPrincipal.Identity.Name;
            //
            var result = new RecognizedWordStruct();

            try {
                List<RecognizedWordEn> recognizedWordList =
                    Service

                    .RecognizedWordService
                    .GetWordClass(
                        recognizedWordSt.RecognizedWord.UserId,
                        recognizedWordSt.RecognizedWord.WordClassId
                    );

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                result.RecognizedWord = null;
                if (recognizedWordList != null) {
                    result.Message = $"{recognizedWordList.Count} Items found !";
                    result.ItemsFound = recognizedWordList.Count;
                    result.RecognizedWordList = recognizedWordList;
                } else {
                    result.Message = $"Items NO found !";
                    result.ItemsFound = 0;
                    result.RecognizedWordList = null;
                }
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.RecognizedWord = null;
                result.RecognizedWordList = null;
                Log.WriteToFile(
                    recognizedWordSt.RecognizedWord.UserId,
                    "api/RecognizedWord/GetWordClass",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetWordClass

        #region GetAllxUser

        [System.Web.Http.Route("api/RecognizedWord/GetAllxUser")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RecognizedWordStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetAllxUser(RecognizedWordStruct rwSt) {
            //
            string username = Thread.CurrentPrincipal.Identity.Name;
            //
            UserEn user = new UserEn();
            var result = new RecognizedWordStruct();
            try {
                //  Validating user
                user = Service
                   .UserService
                   .UserGetNameToken(rwSt.Username, rwSt.Token);
                if (user == null) {
                    throw new Exception("Invalid User");
                }

                List<RecognizedWordEn> recognizedWordList =
                    Service
                    .RecognizedWordService
                    .GetAllxUser(user.UserId);

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                result.RecognizedWord = null;
                if (recognizedWordList != null) {
                    result.Message = $"{recognizedWordList.Count} Items found !";
                    result.ItemsFound = recognizedWordList.Count;
                    result.RecognizedWordList = recognizedWordList;
                } else {
                    result.Message = $"Items NO found !";
                    result.ItemsFound = 0;
                    result.RecognizedWordList = null;
                }
                Log.WriteToFile(
                    rwSt.Username,
                    "api/RecognizedWord/GetAllxUser",
                    result.Message
                );
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.RecognizedWord = null;
                result.RecognizedWordList = null;
                Log.WriteToFile(
                    $"{rwSt.Username}",
                    "api/RecognizedWord/GetAllxUser",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetAllxUser
    }
}