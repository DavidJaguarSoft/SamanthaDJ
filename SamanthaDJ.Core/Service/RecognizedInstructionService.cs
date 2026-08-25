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
    
    public class RecognizedInstructionService {

        #region Constructors

        public RecognizedInstructionService() {}

        #endregion Constructors

        #region GetAllxUser

        public (
           bool IsSuccess,
           RecognizedInstructionStruct RecognizedInstructionSt,
           string ErrorMessage)
           GetAllxUser<T>(string username, string token) {
            RecognizedInstructionStruct recognizedInstructionSt = new RecognizedInstructionStruct();
            recognizedInstructionSt.Username = username;
            recognizedInstructionSt.Token = token;
            Security security = new Security();
            return Request<RecognizedInstructionStruct>(
                $"{security.Decrypt(Global.UrlAPI)}{APIs.RecognizedInstructionGetAllxUser}",
                recognizedInstructionSt
            );
        }

        #endregion GetAllxUser

        #region Private Methods

        private (
            bool IsSuccess,
            RecognizedInstructionStruct RecognizedWordSt,
            string ErrorMessage
        ) Request<T>(string url, RecognizedInstructionStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<RecognizedInstructionStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private (
            bool IsSuccess,
            RecognizedInstructionStruct StructResponse,
            string ErrorMessage
        ) Common(
            HttpStatusCode pIsSuccess,
            string pResponseObject,
            string pResponseMessage
        ) {
            //
            bool isOk = false;
            RecognizedInstructionStruct objectSt = new RecognizedInstructionStruct();
            string error = "";
            if (pIsSuccess == HttpStatusCode.OK) {
                objectSt = Newtonsoft
                    .Json
                    .JsonConvert
                    .DeserializeObject<RecognizedInstructionStruct>(pResponseObject);
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
