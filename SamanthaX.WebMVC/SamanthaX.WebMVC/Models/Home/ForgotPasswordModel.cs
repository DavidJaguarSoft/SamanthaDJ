using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.WebMVC.Models.Home {
    
    public class ForgotPasswordModel {

        [Required(ErrorMessage = "Your EMail is required")]
        [EmailAddress(ErrorMessage = "The EMail is not is a valid Format")]
        public string EMail { get; set; }
    }
}
