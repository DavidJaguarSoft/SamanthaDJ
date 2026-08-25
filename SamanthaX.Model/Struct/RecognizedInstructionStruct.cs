using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class RecognizedInstructionStruct : StructResponse {

        #region Properties

        public Entity.RecognizedInstructionEn RecognizeInstruction { get; set; }
        public List<Entity.RecognizedInstructionEn> RecognizeInstructionList { get; set; }

        #endregion

        #region Constructor

        public RecognizedInstructionStruct() {
            this.RecognizeInstruction = new Entity.RecognizedInstructionEn();
            this.RecognizeInstructionList = new List<Entity.RecognizedInstructionEn>();
        }

        #endregion
    }
}