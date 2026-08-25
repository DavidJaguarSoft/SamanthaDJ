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
    public class RecognizedInstructionController : ApiController {

        #region GetId

        [System.Web.Http.Route("api/RecognizedInstruction/GetId")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RecognizedInstructionStruct))]
        public IHttpActionResult GetId(RecognizedInstructionStruct recognizedInstructionSt) {

            var result = new RecognizedInstructionStruct();

            try {

                RecognizedInstructionEn oResult =
                    Service
                    .RecognizedInstructionService
                    .GetId(recognizedInstructionSt.RecognizeInstruction.RecognizedInstructionId);

                result.StatusOk = true;
                result.StackTrace = String.Empty;

                result.StatusOk = true;
                result.Message = oResult == null ? "Item could not have been found !" : "Item found !";
                result.ItemsFound = oResult == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.RecognizeInstruction = oResult;
                result.RecognizeInstructionList = null;
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.RecognizeInstruction = null;
                result.RecognizeInstructionList = null;
                Log.WriteToFile(
                    recognizedInstructionSt.RecognizeInstruction.UserId,
                    "api/RecognizedInstruction/GetId",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetId

        #region GetAll

        [System.Web.Http.Route("api/RecognizedInstruction/GetAll")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RecognizedInstructionStruct))]
        public IHttpActionResult GetAll(RecognizedInstructionStruct recognizedInstructionSt) {

            var result = new RecognizedInstructionStruct();

            try {

                List<RecognizedInstructionEn> recognizedWordList =
                    Service
                    .RecognizedInstructionService
                    .GetAll(recognizedInstructionSt.RecognizeInstruction.UserId);

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                result.RecognizeInstruction = null;
                if (recognizedWordList != null) {
                    result.Message = $"{recognizedWordList.Count} Items found !";
                    result.ItemsFound = recognizedWordList.Count;
                    result.RecognizeInstructionList = recognizedWordList;
                } else {
                    result.Message = $"Items NO found !";
                    result.ItemsFound = 0;
                    result.RecognizeInstructionList = null;
                }
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.RecognizeInstruction = null;
                result.RecognizeInstructionList = null;
                Log.WriteToFile(
                    recognizedInstructionSt.RecognizeInstruction.UserId,
                    "api/RecognizedInstruction/GetAll",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetAll

        #region Save

        [System.Web.Http.Route("api/RecognizedInstruction/Save")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RecognizedInstructionStruct))]
        public IHttpActionResult Save(RecognizedInstructionStruct recognizedInstructionSt) {

            var result = new RecognizedInstructionStruct();

            try {

                RecognizedInstructionEn oResult =
                    Service
                    .RecognizedInstructionService
                    .Save(recognizedInstructionSt.RecognizeInstruction);

                result.StatusOk = true;
                result.StackTrace = String.Empty;

                result.StatusOk = true;
                result.Message = oResult == null ? "Item could not have been found !" : "Item found !";
                result.ItemsFound = oResult == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.RecognizeInstruction = oResult;
                result.RecognizeInstructionList = null;
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.RecognizeInstruction = null;
                result.RecognizeInstructionList = null;
                Log.WriteToFile(
                    recognizedInstructionSt.RecognizeInstruction.UserId,
                    "api/RecognizedInstruction/Save",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }
            return Ok(result);
        }

        #endregion Save

        #region Enable

        [System.Web.Http.Route("api/RecognizedInstruction/Enable")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RecognizedInstructionStruct))]
        public IHttpActionResult Enable(RecognizedInstructionStruct recognizedInstructionSt) {

            var result = new RecognizedInstructionStruct();

            try {
                RecognizedInstructionEn oResult =
                    Service
                    .RecognizedInstructionService
                    .Enable(
                        recognizedInstructionSt.RecognizeInstruction.RecognizedInstructionId,
                        recognizedInstructionSt.RecognizeInstruction.Enabled
                    );
                result.StatusOk = true;
                result.StackTrace = String.Empty;

                result.StatusOk = true;
                result.Message = oResult == null ? "Item could not have been found !" : "Item found !";
                result.ItemsFound = oResult == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.RecognizeInstruction = oResult;
                result.RecognizeInstructionList = null;
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.RecognizeInstruction = null;
                result.RecognizeInstructionList = null;
                Log.WriteToFile(
                    recognizedInstructionSt.RecognizeInstruction.UserId,
                    "api/RecognizedInstruction/Enable",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion Enable

        #region GetAllxUser

        [System.Web.Http.Route("api/RecognizedInstruction/GetAllxUser")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RecognizedInstructionStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetAllxUser(RecognizedInstructionStruct riSt) {
            //
            string username = Thread.CurrentPrincipal.Identity.Name;
            //
            UserEn user = new UserEn();
            var result = new RecognizedInstructionStruct();

            try {
                //  Validating user
                user = Service
                   .UserService
                   .UserGetNameToken(riSt.Username, riSt.Token);
                if (user == null) {
                    throw new Exception("Invalid User");
                }
                //
                List<RecognizedInstructionEn> recognizedWordList =
                    Service
                    .RecognizedInstructionService
                    .GetAll(user.UserId);

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                result.RecognizeInstruction = null;
                if (recognizedWordList != null) {
                    result.Message = $"{recognizedWordList.Count} Items found !";
                    result.ItemsFound = recognizedWordList.Count;
                    result.RecognizeInstructionList = recognizedWordList;
                } else {
                    result.Message = $"Items NO found !";
                    result.ItemsFound = 0;
                    result.RecognizeInstructionList = null;
                }
                Log.WriteToFile(
                    riSt.Username,
                    "api/RecognizedInstruction/GetAllxUser",
                    result.Message
                );
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.RecognizeInstruction = null;
                result.RecognizeInstructionList = null;
                Log.WriteToFile(
                    riSt.Username,
                    "api/RecognizedInstruction/GetAllxUser",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetAllxUser
    }
}