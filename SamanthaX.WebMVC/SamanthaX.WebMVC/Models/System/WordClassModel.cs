using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.WebMVC.Models.System {
    
    public class WordClassModel {

        #region properties

        public int WordClassId { get; set; }

        [Required(ErrorMessage = "You must provide a Code")]
        public string Code { get; set; }

        [Required(ErrorMessage = "You must provide a name for the Word Class")]
        public string Name { get; set; }

        public string Description { get; set; }
        public string Example { get; set; }
        public DateTime DateRegistration { get; set; }
        public bool Enabled { get; set; }

        #endregion

        #region Constructors

        public WordClassModel() {
            WordClassId = 0;
            Code = String.Empty;
            Name = String.Empty;
            Description = String.Empty;
            Example = String.Empty;
            DateRegistration = DateTime.MinValue;
            Enabled = false;
        }

        #endregion Constructors
    }
}
