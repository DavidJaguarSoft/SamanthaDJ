using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class UserTypeStruct : StructResponse {

        #region Properties

        public Entity.UserTypeEn UserType { get; set; }
        public List<Entity.UserTypeEn> UserTypeList { get; set; }

        #endregion

        #region Constructor

        public UserTypeStruct() {
            this.UserType = new Entity.UserTypeEn();
            this.UserTypeList = new List<Entity.UserTypeEn>();
        }

        #endregion
    }
}