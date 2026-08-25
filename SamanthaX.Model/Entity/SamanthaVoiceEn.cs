using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Model.Entity {

    public class SamanthaVoiceEn {

        #region properties

        public int SamanthaVoiceId { get; set; }
        public int UserId { get; set; }
        public int LanguageId { get; set; }
        public string AIName { get; set; }
        public string OrderYou { get; set; }
        public string VoiceProcessingDefault { get; set; }
        public string VoiceSolutionDefault { get; set; }
        public string VoiceCancelDefault { get; set; }
        public string VoiceFailDefault { get; set; }
        public string AnExceptionOcurred { get; set; }
        public string WordsToIgnore { get; set; }
        public DateTime DateRegistration { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Enabled { get; set; }
        #endregion

        #region Constructor

        public SamanthaVoiceEn() {
            SamanthaVoiceId = 0;
            UserId = 0;
            LanguageId = 0;
            AIName = string.Empty;
            OrderYou = string.Empty;
            VoiceProcessingDefault = string.Empty;
            VoiceSolutionDefault = string.Empty;
            VoiceCancelDefault = string.Empty;
            VoiceFailDefault = string.Empty;
            AnExceptionOcurred = string.Empty;
            WordsToIgnore = string.Empty;
            DateRegistration = DateTime.MinValue;
            LastUpdate = DateTime.MinValue;
            Enabled = false;
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.DataRow _dataRow) {
            SamanthaVoiceId = (int)_dataRow["SamanthaVoiceId"];
            UserId = (int)_dataRow["UserId"];
            LanguageId = (int)_dataRow["LanguageId"];
            AIName = _dataRow["AIName"].ToString();
            OrderYou = _dataRow["OrderYou"].ToString();
            VoiceProcessingDefault = _dataRow["VoiceProcessingDefault"].ToString();
            VoiceSolutionDefault = _dataRow["VoiceSolutionDefault"].ToString();
            VoiceCancelDefault = _dataRow["VoiceCancelDefault"].ToString();
            VoiceFailDefault = _dataRow["VoiceFailDefault"].ToString();
            AnExceptionOcurred = _dataRow["AnExceptionOcurred"].ToString();
            WordsToIgnore = _dataRow["WordsToIgnore"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            LastUpdate = (DateTime)_dataRow["LastUpdate"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.SqlClient.SqlDataReader _dataRow) {
            SamanthaVoiceId = (int)_dataRow["SamanthaVoiceId"];
            UserId = (int)_dataRow["UserId"];
            LanguageId = (int)_dataRow["LanguageId"];
            AIName = _dataRow["AIName"].ToString();
            OrderYou = _dataRow["OrderYou"].ToString();
            VoiceProcessingDefault = _dataRow["VoiceProcessingDefault"].ToString();
            VoiceSolutionDefault = _dataRow["VoiceSolutionDefault"].ToString();
            VoiceCancelDefault = _dataRow["VoiceCancelDefault"].ToString();
            VoiceFailDefault = _dataRow["VoiceFailDefault"].ToString();
            AnExceptionOcurred = _dataRow["AnExceptionOcurred"].ToString();
            WordsToIgnore = _dataRow["WordsToIgnore"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            LastUpdate = (DateTime)_dataRow["LastUpdate"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

    }
}
