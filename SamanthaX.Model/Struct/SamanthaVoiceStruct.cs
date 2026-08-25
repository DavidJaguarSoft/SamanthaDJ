using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model.Struct {

    public class SamanthaVoiceStruct : StructResponse {

        #region Properties

        public Entity.SamanthaVoiceEn SamanthaVoice { get; set; }
        public List<Entity.SamanthaVoiceEn> SamanthaVoiceList { get; set; }

        #endregion

        #region Constructor

        public SamanthaVoiceStruct() {
            this.SamanthaVoice = new Entity.SamanthaVoiceEn();
            this.SamanthaVoiceList = new List<Entity.SamanthaVoiceEn>();
        }

        #endregion
    }
}