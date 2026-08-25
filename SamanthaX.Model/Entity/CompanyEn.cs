using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Model.Entity {

    public class CompanyEn {

        #region properties

        public int CompanyId { get; set; }
        public int CompanyTypeId { get; set; }
        public string Tradename { get; set; }
        public string BusinessName { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FTR { get; set; }
        public string PRK { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string CrossingStreets { get; set; }
        public string Colony { get; set; }
        public string City { get; set; }
        public string Municipality { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string CellPhoneNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public DateTime DateRegistration { get; set; }
        public bool Enabled { get; set; }
        //
        public CompanyTypeEn CompanyType { get; set; }

        #endregion

        #region Constructor

        public CompanyEn() {
            CompanyId = 0;
            CompanyTypeId = 0;
            Tradename = String.Empty;
            BusinessName = String.Empty;
            Name = String.Empty;
            FirstName = String.Empty;
            LastName = String.Empty;
            FTR = String.Empty;
            PRK = String.Empty;
            Street = String.Empty;
            StreetNumber = String.Empty;
            CrossingStreets = String.Empty;
            Colony = String.Empty;
            City = String.Empty;
            Municipality = String.Empty;
            State = String.Empty;
            Country = String.Empty;
            PostalCode = String.Empty;
            CellPhoneNumber = String.Empty;
            PhoneNumber = String.Empty;
            EMail = String.Empty;
            DateRegistration = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            Enabled = false;
            //
            CompanyType = new CompanyTypeEn();
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.DataRow _dataRow) {
            CompanyId = (int)_dataRow["CompanyId"];
            CompanyTypeId = (int)_dataRow["CompanyTypeId"];
            Tradename = _dataRow["Tradename"].ToString();
            BusinessName = _dataRow["BusinessName"].ToString();
            Name = _dataRow["Name"].ToString();
            FirstName = _dataRow["FirstName"].ToString();
            LastName = _dataRow["LastName"].ToString();
            FTR = _dataRow["FTR"].ToString();
            PRK = _dataRow["PRK"].ToString();
            Street = _dataRow["Street"].ToString();
            StreetNumber = _dataRow["StreetNumber"].ToString();
            CrossingStreets = _dataRow["CrossingStreets"].ToString();
            Colony = _dataRow["Colony"].ToString();
            City = _dataRow["City"].ToString();
            Municipality = _dataRow["Municipality"].ToString();
            State = _dataRow["State"].ToString();
            Country = _dataRow["Country"].ToString();
            PostalCode = _dataRow["PostalCode"].ToString();
            CellPhoneNumber = _dataRow["CellPhoneNumber"].ToString();
            PhoneNumber = _dataRow["PhoneNumber"].ToString();
            EMail = _dataRow["EMail"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.SqlClient.SqlDataReader _dataRow) {
            CompanyId = (int)_dataRow["CompanyId"];
            CompanyTypeId = (int)_dataRow["CompanyTypeId"];
            Tradename = _dataRow["Tradename"].ToString();
            BusinessName = _dataRow["BusinessName"].ToString();
            Name = _dataRow["Name"].ToString();
            FirstName = _dataRow["FirstName"].ToString();
            LastName = _dataRow["LastName"].ToString();
            FTR = _dataRow["FTR"].ToString();
            PRK = _dataRow["PRK"].ToString();
            Street = _dataRow["Street"].ToString();
            StreetNumber = _dataRow["StreetNumber"].ToString();
            CrossingStreets = _dataRow["CrossingStreets"].ToString();
            Colony = _dataRow["Colony"].ToString();
            City = _dataRow["City"].ToString();
            Municipality = _dataRow["Municipality"].ToString();
            State = _dataRow["State"].ToString();
            Country = _dataRow["Country"].ToString();
            PostalCode = _dataRow["PostalCode"].ToString();
            CellPhoneNumber = _dataRow["CellPhoneNumber"].ToString();
            PhoneNumber = _dataRow["PhoneNumber"].ToString();
            EMail = _dataRow["EMail"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

    }
}
