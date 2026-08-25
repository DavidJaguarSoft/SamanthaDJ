using Newtonsoft.Json;
using SamanthaX.Library.Function;
using SamanthaX.Library.HttpService;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using SamanthaX.WebMVC.Models.System;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SamanthaX.WebMVC.Controllers {

    public class RecognizedInstructionController : Controller {

        #region IndexRI

        // GET:
        public ActionResult IndexRI() {
            return RedirectToAction("Catalog");
        }

        #endregion IndexRI

        #region DashboardRI

        public ActionResult DashboardRI() {
            if (Session["SystemUserId"] == null) {
                return RedirectToAction("Index", "Home");
            }
            ViewData["ErrorMessageSX"] = null;
            List<RecognizedInstructionEn> riList = new List<RecognizedInstructionEn>();
            //
            RecognizedInstructionHS oHS = new RecognizedInstructionHS();
            var response =
                oHS.GetAll<RecognizedInstructionStruct>(Convert.ToInt32(Session["SystemUserId"]));
            if (response.IsSuccess) {
                riList = response.RecognizedInstructionSt.RecognizeInstructionList;
                if(riList == null) {
                    riList = new List<RecognizedInstructionEn>();
                }
                return View(riList);
            } else {
                ViewData["ErrorMessageSX"] = $"The following was detected: {response.ErrorMessage}";
                return View(new List<RecognizedInstructionEn>());
            }
        }

        #endregion DashboardRI

        #region EditRI

        public ActionResult EditRI(
            string RecognizedInstructionId,
            string GrammarSelectedId,
            string Origin
        ) {
            if (Session["SystemUserId"] == null) {
                return RedirectToAction("Index", "Home");
            }
            int iSystemUserId = Convert.ToInt32(Session["SystemUserId"]);
            int iGrammarSelectedId = Convert.ToInt32(GrammarSelectedId);

            GrammarHS oGHS = new GrammarHS();
            var responseG =
                oGHS.GetAll<GrammarStruct>(iSystemUserId);
            if(responseG.IsSuccess == true) {
                ViewBag.RecognizedInstructionList = responseG.GrammarSt.GrammarList;
            } else {
                ViewData["ErrorMessageSX"] = $"The following was detected: {responseG.ErrorMessage}";
                return RedirectToAction("Error", "System");
            }

            RecognizedInstructionModel oRIModel = new RecognizedInstructionModel();
            oRIModel.UserId = iSystemUserId;

            //  From Create button
            if (Origin.Equals("CREATE")) {
                oRIModel.RecognizedInstructionId = 0;
                Session["RI_Edit_GrammarSelectedId"] = "0";
                oRIModel.PanelGrammar = true;
                //return View(oRIModel);
            }

            if(Origin.Equals("EDITING")) {
                if(string.IsNullOrEmpty(RecognizedInstructionId)) {
                    return RedirectToAction("Error", "System", new { ErrorMessage = "Invalid Registration" });
                }
                oRIModel.RecognizedInstructionId = Convert.ToInt32(RecognizedInstructionId);
                RecognizedInstructionHS riHS = new RecognizedInstructionHS();
                var responseRI = riHS.GetId<RecognizedInstructionStruct>(oRIModel.RecognizedInstructionId);
                if(responseRI.IsSuccess) {
                    oRIModel = oRIModel.CloneModelFromEntity(responseRI.RecognizedInstructionSt.RecognizeInstruction);
                    oRIModel.ArmedInstruction = oRIModel.Code;
                    oRIModel.PanelGrammar = false;
                    oRIModel.PanelInstruction = false;
                    oRIModel.PanelData = true;
                } else {
                    return RedirectToAction("Error", "System", new { ErrorMessage = responseRI.ErrorMessage });
                }
            }

            if (Origin.Equals("FORWARD_PANEL_INSTRUCTION")) {
                //Session["RI_Edit_ GrammarSelectedId"] = pGrammarSelectedId;
                var responseGGetId = oGHS.GetId<RecognizedInstructionStruct>(iGrammarSelectedId);
                if (responseGGetId.IsSuccess == true) {
                    oRIModel.GrammarId = iGrammarSelectedId;
                    oRIModel.Grammar = responseGGetId.GrammarSt.Grammar.Name;
                    oRIModel.PanelGrammar = false;
                    oRIModel.PanelInstruction = true;
                    oRIModel.PanelData = false;
                    //
                    GrammarBuilderHS gbHS = new GrammarBuilderHS();
                    var responseGB = gbHS.GetGrammar<GrammarBuilderStruct>(
                        iSystemUserId,
                        iGrammarSelectedId
                    );
                    if (responseGB.IsSuccess) {
                        oRIModel.GrammarBuilderList = responseGB.GrammarBuilderSt.GrammarBuilderList;
                        //return View(oRIModel);
                    } else {
                        return RedirectToAction("Error", "System", new { ErrorMessage = responseGB.ErrorMessage });
                    }
                } else {
                    return RedirectToAction("Error", "System", new { ErrorMessae = responseGGetId.ErrorMessage});
                }
            }

            if (Origin.Equals("BACK_PANEL_GRAMMAR")) {
                if (Session["RI_Edit_GrammarSelectedId"] == null ||
                    Session["RI_Edit_Model"] == null) {
                    return RedirectToAction("Error", "System");
                }
                oRIModel = (RecognizedInstructionModel)Session["RI_Edit_Model"];
                oRIModel.PanelGrammar = true;
                oRIModel.PanelInstruction = false;
                oRIModel.PanelData = false;
                //return View(oRIModel);
            }
            
            if (Origin.Equals("FORWARD_PANEL_DATA")) {
                if(Session["RI_Edit_ArmedInstruction"] == null) {
                    return RedirectToAction(
                        "Error",
                        "System",
                        new {ErrorMessage = "Session Data not found !" }
                    );
                }
                oRIModel = (RecognizedInstructionModel)Session["RI_Edit_Model"];
                oRIModel.ArmedInstruction = Session["RI_Edit_ArmedInstruction"].ToString();
                oRIModel.Code = oRIModel.ArmedInstruction;
                oRIModel.PanelGrammar = false;
                oRIModel.PanelInstruction = false;
                oRIModel.PanelData = true;
                //return View(oRIModel);
            }

            if(Origin.Equals("BACK_PANEL_INSTRUCTION")) {
                if(Session["RI_Edit_Model"] == null) {
                    return RedirectToAction("Error", "System", new { MessageError = "The Session variable was not found"});
                }
                oRIModel = (RecognizedInstructionModel)Session["RI_Edit_Model"];
                oRIModel.PanelGrammar = false;
                oRIModel.PanelInstruction = true;
                oRIModel.PanelData = false;
            }

            Session["RI_Edit_Model"] = oRIModel;
            return View(oRIModel);
        }

        [HttpPost]
        public ActionResult EditRI(RecognizedInstructionModel pRIModel) {
            if (Session["RI_Edit_Model"] == null) {
                return RedirectToAction("Error", "System", new {ErrorMessage = "The Session variable was not found" });
            }

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

            RecognizedInstructionModel oRIModel = (RecognizedInstructionModel)Session["RI_Edit_Model"];
            ViewData["ErrorMessageSX"] = null;
            //
            oRIModel.Code = pRIModel.Code;
            oRIModel.Instruction = HandleText.StringHandle(pRIModel.Instruction);
            oRIModel.Description = HandleText.StringHandle(pRIModel.Description);
            oRIModel.VoiceProcessing = HandleText.StringHandle(pRIModel.VoiceProcessing);
            oRIModel.VoiceSolution = HandleText.StringHandle(pRIModel.VoiceSolution);
            oRIModel.VoiceCancel = HandleText.StringHandle(pRIModel.VoiceCancel);
            oRIModel.VoiceFail = HandleText.StringHandle(pRIModel.VoiceFail);
            oRIModel.Enabled = true;
            oRIModel.PanelGrammar = false;
            oRIModel.PanelInstruction = false;
            oRIModel.PanelData = true;
            //
            if (ModelState.IsValid) {
                RecognizedInstructionHS oHS = new RecognizedInstructionHS();
                RecognizedInstructionEn oToSave = CloneModelToEntity(oRIModel);
                var response =
                    oHS.Save<RecognizedInstructionStruct>(oToSave);

                if (response.IsSuccess == true) {
                    return RedirectToAction("DashboardRI");
                } else {
                    ViewData["ErrorMessageSX"] = $"The following was detected: {response.ErrorMessage}";
                    return View(oRIModel);
                }

            } else {
                return View(oRIModel);
            }
        }

        #endregion EditRI

        #region EnableRI

        public ActionResult EnableRI(int RecognizedInstructionId) {
            RecognizedInstructionHS oHS = new RecognizedInstructionHS();
            var response =
                oHS.GetId<RecognizedInstructionStruct>(RecognizedInstructionId);
            if (response.IsSuccess == true) {
                Session["RecognizedInstructionDeleteId"] = RecognizedInstructionId.ToString();
                RecognizedInstructionEn oRI = new RecognizedInstructionEn();
                oRI.RecognizedInstructionId = response.RecognizedInstructionSt.RecognizeInstruction.RecognizedInstructionId;
                oRI.UserId = response.RecognizedInstructionSt.RecognizeInstruction.RecognizedInstructionId;
                oRI.LanguageId = response.RecognizedInstructionSt.RecognizeInstruction.UserId;
                oRI.GrammarId = response.RecognizedInstructionSt.RecognizeInstruction.GrammarId;
                oRI.Grammar = response.RecognizedInstructionSt.RecognizeInstruction.Grammar;
                oRI.Code = response.RecognizedInstructionSt.RecognizeInstruction.Code;
                oRI.Instruction = response.RecognizedInstructionSt.RecognizeInstruction.Instruction;
                oRI.Description = response.RecognizedInstructionSt.RecognizeInstruction.Description;
                oRI.VoiceProcessing = response.RecognizedInstructionSt.RecognizeInstruction.VoiceProcessing;
                oRI.VoiceSolution = response.RecognizedInstructionSt.RecognizeInstruction.VoiceSolution;
                oRI.VoiceCancel = response.RecognizedInstructionSt.RecognizeInstruction.VoiceCancel;
                oRI.VoiceFail = response.RecognizedInstructionSt.RecognizeInstruction.VoiceFail;
                oRI.DateRegistration = response.RecognizedInstructionSt.RecognizeInstruction.DateRegistration;
                oRI.LastUpdate = response.RecognizedInstructionSt.RecognizeInstruction.LastUpdate;
                oRI.Enabled = response.RecognizedInstructionSt.RecognizeInstruction.Enabled;
                return View(oRI);
            } else {
                return RedirectToAction("Error", "System", new { ErrorMessage = response.ErrorMessage});
            }
        }

        [HttpPost]
        public ActionResult EnableRI() {
            int idRI = Convert.ToInt32(Session["RecognizedInstructionDeleteId"]);

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

            RecognizedInstructionHS oRIHS = new RecognizedInstructionHS();
            var response = oRIHS.GetId<RecognizedInstructionStruct>(idRI);
            if (response.IsSuccess == true) {
                var responseEnable =
                oRIHS.Delete<RecognizedInstructionStruct>(
                    idRI,
                    !response.RecognizedInstructionSt.RecognizeInstruction.Enabled
                );
                if (responseEnable.IsSuccess == true) {
                    return RedirectToAction("DashboardRI", "RecognizedInstruction");
                } else {
                    return RedirectToAction("Error", "System", new { ErrorMessage = responseEnable.ErrorMessage});
                }
            } else {
                return RedirectToAction("Error", "System", new { ErrorMessage = response.ErrorMessage});
            }
        }

        #endregion EnableRI

        #region Methods

        public JsonResult NoSelectWordOnChange(string WordClassSelectedCode) {
            if(Session["RI_Edit_GrammarSelectedId"] == null) {
                RedirectToAction("Error", "System");
                return new JsonResult();
            }
            RecognizedInstructionModel oRIModel =
                (RecognizedInstructionModel)Session["RI_Edit_Model"];
            foreach (GrammarBuilderEn item in oRIModel.GrammarBuilderList) {
                if (item.WordClassCode == WordClassSelectedCode) {
                    item.RecognizedWordSelectedId = 0;
                    break;
                }
            }
            var json = JsonConvert.SerializeObject(oRIModel);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectWordOnChange(string RecognizedWordSelectedId, string WordClassSelectedCode) {
            int rwSelectedId = Convert.ToInt32(RecognizedWordSelectedId);
            if (Session["RI_Edit_GrammarSelectedId"] == null) {
                RedirectToAction("Error", "System");
            }
            RecognizedInstructionModel oRIModel =
                (RecognizedInstructionModel)Session["RI_Edit_Model"];
            foreach (GrammarBuilderEn item in oRIModel.GrammarBuilderList) {
                if (item.WordClassCode == WordClassSelectedCode) {
                    item.RecognizedWordSelectedId = rwSelectedId;
                    break;
                }
            }
            var json = JsonConvert.SerializeObject(oRIModel);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GenerateInstruction() {
            string stArmedInstruction = string.Empty;
            //
            if(Session["RI_Edit_GrammarSelectedId"] == null) {
                RedirectToAction("Error", "System");
            }
            RecognizedInstructionModel oRIModel =
                (RecognizedInstructionModel)Session["RI_Edit_Model"];
            bool thisOneMissing = false;
            foreach(GrammarBuilderEn item in oRIModel.GrammarBuilderList) {
                if(item.RecognizedWordSelectedId == 0) {
                    thisOneMissing = true;
                    break;
                }
                foreach (RecognizedWordEn irw in item.RecognizedWordsList) {
                    if (item.RecognizedWordSelectedId.Equals(irw.RecognizedWordId)) {
                        stArmedInstruction = stArmedInstruction + irw.Code + "_";
                    }
                }
            }
            if (thisOneMissing) {
                stArmedInstruction = string.Empty;
            } else {
                //  Remove the last underscore
                stArmedInstruction = stArmedInstruction.Substring(0, stArmedInstruction.Length - 1);
            }
            Session["RI_Edit_ArmedInstruction"] = stArmedInstruction;
            var json = JsonConvert.SerializeObject(stArmedInstruction);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public RecognizedInstructionEn CloneModelToEntity(
            RecognizedInstructionModel oModel
        ) {
            RecognizedInstructionEn oEntity = new RecognizedInstructionEn();
            oEntity.RecognizedInstructionId = oModel.RecognizedInstructionId;
            oEntity.UserId = oModel.UserId;
            oEntity.LanguageId = 2;
            oEntity.GrammarId = oModel.GrammarId;
            oEntity.Grammar = oModel.Grammar;
            oEntity.Code = oModel.Code;
            oEntity.Instruction = oModel.Instruction;
            oEntity.Description = oModel.Description;
            oEntity.VoiceProcessing = oModel.VoiceProcessing;
            oEntity.VoiceSolution = oModel.VoiceSolution;
            oEntity.VoiceCancel = oModel.VoiceCancel;
            oEntity.VoiceFail = oModel.VoiceFail;
            oEntity.DateRegistration = DateTime.Now;
            oEntity.LastUpdate = DateTime.Now;
            oEntity.Enabled = oModel.Enabled;
            return oEntity;
        }

        #endregion Methods

        #region Unused Methods

        public ActionResult StepTwoInstructionBuilder(string grammarSelectedId) {
            Session["RI_Edit_GrammarSelectedId"] = grammarSelectedId;
            return RedirectToAction(
                "Edit",
                "RecognizedInstruction",
                new {
                    GrammarSelectedId = Convert.ToInt32(grammarSelectedId),
                    Origin = "SELECT_GRAMMAR"
                }
            );
        }

        #endregion Unused Methods
    }
}