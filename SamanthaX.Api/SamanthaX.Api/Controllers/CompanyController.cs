using SamanthaX.Api.Utils;
using SamanthaX.API.Utils;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace SamanthaX.API.Controllers {

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CompanyController : ApiController {

        #region GetId

        [System.Web.Http.Route("api/Company/GetId")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(CompanyStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetId(CompanyStruct companyEs) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new CompanyStruct();

            try {
                CompanyEn company = Service.CompanyService.GetId(companyEs.Company.CompanyId);

                result.StatusOk = true;
                if (company != null) {
                    result.Message = "Item found";
                    company.CompanyType =
                        Service
                        .CompanyTypeService.GetId(company.CompanyTypeId);
                    result.ItemsFound = 1;
                } else {
                    result.Message = "Item NO found !";
                    result.ItemsFound = 0;
                }
                result.Company = company;
                result.CompanyList = null;
                //
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.Company = null;
                result.CompanyList = null;
                Log.WriteToFile(
                    $"Company_{companyEs.Company.CompanyId.ToString("####")}",
                    "api/Company/GetId",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion GetId

        #region Save

        [System.Web.Http.Route("api/Company/Save")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(CompanyStruct))]
        [BasicAuthentication]
        public IHttpActionResult Save(CompanyStruct companyEs) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new CompanyStruct();

            try {
                CompanyEn company = Service.CompanyService.Save(companyEs.Company);

                result.StatusOk = true;
                if(company != null) {
                    result.Message = "Item saved!";
                    company.CompanyType =
                        Service
                        .CompanyTypeService.GetId(company.CompanyTypeId);
                    result.ItemsFound = 1;
                } else {
                    result.Message = "Item could not have been saved !";
                    result.ItemsFound = 0;
                }
                result.Company = company;
                result.CompanyList = null;
            } catch (Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.Company = null;
                result.CompanyList = null;
                Log.WriteToFile(
                    $"Company_{companyEs.Company.CompanyId.ToString("####")}",
                    "api/Company/Save",
                    $"Message: {ex.Message}.\nStackTrace: {ex.StackTrace}"
                );
            }

            return Ok(result);
        }

        #endregion Save
    }
}