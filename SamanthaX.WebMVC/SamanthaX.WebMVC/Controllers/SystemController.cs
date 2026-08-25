using SamanthaX.Library.Function;
using SamanthaX.Library.HttpService;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using SamanthaX.WebMVC.Models.Home;
using SamanthaX.WebMVC.Models.System;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace SamanthaX.WebMVC.Controllers {

    public class SystemController : Controller {

        #region Index

        public ActionResult Index() {
            return View();
        }

        #endregion Index

        #region About

        public ActionResult About() {
            return View();
        }

        #endregion About

        #region WelcomeNewUser

        public ActionResult WelcomeNewUser() {
            return View();
        }

        #endregion WelcomeNewUser

        #region MyProfile

        public ActionResult MyProfile() {
            if ((Session["SystemUserId"] == null) || (Session["SystemUserCompanyId"] == null)) {
                return RedirectToAction(
                    "Index",
                    "Home",
                    new { ErrorMessage = "No user data found" }
                );
            }
            int companyId = Convert.ToInt32(Session["SystemUserCompanyId"]);
            int userId = Convert.ToInt32(Session["SystemUserId"]);
            MyProfileModel oMyProfile = new MyProfileModel();
            //
            CompanyHS hsCompany = new CompanyHS();
            var responseCompany = hsCompany.GetId<CompanyStruct>(companyId);
            if (responseCompany.IsSuccess) {
                SamanthaVoiceHS hsSV = new SamanthaVoiceHS();
                var responseSV = hsSV.GetUser<SamanthaVoiceStruct>(companyId);
                if (responseSV.IsSuccess) {
                    CompanyEn companyEn = new CompanyEn();
                    companyEn = responseCompany.CompanySt.Company;
                    SamanthaVoiceEn svEn = new SamanthaVoiceEn();
                    svEn = responseSV.SamanthaVoiceSt.SamanthaVoice;
                    oMyProfile = new MyProfileModel {
                        CompanyId = companyEn.CompanyId,
                        Tradename = companyEn.Tradename,
                        BusinessName = companyEn.BusinessName,
                        Name = companyEn.Name,
                        FirstName = companyEn.FirstName,
                        LastName = companyEn.LastName,
                        FTR = companyEn.FTR,
                        PRK = companyEn.PRK,
                        Street = companyEn.Street,
                        StreetNumber = companyEn.StreetNumber,
                        CrossingStreets = companyEn.CrossingStreets,
                        Colony = companyEn.Colony,
                        City = companyEn.City,
                        Municipality = companyEn.Municipality,
                        State = companyEn.State,
                        Country = companyEn.Country,
                        PostalCode = companyEn.PostalCode,
                        CellPhoneNumber = companyEn.CellPhoneNumber,
                        PhoneNumber = companyEn.PhoneNumber,
                        EMail = companyEn.EMail,
                        Token = (string)Session["SystemUserToken"],
                        //  Samantha Voice
                        SamanthaVoiceId = svEn.SamanthaVoiceId,
                        UserId = svEn.UserId,
                        AIName = svEn.AIName,
                        OrderYou = svEn.OrderYou,
                        VoiceProcessingDefault = svEn.VoiceProcessingDefault,
                        VoiceSolutionDefault = svEn.VoiceSolutionDefault,
                        VoiceCancelDefault = svEn.VoiceCancelDefault,
                        VoiceFailDefault = svEn.VoiceFailDefault,
                        AnExceptionOcurred = svEn.AnExceptionOcurred,
                        LastUpdate = svEn.LastUpdate,
                    };
                } else {
                    return RedirectToAction(
                        "Error",
                        "System",
                        new { ErrorMessage = $"{responseSV.ErrorMessage}" }
                    );
                }
            } else {
                return RedirectToAction(
                    "Error",
                    "System",
                    new { ErrorMessage = $"{responseCompany.ErrorMessage}" }
                );
            }
            return View(oMyProfile);
        }

        [HttpPost]
        public ActionResult MyProfile(MyProfileModel oMyProfile) {
            if ((Session["SystemUserId"] == null) || (Session["SystemUserCompanyId"] == null)) {
                return RedirectToAction(
                    "Index",
                    "Home",
                    new { ErrorMessage = "No user data found" }
                );
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

            int companyId = Convert.ToInt32(Session["SystemUserCompanyId"]);
            int userId = Convert.ToInt32(Session["SystemUserId"]);
            if (ModelState.IsValid) {
                CompanyHS hsCompany = new CompanyHS();
                CompanyEn companyEn = new CompanyEn {
                    CompanyId = companyId,
                    CompanyTypeId = 0,
                    Tradename = HandleText.StringHandle(oMyProfile.Tradename),
                    BusinessName = HandleText.StringHandle(oMyProfile.BusinessName),
                    Name = HandleText.StringHandle(oMyProfile.Name),
                    FirstName = HandleText.StringHandle(oMyProfile.FirstName),
                    LastName = HandleText.StringHandle(oMyProfile.LastName),
                    FTR = HandleText.StringHandle(oMyProfile.FTR),
                    PRK = HandleText.StringHandle(oMyProfile.PRK),
                    Street = HandleText.StringHandle(oMyProfile.Street),
                    StreetNumber = HandleText.StringHandle(oMyProfile.StreetNumber),
                    CrossingStreets = HandleText.StringHandle(oMyProfile.CrossingStreets),
                    Colony = HandleText.StringHandle(oMyProfile.Colony),
                    City = HandleText.StringHandle(oMyProfile.City),
                    Municipality = HandleText.StringHandle(oMyProfile.Municipality),
                    State = HandleText.StringHandle(oMyProfile.State),
                    Country = HandleText.StringHandle(oMyProfile.Country),
                    PostalCode = HandleText.StringHandle(oMyProfile.PostalCode),
                    CellPhoneNumber = HandleText.StringHandle(oMyProfile.CellPhoneNumber),
                    PhoneNumber = HandleText.StringHandle(oMyProfile.PhoneNumber),
                    EMail = HandleText.StringHandle(oMyProfile.EMail),
                    DateRegistration = DateTime.Now,
                    Enabled = true,
                };
                var responseCompany = hsCompany.Save<CompanyStruct>(companyEn);
                //
                SamanthaVoiceHS hsSV = new SamanthaVoiceHS();
                SamanthaVoiceEn svEn = new SamanthaVoiceEn {
                    SamanthaVoiceId = oMyProfile.SamanthaVoiceId,
                    UserId = oMyProfile.UserId,
                    LanguageId = 2,
                    AIName = oMyProfile.AIName,
                    OrderYou = oMyProfile.OrderYou,
                    VoiceProcessingDefault = oMyProfile.VoiceProcessingDefault,
                    VoiceSolutionDefault = oMyProfile.VoiceSolutionDefault,
                    VoiceCancelDefault = oMyProfile.VoiceCancelDefault,
                    VoiceFailDefault = oMyProfile.VoiceFailDefault,
                    AnExceptionOcurred = oMyProfile.AnExceptionOcurred,
                    DateRegistration = DateTime.Now,
                    LastUpdate = DateTime.Now,
                    Enabled = true,
                };
                var responseSV = hsSV.Save<SamanthaVoiceStruct>(svEn);
                if (responseSV.IsSuccess) {
                    return RedirectToAction("Index", "System");
                } else {
                    return RedirectToAction(
                        "Error",
                        "System",
                        new { ErrorMessage = $"{responseSV.ErrorMessage}" }
                    );
                }
            }
            return View(oMyProfile);
        }

        #endregion MyProfile

        #region TechnicalSupport

        public ActionResult TechnicalSupport() {
            return View();
        }

        [HttpPost]
        public ActionResult TechnicalSupport(string guest) {
            return View();
        }

        #endregion TechnicalSupport

        #region Logout

        public ActionResult Logout() {
            return View();
        }

        [HttpPost]
        public ActionResult Logout(string answer) {
            Session["SystemUserId"] = null;
            Session["SystemUserEMail"] = null;
            return RedirectToAction("Index", "Home");
        }

        #endregion Logout

        #region Error

        public ActionResult Error(string ErrorMessage) {
            ViewBag.SystemErrorMessage = ErrorMessage == null? "The Error was Not Specified": ErrorMessage;
            return View();
        }

        #endregion Error
    }
}
