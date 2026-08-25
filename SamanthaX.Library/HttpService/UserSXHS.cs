using SamanthaX.Library.Global;
using SamanthaX.Model.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Library.HttpService {
    
    public class UserSXHS {

        public UserSXHS() {}

        public (
            bool IsSuccess,
            UserStruct UserSt,
            string ErrorMessage
        ) AuthenticateUser<T>(string pUserName, string pPassword) {
            UserStruct loginUser = new UserStruct();
            loginUser.Username = pUserName;
            loginUser.Password = pPassword;
            return Request<UserStruct>(
                $"{Variable.UrlApi}{APIs.AuthenticateUser}",
                loginUser
            );
        }

        private (
            bool IsSuccess,
            UserStruct UserSt,
            string ErrorMessage
        ) Request<T>(string url, UserStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<UserStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private (
            bool IsSuccess,
            UserStruct StructResponse,
            string ErrorMessage
        ) Common(
            HttpStatusCode pIsSuccess,
            string pResponseObject,
            string pResponseMessage
        ) {
            bool isOk = false;
            UserStruct objectSt = new UserStruct();
            string error = "";
            if (pIsSuccess == HttpStatusCode.OK) {
                objectSt = 
                    Newtonsoft
                    .Json
                    .JsonConvert
                    .DeserializeObject<UserStruct>(pResponseObject);

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
