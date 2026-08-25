using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Model.Entity {

    public class RegistrationEn {

        #region properties

        public int RegistrationId { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
        public int LanguageId { get; set; }
        public bool CompletedRegistration { get; set; }
        public DateTime DateRegistration { get; set; }

        #endregion

        #region Constructor

        public RegistrationEn() {

            RegistrationId = 0;
            EMail = String.Empty;
            Password = String.Empty;
            Token = String.Empty;
            CompanyId = 0;
            Company = String.Empty;
            FirstName = String.Empty;
            LastName = String.Empty;
            UserId = 0;
            LanguageId = 0;
            CompletedRegistration = false;
            DateRegistration = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.DataRow _dataRow) {
            RegistrationId = (int)_dataRow["RegistrationId"];
            EMail = _dataRow["EMail"].ToString();
            Password = _dataRow["Password"].ToString();
            Token = _dataRow["Token"].ToString();
            CompanyId = (int)_dataRow["CompanyId"];
            Company = _dataRow["Company"].ToString();
            FirstName = _dataRow["FirstName"].ToString();
            LastName = _dataRow["LastName"].ToString();
            UserId = (int)_dataRow["UserId"];
            LanguageId = (int)_dataRow["LanguageId"];
            CompletedRegistration = (bool)_dataRow["CompletedRegistration"];
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.SqlClient.SqlDataReader _dataRow) {
            RegistrationId = (int)_dataRow["RegistrationId"];
            EMail = _dataRow["EMail"].ToString();
            Password = _dataRow["Password"].ToString();
            Token = _dataRow["Token"].ToString();
            CompanyId = (int)_dataRow["CompanyId"];
            Company = _dataRow["Company"].ToString();
            FirstName = _dataRow["FirstName"].ToString();
            LastName = _dataRow["LastName"].ToString();
            UserId = (int)_dataRow["UserId"];
            LanguageId = (int)_dataRow["LanguageId"];
            CompletedRegistration = (bool)_dataRow["CompletedRegistration"];
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
        }

        #endregion

    }
}
