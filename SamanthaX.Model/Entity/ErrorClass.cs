using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Model.Entity {
    public class ErrorClass
    {

        #region Privates Attribs

        private String _Code;
        private String _Message;
        private String _Track;

        #endregion

        #region Constructors and Destructors

        public ErrorClass()
        {
            _Code = String.Empty;
            _Message = String.Empty;
            _Track = String.Empty;
        }

        #endregion

        #region Propierties

        public String Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public String Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public String Track
        {
            get { return _Track; }
            set { _Track = value; }
        }

        //  Propierties
        #endregion

        #region Public Methods


        //  Public Methods
        #endregion

    }
}
