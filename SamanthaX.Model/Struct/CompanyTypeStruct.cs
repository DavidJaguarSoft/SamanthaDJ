using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class CompanyTypeStruct : StructResponse {

        #region Properties

        public Entity.CompanyTypeEn CompanyType { get; set; }
        public List<Entity.CompanyTypeEn> CompanyTypeList { get; set; }

        #endregion

        #region Constructor

        public CompanyTypeStruct() {
            this.CompanyType = new Entity.CompanyTypeEn();
            this.CompanyTypeList = new List<Entity.CompanyTypeEn>();
        }

        #endregion
    }
}