using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.WebMVC.Models.Home {
    
    public class RegistrationModel : ValidationAttribute {

        public string Company { get; set; }

        [Required(ErrorMessage = "You must provide your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must provide your Second Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "EMail is required")]
        [EmailAddress(ErrorMessage = "The EMail is not in a valid format")]
        public string EMail { get; set; }

        [Required(ErrorMessage = "You must provide a Password of at least 8 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must confirm your Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Terms and Conditions")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must mark the checkbox after you have read the terms and conditions")]
        public bool IReadTerms { get; set; }

        public RegistrationModel() {
            this.Company = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.EMail = string.Empty;
            this.Password = string.Empty;
            this.ConfirmPassword = string.Empty;
            this.IReadTerms = false;
        }

    }
}
