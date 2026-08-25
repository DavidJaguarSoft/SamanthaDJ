using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.WebMVC.Models.Home {
    
    public class RegistrationTokenModel {

        [Required(ErrorMessage = "Your EMail is required")]
        [EmailAddress(ErrorMessage = "The EMail is not is a valid Format")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "You must provide a Token that was sent to your EMail")]
        public string Token { get; set; }

        public RegistrationTokenModel() {
            this.EMail = string.Empty;
            this.Token = string.Empty;
        }

    }
}
