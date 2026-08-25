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
    
    public class SamanthaVoiceService {

        #region Constructors

        public SamanthaVoiceService() {}

        #endregion Constructors

        #region GetAllxUser

        public (
            bool IsSuccess,
            SamanthaVoiceStruct SamanthaVoiceSt,
            string ErrorMessage)
            GetAllxUser<T>(string username, string token) {
            SamanthaVoiceStruct svSt = new SamanthaVoiceStruct();
            svSt.Username = username;
            svSt.Token = token;
            svSt.SamanthaVoice.LanguageId = 2;
            Security security = new Security();
            return Request<SamanthaVoiceStruct>(
                $"{security.Decrypt(Global.UrlAPI)}{APIs.SamanthaVoiceGetAllxUser}",
                svSt
            );
        }

        #endregion GetAllxUser

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
