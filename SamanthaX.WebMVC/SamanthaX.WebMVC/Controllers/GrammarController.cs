using Newtonsoft.Json;
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

    public class GrammarController : Controller {

        #region IndexG

        // GET: Grammar
        public ActionResult IndexG() {
            return RedirectToAction("DashboardG");
        }

        #endregion IndexG

        #region DashboardG

        public ActionResult DashboardG() {
            if (Session["SystemUserId"] == null) {
                return RedirectToAction("Index", "Home", new { ErrorMessage = "No user data found" });
            }
            ViewData["ErrorMessageSX"] = null;
            List<GrammarEn> grammarList = new List<GrammarEn>();
            //
            GrammarHS oHS = new GrammarHS();
            var response =
                oHS
                .GetAll<GrammarStruct>(
                    Convert.ToInt32(Session["SystemUserId"])
                );
            if (response.IsSuccess == true) {
                grammarList = response.GrammarSt.GrammarList;
                return View(grammarList);
            } else {
                return RedirectToAction(
                    "Error",
                    "System",
                    new { ErrorMessage = response.ErrorMessage }
                );
            }
        }

        #endregion DashboardG

        #region EditG

        public ActionResult EditG(
            string grammarId,
            string idWordClassSelected,
            string processWordClassId,
            string origin
        ) {
            if (Session["SystemUserId"] == null) {
                return RedirectToAction("Index", "Home");
            }

            #region CREATE

            //  From Create button
            if (origin.Equals("CREATE")) {
                Session["Grammar_Edit_GrammarBuilder"] = null;
                return View(new GrammarModel());
            }

            #endregion CREATE

            GrammarModel oGrammarModel = new GrammarModel();

            #region UPDATING

            if (origin.Equals("UPDATING")) {
                Session["Grammar_Edit_GrammarBuilder"] = null;
                GrammarHS gHS = new GrammarHS();
                var responseG = gHS.GetId<GrammarStruct>(Convert.ToInt32(grammarId));
                if (responseG.IsSuccess == true) {
                    GrammarModel oGModel = new GrammarModel();
                    oGModel.GrammarId = responseG.GrammarSt.Grammar.GrammarId;
                    oGModel.Code = responseG.GrammarSt.Grammar.Code;
                    oGModel.Name = responseG.GrammarSt.Grammar.Name;
                    oGModel.Description = responseG.GrammarSt.Grammar.Description;
                    oGModel.DateRegistration = DateTime.MinValue;
                    oGModel.Enabled = responseG.GrammarSt.Grammar.Enabled;
                    //
                    Session["Grammar_Edit_grammarId"] = oGModel.GrammarId;
                    Session["Grammar_Edit_code"] = oGModel.Code;
                    Session["Grammar_Edit_name"] = oGModel.Name;
                    Session["Grammar_Edit_description"] = oGModel.Description;
                    
                    List<GrammarBuilderEn> myGBList =
                        responseG.GrammarSt.Grammar.GrammarBuilderList;
                    foreach (GrammarBuilderEn igb in myGBList) {
                        igb.sequenceDecimal = Convert.ToDouble(igb.Sequence);
                    }
                    myGBList.Sort((p1, p2) => p1.sequenceDecimal.CompareTo(p2.sequenceDecimal));
                    Session["Grammar_Edit_GrammarBuilder"] = myGBList;

                    List<GrammarBuilderEn> listGBaux = new List<GrammarBuilderEn>();
                    foreach (GrammarBuilderEn item in myGBList) {
                        GrammarBuilderEn aux = new GrammarBuilderEn();
                        aux.GrammarBuilderId = item.GrammarBuilderId;
                        aux.GrammarId = item.GrammarId;
                        aux.WordClassId = item.WordClassId;
                        aux.WordClassCode = item.WordClassCode;
                        aux.WordClassName = item.WordClassName;
                        aux.Sequence = item.Sequence;
                        aux.DateRegistration = item.DateRegistration;
                        aux.Enabled = item.Enabled;
                        aux.RecognizedWordsList = item.RecognizedWordsList;
                        aux.sequenceDecimal = item.sequenceDecimal;
                        aux.RecognizedWordSelectedId = item.RecognizedWordSelectedId;
                        listGBaux.Add(aux);
                    }

                    Session["Grammar_Edit_GrammarBuilder_Original"] = listGBaux;
                    oGModel.GrammarBuilderList = myGBList;
                    //
                    return View(oGModel);
                } else {
                    return RedirectToAction(
                        "Error",
                        "System",
                        new { ErrorMessage = responseG.ErrorMessage }
                    );
                }
            }

            #endregion UPDATING

            oGrammarModel.GrammarId = Convert.ToInt32(Session["Grammar_Edit_grammarId"]);
            oGrammarModel.Code = Convert.ToString(Session["Grammar_Edit_code"]);
            oGrammarModel.Name = Session["Grammar_Edit_name"].ToString();
            oGrammarModel.Description = Session["Grammar_Edit_description"].ToString();
            List<GrammarBuilderEn> listGB = (List<GrammarBuilderEn>)Session["Grammar_Edit_GrammarBuilder"];

            #region SELWC

            if (origin.Equals("SELWC")) {

                //  From Select WordClass, then previous information
                int wcIdSelected = Convert.ToInt32(idWordClassSelected);
                idWordClassSelected = null;
                //
                //  Validatge that no repeated words are added
                bool alreadyOk = false; ;
                foreach (GrammarBuilderEn igb in listGB) {
                    if (igb.WordClassId == wcIdSelected) {
                        alreadyOk = true;
                        break;
                    }
                }
                if (alreadyOk) {
                    wcIdSelected = 0;
                }
                //
                //  Search WordClass recorda
                //  If <0> then Nothing added
                if (wcIdSelected > 0) {
                    WordClassHS oHS = new WordClassHS();
                    var response = oHS.GetId<WordClassStruct>(wcIdSelected);
                    if (response.IsSuccess == true) {
                        GrammarBuilderEn oGrammarBuilder = new GrammarBuilderEn();
                        oGrammarBuilder.GrammarBuilderId = 0;
                        oGrammarBuilder.GrammarId = oGrammarModel.GrammarId;
                        oGrammarBuilder.WordClassId = wcIdSelected;
                        oGrammarBuilder.WordClassCode = response.WordClassSt.WordClass.Code;
                        oGrammarBuilder.WordClassName = response.WordClassSt.WordClass.Name;
                        oGrammarBuilder.Sequence = 0;
                        oGrammarBuilder.DateRegistration = DateTime.Now;
                        oGrammarBuilder.Enabled = true;
                        listGB.Add(oGrammarBuilder);
                        //  Assing sequence
                        int sequencer = 0;
                        foreach (GrammarBuilderEn e in listGB) {
                            sequencer++;
                            e.Sequence = sequencer;
                        }
                        //  Update list Session
                        Session["Grammar_Edit_GrammarBuilder"] = listGB;
                    } else {
                        ViewData["ErrorMessageSX"] = $"The following was detected: {response.ErrorMessage}";
                    }
                }
                oGrammarModel.GrammarBuilderList = listGB;
                return View(oGrammarModel);
            }

            #endregion SELWC

            #region UP-DOWN

            if(origin.Equals("UP") || origin.Equals("DOWN")) {
                //
                foreach (GrammarBuilderEn igb in listGB) {
                    igb.sequenceDecimal = Convert.ToDouble(igb.Sequence);
                }
                //  Get the sequence of the Word Class
                foreach (GrammarBuilderEn igb in listGB) {
                    if(igb.WordClassId == Convert.ToInt32(processWordClassId)) {
                        if (origin.Equals("UP")) {
                            if (igb.sequenceDecimal > 1) {
                                igb.sequenceDecimal = igb.sequenceDecimal - 1.5;
                            }
                        } else {
                            igb.sequenceDecimal = igb.sequenceDecimal + 1.5;
                        }
                        break;
                    }
                }
                listGB.Sort((p1, p2) => p1.sequenceDecimal.CompareTo(p2.sequenceDecimal));
                //
                int sequencer = 0;
                foreach (GrammarBuilderEn e in listGB) {
                    sequencer++;
                    e.Sequence = sequencer;
                }
                //  Update list Session
                Session["Grammar_Edit_GrammarBuilder"] = listGB;
                oGrammarModel.GrammarBuilderList = listGB;
                return View(oGrammarModel);
            }

            #endregion UP-DOWN

            #region DELWC

            if (origin.Equals("DELWC")) {

                int wcToDelete = Convert.ToInt32(processWordClassId);

                if (wcToDelete > 0) {
                    //  Delete it
                    listGB.RemoveAll(r => r.WordClassId == Convert.ToInt32(wcToDelete));
                    if (listGB.Count == 0) {
                        Session["Grammar_Edit_GrammarBuilder"] = null;
                    } else {
                        //  Assing sequence
                        int sequencer = 0;
                        foreach (GrammarBuilderEn e in listGB) {
                            sequencer++;
                            e.Sequence = sequencer;
                        }
                        Session["Grammar_Edit_GrammarBuilder"] = listGB;
                    }
                } else {
                    //  Back
                }
                oGrammarModel.GrammarBuilderList = listGB;

                return View(oGrammarModel);
            }

            #endregion DELWC

            //  It is a mistake to comes this far
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult EditG(GrammarModel oGModel) {

            #region Validation

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

            if (Session["SystemUserId"] == null) {
                return RedirectToAction("Index", "Home", new { ErrorMessage = "No user data found" });
            }
            
            if (oGModel.GrammarId > 0 && Session["Grammar_Edit_GrammarBuilder_Original"] == null) {
                return RedirectToAction(
                        "Error",
                        "System",
                        new { ErrorMessage = "No data found" }
                    );
            }

            #endregion Validation

            ViewData["ErrorMessageSX"] = null;
            List<GrammarBuilderEn> listMyGBCurrent =
                (List<GrammarBuilderEn>)Session["Grammar_Edit_GrammarBuilder"];

            if (listMyGBCurrent == null || listMyGBCurrent.Count == 0) {
                ViewData["ErrorMessageSX"] = "You must define a grammar with the different Word Classes";
                oGModel.GrammarBuilderList = null;
                return View(oGModel);
            }

            if (ModelState.IsValid) {
                try {
                    GrammarHS oGHS = new GrammarHS();
                    GrammarEn oGrammar = new GrammarEn();
                    oGrammar.GrammarId = oGModel.GrammarId;
                    oGrammar.UserId = Convert.ToInt32(Session["SystemUserId"]);
                    oGrammar.LanguageId = 2;
                    oGrammar.Code = oGModel.Code;
                    oGrammar.Name = oGModel.Name;
                    oGrammar.Description = oGModel.Description != null
                        ? oGModel.Description
                        : string.Empty;
                    oGrammar.DateRegistration = DateTime.Now;
                    oGrammar.Enabled = true;
                    //
                    var response =
                        oGHS.Save<GrammarStruct>(oGrammar);
                    if (response.IsSuccess == true) {
                        //
                        GrammarBuilderHS oGBHS = new GrammarBuilderHS();
                        foreach (GrammarBuilderEn item in listMyGBCurrent) {
                            var responseGB = 
                                oGBHS.Save<GrammarBuilderStruct>(
                                    item.GrammarBuilderId,
                                    response.GrammarSt.Grammar.GrammarId,
                                    item.WordClassId,
                                    item.Sequence
                                );
                        }

                        #region Validation Deleted

                        if (oGModel.GrammarId > 0) {
                            //  Compare the original list with the current one to determine if
                            //  any records were deleted
                            List<GrammarBuilderEn> listMyGBOriginal =
                                (List<GrammarBuilderEn>)Session["Grammar_Edit_GrammarBuilder_Original"];

                            foreach (GrammarBuilderEn iorig in listMyGBOriginal) {
                                bool isDeleted = true;
                                foreach (GrammarBuilderEn icurr in listMyGBCurrent) {
                                    if (iorig.GrammarBuilderId == icurr.GrammarBuilderId) {
                                        isDeleted = false;
                                        break;
                                    }
                                }
                                if (isDeleted) {
                                    iorig.deleted = true;
                                    var responseDel = 
                                        oGBHS.Delete<GrammarBuilderStruct>(iorig.GrammarBuilderId);
                                }
                            }
                            //foreach (GrammarBuilderEn iorig in listMyGBOriginal) {
                            //    if (iorig.deleted) {
                            //        iorig.deleted = true;
                            //    }
                            //}
                        }

                        #endregion Validation Deleted

                        return RedirectToAction("DashboardG", "Grammar");
                    } else {
                        ViewData["ErrorMessageSX"] = response.ErrorMessage;
                        oGModel.GrammarBuilderList = listMyGBCurrent;
                        return View(oGModel);
                    }
                } catch(Exception ex) {
                    return RedirectToAction(
                        "Error",
                        "System",
                        new { ErrorMessage = $"{ex.Message}.\nStackTrace: {ex.StackTrace}" }
                    );
                }
            } else {
                oGModel.GrammarBuilderList = listMyGBCurrent;
                return View(oGModel);
            }
        }

        #endregion EditG

        #region EnableG

        public ActionResult EnableG(int grammarId) {
            GrammarHS oHS = new GrammarHS();
            var response =
                oHS.GetId<GrammarStruct>(grammarId);
            if (response.IsSuccess == true) {
                Session["GrammarDeleteId"] = grammarId.ToString();
                GrammarModel oGModel = new GrammarModel();
                oGModel.GrammarId = response.GrammarSt.Grammar.GrammarId;
                oGModel.Code = response.GrammarSt.Grammar.Code;
                oGModel.Name = response.GrammarSt.Grammar.Name;
                oGModel.Description = response.GrammarSt.Grammar.Description;
                oGModel.DateRegistration = DateTime.MinValue;
                oGModel.Enabled = response.GrammarSt.Grammar.Enabled;
                return View(oGModel);
            } else {
                return RedirectToAction(
                    "Error",
                    "System",
                    new { ErrorMessage = response.ErrorMessage }
                );
            }
        }

        [HttpPost]
        public ActionResult EnableG(GrammarModel oModel) {
            int idRW = Convert.ToInt32(Session["GrammarDeleteId"]);

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

            GrammarHS oHS = new GrammarHS();
            var response =
                oHS.GetId<GrammarStruct>(idRW);
            if (response.IsSuccess == true) {
                var responseEnable =
                oHS.Delete<GrammarStruct>(idRW, !response.GrammarSt.Grammar.Enabled);
                if (responseEnable.IsSuccess == true) {
                    return RedirectToAction("DashboardG", "Grammar");
                } else {
                    return RedirectToAction(
                        "Error",
                        "System",
                        new { ErrorMessage = responseEnable.ErrorMessage }
                    );
                }
            } else {
                return RedirectToAction(
                    "Error",
                    "System",
                    new { ErrorMessage = response.ErrorMessage }
                );
            }
        }

        #endregion EnableG

        #region SelectWordClassG

        public ActionResult SelectWordClassG() {
            if(Session["SystemUserId"] == null) {
                return RedirectToAction("Index", "Home");
            }
            //
            List<WordClassEn> wordClassList = new List<WordClassEn>();
            WordClassHS oHS = new WordClassHS();
            var response =
                oHS.GetAll<WordClassStruct>(
                    Convert.ToInt32(Session["SystemUserId"]),
                    allowRecognizedWord: true
                    );
            if(response.IsSuccess == true) {
                wordClassList = response.WordClassSt.WordClassList;
                //  Delete the already selected WordClass
                List<GrammarBuilderEn> wordsClassAlreadySelected =
                    (List<GrammarBuilderEn>)Session["Grammar_Edit_GrammarBuilder"];
                List<WordClassEn> newListFiltered = new List<WordClassEn>();
                foreach(WordClassEn isearch in wordClassList) {
                    bool alreadyOk = false;
                    foreach(GrammarBuilderEn ialready in wordsClassAlreadySelected) {
                        if(ialready.WordClassId == isearch.WordClassId) {
                            alreadyOk = true;
                            break;
                        }
                    }
                    if(alreadyOk == false) {
                        newListFiltered.Add(isearch);
                    }
                }
                return View(newListFiltered);
            } else {
                return RedirectToAction(
                    "Error",
                    "System",
                    new { ErrorMessage = response.ErrorMessage }
                );
            }
        }

        //public ActionResult SelectWordClass(
        //    string grammarId,
        //    string code,
        //    string name,
        //    string description) {

        //    if(Session["SystemUserId"] == null) {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    Session["Grammar_Edit_grammarId"] = grammarId;
        //    Session["Grammar_Edit_code"] = code;
        //    Session["Grammar_Edit_name"] = name;
        //    Session["Grammar_Edit_description"] = description;
        //    if(Session["Grammar_Edit_GrammarBuilder"] == null) {
        //        Session["Grammar_Edit_GrammarBuilder"] = new List<GrammarBuilderEn>();
        //    } else {

        //    }
        //    //
        //    ViewData["ErrorMessageSX"] = null;
        //    List<WordClassEn> wordClassList = new List<WordClassEn>();
        //    WordClassHS oHS = new WordClassHS();
        //    var response =
        //        oHS.GetAll<WordClassStruct>(
        //            Convert.ToInt32(Session["SystemUserId"]),
        //            allowRecognizedWord: true
        //            );
        //    if(response.IsSuccess == true) {
        //        wordClassList = response.WordClassSt.WordClassList;
        //        //
        //        //  Delete the already selected WordClass
        //        List<GrammarBuilderEn> wordsClassAlreadySelected = (List<GrammarBuilderEn>)Session["Grammar_Edit_GrammarBuilder"];
        //        List<WordClassEn> newList = new List<WordClassEn>();
        //        foreach(WordClassEn isearch in wordClassList) {
        //            bool alreadyOk = false;
        //            foreach(GrammarBuilderEn ialready in wordsClassAlreadySelected) {
        //                if(ialready.WordClassId == isearch.WordClassId) {
        //                    alreadyOk = true;
        //                    break;
        //                }
        //            }
        //            if(alreadyOk == false) {
        //                newList.Add(isearch);
        //            }
        //        }
        //        //
        //        return View(newList);
        //    } else {
        //        ViewData["ErrorMessageSX"] = $"Se detectó lo siguiente: {response.ErrorMessage}";
        //        return Vi ew();
        //    }
        //}

        [HttpPost]
        public ActionResult SelectWordClassG(string WordClassId) {
            return RedirectToAction("EditG","Grammar");
        }

        #endregion SelectWordClassG

        #region DeleteWordClassG

        public ActionResult DeleteWordClassG(string wordClassId) {
            //
            GrammarModel oGrammarModel = new GrammarModel();
            oGrammarModel.GrammarId = Convert.ToInt32(Session["Grammar_Edit_grammarId"]);
            oGrammarModel.Code = Convert.ToString(Session["Grammar_Edit_code"]);
            oGrammarModel.Name = Session["Grammar_Edit_name"].ToString();
            oGrammarModel.Description = Session["Grammar_Edit_description"].ToString();
            oGrammarModel.GrammarBuilderList = null;
            //
            WordClassHS oWCHS = new WordClassHS();
            var response = oWCHS.GetId<GrammarBuilderStruct>(
                    Convert.ToInt32(wordClassId)
                );
            if (response.IsSuccess) {
                GrammarBuilderEn oGB = new GrammarBuilderEn();
                oGB.GrammarBuilderId = 0;
                oGB.GrammarId = 0;
                oGB.WordClassId = response.WordClassSt.WordClass.WordClassId;
                oGB.WordClassCode = response.WordClassSt.WordClass.Code;
                oGB.WordClassName = response.WordClassSt.WordClass.Name;
                oGB.Sequence = 0;
                oGB.DateRegistration = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
                oGB.Enabled = false;
                return View(oGB);
            } else {
                return RedirectToAction(
                    "Error",
                    "System",
                    new { ErrorMessage = response.ErrorMessage }
                );
            }
        }

        [HttpPost]
        public ActionResult DeleteWordClassG(string processWordClassId, string origin) {
            return RedirectToAction(
                "EditG",
                "Grammar",
                new {
                    grammarId="",
                    idWordClassSelected="",
                    processWordClassId = processWordClassId,
                    origin = "DELWC"
                }
            );
        }

        #endregion DeleteWordClassG

        #region Methods

        public ActionResult ProcessItemGrammarBuilder(
            string grammarId,
            string code,
            string name,
            string description,
            string wordClassId,
            string method) {

            //  Save Grammar Model
            Session["Grammar_Edit_grammarId"] = grammarId;
            Session["Grammar_Edit_code"] = code;
            Session["Grammar_Edit_name"] = name;
            Session["Grammar_Edit_description"] = description;
            if (Session["Grammar_Edit_GrammarBuilder"] == null) {
                Session["Grammar_Edit_GrammarBuilder"] = new List<GrammarBuilderEn>();
            }

            if(method.Equals("SEL")) 
                return RedirectToAction("SelectWordClassG", "Grammar");
            
            if(method.Equals("UP") || method.Equals("DOWN"))
                return RedirectToAction(
                    "editG",
                    "Grammar",
                    new { processWordClassId = wordClassId, origin = method })
                ;

            if(method.Equals("DEL"))
                return RedirectToAction("DeleteWordClassG", "Grammar", new { wordClassId = wordClassId });
            return RedirectToAction(
                "Error",
                "System",
                new { ErrorMessage = "The process could not be determined" }
            );
        }

        #endregion Methods

        #region Unused Methods

        [HttpPost]
        public JsonResult Refererence_NotUsed(string wordClassSelected, string otroParametro) {
            //ViewData["ErrorMessageWordClassSelect"] = null;
            var resultado = string.Empty;
            if (string.IsNullOrEmpty(wordClassSelected)) {
                resultado = "You must select a Word Class";
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            List<GrammarBuilderEn> listTemp = new List<GrammarBuilderEn>();
            if (Session["Grammar_GrammarBuilderList"] == null) {
                Session["Grammar_GrammarBuilderList"] = new List<GrammarBuilderEn>();
            } else {
                listTemp = (List<GrammarBuilderEn>)Session["Grammar_GrammarBuilderList"];
            }
            GrammarBuilderEn oGB = new GrammarBuilderEn();
            oGB.GrammarBuilderId = 0;
            oGB.GrammarId = 0;
            oGB.WordClassId = 0;
            oGB.WordClassCode = "Hello Word";
            oGB.Sequence = 1;
            oGB.DateRegistration = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            oGB.Enabled = true;
            oGB.RecognizedWordsList = null;

            listTemp.Add(oGB);
            Session["Grammar_GrammarBuilderList"] = listTemp;

            resultado = $"Ok-{listTemp.Count}";
            //return Json(resultado, JsonRequestBehavior.AllowGet);
            var json = JsonConvert.SerializeObject(listTemp);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        #endregion Unused Methods
    }
}