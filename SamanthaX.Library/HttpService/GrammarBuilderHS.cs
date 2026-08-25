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
    
    public class GrammarBuilderHS {

        #region Constructors

        public GrammarBuilderHS() {}

        #endregion Constructors

        #region GetId

        public (
            bool IsSuccess,
            GrammarBuilderStruct GrammarBuilderSt,
            string ErrorMessage)
            GetId<T>(int grammarBuilderId) {
            GrammarBuilderStruct grammarBuilderSt = new GrammarBuilderStruct();
            grammarBuilderSt.GrammarBuilder.GrammarBuilderId = grammarBuilderId;
            return Request<GrammarBuilderStruct>(
                $"{Variable.UrlApi}{APIs.GrammarBuilderGetId}",
                grammarBuilderSt
            );
        }

        #endregion GetId

        #region GetGrammar

        public (
           bool IsSuccess,
           GrammarBuilderStruct GrammarBuilderSt,
           string ErrorMessage)
        GetGrammar<T>(int userId, int grammarId) {
            GrammarBuilderStruct grammarBuilderSt = new GrammarBuilderStruct();
            grammarBuilderSt.GrammarBuilder.GrammarId = grammarId;
            grammarBuilderSt.UserId = userId;
            grammarBuilderSt.LoadRecognizedWord = true;
            return Request<GrammarBuilderStruct>(
                $"{Variable.UrlApi}{APIs.GrammarBuilderGetGrammar}",
                grammarBuilderSt
            );
        }

        #endregion GetGrammar

        #region Save

        public (
           bool IsSuccess,
           GrammarBuilderStruct GrammarBuilderSt,
           string ErrorMessage)
           Save<T>(
                int grammarBuilderId,
                int grammarId,
                int wordClassId,
                int sequence
            ) {
            GrammarBuilderStruct grammarBuilderSt = new GrammarBuilderStruct();
            grammarBuilderSt.GrammarBuilder.GrammarBuilderId = grammarBuilderId;
            grammarBuilderSt.GrammarBuilder.GrammarId = grammarId;
            grammarBuilderSt.GrammarBuilder.WordClassId = wordClassId;
            grammarBuilderSt.GrammarBuilder.Sequence = sequence;
            grammarBuilderSt.GrammarBuilder.DateRegistration = DateTime.Now;
            grammarBuilderSt.GrammarBuilder.Enabled = true;
            return Request<GrammarBuilderStruct>(
                $"{Variable.UrlApi}{APIs.GrammarBuilderSave}",
                grammarBuilderSt
            );
        }

        #endregion Save

        #region Enable

        public (
           bool IsSuccess,
           GrammarBuilderStruct GrammarBuilderSt,
           string ErrorMessage)
           Enable<T>(int grammarBuilderId, bool enabled) {
            GrammarBuilderStruct grammarBuilderSt = new GrammarBuilderStruct();
            grammarBuilderSt.GrammarBuilder.GrammarBuilderId = grammarBuilderId;
            grammarBuilderSt.GrammarBuilder.Enabled = enabled;
            return Request<GrammarBuilderStruct>(
                $"{Variable.UrlApi}{APIs.GrammarBuilderEnable}",
                grammarBuilderSt
            );
        }

        #endregion Enable

        #region Delete

        public (
           bool IsSuccess,
           GrammarBuilderStruct GrammarBuilderSt,
           string ErrorMessage)
           Delete<T>(int grammarBuilderId) {
            GrammarBuilderStruct grammarBuilderSt = new GrammarBuilderStruct();
            grammarBuilderSt.GrammarBuilder.GrammarBuilderId = grammarBuilderId;
            return Request<GrammarBuilderStruct>(
                $"{Variable.UrlApi}{APIs.GrammarBuilderDelete}",
                grammarBuilderSt
            );
        }

        #endregion Delete

        #region Private Methods

        private (
            bool IsSuccess,
            GrammarBuilderStruct GrammarBuilderSt,
            string ErrorMessage
        ) Request<T>(string url, GrammarBuilderStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<GrammarBuilderStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private (
            bool IsSuccess,
            GrammarBuilderStruct StructResponse,
            string ErrorMessage
        ) Common(
            HttpStatusCode pIsSuccess,
            string pResponseObject,
            string pResponseMessage
        ) {
            //
            bool isOk = false;
            GrammarBuilderStruct objectSt = new GrammarBuilderStruct();
            string error = "";
            if (pIsSuccess == HttpStatusCode.OK) {
                objectSt = Newtonsoft
                    .Json
                    .JsonConvert
                    .DeserializeObject<GrammarBuilderStruct>(pResponseObject);
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
