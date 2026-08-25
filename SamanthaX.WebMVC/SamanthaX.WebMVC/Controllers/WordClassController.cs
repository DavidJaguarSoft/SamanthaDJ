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

    public class WordClassController : Controller {

        #region IndexWC

        // GET: WordClass
        public ActionResult IndexWX() {
            return RedirectToAction("CatalgoWC");
        }

        #endregion IndexWC

        #region CatalogWC

        public ActionResult CatalogWC() {
            if(Session["SystemUserId"] == null) {
                return RedirectToAction("Index", "Home", new { ErrorMessage = "No user data found"});
            }
            List<WordClassEn> wordClassList = new List<WordClassEn>();
            //
            WordClassHS oHS = new WordClassHS();
            var response =
                oHS.GetAll<WordClassStruct>(Convert.ToInt32(Session["SystemUserId"]));
            if(response.IsSuccess == true) {
                wordClassList = response.WordClassSt.WordClassList;
                return View(wordClassList);
            } else {
                return RedirectToAction("Error", "System", new { ErrorMessage = response.ErrorMessage});
            }
        }

        #endregion CatalogWC

        #region EditWC

        public ActionResult EditWC(int wordClassId) {
            if (Session["SystemUserId"] == null) {
                return RedirectToAction("Index", "Home", new { ErrorMessage = "No user data found" });
            }
            if (wordClassId == 0) return View(new WordClassModel());

            WordClassHS oHS = new WordClassHS();
            var response =
                oHS.GetId<WordClassStruct>(wordClassId);
            if(response.IsSuccess == true) {
                WordClassModel oWCModel = new WordClassModel();
                oWCModel.WordClassId = response.WordClassSt.WordClass.WordClassId;
                oWCModel.Code = response.WordClassSt.WordClass.Code;
                oWCModel.Name = response.WordClassSt.WordClass.Name;
                oWCModel.Description = response.WordClassSt.WordClass.Description;
                oWCModel.Example = response.WordClassSt.WordClass.Example;
                oWCModel.DateRegistration = DateTime.MinValue;
                oWCModel.Enabled = response.WordClassSt.WordClass.Enabled;
                return View(oWCModel);
            } else {
                return RedirectToAction("Error", "System", new { ErrorMessage = response.ErrorMessage});
            }
        }

        [HttpPost]
        public ActionResult EditWC(WordClassModel oWordClassModel) {
            if(ModelState.IsValid) {
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
                try {
                    WordClassHS oHS = new WordClassHS();
                    var response =
                        oHS.Save<WordClassStruct>(
                            oWordClassModel.WordClassId,
                            Convert.ToInt32(Session["SystemUserId"]),
                            oWordClassModel.Code,
                            oWordClassModel.Name,
                            oWordClassModel.Description == null ? "" : oWordClassModel.Description,
                            oWordClassModel.Example == null ? "" : oWordClassModel.Example
                        );
                    if(response.IsSuccess == true) {
                        return RedirectToAction("CatalogWC", "WordClass");
                    } else {
                        return RedirectToAction("Error", "System", new { ErrorMessage = response.ErrorMessage });
                    }

                } catch(Exception ex) {
                    return RedirectToAction("Error", "System", new { ErrorMessage = $"{ex.Message}.\nStackTrace: {ex.StackTrace}" });
                }
            } else {
                return View(oWordClassModel);
            }
        }

        #endregion Edit

        #region EnableWC

        public ActionResult EnableWC(int wordClassId) {
            WordClassHS oHS = new WordClassHS();
            var response =
                oHS.GetId<WordClassStruct>(wordClassId);
            if(response.IsSuccess == true) {
                Session["WordClassDeleteId"] = wordClassId.ToString();
                WordClassModel oWCModel = new WordClassModel();
                oWCModel.WordClassId = response.WordClassSt.WordClass.WordClassId;
                oWCModel.Code = response.WordClassSt.WordClass.Code;
                oWCModel.Name = response.WordClassSt.WordClass.Name;
                oWCModel.Description = response.WordClassSt.WordClass.Description;
                oWCModel.Example = response.WordClassSt.WordClass.Example;
                oWCModel.DateRegistration = DateTime.MinValue;
                oWCModel.Enabled = response.WordClassSt.WordClass.Enabled;
                return View(oWCModel);
            } else {
                return RedirectToAction("Error", "System", new { ErrorMessage = response.ErrorMessage });
            }
        }

        [HttpPost]
        public ActionResult EnableWC(WordClassModel oWCM) {
            int idWC = Convert.ToInt32(Session["WordClassDeleteId"]);

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

            WordClassHS oHS = new WordClassHS();
            var response =
                oHS.GetId<WordClassStruct>(idWC);
            if(response.IsSuccess == true) {
                var responseEnable =
                oHS.Delete<WordClassStruct>(idWC, !response.WordClassSt.WordClass.Enabled);
                if(responseEnable.IsSuccess == true) {
                    return RedirectToAction("CatalogWC", "WordClass");
                } else {
                    return RedirectToAction("Error", "System", new { ErrorMessage = responseEnable.ErrorMessage });
                }
            } else {
                return RedirectToAction("Error", "System", new { ErrorMessage = response.ErrorMessage });
            }
        }

        #endregion Enable
    }
}