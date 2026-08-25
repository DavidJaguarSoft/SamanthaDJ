using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Model.Entity {

    public class LanguageEn {

        #region properties

        public int LanguageId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime DateRegistration { get; set; }
        public bool Enabled { get; set; }

        #endregion

        #region Constructor

        public LanguageEn() {
            LanguageId = 0;
            Code = String.Empty;
            Name = String.Empty;
            DateRegistration = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            Enabled = false;
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.DataRow _dataRow) {
            LanguageId = (int)_dataRow["LanguageId"];
            Code = _dataRow["Code"].ToString();
            Name = _dataRow["Name"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.SqlClient.SqlDataReader _dataRow) {
            LanguageId = (int)_dataRow["LanguageId"];
            Code = _dataRow["Code"].ToString();
            Name = _dataRow["Name"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion
    }
}
