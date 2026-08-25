using SamanthaX.Library.Global;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Library.HttpService {
    
    public class RecognizedWordHS {

        #region Constructors

        public RecognizedWordHS() {}

        #endregion Constructors

        #region GetId

        public (
            bool IsSuccess,
            RecognizedWordStruct RecognizedWordSt,
            string ErrorMessage)
            GetId<T>(int recognizedWordId) {
            RecognizedWordStruct recognizedWordSt = new RecognizedWordStruct();
            recognizedWordSt.RecognizedWord.RecognizedWordId = recognizedWordId;
            return Request<RecognizedWordStruct>(
                $"{Variable.UrlApi}{APIs.RecognizedWordGetId}",
                recognizedWordSt
            );
        }

        #endregion GetId

        #region GedAll

        public (
           bool IsSuccess,
           RecognizedWordStruct RecognizedWordSt,
           string ErrorMessage)
           GetAll<T>(int userId) {
            RecognizedWordStruct recognizedWordSt = new RecognizedWordStruct();
            recognizedWordSt.RecognizedWord.UserId = userId;
            return Request<RecognizedWordStruct>(
                $"{Variable.UrlApi}{APIs.RecognizedWordGetAll}",
                recognizedWordSt
            );
        }

        #endregion GedAll

        #region Save

        public (
           bool IsSuccess,
           RecognizedWordStruct RecognizedWordSt,
           string ErrorMessage)
           Save<T>(
                int recognizedWordId,
                int userId,
                int languageId,
                string code,
                int wordClassId,
                string relatedWords) {
            RecognizedWordStruct recognizedWordSt = new RecognizedWordStruct();
            recognizedWordSt.RecognizedWord.RecognizedWordId = recognizedWordId;
            recognizedWordSt.RecognizedWord.UserId = userId;
            recognizedWordSt.RecognizedWord.LanguageId = languageId;
            recognizedWordSt.RecognizedWord.Code = code;
            recognizedWordSt.RecognizedWord.WordClassId = wordClassId;
            recognizedWordSt.RecognizedWord.RelatedWords = relatedWords;
            recognizedWordSt.RecognizedWord.DateRegistration = DateTime.Now;
            recognizedWordSt.RecognizedWord.Enabled = true;
            return Request<RecognizedWordStruct>(
                $"{Variable.UrlApi}{APIs.RecognizedWordSave}",
                recognizedWordSt
            );
        }

        #endregion Save

        #region Enable

        public (
           bool IsSuccess,
           RecognizedWordStruct RecognizedWordSt,
           string ErrorMessage)
           Delete<T>(int recognizedWordId, bool enabled) {
            RecognizedWordStruct recognizedWordSt = new RecognizedWordStruct();
            recognizedWordSt.RecognizedWord.RecognizedWordId = recognizedWordId;
            recognizedWordSt.RecognizedWord.Enabled = enabled;
            return Request<RecognizedWordStruct>(
                $"{Variable.UrlApi}{APIs.RecognizedWordEnable}",
                recognizedWordSt
            );
        }

        #endregion Enable

        #region Private Methods

        private (
            bool IsSuccess,
            RecognizedWordStruct RecognizedWordSt,
            string ErrorMessage
        ) Request<T>(string url, RecognizedWordStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<RecognizedWordStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private (
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
