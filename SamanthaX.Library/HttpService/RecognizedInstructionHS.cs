using SamanthaX.Library.Global;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Library.HttpService {
    
    public class RecognizedInstructionHS {

        #region Constructors

        public RecognizedInstructionHS() {}

        #endregion Constructors

        #region GetId

        public (
            bool IsSuccess,
            RecognizedInstructionStruct RecognizedInstructionSt,
            string ErrorMessage)
            GetId<T>(int recognizedInstructionId) {
            RecognizedInstructionStruct recognizedInstructionSt = new RecognizedInstructionStruct();
            recognizedInstructionSt.RecognizeInstruction.RecognizedInstructionId = recognizedInstructionId;
            return Request<RecognizedInstructionStruct>(
                $"{Variable.UrlApi}{APIs.RecognizedInstructionGetId}",
                recognizedInstructionSt
            );
        }

        #endregion GetId

        #region GedAll

        public (
           bool IsSuccess,
           RecognizedInstructionStruct RecognizedInstructionSt,
           string ErrorMessage)
           GetAll<T>(int userId) {
            RecognizedInstructionStruct recognizedInstructionSt = new RecognizedInstructionStruct();
            recognizedInstructionSt.RecognizeInstruction.UserId = userId;
            return Request<RecognizedInstructionStruct>(
                $"{Variable.UrlApi}{APIs.RecognizedInstructionGetAll}",
                recognizedInstructionSt
            );
        }

        #endregion GedAll

        #region Save

        public (
           bool IsSuccess,
           RecognizedInstructionStruct RecognizedInstructionSt,
           string ErrorMessage)
           Save<T>(RecognizedInstructionEn oRI) {
            RecognizedInstructionStruct oRISt = new RecognizedInstructionStruct();
            oRISt.RecognizeInstruction.RecognizedInstructionId = oRI.RecognizedInstructionId;
            oRISt.RecognizeInstruction.UserId = oRI.UserId;
            oRISt.RecognizeInstruction.LanguageId = oRI.LanguageId;
            oRISt.RecognizeInstruction.GrammarId = oRI.GrammarId;
            oRISt.RecognizeInstruction.Grammar = oRI.Grammar;
            oRISt.RecognizeInstruction.Code = oRI.Code;
            oRISt.RecognizeInstruction.Instruction = oRI.Instruction;
            oRISt.RecognizeInstruction.Description = oRI.Description;
            oRISt.RecognizeInstruction.VoiceProcessing = oRI.VoiceProcessing;
            oRISt.RecognizeInstruction.VoiceSolution = oRI.VoiceSolution;
            oRISt.RecognizeInstruction.VoiceCancel = oRI.VoiceCancel;
            oRISt.RecognizeInstruction.VoiceFail = oRI.VoiceFail;
            oRISt.RecognizeInstruction.DateRegistration = oRI.DateRegistration;
            oRISt.RecognizeInstruction.LastUpdate = oRI.LastUpdate;
            oRISt.RecognizeInstruction.Enabled = oRI.Enabled;
            return Request<RecognizedInstructionStruct>(
                $"{Variable.UrlApi}{APIs.RecognizedInstructionSave}",
                oRISt
            );
        }

        #endregion Save

        #region Enable

        public (
           bool IsSuccess,
           RecognizedInstructionStruct RecognizedInstructionSt,
           string ErrorMessage)
           Delete<T>(int recognizedInstructionId, bool enabled) {
            RecognizedInstructionStruct recognizedInstructionSt = new RecognizedInstructionStruct();
            recognizedInstructionSt.RecognizeInstruction.RecognizedInstructionId = recognizedInstructionId;
            recognizedInstructionSt.RecognizeInstruction.Enabled = enabled;
            return Request<RecognizedInstructionStruct>(
                $"{Variable.UrlApi}{APIs.RecognizedInstructionEnable}",
                recognizedInstructionSt
            );
        }

        #endregion Enable

        #region Private Methods

        private (
            bool IsSuccess,
            RecognizedInstructionStruct RecognizedWordSt,
            string ErrorMessage
        ) Request<T>(string url, RecognizedInstructionStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<RecognizedInstructionStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private (
            bool IsSuccess,
            RecognizedInstructionStruct StructResponse,
            string ErrorMessage
        ) Common(
            HttpStatusCode pIsSuccess,
            string pResponseObject,
            string pResponseMessage
        ) {
            //
            bool isOk = false;
            RecognizedInstructionStruct objectSt = new RecognizedInstructionStruct();
            string error = "";
            if (pIsSuccess == HttpStatusCode.OK) {
                objectSt = Newtonsoft
                    .Json
                    .JsonConvert
                    .DeserializeObject<RecognizedInstructionStruct>(pResponseObject);
                if (objectSt.StatusOk)
                    isOk = true;
                else
                    error = objectSt.Message;
            } else {
                error = pResponseMessage;
            }
            return (isOk, objectSt, error);
        }

        #endregion Private Methods
    }
}
