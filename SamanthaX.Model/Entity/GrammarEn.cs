using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Model.Entity {
    public class GrammarEn {

        #region properties

        public int GrammarId { get; set; }
        public int UserId { get; set; }
        public int LanguageId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateRegistration { get; set; }
        public bool Enabled { get; set; }
        //
        public List<GrammarBuilderEn> GrammarBuilderList { get; set; }

        #endregion

        #region Constructor

        public GrammarEn() {
            GrammarId = 0;
            UserId = 0;
            LanguageId = 0;
            Code = String.Empty;
            Name = String.Empty;
            Description = String.Empty;
            DateRegistration = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            Enabled = false;
            //
            GrammarBuilderList = new List<GrammarBuilderEn>();
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.DataRow _dataRow) {
            GrammarId = (int)_dataRow["GrammarId"];
            UserId = (int)_dataRow["UserId"];
            LanguageId = (int)_dataRow["LanguageId"];
            Code = _dataRow["Code"].ToString();
            Name = _dataRow["Name"].ToString();
            Description = _dataRow["Description"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.SqlClient.SqlDataReader _dataRow) {
            GrammarId = (int)_dataRow["GrammarId"];
            UserId = (int)_dataRow["UserId"];
            LanguageId = (int)_dataRow["LanguageId"];
            Code = _dataRow["Code"].ToString();
            Name = _dataRow["Name"].ToString();
            Description = _dataRow["Description"].ToString();
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion
    }
}
