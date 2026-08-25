using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaDJ.ServiceWPF.Models {

    public class BridgeResponse {

        /// <summary>
        ///     Types: "PING", "DATA", "TODO", "ERROR", "TEST",
        /// </summary>
        public string Type { get; set; }
        public string Info { get; set; }
        public string Detail { get; set; }
        public string IsSpeaker { get; set; }
        public string IsError { get; set; }

        public BridgeResponse() {
            Type = "DATA";
            Info = "";
            Detail = "";
            IsSpeaker = "false";
            IsError = "false";
        }
    }
}
