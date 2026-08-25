using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Library.Function {

    public class HandleText {

        public static string StringHandle(object guest) {
            return guest == null? string.Empty : guest.ToString();
        }
    }
}
