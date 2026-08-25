using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class CompanyStruct : StructResponse {

        #region Properties

        public Entity.CompanyEn Company { get; set; }
        public List<Entity.CompanyEn> CompanyList { get; set; }

        #endregion

        #region Constructor

        public CompanyStruct() {
            this.Company = new Entity.CompanyEn();
            this.CompanyList = new List<Entity.CompanyEn>();
        }

        #endregion
    }
}