using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class RegistrationStruct : StructResponse {

        #region Properties

        public Entity.RegistrationEn Registration { get; set; }
        public List<Entity.RegistrationEn> RegistrationList { get; set; }


        #endregion

        #region Constructor

        public RegistrationStruct() {
            this.Registration = new Entity.RegistrationEn();
            this.RegistrationList = new List<Entity.RegistrationEn>();
        }

        #endregion
    }
}