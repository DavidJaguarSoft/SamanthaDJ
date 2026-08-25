using SamanthaX.Library.HttpService;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using SamanthaX.WebMVC.Models.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamanthaX.WebMVC.Controllers {

    public class RecognizedWordController : Controller {

        #region IndexRW

        // GET: RecognizedWord
        public ActionResult IndexRW() {
            return RedirectToAction("CatalogRW");
        }

        #endregion IndexRW

        #region CatalogRW

        public ActionResult CatalogRW() {
            if(Session["SystemUserId"] == null) {
                return RedirectToAction("Index", "Home", new { ErrorMessage = "No user data found" });
            }
            List<RecognizedWordEn> recognizedWordList = new List<RecognizedWordEn>();
            //
            RecognizedWordHS oHS = new RecognizedWordHS();
            var response =
                oHS
                .GetAll<RecognizedWordStruct>(
                    Convert.ToInt32(Session["SystemUserId"])
                );
            if(response.IsSuccess == true) {
                recognizedWordList = response.RecognizedWordSt.RecognizedWordList;
                return View(recognizedWordList);
            } else {
                return RedirectToAction(
                    "Error",
                    "System",
                    new { ErrorMessage = response.ErrorMessage }
                );
            }
        }

        #endregion CatalogRW

        #region EditRW

        public ActionResult EditRW(int recognizedWordId) {
            if (Session["SystemUserId"] == null) {
                return RedirectToAction("Index", "Home", new { ErrorMessage = "No user data found" });
            }

            WordClassHS oWCHS = new WordClassHS();
            var responseWC =
                oWCHS
                .GetAll<RecognizedWordStruct>(
                    Convert.ToInt32(Session["SystemUserId"])
                );
            if((responseWC.IsSuccess)) {
                ViewBag.WordClassList = responseWC.WordClassSt.WordClassList;
            } else {
                return RedirectToAction(
                    "Error",
                    "System",
                    new { ErrorMessage = responseWC.ErrorMessage }
                );
            }

            if (recognizedWordId == 0) return View(new RecognizedWordModel());

            RecognizedWordHS oRWSH = new RecognizedWordHS();
            var response =
                oRWSH.GetId<RecognizedWordStruct>(recognizedWordId);
            if(response.IsSuccess == true) {
                RecognizedWordModel oWCModel = new RecognizedWordModel();
                oWCModel.RecognizedWordId = recognizedWordId;
                oWCModel.Code = response.RecognizedWordSt.RecognizedWord.Code;
                oWCModel.WordClassId = response.RecognizedWordSt.RecognizedWord.WordClassId;
                oWCModel.WordClass = response.RecognizedWordSt.RecognizedWord.WordClass;
                oWCModel.RelatedWords = response.RecognizedWordSt.RecognizedWord.RelatedWords;
                oWCModel.DateRegistration = DateTime.MinValue;
                oWCModel.Enabled = response.RecognizedWordSt.RecognizedWord.Enabled;
                return View(oWCModel);
            } else {
                return RedirectToAction(
                    "Error",
                    "System",
                    new { ErrorMessage = response.ErrorMessage }
                );
            }
        }

        [HttpPost]
        public ActionResult EditRW(RecognizedWordModel oRWModel) {
            WordClassHS wcHS = new WordClassHS();
            var responseWC =
                    wcHS
                    .GetAll<RecognizedWordStruct>(
                        Convert.ToInt32(Session["SystemUserId"])
                    );
            if((responseWC.IsSuccess)) {
                ViewBag.WordClassList = responseWC.WordClassSt.WordClassList;
            }
            if(ModelState.IsValid) {
                try {
                    string myToken = (string)Session["SystemUserEMail"];
                    if (myToken.Equals("MyEMail@MyDomain.com")) {
                        return RedirectToAction(
                            "Error",
                            "System",
                            new {
                                ErrorMessage = $"You cannot edit or create records with the account *MyEMail@MyDomain.com*"
                            }
                        );
                    }
                    RecognizedWordHS oHS = new RecognizedWordHS();
                    var responseRW =
                        oHS.Save<WordClassStruct>(
                            oRWModel.RecognizedWordId,
                            Convert.ToInt32(Session["SystemUserId"]),
                            2,
                            oRWModel.Code,
                            oRWModel.WordClassId,
                            oRWModel.RelatedWords
                        );
                    if (responseRW.IsSuccess == true) {
                        return RedirectToAction("CatalogRW", "RecognizedWord");
                    } else {
                        return RedirectToAction("Error", "System", new { ErrorMessage = responseRW.ErrorMessage });
                    }
                } catch(Exception ex) {
                    return RedirectToAction(
                        "Error",
                        "System",
                        new { ErrorMessage = $"{ex.Message}.\nStackTrace: {ex.StackTrace}" }
                    );
                }
            } else {
                ViewData["ErrorMessageSX"] = "You must provide all fields";
                return View(oRWModel);
            }
        }

        #endregion EditRW

        #region EnableRW

        public ActionResult EnableRW(int recognizedWordId) {
            RecognizedWordHS oHS = new RecognizedWordHS();
            var response =
                oHS.GetId<RecognizedWordStruct>(recognizedWordId);
            if(response.IsSuccess == true) {
                Session["RecognizedWordDeleteId"] = recognizedWordId.ToString();
                RecognizedWordModel oRWModel = new RecognizedWordModel();
                oRWModel.RecognizedWordId = response.RecognizedWordSt.RecognizedWord.WordClassId;
                oRWModel.Code = response.RecognizedWordSt.RecognizedWord.Code;
                oRWModel.WordClassId = response.RecognizedWordSt.RecognizedWord.WordClassId;
                oRWModel.WordClass = response.RecognizedWordSt.RecognizedWord.WordClass;
                oRWModel.RelatedWords = response.RecognizedWordSt.RecognizedWord.RelatedWords;
                oRWModel.DateRegistration = DateTime.MinValue;
                oRWModel.Enabled = response.RecognizedWordSt.RecognizedWord.Enabled;
                return View(oRWModel);
            } else {
                return RedirectToAction("Error", "System", new { ErrorMessage = response.ErrorMessage });
            }
        }

        [HttpPost]
        public ActionResult EnableRW(RecognizedWordModel oRWM) {
            int idRW = Convert.ToInt32(Session["RecognizedWordDeleteId"]);

            string myToken = (string)Session["SystemUserEMail"];
            if (myToken.Equals("MyEMail@MyDomain.com")) {
                return RedirectToAction(
                    "Error",
                    "System",
                    new {
                        ErrorMessage = $"You cannot edit or create records with the account *MyEMail@MyDomain.com*"
                    }
                );
            }

            RecognizedWordHS oHS = new RecognizedWordHS();
            var response =
                oHS.GetId<RecognizedWordStruct>(idRW);
            if(response.IsSuccess == true) {
                var responseEnable =
                oHS.Delete<RecognizedWordStruct>(idRW, !response.RecognizedWordSt.RecognizedWord.Enabled);
                if(responseEnable.IsSuccess == true) {
                    return RedirectToAction("CatalogRW", "RecognizedWord");
                } else {
                    return RedirectToAction("Error", "System", new { ErrorMessage = responseEnable.ErrorMessage });
                }
            } else {
                return RedirectToAction("Error", "System", new { ErrorMessage = response.ErrorMessage });
            }
        }

        #endregion EnableRW

    }
}