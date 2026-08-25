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
    
    public class CompanyHS {

        #region Constructors

        public CompanyHS() {}

        #endregion Constructors

        #region GetId

        public (
            bool IsSuccess,
            CompanyStruct CompanySt,
            string ErrorMessage)
            GetId<T>(int companyId) {
            CompanyStruct grammarSt = new CompanyStruct();
            grammarSt.Company.CompanyId = companyId;
            return Request<CompanyStruct>(
                $"{Variable.UrlApi}{APIs.CompanyGetId}",
                grammarSt
            );
        }

        #endregion GetId

        #region Save

        public (
           bool IsSuccess,
           CompanyStruct CompanySt,
           string ErrorMessage)
           Save<T>(CompanyEn oCompany) {
            CompanyStruct companySt = new CompanyStruct();
            companySt.Company = new CompanyEn();
            companySt.Company = oCompany;
            return Request<CompanyStruct>(
                $"{Variable.UrlApi}{APIs.CompanySave}",
                companySt
            );
        }

        #endregion Save

        #region Private Methods

        private (
            bool IsSuccess,
            CompanyStruct CompanySt,
            string ErrorMessage
        ) Request<T>(string url, CompanyStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<CompanyStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private (
            bool IsSuccess,
            CompanyStruct StructResponse,
            string ErrorMessage
        ) Common(
            HttpStatusCode pIsSuccess,
            string pResponseObject,
            string pResponseMessage
        ) {
            //
            bool isOk = false;
            CompanyStruct objectSt = new CompanyStruct();
            string error = "";
            if (pIsSuccess == HttpStatusCode.OK) {
                objectSt = Newtonsoft
                    .Json
                    .JsonConvert
                    .DeserializeObject<CompanyStruct>(pResponseObject);
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
