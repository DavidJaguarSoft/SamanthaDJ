using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaDJ.ServiceWPF.Tools {

    public enum ClientRequest {
        PING = 0,
        DATA = 1,   // Relevant information and send to speaker
        INFO = 2,   // Supplementary information for the log. Do not send to speaker
        TEST = 3,
        ERROR = 100,  // Relevant information about an error
    }
}
