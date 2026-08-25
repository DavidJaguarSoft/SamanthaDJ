using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class GrammarBuilderStruct : StructResponse {

        #region Properties

        public Entity.GrammarBuilderEn GrammarBuilder { get; set; }
        public List<Entity.GrammarBuilderEn> GrammarBuilderList { get; set; }
        public bool LoadRecognizedWord { get; set; }

        #endregion

        #region Constructor

        public GrammarBuilderStruct() {
            this.GrammarBuilder = new Entity.GrammarBuilderEn();
            this.GrammarBuilderList = new List<Entity.GrammarBuilderEn>();
            LoadRecognizedWord = false;
        }

        #endregion
    }
}