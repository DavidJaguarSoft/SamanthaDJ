using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.WebMVC.Models.System {
    
    public class RecognizedWordModel {

        #region properties

        public int RecognizedWordId { get; set; }

        [Required(ErrorMessage = "You must provide a Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "You must provide a Word Class")]
        public int WordClassId { get; set; }

        public string WordClass { get; set; }

        [Required(ErrorMessage = "You must provide Related Words separated by commas")]
        public string RelatedWords { get; set; }

        public DateTime DateRegistration { get; set; }
        public bool Enabled { get; set; }

        #endregion

        #region Constructors

        public RecognizedWordModel() {
            WordClassId = 0;
            Code = String.Empty;
            WordClassId = 0;
            WordClass = String.Empty;
            RelatedWords = String.Empty;
            DateRegistration = DateTime.MinValue;
            Enabled = false;
        }

        #endregion Constructors
    }
}
