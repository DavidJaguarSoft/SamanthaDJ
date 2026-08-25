using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SamanthaX.Model {

    public class StructResponse {

        #region Properties

        public bool StatusOk { get; set; }
        public string Message { get; set; }
        public int ItemsFound { get; set; }
        public string StackTrace { get; set; }
        //
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        #endregion Properties

        #region Constructor

        public StructResponse() {
            this.StatusOk = false;
            this.Message = String.Empty;
            this.ItemsFound = 0;
            this.StackTrace = String.Empty;
            //
            this.UserId = 0;
            this.CompanyId = 0;
            this.Username = string.Empty;
            this.Password = string.Empty;
            this.Token = string.Empty;
        }

        #endregion Constructor
    }
} 