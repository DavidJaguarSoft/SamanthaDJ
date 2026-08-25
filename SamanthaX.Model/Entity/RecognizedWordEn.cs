using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Model.Entity {
    public class RecognizedWordEn {

        #region properties

        public int RecognizedWordId { get; set; }
        public int UserId { get; set; }
        public int LanguageId { get; set; }
        public string Code { get; set; }
        public int WordClassId { get; set; }
        public string WordClass { get; set; }
        public string RelatedWords { get; set; }
        public DateTime DateRegistration { get; set; }
        public bool Enabled { get; set; }

        #endregion

        #region Constructor

        public RecognizedWordEn() {
            RecognizedWordId = 0;
            UserId = 0;
            LanguageId = 0;
            Code = String.Empty;
            WordClassId = 0;
            WordClass = String.Empty;
            RelatedWords = String.Empty;
            DateRegistration = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            Enabled = false;
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.DataRow _dataRow) {
            RecognizedWordId = (int)_dataRow["RecognizedWordId"];
            UserId = (int)_dataRow["UserId"];
            LanguageId = (int)_dataRow["LanguageId"];
            Code = _dataRow["Code"].ToString();
            WordClassId = (int)_dataRow["WordClassId"];
            WordClass = _dataRow["WordClass"].ToString();
            RelatedWords = _dataRow["RelatedWords"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.SqlClient.SqlDataReader _dataRow) {
            RecognizedWordId = (int)_dataRow["RecognizedWordId"];
            UserId = (int)_dataRow["UserId"];
            LanguageId = (int)_dataRow["LanguageId"];
            Code = _dataRow["Code"].ToString();
            WordClassId = (int)_dataRow["WordClassId"];
            WordClass = _dataRow["WordClass"].ToString();
            RelatedWords = _dataRow["RelatedWords"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

    }
}
