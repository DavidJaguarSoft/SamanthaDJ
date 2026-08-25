using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.WebMVC.Models.Home {
    
    public  class LoginModel {

        [Required(ErrorMessage = "Your EMail is required")]
        public string User { get; set; }

        [Required(ErrorMessage = "You must provide a Password")]
        public string Password { get; set; }

        public LoginModel() {
            this.User = string.Empty;
            this.Password = string.Empty;
        }

        public LoginModel(string User, string Password) {
            this.User = User;
            this.Password = Password;
        }
    }
}
