using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SamanthaX.Model.Entity {

    public class WordClassEn {

        #region properties

        public int WordClassId { get; set; }
        public int UserId {  get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Example { get; set; }
        public DateTime DateRegistration { get; set; }
        public bool Enabled { get; set; }

        public List<RecognizedWordEn> RecognizedWords { get; set; }

        #endregion

        #region Constructor

        public WordClassEn() {
            WordClassId = 0;
            UserId = 0;
            Code = String.Empty;
            Name = String.Empty;
            Description = String.Empty;
            Example = String.Empty;
            DateRegistration = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            Enabled = false;

            RecognizedWords = new List<RecognizedWordEn>();
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.DataRow _dataRow) {
            WordClassId = (int)_dataRow["WordClassId"];
            UserId = (int)_dataRow["UserId"];
            Code = _dataRow["Code"].ToString();
            Name = _dataRow["Name"].ToString();
            Description = _dataRow["Description"].ToString();
            Example = _dataRow["Example"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.SqlClient.SqlDataReader _dataRow) {
            WordClassId = (int)_dataRow["WordClassId"];
            UserId = (int)_dataRow["UserId"];
            Code = _dataRow["Code"].ToString();
            Name = _dataRow["Name"].ToString();
            Description = _dataRow["Description"].ToString();
            Example = _dataRow["Example"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

    }
}
