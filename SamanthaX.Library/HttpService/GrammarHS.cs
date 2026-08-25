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
    
    public class GrammarHS {

        #region Constructors

        public GrammarHS() {}

        #endregion Constructors

        #region GetId

        public (
            bool IsSuccess,
            GrammarStruct GrammarSt,
            string ErrorMessage)
            GetId<T>(int grammarId) {
            GrammarStruct grammarSt = new GrammarStruct();
            grammarSt.Grammar.GrammarId = grammarId;
            return Request<GrammarStruct>(
                $"{Variable.UrlApi}{APIs.GrammarGetId}",
                grammarSt
            );
        }

        #endregion GetId

        #region GetAll

        public (
           bool IsSuccess,
           GrammarStruct GrammarSt,
           string ErrorMessage)
           GetAll<T>(int userId) {
            GrammarStruct grammarSt = new GrammarStruct();
            grammarSt.Grammar.UserId = userId;
            return Request<GrammarStruct>(
                $"{Variable.UrlApi}{APIs.GrammarGetAll}",
                grammarSt
            );
        }

        #endregion GetAll

        #region Save

        public (
           bool IsSuccess,
           GrammarStruct GrammarSt,
           string ErrorMessage)
           Save<T>(GrammarEn oGrammar) {
            GrammarStruct grammarSt = new GrammarStruct();
            grammarSt.Grammar.GrammarId = oGrammar.GrammarId;
            grammarSt.Grammar.UserId = oGrammar.UserId;
            grammarSt.Grammar.LanguageId = oGrammar.LanguageId;
            grammarSt.Grammar.Code = oGrammar.Code;
            grammarSt.Grammar.Name = oGrammar.Name;
            grammarSt.Grammar.Description = oGrammar.Description;
            grammarSt.Grammar.DateRegistration = DateTime.Now;
            grammarSt.Grammar.Enabled = true;
            return Request<GrammarStruct>(
                $"{Variable.UrlApi}{APIs.GrammarSave}",
                grammarSt
            );
        }

        #endregion Save

        #region Enable

        public (
           bool IsSuccess,
           GrammarStruct GrammarSt,
           string ErrorMessage)
           Delete<T>(int grammarId, bool enabled) {
            GrammarStruct grammarSt = new GrammarStruct();
            grammarSt.Grammar.GrammarId = grammarId;
            grammarSt.Grammar.Enabled = enabled;
            return Request<GrammarStruct>(
                $"{Variable.UrlApi}{APIs.GrammarEnable}",
                grammarSt
            );
        }

        #endregion Enable

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
