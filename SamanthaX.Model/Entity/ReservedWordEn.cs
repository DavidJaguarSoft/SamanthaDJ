using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Model.Entity {

    public class ReservedWordEn {
        #region properties

        public int ReservedWordId { get; set; }
        public int LanguageId { get; set; }
        public string SamanthaName { get; set; }
        public string ExecuteRightNow { get; set; }
        public string CancelCommand { get; set; }
        public string IgnoredWords { get; set; }
        public DateTime DateRegistration { get; set; }
        public bool Enabled { get; set; }

        #endregion

        #region Constructor

        public ReservedWordEn() {
            ReservedWordId = 0;
            LanguageId = 0;
            SamanthaName = String.Empty;
            ExecuteRightNow = String.Empty;
            CancelCommand = String.Empty;
            IgnoredWords = String.Empty;
            DateRegistration = System.DateTime.MinValue;
            Enabled = false;
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.DataRow _dataRow) {
            ReservedWordId = Convert.ToInt32(_dataRow["ReservedWordId"]);
            LanguageId = Convert.ToInt32(_dataRow["LanguageId"]);
            SamanthaName = _dataRow["SamanthaName"].ToString();
            ExecuteRightNow = _dataRow["ExecuteRightNow"].ToString();
            CancelCommand = _dataRow["CancelCommand"].ToString();
            IgnoredWords = _dataRow["IgnoredWords"].ToString();
            DateRegistration = Convert.ToDateTime(_dataRow["DateRegistration"].ToString());
            Enabled = Convert.ToBoolean(_dataRow["Enabled"].ToString());
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.SqlClient.SqlDataReader _dataRow) {
            ReservedWordId = Convert.ToInt32(_dataRow["ReservedWordId"]);
            LanguageId = Convert.ToInt32(_dataRow["LanguageId"]);
            SamanthaName = _dataRow["SamanthaName"].ToString();
            ExecuteRightNow = _dataRow["ExecuteRightNow"].ToString();
            CancelCommand = _dataRow["CancelCommand"].ToString();
            IgnoredWords = _dataRow["IgnoredWords"].ToString();
            DateRegistration = Convert.ToDateTime(_dataRow["DateRegistration"].ToString());
            Enabled = Convert.ToBoolean(_dataRow["Enabled"].ToString());
        }

        #endregion

    }
}
