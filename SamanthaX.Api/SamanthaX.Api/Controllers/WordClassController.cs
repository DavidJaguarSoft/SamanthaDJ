using SamanthaX.Api.Utils;
using SamanthaX.API.Utils;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using SamanthaX.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace SamanthaX.API.Controllers {

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WordClassController : ApiController {

        #region GetId

        [System.Web.Http.Route("api/WordClass/GetId")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetId(WordClassStruct wordClassSt) {

            var result = new WordClassStruct();

            try {

                WordClassEn oWordClass =
                    Service
                    .WordClassService.GetId(wordClassSt.WordClass.WordClassId);

                result.StatusOk = oWordClass == null ? false:true;
                result.StackTrace = String.Empty;

                result.StatusOk = true;
                result.Message = oWordClass == null ? "Item could not have been found !" : "Item found !";
                result.ItemsFound = oWordClass == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.WordClass = oWordClass;
                result.WordClassList = null;
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.WordClass = null;
                result.WordClassList = null;
                Log.WriteToFile(
                    wordClassSt.WordClass.UserId,
                    "api/WordClass/GetId",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetId

        #region GetAll

        [System.Web.Http.Route("api/WordClass/GetAll")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetAll(WordClassStruct wordClassSt) {

            var result = new WordClassStruct();

            try {
                List<WordClassEn> wClassList =
                    Service
                    .WordClassService.GetAll(wordClassSt.WordClass.UserId);

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                result.WordClass = null;
                if(wClassList != null) {
                    result.Message = $"{wClassList.Count} Items found !";
                    result.ItemsFound = wClassList.Count;
                    //
                    if(wordClassSt.AllowRecognizedWord) {
                        foreach(WordClassEn iwc in wClassList) {
                            List<RecognizedWordEn> rWords = RecognizedWordService
                                .GetWordClass(
                                    wordClassSt.WordClass.UserId,
                                    iwc.WordClassId
                                    );
                            iwc.RecognizedWords = rWords;
                        }
                    }
                    //
                    result.WordClassList = wClassList;
                } else {
                    result.Message = $"Items NO found !";
                    result.ItemsFound = 0;
                    result.WordClassList = null;
                }
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound= 0;
                result.StackTrace = ex.StackTrace;
                result.WordClass = null;
                result.WordClassList = null;
                Log.WriteToFile(
                    wordClassSt.WordClass.UserId,
                    "api/WordClass/GetAll",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetAll

        #region Save

        [System.Web.Http.Route("api/WordClass/Save")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(WordClassStruct))]
        [BasicAuthentication]
        public IHttpActionResult Save(WordClassStruct wordClassSt) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new WordClassStruct();

            try {
                WordClassEn wordClass = Service.WordClassService.Save(wordClassSt.WordClass);

                result.StatusOk = true;
                result.Message = wordClass == null ? "Item could not have been saved !" : "Item saved !";
                result.ItemsFound = wordClass == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.WordClass = wordClass;
                result.WordClassList = null;
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.WordClass = null;
                result.WordClassList = null;
                Log.WriteToFile(
                    wordClassSt.WordClass.UserId,
                    "api/WordClass/Save",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion Save

        #region Enable

        [System.Web.Http.Route("api/WordClass/Enable")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Enable(WordClassStruct wordClassSt) {

            var result = new WordClassStruct();

            try {

                WordClassEn oWordClass =
                    Service
                    .WordClassService.Enable(
                        wordClassSt.WordClass.WordClassId,
                        wordClassSt.WordClass.Enabled
                    );

                result.StatusOk = true;
                result.StackTrace = String.Empty;

                result.StatusOk = true;
                result.Message = oWordClass == null ? "Item could not have been Deleted !" : "Item Deleted !";
                result.ItemsFound = oWordClass == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.WordClass = oWordClass;
                result.WordClassList = null;
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.WordClass = null;
                result.WordClassList = null;
                Log.WriteToFile(
                    wordClassSt.WordClass.UserId,
                    "api/WordClass/Enable",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion Enable
    }
}