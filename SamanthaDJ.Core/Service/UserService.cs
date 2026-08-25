using SamanthaX.Core.Utils;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Core.Service {
    
    public class UserService {

        #region Constructors

        public UserService() {}

        #endregion Constructors

        #region GetNamePassword

        public (
           bool IsSuccess,
           UserStruct UserSt,
           string ErrorMessage)
           GetNamePassword<T>(string username, string password) {
            UserStruct rwSt = new UserStruct();
            rwSt.Username = username;
            rwSt.Password = password;
            Security security = new Security();
            return Request<UserStruct>(
                $"{security.Decrypt(Global.UrlAPI)}{APIs.UserGetNamePassword}",
                rwSt
            );
        }

        #endregion GetNamePassword

        #region GetNameToken

        public (
           bool IsSuccess,
           UserStruct UserSt,
           string ErrorMessage)
           GetNameToken<T>(string username, string token) {
            UserStruct rwSt = new UserStruct();
            rwSt.Username = username;
            rwSt.Token = token;
            Security security = new Security();
            return Request<UserStruct>(
                $"{security.Decrypt(Global.UrlAPI)}{APIs.UserGetNameToken}",
                rwSt
            );
        }

        #endregion GetNameToken

        #region Private Methods

        public static (
            bool IsSuccess,
            UserStruct UserSt,
            string ErrorMessage
        ) Request<T>(string url, UserStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<UserStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private static (
            bool IsSuccess,
            UserStruct StructResponse,
            string ErrorMessage
        ) Common(
            HttpStatusCode pIsSuccess,
            string pResponseObject,
            string pResponseMessage
        ) {
            //
            bool isOk = false;
            UserStruct objectSt = new UserStruct();
            string error = "";
            if (pIsSuccess == HttpStatusCode.OK) {
                objectSt = Newtonsoft
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

        #endregion Private Methods
    }
}
