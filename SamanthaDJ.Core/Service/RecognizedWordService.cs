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
    
    public class RecognizedWordService {

        #region Constructors

        public RecognizedWordService() {}

        #endregion Constructors

        #region GetAllxUser

        public (
           bool IsSuccess,
           RecognizedWordStruct RecognizedWordSt,
           string ErrorMessage)
           GetAllxUser<T>(string username, string token) {
            RecognizedWordStruct rwSt = new RecognizedWordStruct();
            rwSt.Username = username;
            rwSt.Token = token;
            Security security = new Security();
            return Request<RecognizedWordStruct>(
                $"{security.Decrypt(Global.UrlAPI)}{APIs.RecognizedWordGetAllxUser}",
                rwSt
            );
        }

        #endregion GetAllxUser

        #region Private Methods

        public static (
            bool IsSuccess,
            RecognizedWordStruct RecognizedWordSt,
            string ErrorMessage
        ) Request<T>(string url, RecognizedWordStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<RecognizedWordStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private static (
            bool IsSuccess,
            RecognizedWordStruct StructResponse,
            string ErrorMessage
        ) Common(
            HttpStatusCode pIsSuccess,
            string pResponseObject,
            string pResponseMessage
        ) {
            //
            bool isOk = false;
            RecognizedWordStruct objectSt = new RecognizedWordStruct();
            string error = "";
            if (pIsSuccess == HttpStatusCode.OK) {
                objectSt = Newtonsoft
                    .Json
                    .JsonConvert
                    .DeserializeObject<RecognizedWordStruct>(pResponseObject);
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
