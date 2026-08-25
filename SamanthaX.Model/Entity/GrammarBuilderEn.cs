using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Model.Entity {
    public class GrammarBuilderEn {

        #region properties

        public int GrammarBuilderId { get; set; }
        public int GrammarId { get; set; }
        public int WordClassId { get; set; }
        public string WordClassCode { get; set; }
        public string WordClassName { get; set; }
        public int Sequence {  get; set; }
        public DateTime DateRegistration { get; set; }
        public bool Enabled { get; set; }
        //
        public List<RecognizedWordEn> RecognizedWordsList { get; set; }
        
        //  Virtual Properties
        public double sequenceDecimal { get; set; }
        public int RecognizedWordSelectedId { get; set; }
        public bool deleted { get; set; }

        #endregion

        #region Constructor

        public GrammarBuilderEn() {
            GrammarBuilderId = 0;
            GrammarId = 0;
            WordClassId = 0;
            WordClassCode = string.Empty;
            WordClassName = string.Empty;
            Sequence = 0;
            DateRegistration = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            Enabled = false;
            //
            RecognizedWordsList = new List<RecognizedWordEn>();
            //
            sequenceDecimal = 0.0;
            RecognizedWordSelectedId = 0;
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.DataRow _dataRow) {
            GrammarBuilderId = (int)_dataRow["GrammarBuilderId"];
            GrammarId = (int)_dataRow["GrammarId"];
            WordClassId = (int)_dataRow["WordClassId"];
            WordClassCode = _dataRow["WordClassCode"].ToString();
            WordClassName = _dataRow["WordClassName"].ToString();
            Sequence = (int)_dataRow["Sequence"];
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion

        #region extract_data

        public void ExtractData(System.Data.SqlClient.SqlDataReader _dataRow) {
            GrammarBuilderId = (int)_dataRow["GrammarBuilderId"];
            GrammarId = (int)_dataRow["GrammarId"];
            WordClassId = (int)_dataRow["WordClassId"];
            WordClassCode = _dataRow["WordClassCode"].ToString();
            WordClassName = _dataRow["WordClassName"].ToString();
            Sequence = (int)_dataRow["Sequence"];
            DateRegistration = (DateTime)_dataRow["DateRegistration"];
            Enabled = (bool)_dataRow["Enabled"];
        }

        #endregion
    }
}
