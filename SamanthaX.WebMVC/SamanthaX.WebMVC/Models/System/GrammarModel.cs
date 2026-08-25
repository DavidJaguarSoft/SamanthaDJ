using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.WebMVC.Models.System {
    
    public class GrammarModel {

        #region properties

        public int GrammarId { get; set; }

        [Required(ErrorMessage = "You must provide a Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "You must provide a Name")]
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime DateRegistration { get; set; }
        public bool Enabled { get; set; }
        public List<GrammarBuilderEn> GrammarBuilderList { get; set; }

        #endregion

        #region Constructors

        public GrammarModel() {
            GrammarId = 0;
            Code = String.Empty;
            Name = String.Empty;
            Description = String.Empty;
            DateRegistration = DateTime.MinValue;
            Enabled = false;
            GrammarBuilderList = new List<GrammarBuilderEn>();
        }

        #endregion Constructors
    }
}
