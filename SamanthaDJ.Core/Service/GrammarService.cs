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
    
    public class GrammarService {

        #region Constructors

        public GrammarService() {}

        #endregion Constructors

        #region GetAllxUser

        public (
           bool IsSuccess,
           GrammarStruct GrammarSt,
           string ErrorMessage)
           GetAllxUser<T>(string username, string token) {
            GrammarStruct grammarSt = new GrammarStruct();
            grammarSt.Username = username;
            grammarSt.Token = token;
            Security security = new Security();
            return Request<GrammarStruct>(
                $"{security.Decrypt(Global.UrlAPI)}{APIs.GrammarGetAllxUser}",
                grammarSt
            );
        }

        #endregion GetAllxUser

        #region Private Methods

        private (
            bool IsSuccess,
            GrammarStruct GrammarSt,
            string ErrorMessage
        ) Request<T>(string url, GrammarStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<GrammarStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private (
            bool IsSuccess,
            GrammarStruct StructResponse,
            string ErrorMessage
        ) Common(
            HttpStatusCode pIsSuccess,
            string pResponseObject,
            string pResponseMessage
        ) {
            //
            bool isOk = false;
            GrammarStruct objectSt = new GrammarStruct();
            string error = "";
            if (pIsSuccess == HttpStatusCode.OK) {
                objectSt = Newtonsoft
                    .Json
                    .JsonConvert
                    .DeserializeObject<GrammarStruct>(pResponseObject);
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
