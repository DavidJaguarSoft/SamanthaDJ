using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.WebMVC.Models.System {
    
    public class RecognizedInstructionModel {

        #region properties

        public int RecognizedInstructionId { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage = "You must select a Grammar")]
        public int GrammarId { get; set; }
        public string Grammar { get; set; }

        [Required(ErrorMessage = "You must provide a Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "You mus provide a Instruction")]
        public string Instruction { get; set; }

        public string Description { get; set; }
        public string VoiceProcessing { get; set; }
        public string VoiceSolution { get; set; }
        public string VoiceCancel { get; set; }
        public string VoiceFail { get; set; }
        public bool Enabled { get; set; }
        //
        public bool PanelGrammar { get; set; }
        public bool PanelInstruction { get; set; }
        public bool PanelData { get; set; }
        //
        public List<GrammarBuilderEn> GrammarBuilderList { get; set; }
        //
        public string ArmedInstruction { get; set; }

        #endregion Properties

        #region Constructor

        public RecognizedInstructionModel() {
            RecognizedInstructionId = 0;
            UserId = 0;
            GrammarId = 0;
            Grammar = string.Empty;
            Code = String.Empty;
            Instruction = String.Empty;
            Description = String.Empty;
            VoiceProcessing = String.Empty;
            VoiceSolution = String.Empty;
            VoiceCancel = String.Empty;
            VoiceFail = String.Empty;
            Enabled = false;
            //
            PanelGrammar = false;
            PanelInstruction = false;
            PanelData = false;
            //
            ArmedInstruction = string.Empty;
        }

        #endregion Constructor

        #region Methods

        public RecognizedInstructionModel CloneModelFromEntity(
            RecognizedInstructionEn oEntity
        ) {
            RecognizedInstructionModel oModel = new RecognizedInstructionModel();
            oModel.RecognizedInstructionId = oEntity.RecognizedInstructionId;
            oModel.UserId = oEntity.UserId;
            oModel.GrammarId = oEntity.GrammarId;
            oModel.Grammar = oEntity.Grammar;
            oModel.Code = oEntity.Code;
            oModel.Instruction = oEntity.Instruction;
            oModel.Description = oEntity.Description;
            oModel.VoiceProcessing = oEntity.VoiceProcessing;
            oModel.VoiceSolution = oEntity.VoiceSolution;
            oModel.VoiceCancel = oEntity.VoiceCancel;
            oModel.VoiceFail = oEntity.VoiceFail;
            oModel.Enabled = oEntity.Enabled;
            return oModel;
        }

        #endregion Methods
    }
}
