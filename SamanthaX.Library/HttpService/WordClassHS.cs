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
    
    public class WordClassHS {

        #region Constructors

        public WordClassHS() {}

        #endregion Constructors

        #region GetId

        public (
            bool IsSuccess,
            WordClassStruct WordClassSt,
            string ErrorMessage)
            GetId<T>(int wordClassId) {
            WordClassStruct wordClassSt = new WordClassStruct();
            wordClassSt.WordClass.WordClassId = wordClassId;
            return Request<WordClassStruct>(
                $"{Variable.UrlApi}{APIs.WordClassGetId}",
                wordClassSt
            );
        }

        #endregion GetId

        #region GetAll

        public (
           bool IsSuccess,
           WordClassStruct WordClassSt,
           string ErrorMessage)
           GetAll<T>(int userId, bool allowRecognizedWord = false) {
            WordClassStruct wordClassSt = new WordClassStruct();
            wordClassSt.AllowRecognizedWord = allowRecognizedWord;
            wordClassSt.WordClass.UserId = userId;
            return Request<WordClassStruct>(
                $"{Variable.UrlApi}{APIs.WordClassGetAll}",
                wordClassSt
            );
        }

        #endregion GetAll

        #region Save

        public (
           bool IsSuccess,
           WordClassStruct WordClassSt,
           string ErrorMessage)
           Save<T>(
                int wordClassId,
                int userId,
                string code,
                string name,
                string description,
                string example) {
            WordClassStruct wordClassSt = new WordClassStruct();
            wordClassSt.WordClass.WordClassId = wordClassId;
            wordClassSt.WordClass.UserId = userId;
            wordClassSt.WordClass.Code = code;
            wordClassSt.WordClass.Name = name;
            wordClassSt.WordClass.Description = description;
            wordClassSt.WordClass.Example = example;
            wordClassSt.WordClass.DateRegistration = DateTime.Now;
            wordClassSt.WordClass.Enabled = true;
            return Request<WordClassStruct>(
                $"{Variable.UrlApi}{APIs.WordClassSave}",
                wordClassSt
            );
        }

        #endregion Save

        #region Enable

        public (
           bool IsSuccess,
           WordClassStruct WordClassSt,
           string ErrorMessage)
           Delete<T>(int wordClassId, bool enabled) {
            WordClassStruct wordClassSt = new WordClassStruct();
            wordClassSt.WordClass.WordClassId = wordClassId;
            wordClassSt.WordClass.Enabled = enabled;
            return Request<WordClassStruct>(
                $"{Variable.UrlApi}{APIs.WordClassEnable}",
                wordClassSt
            );
        }

        #endregion Enable

        #region Private Methods

        private (
            bool IsSuccess,
            WordClassStruct WordClassSt,
            string ErrorMessage
        ) Request<T>(string url, WordClassStruct objectRequest) {
            APIGeneric oAPIGeneric = new APIGeneric();
            var response = oAPIGeneric.GetAPI<WordClassStruct>(url, objectRequest);
            var DeserializeUser = Common(response.IsSuccess, response.stObjectStruct, response.ErrorMessage);
            return (DeserializeUser.IsSuccess, DeserializeUser.StructResponse, DeserializeUser.ErrorMessage);
        }

        private (
            bool IsSuccess,
            WordClassStruct StructResponse,
            string ErrorMessage
        ) Common(
            HttpStatusCode pIsSuccess,
            string pResponseObject,
            string pResponseMessage
        ) {
            //
            bool isOk = false;
            WordClassStruct objectSt = new WordClassStruct();
            string error = "";
            if (pIsSuccess == HttpStatusCode.OK) {
                objectSt = Newtonsoft
                    .Json
                    .JsonConvert
                    .DeserializeObject<WordClassStruct>(pResponseObject);
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
