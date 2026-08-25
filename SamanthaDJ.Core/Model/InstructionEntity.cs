using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Core.Model {

    public class InstructionResponse {
        public List<InstructionEntity> RecognizedInstructionList { get; set; }
        public List<InstructionEntity> UnrecognizedInstructionList { get; set; }

        public InstructionResponse() {
            RecognizedInstructionList = new List<InstructionEntity>();
            UnrecognizedInstructionList = new List<InstructionEntity>();
        }
    }

    public class InstructionEntity {
        public int InstructionId { get; set; }
        public int InstructionTypeId { get; set; }
        public string Grammar { get; set; }
        public string Sentence { get; set; }
        public string InstructionCode { get; set; }
        public string Instruction { get; set; }
        public string Description { get; set; }
        //public double Confidence { get; set; }
        public string VoiceProcessing { get; set; }
        public string VoiceSolution { get; set; }
        public string VoiceEnding { get; set; }
        public string VoiceCancel { get; set; }
        public string VoiceFail { get; set; }
        public bool InstructionFound { get; set; }

        public InstructionEntity() {
            InstructionId = 0;
            InstructionTypeId = 0;
            Grammar = string.Empty;
            Sentence = string.Empty;
            InstructionCode = string.Empty;
            Instruction = string.Empty;
            Description = string.Empty;
            //Confidence = 0;
            VoiceProcessing = string.Empty;
            VoiceSolution = string.Empty;
            VoiceEnding = string.Empty;
            VoiceCancel = string.Empty;
            VoiceFail = string.Empty;
            InstructionFound = false;
        }
    }
}
