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
    
    public class GrammarBuilderService {

        #region Constructors

        public GrammarBuilderService() {}

        #endregion Constructors

        public static (
            bool IsSuccess,
            GrammarBuilderStruct GrammarBuilderSt,
            string ErrorMessage
        ) Request<T>(string url, GrammarBuilderStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<GrammarBuilderStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private static(
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
    }
}
