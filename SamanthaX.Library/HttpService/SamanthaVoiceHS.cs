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
    
    public class SamanthaVoiceHS {

        #region Constructors

        public SamanthaVoiceHS() {}

        #endregion Constructors

        #region GetUser

        public (
            bool IsSuccess,
            SamanthaVoiceStruct SamanthaVoiceSt,
            string ErrorMessage)
            GetUser<T>(int userId) {
            SamanthaVoiceStruct svSt = new SamanthaVoiceStruct();
            svSt.SamanthaVoice.UserId = userId;
            svSt.SamanthaVoice.LanguageId = 2;
            return Request<SamanthaVoiceStruct>(
                $"{Variable.UrlApi}{APIs.SamanthaVoiceGetUser}",
                svSt
            );
        }

        #endregion GetUser

        #region Save

        public (
           bool IsSuccess,
           SamanthaVoiceStruct SamanthaVoiceSt,
           string ErrorMessage)
           Save<T>(SamanthaVoiceEn svEn) {
            SamanthaVoiceStruct svSt = new SamanthaVoiceStruct();
            svSt.SamanthaVoice = new SamanthaVoiceEn();
            svSt.SamanthaVoice = svEn;
            return Request<SamanthaVoiceStruct>(
                $"{Variable.UrlApi}{APIs.SamanthaVoiceSave}",
                svSt
            );
        }

        #endregion Save

        #region Private Methods

        private (
            bool IsSuccess,
            SamanthaVoiceStruct SamanthaVoiceSt,
            string ErrorMessage
        ) Request<T>(string url, SamanthaVoiceStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<SamanthaVoiceStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private (
            bool IsSuccess,
            SamanthaVoiceStruct StructResponse,
            string ErrorMessage
        ) Common(
            HttpStatusCode pIsSuccess,
            string pResponseObject,
            string pResponseMessage
        ) {
            //
            bool isOk = false;
            SamanthaVoiceStruct objectSt = new SamanthaVoiceStruct();
            string error = "";
            if (pIsSuccess == HttpStatusCode.OK) {
                objectSt = Newtonsoft
                    .Json
                    .JsonConvert
                    .DeserializeObject<SamanthaVoiceStruct>(pResponseObject);
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
