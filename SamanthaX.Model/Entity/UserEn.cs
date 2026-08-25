using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Model.Entity {

    public class UserEn {

        #region properties

        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int UserTypeId { get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime DateRegistration { get; set; }
        public bool Enabled { get; set; }
        //
        public CompanyEn Company { get; set; }
        public UserTypeEn UserType { get; set; }
        public Entity.LanguageEn Language { get; set; }

        #endregion

        #region Constructor

        public UserEn() {
            UserId = 0;
            CompanyId = 0;
            UserTypeId = 0;
            LanguageId = 0;
            Name = String.Empty;
            Password = String.Empty;
            Token = string.Empty;
            DateRegistration = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            Enabled = false;
            //
            Company = new CompanyEn();
            UserType = new UserTypeEn();
            Language = new LanguageEn();
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.DataRow _dataRow) {
            UserId = (int)_dataRow["UserId"];
            CompanyId = (int)_dataRow["CompanyId"];
            UserTypeId = (int)_dataRow["UserTypeId"];
            LanguageId = (int)_dataRow["LanguageId"];
            Name = _dataRow["Name"].ToString();
            Password = _dataRow["Password"].ToString();
            Token = _dataRow["Token"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.SqlClient.SqlDataReader _dataRow) {
            UserId = (int)_dataRow["UserId"];
            CompanyId = (int)_dataRow["CompanyId"];
            UserTypeId = (int)_dataRow["UserTypeId"];
            LanguageId = (int)_dataRow["LanguageId"];
            Name = _dataRow["Name"].ToString();
            Password = _dataRow["Password"].ToString();
            Token = _dataRow["Token"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

    }
}
