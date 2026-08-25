using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class RecognizedWordStruct : StructResponse {

        #region Properties

        public Entity.RecognizedWordEn RecognizedWord { get; set; }
        public List<Entity.RecognizedWordEn> RecognizedWordList { get; set; }

        #endregion

        #region Constructor

        public RecognizedWordStruct() {
            this.RecognizedWord = new Entity.RecognizedWordEn();
            this.RecognizedWordList = new List<Entity.RecognizedWordEn>();
        }

        #endregion
    }
}