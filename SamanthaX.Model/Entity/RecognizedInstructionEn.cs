using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Model.Entity {

    public class RecognizedInstructionEn {

        #region properties

        public int RecognizedInstructionId { get; set; }
        public int UserId { get; set; }
        public int InstructionTypeId { get; set; }
        public string InstructionType { get; set; }
        public int LanguageId { get; set; }
        public int GrammarId { get; set; }
        public string Grammar { get; set; }
        public int ProjectId { get; set; }
        public string Project { get; set; }
        public string Code { get; set; }
        public string Instruction { get; set; }
        public string Description { get; set; }
        public double Confidence { get; set;  }
        public string VoiceProcessing { get; set; }
        public string VoiceEnding { get; set;  }
        public string VoiceSolution { get; set; }
        public string VoiceCancel { get; set; }
        public string VoiceFail { get; set; }
        public DateTime DateRegistration { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Enabled { get; set; }
        
        //  Virtual Properties
        public bool AnyWordNotFound { get; set; }
        public bool MarkedToAdd { get; set; }

        #endregion Properties

        #region Constructor

        public RecognizedInstructionEn() {

            RecognizedInstructionId = 0;
            UserId = 0;
            InstructionTypeId = 0;
            InstructionType = string.Empty;
            LanguageId = 0;
            GrammarId = 0;
            Grammar = string.Empty;
            Code = String.Empty;
            Instruction = String.Empty;
            Description = String.Empty;
            Confidence = Double.MinValue;
            VoiceProcessing = String.Empty;
            VoiceEnding = String.Empty;
            VoiceSolution = String.Empty;
            VoiceCancel = String.Empty;
            VoiceFail = String.Empty;
            DateRegistration = DateTime.MinValue;
            LastUpdate = DateTime.MinValue;
            Enabled = false;
            //
            AnyWordNotFound = false;
            MarkedToAdd = false;
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.DataRow _dataRow) {
            RecognizedInstructionId = (int)_dataRow["RecognizedInstructionId"];
            InstructionTypeId = (int)_dataRow["InstructionTypeId"];
            InstructionType = _dataRow["InstructionType"].ToString();
            UserId = (int)_dataRow["UserId"];
            LanguageId = (int)_dataRow["LanguageId"];
            GrammarId = (int)_dataRow["GrammarId"];
            Grammar = _dataRow["Grammar"].ToString();
            ProjectId = (int)_dataRow["ProjectId"];
            Project = _dataRow["Project"].ToString();
            Code = _dataRow["Code"].ToString();
            Instruction = _dataRow["Instruction"].ToString();
            Description = _dataRow["Description"].ToString();
            Confidence = Convert.ToDouble(_dataRow["Confidence"]);
            VoiceProcessing = _dataRow["VoiceProcessing"].ToString();
            VoiceSolution = _dataRow["VoiceSolution"].ToString();
            VoiceEnding = _dataRow["VoiceEnding"].ToString();
            VoiceCancel = _dataRow["VoiceCancel"].ToString();
            VoiceFail = _dataRow["VoiceFail"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            LastUpdate = (DateTime)_dataRow["LastUpdate"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.SqlClient.SqlDataReader _dataRow) {
            RecognizedInstructionId = (int)_dataRow["RecognizedInstructionId"];
            InstructionTypeId = (int)_dataRow["InstructionTypeId"];
            InstructionType = _dataRow["InstructionType"].ToString();
            UserId = (int)_dataRow["UserId"];
            LanguageId = (int)_dataRow["LanguageId"];
            GrammarId = (int)_dataRow["GrammarId"];
            Grammar = _dataRow["Grammar"].ToString();
            ProjectId = (int)_dataRow["ProjectId"];
            Project = _dataRow["Project"].ToString();
            Code = _dataRow["Code"].ToString();
            Instruction = _dataRow["Instruction"].ToString();
            Description = _dataRow["Description"].ToString();
            Confidence = Convert.ToDouble(_dataRow["Confidence"]);
            VoiceProcessing = _dataRow["VoiceProcessing"].ToString();
            VoiceSolution = _dataRow["VoiceSolution"].ToString();
            VoiceEnding = _dataRow["VoiceEnding"].ToString();
            VoiceCancel = _dataRow["VoiceCancel"].ToString();
            VoiceFail = _dataRow["VoiceFail"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            LastUpdate = (DateTime)_dataRow["LastUpdate"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

    }
}
