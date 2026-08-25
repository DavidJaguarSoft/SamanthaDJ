using SamanthaX.Api.Utils;
using SamanthaX.API.Utils;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace SamanthaX.API.Controllers {

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GrammarBuilderController : ApiController {

        #region GetId

        [System.Web.Http.Route("api/GrammarBuilder/GetId")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetId(GrammarBuilderStruct oGBSt) {

            var result = new GrammarBuilderStruct();

            try {
                GrammarBuilderEn oGrammarBuilder =
                    Service
                    .GrammarBuilderService.GetId(oGBSt.GrammarBuilder.GrammarBuilderId);

                result.StatusOk = true;
                result.StackTrace = String.Empty;

                result.StatusOk = true;
                result.Message = oGrammarBuilder == null ? "Item could not have been found !" : "Item found !";
                result.ItemsFound = oGrammarBuilder == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.GrammarBuilder = oGrammarBuilder;
                result.GrammarBuilderList = null;
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.GrammarBuilder = null;
                result.GrammarBuilderList = null;
                Log.WriteToFile(
                    $"GrammarBuilderId_{oGBSt.GrammarBuilder.GrammarBuilderId.ToString("####")}",
                    "api/GrammarBuilder/GetId",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetId

        #region GetGrammar

        [System.Web.Http.Route("api/GrammarBuilder/GetGrammar")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetGrammar(GrammarBuilderStruct oGBSt) {

            var result = new GrammarBuilderStruct();

            try {
                List<GrammarBuilderEn> oGrammarBuilderList =
                    Service
                    .GrammarBuilderService.GetGrammar(oGBSt.GrammarBuilder.GrammarId);

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                result.GrammarBuilder = null;
                if(oGrammarBuilderList != null) {
                    if (oGBSt.LoadRecognizedWord) {
                        foreach (GrammarBuilderEn item in oGrammarBuilderList) {
                            List<RecognizedWordEn> rwList =
                                Service
                                .RecognizedWordService
                                .GetWordClass(oGBSt.UserId, item.WordClassId);
                            item.RecognizedWordsList = rwList;
                        }
                    }
                    result.Message = $"{oGrammarBuilderList.Count} Items found !";
                    result.ItemsFound = oGrammarBuilderList.Count;
                    result.GrammarBuilderList = oGrammarBuilderList;
                } else {
                    result.Message = $"Items NO found !";
                    result.ItemsFound = 0;
                    result.GrammarBuilderList = null;
                }
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound= 0;
                result.StackTrace = ex.StackTrace;
                result.GrammarBuilder = null;
                result.GrammarBuilderList = null;
                Log.WriteToFile(
                    $"GrammarBuilderId_{oGBSt.GrammarBuilder.GrammarBuilderId.ToString("####")}",
                    "api/GrammarBuilder/GetGrammar",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetGrammar

        #region Save

        [System.Web.Http.Route("api/GrammarBuilder/Save")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(GrammarBuilderStruct))]
        [BasicAuthentication]
        public IHttpActionResult Save(GrammarBuilderStruct oGBSt) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new GrammarBuilderStruct();

            try {
                GrammarBuilderEn oGrammarBuilder = 
                    Service
                    .GrammarBuilderService
                    .Save(oGBSt.GrammarBuilder);

                result.StatusOk = true;
                result.Message = oGrammarBuilder == null ? "Item could not have been saved !" : "Item saved !";
                result.ItemsFound = oGrammarBuilder == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.GrammarBuilder = oGrammarBuilder;
                result.GrammarBuilderList = null;
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.GrammarBuilder = null;
                result.GrammarBuilderList = null;
                Log.WriteToFile(
                    $"GrammarBuilderId_{oGBSt.GrammarBuilder.GrammarBuilderId.ToString("####")}",
                    "api/GrammarBuilder/Save",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }
            return Ok(result);
        }

        #endregion Save

        #region Enable

        [System.Web.Http.Route("api/GrammarBuilder/Enable")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Enable(GrammarBuilderStruct oGBSt) {

            var result = new GrammarBuilderStruct();

            try {
                GrammarBuilderEn oGrammarBuilder =
                    Service
                    .GrammarBuilderService.Enable(
                        oGBSt.GrammarBuilder.WordClassId,
                        oGBSt.GrammarBuilder.Enabled
                    );

                result.StatusOk = true;
                result.StackTrace = String.Empty;

                result.StatusOk = true;
                result.Message = oGrammarBuilder == null ? "Item could not have been Deleted !" : "Item Deleted !";
                result.ItemsFound = oGrammarBuilder == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.GrammarBuilder = oGrammarBuilder;
                result.GrammarBuilderList = null;
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.GrammarBuilder = null;
                result.GrammarBuilderList = null;
                Log.WriteToFile(
                    $"GrammarBuilderId_{oGBSt.GrammarBuilder.GrammarBuilderId.ToString("####")}",
                    "api/GrammarBuilder/Enable",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }
            return Ok(result);
        }

        #endregion Enable

        #region Delete

        [System.Web.Http.Route("api/GrammarBuilder/Delete")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Delete(GrammarBuilderStruct oGBSt) {

            var result = new GrammarBuilderStruct();

            try {
                bool responseOk =
                    Service
                    .GrammarBuilderService.Delete(
                        oGBSt.GrammarBuilder.GrammarBuilderId
                    );
                if (responseOk) {
                    result.StatusOk = true;
                    result.Message = "The record was deleted";
                } else {
                    result.StatusOk = false;
                    result.Message = "The record was not deleted";
                }
                result.StackTrace = String.Empty;
                result.ItemsFound = 0;
                result.StackTrace = String.Empty;
                result.GrammarBuilder = null;
                result.GrammarBuilderList = null;
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.GrammarBuilder = null;
                result.GrammarBuilderList = null;
                Log.WriteToFile(
                    $"GrammarBuilderId_{oGBSt.GrammarBuilder.GrammarBuilderId.ToString("####")}",
                    "api/GrammarBuilder/Delete",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }
            return Ok(result);
        }

        #endregion Delete
    }
}