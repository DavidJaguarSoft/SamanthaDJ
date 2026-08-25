using SamanthaX.Library.Global;
using SamanthaX.Model.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Library.HttpService {
    
    public class RegistrationHS {

        public RegistrationHS() {}

        public (
            bool IsSuccess,
            RegistrationStruct RegistrationSt,
            string ErrorMessage)
            GetEMail<T>(string pEMail) {
            RegistrationStruct registrationSt = new RegistrationStruct();
            registrationSt.Registration.EMail = pEMail;
            return Request<RegistrationStruct>(
                $"{Variable.UrlApi}{APIs.RegistrationGetEMail}",
                registrationSt
            );
        }

        public (
           bool IsSuccess,
           RegistrationStruct RegistrationSt,
           string ErrorMessage)
           SendToken<T>(
                string company,
                string firstName,
                string lastName,
                string email,
                string password,
                int languageId,
                string token) {
            RegistrationStruct registrationSt = new RegistrationStruct();
            registrationSt.Registration.Company = company;
            registrationSt.Registration.FirstName = firstName;
            registrationSt.Registration.LastName = lastName;
            registrationSt.Registration.EMail = email;
            registrationSt.Registration.Password = password;
            registrationSt.Registration.LanguageId = languageId;
            registrationSt.Registration.Token = token;
            return Request<RegistrationStruct>(
                $"{Variable.UrlApi}{APIs.RegistrationSendToken}",
                registrationSt
            );
        }

        public (
           bool IsSuccess,
           RegistrationStruct RegistrationSt,
           string ErrorMessage)
           CreateUser<T>(
                int registrationId,
                string company,
                string firstName,
                string lastName,
                string email,
                string password,
                int languageId) {
            RegistrationStruct registrationSt = new RegistrationStruct();
            registrationSt.Registration.RegistrationId = registrationId;
            registrationSt.Registration.Company = company;
            registrationSt.Registration.FirstName = firstName;
            registrationSt.Registration.LastName = lastName;
            registrationSt.Registration.EMail = email;
            registrationSt.Registration.Password = password;
            registrationSt.Registration.LanguageId = languageId;
            return Request<RegistrationStruct>(
                $"{Variable.UrlApi}{APIs.RegistrationCreateUser}",
                registrationSt
            );
        }

        public (
            bool IsSuccess,
            RegistrationStruct RegistrationSt,
            string ErrorMessage)
            SendPasswordToEMail<T>(string pEMail) {
            RegistrationStruct registrationSt = new RegistrationStruct();
            registrationSt.Registration.EMail = pEMail;
            return Request<RegistrationStruct>(
                $"{Variable.UrlApi}{APIs.RegistrationSendPasswordToEMail}",
                registrationSt
            );
        }

        private (
            bool IsSuccess,
            RegistrationStruct UserSt,
            string ErrorMessage
        ) Request<T>(string url, RegistrationStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<RegistrationStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private (
            bool IsSuccess,
            RegistrationStruct StructResponse,
            string ErrorMessage
        ) Common(
            HttpStatusCode pIsSuccess,
            string pResponseObject,
            string pResponseMessage
        ) {
            //
            bool isOk = false;
            RegistrationStruct objectSt = new RegistrationStruct();
            string error = "";
            if (pIsSuccess == HttpStatusCode.OK) {
                objectSt =
                    Newtonsoft
                    .Json
                    .JsonConvert
                    .DeserializeObject<RegistrationStruct>(pResponseObject);
                if (objectSt.StatusOk)
                    isOk = true;
                else
                    error = objectSt.Message;
            } else {
                error = pResponseMessage;
            }
            return (isOk, objectSt, error);
        }
    }
}
