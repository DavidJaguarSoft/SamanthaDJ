using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class GrammarStruct : StructResponse {

        #region Properties

        public Entity.GrammarEn Grammar { get; set; }
        public List<Entity.GrammarEn> GrammarList { get; set; }
        

        #endregion

        #region Constructor

        public GrammarStruct() {
            this.Grammar = new Entity.GrammarEn();
            this.GrammarList = new List<Entity.GrammarEn>();
            
        }

        #endregion
    }
}