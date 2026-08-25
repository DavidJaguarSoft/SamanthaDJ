using Microsoft.Ajax.Utilities;
using SamanthaX.Library.HttpService;
using SamanthaX.Library.Security;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using SamanthaX.WebMVC.Models.Home;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

/*
 * PENDIENTE
 *  -   Pagina para cambiar contraseña
 */

namespace SamanthaX.WebMVC.Controllers {

    public class HomeController :Controller {

        #region Index

        public ActionResult Index() {
            return View();
        }

        #endregion Index

        #region About

        public ActionResult About() {
            ViewBag.Message = "Acerca de David Jaguar Soft";
            return View();
        }

        #endregion About

        #region Contact

        public ActionResult Contact() {
            ViewBag.Message = "Contáctanos";
            return View();
        }

        #endregion Contact

        #region Login

        //  GET: Acceso
        public ActionResult Login() {
            //ViewBag.ilist1 = new List<ProductEn>();
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel pLogin) {
            ViewData["ErrorMessageSX"] = null;
            if(ModelState.IsValid) {
                try {
                    Security security = new Security();
                    string passEncrypt = security.Encrypt(pLogin.Password);
                    string mioDes = security.Decrypt(passEncrypt);

                    #region Login

                    UserSXHS userHS = new UserSXHS();
                    var responseLogin = 
                        userHS.AuthenticateUser<UserStruct>(pLogin.User, pLogin.Password);
                    if (responseLogin.IsSuccess == true) {
                        Session["SystemUserId"] = responseLogin.UserSt.User.UserId;
                        Session["SystemUserCompanyId"] = responseLogin.UserSt.User.UserId;
                        Session["SystemUserEMail"] = responseLogin.UserSt.User.Name;
                        Session["SystemUserToken"] = responseLogin.UserSt.User.Token;
                        return RedirectToAction("Index", "System");
                    } else {
                        ViewData["ErrorMessageSX"] = $"The following was detected: {responseLogin.ErrorMessage}";
                        return View();
                    }

                    #endregion Login

                } catch (Exception ex) {
                    ViewData["ErrorMessageSX"] = $"Ocurrió la siguiente excepción: {ex.Message}";
                    return View();
                }
            } else {
                return View();
            }
        }

        #endregion Login

        #region Registration

        //  Get
        public ActionResult Registration() {
            RegistrationModel oRegistrationModel = new RegistrationModel();
            if(Session["Registration_Object"] != null) {
                oRegistrationModel = (RegistrationModel)Session["Registration_Object"];
            }
            return View(oRegistrationModel);
        }

        [HttpPost]
        public ActionResult Registration(RegistrationModel oRegister) {
            ViewData["ErrorMessageSX"] = null;
            string errorDetected = string.Empty;
            Session["Registration_Object"] = oRegister;

            if (ModelState.IsValid) {
                if (oRegister.Password.Trim().Length < 8) {
                    errorDetected = "* Su contaseña debe ser de al menos 8 carácteres.\n";
                }
                if(!oRegister.Password.Trim().Equals(oRegister.ConfirmPassword.Trim())) {
                    errorDetected += "* La confirmación del Password no coincide.\n";
                }
                if (!string.IsNullOrEmpty(errorDetected)) {
                    ViewData["ErrorMessageSX"] = errorDetected;
                    return View(oRegister);
                }
                    
                #region Registration Process

                //  Verificar si ya se encuentra registrado
                RegistrationHS registrationHS = new RegistrationHS();
                var response =
                    registrationHS.GetEMail<RegistrationStruct>(oRegister.EMail);
                    
                if (response.IsSuccess == true) {
                    //  Si el Correo ya esta registrado
                    Session["UserRegisteredEMail"] = oRegister.EMail;
                    if (response.RegistrationSt.Registration.CompletedRegistration) {
                        //      Si el registro esta completado, mostrar ventana "UserAlreadyRegistered" con las opciones
                        //          * Opcion 1. Ir a "Login"
                        //          * Opcion 2. Ir a "ForgotPassword"
                        
                        return RedirectToAction("Login", "Home");
                    } else {
                        //      mostrar ventana "CompleteYourRegistration"
                        //          <Vista de Registration con datos precargados>
                        //          <Sin la opción de modificar el correo>
                        Session["UserRegisteredDate"] = response.RegistrationSt.Registration.DateRegistration;
                        return RedirectToAction("UserAlreadyRegistered", "Home");
                    }
                } else {
                    //  Si el correo no esta registrado, continuar con el registro
                    //      * Generar un Token de Registro
                    string token = GenerateRandomAlphanumericString(5);
                    //      * Grabar en BD el "PreRegistro" con su Token
                    //      * Enviar por correo registrado el Token
                    Security security = new Security();
                    string passEncrypt = security.Encrypt(oRegister.Password);
                    var responseRegistration =
                        registrationHS.SendToken<UserStruct>(
                            oRegister.Company,
                            oRegister.FirstName,
                            oRegister.LastName,
                            oRegister.EMail,
                            passEncrypt,
                            2,
                            token
                            );
                    if (responseRegistration.IsSuccess == true) {
                        Session["UserEMail"] = oRegister.EMail;
                        Session["UserToken"] = responseRegistration.RegistrationSt.Registration.Token;
                        //      * Mostrar Ventana "RegistrationToken" para confirmar el registro
                        return RedirectToAction("RegistrationToken", "Home");
                    } else {
                        ViewData["ErrorMessageSX"] = $"The following was detected: {responseRegistration.ErrorMessage}";
                        return View(oRegister);
                    }
                }

                #endregion Registration Process
            }
            return View(oRegister);
        }

        private static string GenerateRandomAlphanumericString(int length) {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabdefghjkmnpqrstuvwxy23456789";
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder(length);

            for(int i = 0; i < length; i++) {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

        #endregion Registration

        #region RegistrationToken

        public ActionResult RegistrationToken() {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrationToken(RegistrationTokenModel oTokenModel) {
            if (oTokenModel.Token.Trim().Equals(Session["UserToken"])) {
                RegistrationHS registrationHS = new RegistrationHS();
                var response = registrationHS.GetEMail<RegistrationStruct>(oTokenModel.EMail);
                if (response.IsSuccess == true) {
                    var responseCreate =
                        registrationHS
                            .CreateUser<RegistrationStruct>(
                                response.RegistrationSt.Registration.RegistrationId,
                                 response.RegistrationSt.Registration.Company,
                                 response.RegistrationSt.Registration.FirstName,
                                 response.RegistrationSt.Registration.LastName,
                                 response.RegistrationSt.Registration.EMail,
                                 response.RegistrationSt.Registration.Password,
                                 2
                            );
                    if (responseCreate.IsSuccess) {
                        Session["SystemUserId"] = responseCreate.RegistrationSt.Registration.UserId;
                        Session["SystemUserCompanyId"] = responseCreate.RegistrationSt.Registration.CompanyId;
                        Session["SystemUserEMail"] = responseCreate.RegistrationSt.Registration.EMail;

                        UserSXHS userHS = new UserSXHS();
                        Security security = new Security();
                        var responseLogin =
                            userHS.AuthenticateUser<UserStruct>(
                                responseCreate.RegistrationSt.Registration.EMail,
                                security.Decrypt(responseCreate.RegistrationSt.Registration.Password)
                            );
                        if (responseLogin.IsSuccess == true) {
                            Session["SystemUserToken"] = responseLogin.UserSt.User.Token;
                            return RedirectToAction("Index", "System");
                        } else {
                            ViewData["ErrorMessageSX"] = $"The following was detected: {responseLogin.ErrorMessage}";
                            return View();
                        }
                    }
                    Session["Registration_Object"] = null;
                    return RedirectToAction("WelcomeNewUser", "System");
                } else {
                    ViewData["ErrorMessageSX"] = "Ocurrió un Error en el proceso. Inténtelo mas tarde !";
                    return View();
                }
            } else {
                ViewData["ErrorMessageSX"] = "El Token introducido No coincide con el enviado a su Correo";
                return View();
            }
        }

        #endregion RegistrationToken

        #region TermsAndConditionsEN

        public ActionResult TermsAndConditionsEN() {
            return View();
        }

        #endregion TermsAndConditionsEN

        #region TermsAndConditionsES

        public ActionResult TermsAndConditionsES(
            string company,
            string firstname,
            string lastname,
            string email,
            string origin
        ) {
            if (origin != null && origin.Equals("FromRegister")) {
                RegistrationModel oRM = new RegistrationModel();
                oRM.Company = company;
                oRM.FirstName = firstname;
                oRM.LastName = lastname;
                oRM.EMail = email;

                Session["Registration_Object"] = oRM;
            }
            
            return View();
        }

        #endregion TermsAndConditionsES

        #region ForgotPassword

        public ActionResult ForgotPassword() {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel oForgotPass) {
            ViewData["ErrorMessageSX"] = null;
            string errorDetected = string.Empty;
            if(ModelState.IsValid) {
                RegistrationHS registrationHS = new RegistrationHS();
                var response =
                    registrationHS.SendPasswordToEMail<RegistrationStruct>(oForgotPass.EMail);

                if (response.IsSuccess == true) {
                     return RedirectToAction("PasswordSent", "Home");
                } else {
                    ViewData["ErrorMessageSX"] = "Este correo no se encuentra Registrado";
                }
            }
            return View();
        }

        #endregion ForgotPassword

        #region PasswordSent

        public ActionResult PasswordSent() {
            return View();
        }

        #endregion PasswordSent

        #region UserAlreadyRegistered

        public ActionResult UserAlreadyRegistered() {
            return View();
        }

        #endregion UserAlreadyRegistered

        #region CompleteYourRegistration

        public ActionResult CompleteYourRegistration() {

            if (Session["UserRegisteredEMail"] != null) {
                RegistrationHS registrationHS = new RegistrationHS();
                var response =
                        registrationHS
                        .GetEMail<RegistrationStruct>(
                            Session["UserRegisteredEMail"].ToString()
                        );
                RegistrationModel regModel = new RegistrationModel();
                regModel.Company = response.RegistrationSt.Registration.Company;
                regModel.FirstName = response.RegistrationSt.Registration.FirstName;
                regModel.LastName = response.RegistrationSt.Registration.LastName;
                regModel.EMail = response.RegistrationSt.Registration.EMail;
                regModel.Password = string.Empty;
                regModel.ConfirmPassword = string.Empty;
                return View(regModel);
            } else {
                return RedirectToAction("Index","Home");
            }
        }

        [HttpPost]
        public ActionResult CompleteYourRegistration(RegistrationModel oRegister) {
            ViewData["ErrorMessageSX"] = null;
            string errorDetected = string.Empty;
            if (ModelState.IsValid) {
                if (oRegister.Password.Trim().Length < 8) {
                    errorDetected = "* Su contaseña debe ser de al menos 8 carácteres.\n";
                }
                if (!oRegister.Password.Trim().Equals(oRegister.ConfirmPassword.Trim())) {
                    errorDetected += "* La confirmación del Password no coincide.\n";
                }
                if (!string.IsNullOrEmpty(errorDetected)) {
                    ViewData["ErrorMessageSX"] = errorDetected;
                    return View();
                }
                string token = GenerateRandomAlphanumericString(5);
                //      * Grabar en BD el "PreRegistro" con su Token
                //      * Enviar por correo registrado el Token
                Security security = new Security();
                string passEncrypt = security.Encrypt(oRegister.Password);
                RegistrationHS registrationHS = new RegistrationHS();
                var responseRegistration =
                    registrationHS.SendToken<UserStruct>(
                        oRegister.Company,
                        oRegister.FirstName,
                        oRegister.LastName,
                        oRegister.EMail,
                        passEncrypt,
                        2,
                        token
                        );
                if (responseRegistration.IsSuccess == true) {
                    Session["UserEMail"] = oRegister.EMail;
                    Session["UserToken"] = responseRegistration.RegistrationSt.Registration.Token;
                    //      * Mostrar Ventana "RegistrationToken" para confirmar el registro
                    return RedirectToAction("RegistrationToken", "Home");
                } else {
                    ViewData["ErrorMessageSX"] = $"The following was detected: {responseRegistration.ErrorMessage}";
                    return View();
                }
            }
            return View();
        }

        #endregion CompleteYourRegistration

        #region PrivacyNoticeEN

        public ActionResult PrivacyNoticeEN() {
            return View();
        }

        #endregion PrivacyNoticeEN

        #region PrivacyNoticeES

        public ActionResult PrivacyNoticeES() {
            return View();
        }

        #endregion PrivacyNoticeES
    }
}