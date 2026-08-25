using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class UserStruct : StructResponse {

        #region Properties

        public Entity.UserEn User { get; set; }
        public List<Entity.UserEn> UserList { get; set; }

        #endregion

        #region Constructor

        public UserStruct() {
            this.User = new Entity.UserEn();
            this.UserList = new List<Entity.UserEn>();
        }

        #endregion
    }
}