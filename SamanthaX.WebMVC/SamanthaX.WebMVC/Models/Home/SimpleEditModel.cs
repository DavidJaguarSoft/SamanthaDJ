using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.WebMVC.Models.Home {
    
    public  class SimpleEditModel {

        [Required]
        public string textInput { get; set; }

        public SimpleEditModel() {
            this.textInput = string.Empty;
        }

        public SimpleEditModel(string User, string Password) {
            this.textInput = User;
        }
    }
}
