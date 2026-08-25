using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class WordClassStruct : StructResponse {

        #region Properties

        public Entity.WordClassEn WordClass { get; set; }
        public List<Entity.WordClassEn> WordClassList { get; set; }
        public bool AllowRecognizedWord { get; set; }

        #endregion

        #region Constructor

        public WordClassStruct() {
            this.WordClass = new Entity.WordClassEn();
            this.WordClassList = new List<Entity.WordClassEn>();
            AllowRecognizedWord = false;
        }

        #endregion
    }
}