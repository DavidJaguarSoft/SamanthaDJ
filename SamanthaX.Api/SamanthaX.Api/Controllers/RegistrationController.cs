using SamanthaX.Api.Utils;
using SamanthaX.API.Utils;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.WebPages;

namespace SamanthaX.API.Controllers {

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RegistrationController : ApiController {

        #region GetEMail

        /// <summary>
        ///     
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [System.Web.Http.Route("api/Registration/GetEMail")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RegistrationStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetEMail(RegistrationStruct register) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new RegistrationStruct();

            try {
                //  1- Validamos que el corre no este previamente registrado
                RegistrationEn existingRecord =
                    Service.RegistrationService.GetEMail(register.Registration.EMail);

                if (existingRecord == null) {
                    result.StatusOk = false;
                    result.Message = "No se encontro el Correo";
                    result.ItemsFound = 0;
                    result.Registration = null;
                } else {
                    result.StatusOk = true;
                    result.Message = "Correo encontrado !";
                    result.ItemsFound = 1;
                    result.Registration = existingRecord;
                }
                result.RegistrationList = null;
                //
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.Registration = null;
                result.RegistrationList = null;
                Log.WriteToFile(
                    $"Registration_{register.Registration.EMail}",
                    "api/Registration/GetEMail",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetEMail

        #region SendToken

        /// <summary>
        ///     1- Envia Tokey y Contraseña al correo registrado para iniciar el tramite
        ///     de Registro del cliente
        ///     2- Crea el registro de inicio (sin concluir. *CompletedRegistration* = false
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [System.Web.Http.Route("api/Registration/SendToken")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RegistrationStruct))]
        public IHttpActionResult SendToken(RegistrationStruct register) {

            var result = new RegistrationStruct();

            var fromAddress = new MailAddress("Registration@DavidJaguarSoft.com", "Samantha DJ");
            var toAddress = new MailAddress(register.Registration.EMail, "Samantha DJ. New User");
            const string fromPassword = "2e78St~0p";
            const string subject = "Samantha DJ. Registering a new User";
            string body = $"Your Token is: {register.Registration.Token}\n";
            body += $"Your Password is:  {register.Registration.Password}\n";

            var smtp = new SmtpClient {
                Host = "mail.DavidJaguarSoft.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                
                Credentials = new NetworkCredential("Registration@DavidJaguarSoft.com", fromPassword)
            };

            try {
                //  1- Validamos que el corre no este previamente registrado
                //RegistrationEn existingRecord =
                //    Service.RegistrationService.GetEMail(register.Registration.EMail);
                
                //if(existingRecord == null) {
                    //  2- Registrar el intento de registro
                try {
                    //using (var message = new MailMessage(fromAddress, toAddress) {
                    //    Subject = subject,
                    //    Body = body
                    //}) {
                    //    smtp.Send(message);
                    //}
                    //  3- Crear el registro del usuario
                    RegistrationEn registerSaved =
                        Service.RegistrationService.Save(register.Registration);
                    //
                    result.StatusOk = true;
                    result.Message = "Token enviado";
                    result.Registration = registerSaved;
                    result.RegistrationList = null;
                    //
                } catch (SmtpException ex) {
                    string msg = "Mail cannot be sent because of the server problem:";
                    msg += ex.Message;
                    //throw new Exception(msg);
                    result.StatusOk = false;
                    result.Message = msg;
                    result.ItemsFound = 0;
                    result.StackTrace = ex.StackTrace;
                    result.Registration = null;
                    result.RegistrationList = null;
                }
                //} else {
                //    result.StatusOk = false;
                //    result.Message = "Es correo ya esta en uso !";
                //    result.ItemsFound = 0;
                //    result.Registration = null;
                //    result.RegistrationList = null;
                //}
                //
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.Registration = null;
                result.RegistrationList = null;
                Log.WriteToFile(
                    $"Registration_{register.Registration.EMail}",
                    "api/Registration/SendToken",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion SendToken

        #region CreateUser

        /// <summary>
        ///     1- Envia correo al usuario para Felicitarlo de su registro
        ///     2- Marca su registro como concluido. *CompletedRegistration* = true
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [System.Web.Http.Route("api/Registration/CreateUser")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RegistrationStruct))]
        [BasicAuthentication]
        public IHttpActionResult CreateUser(RegistrationStruct register) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new RegistrationStruct();
            string userName = register.Registration.Company.Length > 0 
                ? register.Registration.Company
                : register.Registration.FirstName.Length > 0 
                    ? register.Registration.FirstName
                    : "Usuario SamanthaX";

            var fromAddress = new MailAddress("hiumanlab.DavidJaguar@gmail.com", "Supernova X");
            var toAddress = new MailAddress(register.Registration.EMail, userName);
            const string fromPassword = "ubst syps fwol ards";
            string subject = $"Gracias por su Usar Supernova X {userName}";
            string body = $"Su registro se ha completado\n";
            body += $"\n";

            var smtp = new SmtpClient {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("hiumanlab.DavidJaguar@gmail.com", fromPassword)
            };

            try {
                try {
                    //using(var message = new MailMessage(fromAddress, toAddress) {
                    //    Subject = subject,
                    //    Body = body
                    //}) {
                    //    smtp.Send(message);
                    //}
                    //  3- Crear el registro del usuario
                    RegistrationEn newUser =
                        Service.RegistrationService.CreateUser(register.Registration);
                    //
                    result.StatusOk = true;
                    result.Message = "Se registró exitosamente";
                    result.Registration = newUser;
                    result.RegistrationList = null;
                    //
                } catch(SmtpException ex) {
                    string msg = "Mail cannot be sent because of the server problem:";
                    msg += ex.Message;
                    //throw new Exception(msg);
                    result.StatusOk = false;
                    result.Message = msg;
                    result.ItemsFound = 0;
                    result.StackTrace = ex.StackTrace;
                    result.Registration = null;
                    result.RegistrationList = null;
                }
                //
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.Registration = null;
                result.RegistrationList = null;
                Log.WriteToFile(
                    $"Registration_{register.Registration.EMail}",
                    "api/Registration/CreateUser",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion CreateUser

        #region SendPasswordToEMail

        /// <summary>
        ///     1- Envia correo al usuario para Felicitarlo de su registro
        ///     2- Marca su registro como concluido. *CompletedRegistration* = true
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [System.Web.Http.Route("api/Registration/SendPasswordToEMail")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(RegistrationStruct))]
        [BasicAuthentication]
        public IHttpActionResult SendPasswordToEMail(RegistrationStruct register) {
            var result = new RegistrationStruct();

            var fromAddress = new MailAddress("hiumanlab.DavidJaguar@gmail.com", "Supernova X");
            var toAddress = new MailAddress(register.Registration.EMail, "Nuevo Usuario Supernova");
            const string fromPassword = "Cibercoppell#33";
            const string subject = "Envio de Token para su registro";
            string body = $"Your Token is: {register.Registration.Token}\n";
            body += $"Your Password is:  {register.Registration.Password}\n";

            var smtp = new SmtpClient {
                Host = "smtp-mail.outlook.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential("javier_balam@hotmail.com", fromPassword)
            };

            try {
                RegistrationEn existingRecord =
                    Service.RegistrationService.GetEMail(register.Registration.EMail);

                if (existingRecord != null) {
                    try {
                        //using (var message = new MailMessage(fromAddress, toAddress) {
                        //    Subject = subject,
                        //    Body = body
                        //}) {
                        //    smtp.Send(message);
                        //}
                        //
                        result.StatusOk = true;
                        result.Message = "Password Enviado";
                        result.Registration = existingRecord;
                        result.RegistrationList = null;
                        //
                    } catch (SmtpException ex) {
                        string msg = "Mail cannot be sent because of the server problem:";
                        msg += ex.Message;
                        result.StatusOk = false;
                        result.Message = msg;
                        result.ItemsFound = 0;
                        result.StackTrace = ex.StackTrace;
                        result.Registration = null;
                        result.RegistrationList = null;
                    }
                } else {
                    result.StatusOk = false;
                    result.Message = "No se encontró ningún Registro de este Correo !";
                    result.ItemsFound = 0;
                    result.Registration = null;
                    result.RegistrationList = null;
                }
                //
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.Registration = null;
                result.RegistrationList = null;
                Log.WriteToFile(
                    $"Registration_{register.Registration.EMail}",
                    "api/Registration/SendPasswordToEMail",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion SendPasswordToEMail
    }
}