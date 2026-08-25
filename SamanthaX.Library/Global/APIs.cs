using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Library.Global {

    public class APIs {

        #region User

        public const string AuthenticateUser = "/api/User/GetNamePassword/";
        public const string RegisterUser = "/api/User/RegisterUser/";

        #endregion User

        #region Registration

        public const string RegistrationSendToken = "/api/Registration/SendToken/";
        public const string RegistrationCreateUser = "/api/Registration/CreateUser/";
        public const string RegistrationSendPasswordToEMail = "/api/Registration/SendPasswordToEMail/";
        public const string RegistrationGetEMail = "/api/Registration/GetEMail/";

        #endregion Registration

        #region Company

        public const string CompanyGetAll = "/api/Company/GetAll";
        public const string CompanyGetId = "/api/Company/GetId";
        public const string CompanySave = "/api/Company/Save";
        public const string CompanyDelete = "/api/Company/Delete";

        #endregion Company

        #region Samantha

        public const string WordClassGetEncrypt = "/api/WordClass/GetEncrypt";
        public const string WordClassGetId = "/api/WordClass/GetId";
        public const string WordClassGetAll = "/api/WordClass/GetAll";
        public const string WordClassSave = "/api/WordClass/Save";
        public const string WordClassEnable = "/api/WordClass/Enable";

        public const string RecognizedWordGetId = "/api/RecognizedWord/GetId";
        public const string RecognizedWordGetAll = "/api/RecognizedWord/GetAll";
        public const string RecognizedWordSave = "/api/RecognizedWord/Save";
        public const string RecognizedWordEnable = "/api/RecognizedWord/Enable";
        public const string RecognizedWordGetWordClass = "/api/RecognizedWord/GetWordClass";

        public const string GrammarBuilderGetId = "/api/GrammarBuilder/GetId";
        public const string GrammarBuilderGetGrammar = "/api/GrammarBuilder/GetGrammar";
        public const string GrammarBuilderSave = "/api/GrammarBuilder/Save";
        public const string GrammarBuilderEnable = "/api/GrammarBuilder/Enable";
        public const string GrammarBuilderDelete = "/api/GrammarBuilder/Delete";

        public const string GrammarGetId = "/api/Grammar/GetId";
        public const string GrammarGetAll = "/api/Grammar/GetAll";
        public const string GrammarSave = "/api/Grammar/Save";
        public const string GrammarEnable = "/api/Grammar/Enable";

        public const string RecognizedInstructionGetId = "/api/RecognizedInstruction/GetId";
        public const string RecognizedInstructionGetAll = "/api/RecognizedInstruction/GetAll";
        public const string RecognizedInstructionSave = "/api/RecognizedInstruction/Save";
        public const string RecognizedInstructionEnable = "/api/RecognizedInstruction/Enable";

        public const string SamanthaVoiceGetUser = "/api/SamanthaVoice/GetUser";
        public const string SamanthaVoiceSave = "/api/SamanthaVoice/Save";

        #endregion
    }
}
