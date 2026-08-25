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
    public class GrammarController : ApiController {

        #region GetId

        [System.Web.Http.Route("api/Grammar/GetId")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(GrammarStruct))]
        public IHttpActionResult GetId(GrammarStruct grammarSt) {

            var result = new GrammarStruct();

            try {
                LanguageEn lang =
                    Service
                    .LanguageService
                    .GetId(grammarSt.Grammar.LanguageId);

                GrammarEn oGrammar =
                    Service
                    .GrammarService
                    .GetId(grammarSt.Grammar.GrammarId);

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                if (oGrammar != null) {

                    List<GrammarBuilderEn> gbList =
                        Service
                        .GrammarBuilderService
                        .GetGrammar(oGrammar.GrammarId);

                    oGrammar.GrammarBuilderList = gbList;
                    if (oGrammar.GrammarBuilderList != null &&
                        oGrammar.GrammarBuilderList.Count > 0) {
                        foreach (GrammarBuilderEn igb in oGrammar.GrammarBuilderList) {
                            List<RecognizedWordEn> wordClassList =
                            Service
                            .RecognizedWordService
                            .GetWordClass(
                                grammarSt.Grammar.UserId,
                                igb.WordClassId
                            );
                            igb.RecognizedWordsList = wordClassList;
                        }
                    }
                    result.Message = oGrammar == null ? "Item could not have been found !" : "Item found !";
                    result.ItemsFound = oGrammar == null ? 0 : 1;
                    result.Grammar = oGrammar;
                    result.GrammarList = null;
                } else {
                    result.Message = $"Item NO found !";
                    result.ItemsFound = 0;
                    result.Grammar = null;
                    result.GrammarList = new List<GrammarEn>();
                }
            } catch (Exception ex) {
                result.StatusOk = false;
                result.ItemsFound = 0;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
                result.Grammar = null;
                result.GrammarList = null;
                Log.WriteToFile(
                    grammarSt.Grammar.UserId,
                    "api/Grammar/GetId",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetId

        #region GetAll

        [System.Web.Http.Route("api/Grammar/GetAll")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(GrammarStruct))]
        public IHttpActionResult GetAll(GrammarStruct grammarSt) {

            var result = new GrammarStruct();

            try {
                LanguageEn lang =
                    Service
                    .LanguageService
                    .GetId(grammarSt.Grammar.LanguageId);

                List<GrammarEn> grammarList =
                    Service
                    .GrammarService
                    .GetAll(
                        grammarSt.Grammar.UserId
                    );

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                if (grammarList != null) {
                    foreach(GrammarEn item in grammarList) {
                        List<GrammarBuilderEn> gbList =
                            Service
                            
                            .GrammarBuilderService
                            .GetGrammar(item.GrammarId);
                        item.GrammarBuilderList = gbList;
                        if(gbList != null && item.GrammarBuilderList.Count > 0) {
                            foreach(GrammarBuilderEn igb in item.GrammarBuilderList) {
                                List<RecognizedWordEn> wordClassList =
                                Service
                                .RecognizedWordService
                                .GetWordClass(
                                    grammarSt.Grammar.UserId,
                                    igb.WordClassId
                                );
                                igb.RecognizedWordsList = wordClassList;
                            }
                        }
                    }
                    result.Message = $"{grammarList.Count} Items found !";
                    result.ItemsFound = grammarList.Count;
                    result.GrammarList = grammarList;
                } else {
                    result.Message = $"Items NO found !";
                    result.ItemsFound = 0;
                    result.GrammarList = new List<GrammarEn>();
                }
                result.Grammar = null;
                //
            } catch (Exception ex) {
                result.StatusOk = false;
                result.ItemsFound = 0;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
                result.Grammar = null;
                result.GrammarList = null;
                Log.WriteToFile(
                    grammarSt.Grammar.UserId,
                    "api/Grammar/GetAll",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetAll

        #region Save

        [System.Web.Http.Route("api/Grammar/Save")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(GrammarStruct))]
        public IHttpActionResult Save(GrammarStruct grammarSt) {

            var result = new GrammarStruct();

            try {
                LanguageEn lang =
                    Service
                    .LanguageService
                    .GetId(grammarSt.Grammar.LanguageId);

                GrammarEn oGrammar =
                    Service
                    .GrammarService
                    .Save(grammarSt.Grammar);

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                result.Grammar = oGrammar;
                result.GrammarList = null;
                if (oGrammar != null) {

                    List<GrammarBuilderEn> gbList =
                        Service
                        .GrammarBuilderService
                        .GetGrammar(oGrammar.GrammarId);
                    result.GrammarList = null;

                    oGrammar.GrammarBuilderList = gbList;
                    if (gbList != null && oGrammar.GrammarBuilderList.Count > 0) {
                        foreach (GrammarBuilderEn igb in oGrammar.GrammarBuilderList) {
                            List<RecognizedWordEn> wordClassList =
                            Service
                            .RecognizedWordService
                            .GetWordClass(
                                grammarSt.Grammar.UserId,
                                igb.WordClassId
                            );
                            igb.RecognizedWordsList = wordClassList;
                        }
                    }
                    result.Message = "Item found !";
                    result.ItemsFound = 1;
                } else {
                    result.Message = $"Item NO found !";
                    result.ItemsFound = 0;
                }
            } catch (Exception ex) {
                result.StatusOk = false;
                result.ItemsFound = 0;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
                result.Grammar = null;
                result.GrammarList = null;
                Log.WriteToFile(
                    grammarSt.Grammar.UserId,
                    "api/Grammar/Save",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion Enable

        #region Enable

        [System.Web.Http.Route("api/Grammar/Enable")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(GrammarStruct))]
        public IHttpActionResult Enable(GrammarStruct grammarSt) {

            var result = new GrammarStruct();

            try {
                LanguageEn lang =
                    Service
                    .LanguageService
                    .GetId(grammarSt.Grammar.LanguageId);

                GrammarEn oGrammar =
                    Service
                    .GrammarService
                    .Enable(
                        grammarSt.Grammar.GrammarId,
                        grammarSt.Grammar.Enabled
                    );

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                if (oGrammar != null) {

                    List<GrammarBuilderEn> gbList =
                        Service
                        .GrammarBuilderService
                        .GetGrammar(oGrammar.GrammarId);
                    result.GrammarList = null;

                    oGrammar.GrammarBuilderList = gbList;
                    if (oGrammar.GrammarBuilderList != null && oGrammar.GrammarBuilderList.Count > 0) {
                        foreach (GrammarBuilderEn igb in oGrammar.GrammarBuilderList) {
                            List<RecognizedWordEn> wordClassList =
                            Service
                            .RecognizedWordService
                            .GetWordClass(
                                grammarSt.Grammar.UserId,
                                igb.WordClassId
                            );
                            igb.RecognizedWordsList = wordClassList;
                        }
                    }
                    result.Message = oGrammar == null ? "Item could not have been found !" : "Item found !";
                    result.ItemsFound = oGrammar == null ? 0 : 1;
                    result.GrammarList = null;
                } else {
                    result.Message = $"Item NO found !";
                    result.ItemsFound = 0;
                    result.GrammarList = new List<GrammarEn>();
                }
                result.Grammar = null;
                //
            } catch (Exception ex) {
                result.StatusOk = false;
                result.ItemsFound = 0;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
                result.Grammar = null;
                result.GrammarList = null;
                Log.WriteToFile(
                    grammarSt.Grammar.UserId,
                    "api/Grammar/Enable",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion Enable

        #region GetAllxUser

        [System.Web.Http.Route("api/Grammar/GetAllxUser")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(GrammarStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetAllxUser(GrammarStruct grammarSt) {
            //
            string username = Thread.CurrentPrincipal.Identity.Name;
            //
            UserEn user = new UserEn();
            var result = new GrammarStruct();
            try {
                //  Validating user
                user = Service
                   .UserService
                   .UserGetNameToken(grammarSt.Username, grammarSt.Token);
                if (user == null) {
                    throw new Exception("Invalid User");
                }
                //
                List<GrammarEn> grammarList =
                    Service
                    .GrammarService
                    .GetAllxUser(user.UserId);

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                if (grammarList != null) {
                    foreach (GrammarEn item in grammarList) {
                        List<GrammarBuilderEn> gbList =
                            Service
                            .GrammarBuilderService
                            .GetGrammar(item.GrammarId);
                        item.GrammarBuilderList = gbList;
                        if (gbList != null && item.GrammarBuilderList.Count > 0) {
                            foreach (GrammarBuilderEn igb in item.GrammarBuilderList) {
                                List<RecognizedWordEn> wordClassList =
                                Service
                                .RecognizedWordService
                                .GetWordClassxUser(
                                    user.UserId,
                                    igb.WordClassId
                                );
                                igb.RecognizedWordsList = wordClassList;
                            }
                        }
                    }
                    result.Message = $"{grammarList.Count} Items found !";
                    result.ItemsFound = grammarList.Count;
                    result.GrammarList = grammarList;
                } else {
                    result.Message = $"Items NO found !";
                    result.ItemsFound = 0;
                    result.GrammarList = new List<GrammarEn>();
                }
                result.Grammar = null;

                Log.WriteToFile(
                    grammarSt.Username,
                    "api/Grammar/GetAllxUser",
                    result.Message
                );
            } catch (Exception ex) {
                result.StatusOk = false;
                result.ItemsFound = 0;
                result.Message = ex.Message;
                result.StackTrace = ex.StackTrace;
                result.Grammar = null;
                result.GrammarList = null;
                Log.WriteToFile(
                    $"{grammarSt.Username}",
                    "api/Grammar/GetAllxUser",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetAllxUser
    }
}