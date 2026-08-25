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
    public class CompanyTypeController : ApiController {

        #region "GetAll"

        [System.Web.Http.Route("api/CompanyType/GetAll")]
        [System.Web.Http.HttpPost]
        [BasicAuthentication]
        public IHttpActionResult GetAll() {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new CompanyTypeStruct();

            try {
                List<CompanyTypeEn> companyTypeList = Service.CompanyTypeService.GetAll();

                result.StatusOk = true;
                result.StackTrace = String.Empty;
                result.CompanyType = null;
                if(companyTypeList != null) {
                    result.Message = $"{companyTypeList.Count} Items found !";
                    result.ItemsFound = companyTypeList.Count;
                    result.CompanyTypeList = companyTypeList;
                } else {
                    result.Message = $"Items NO found !";
                    result.ItemsFound = 0;
                    result.CompanyTypeList = new List<CompanyTypeEn>();
                }
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.CompanyType = null;
                result.CompanyTypeList = null;
            }

            return Ok(result);
        }

        #endregion GetaA

        #region "GetId"

        [System.Web.Http.Route("api/CompanyType/GetId")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(CompanyTypeStruct))]
        [BasicAuthentication]
        public IHttpActionResult GetId(CompanyTypeStruct companyTypeSt) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new CompanyTypeStruct();

            try {
                CompanyTypeEn companyType = Service.CompanyTypeService.GetId(companyTypeSt.CompanyType.CompanyTypeId);

                result.StatusOk = true;
                result.Message = companyType == null ? "Item NO found" : "Item found !";
                result.ItemsFound = companyType == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.CompanyType = companyType;
                result.CompanyTypeList = null;
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.CompanyType = null;
                result.CompanyTypeList = null;
            }

            return Ok(result);
        }

        #endregion

        #region "Save"

        [System.Web.Http.Route("api/CompanyType/Save")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(CompanyTypeStruct))]
        [BasicAuthentication]
        public IHttpActionResult Save(CompanyTypeStruct companyTypeSt) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new CompanyTypeStruct();

            try {
                CompanyTypeEn companyType = Service.CompanyTypeService.Save(companyTypeSt.CompanyType);

                result.StatusOk = true;
                result.Message = companyType == null ? "Item could not have been saved !" : "Item saved !";
                result.ItemsFound = companyType == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.CompanyType = companyType;
                result.CompanyTypeList = null;
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.CompanyType = null;
                result.CompanyTypeList = null;
            }

            return Ok(result);
        }

        #endregion

        #region "Delete"

        [System.Web.Http.Route("api/CompanyType/Delete")]
        [System.Web.Http.HttpPost]
        [ResponseType(typeof(CompanyTypeStruct))]
        [BasicAuthentication]
        public IHttpActionResult Delete(CompanyTypeStruct companyTypeSt) {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var result = new CompanyTypeStruct();

            try {
                CompanyTypeEn companyType = Service.CompanyTypeService.Delete(companyTypeSt.CompanyType);
                //
                result.StatusOk = true;
                result.Message = companyType == null ? "item could not have been affected !" : "Item deleted !";
                result.ItemsFound = companyType == null ? 0 : 1;
                result.StackTrace = String.Empty;
                //
                result.CompanyType = companyType;
                result.CompanyTypeList = null;
                //
            } catch(Exception ex) {
                result.StatusOk = false;
                result.Message = ex.Message;
                result.ItemsFound = 0;
                result.StackTrace = ex.StackTrace;
                result.CompanyType = null;
                result.CompanyTypeList = null;
            }

            return Ok(result);
        }

        #endregion

    }
}