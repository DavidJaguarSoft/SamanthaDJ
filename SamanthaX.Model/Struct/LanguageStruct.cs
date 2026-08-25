using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class LanguageStruct : StructResponse {

        #region Properties

        public Entity.LanguageEn Language { get; set; }
        public List<Entity.LanguageEn> LanguageList { get; set; }

        #endregion

        #region Constructor

        public LanguageStruct() {
            this.Language = new Entity.LanguageEn();
            this.LanguageList = new List<Entity.LanguageEn>();
        }

        #endregion
    }
}