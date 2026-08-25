using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {
    
    public static class RecognizedWordService {

        #region GetId

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static RecognizedWordEn GetId(int wordClassId) {
            return Repository.RecognizedWordRepository.GetId(wordClassId);
        }

        #endregion GetId

        #region GetAll

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Model.Entity.RecognizedWordEn> GetAll(int userId) {
            return Repository.RecognizedWordRepository.GetAll(userId);
        }

        #endregion GetAll

        #region Save

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static RecognizedWordEn Save(RecognizedWordEn oRecognizedWordEn) {
            return Repository.RecognizedWordRepository.Save(oRecognizedWordEn);
        }

        #endregion Save

        #region Enable

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static RecognizedWordEn Enable(int recogWordId, bool enabled) {
            return Repository.RecognizedWordRepository.Enable(recogWordId, enabled);
        }

        #endregion Enable

        #region GetWordClass

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Model.Entity.RecognizedWordEn> GetWordClass(
            int userId,
            int wordClassId
        ) {
            return Repository.RecognizedWordRepository.GetWordClass(userId, wordClassId);
        }

        #endregion GetWordClass

        #region GetWordClassxUser

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Model.Entity.RecognizedWordEn> GetWordClassxUser(
            int userId,
            int wordClassId
        ) {
            return Repository.RecognizedWordRepository.GetWordClassxUser(userId, wordClassId);
        }

        #endregion GetWordClassxUser

        #region GetAllxUser

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Model.Entity.RecognizedWordEn> GetAllxUser(int userId) {
            return Repository.RecognizedWordRepository.GetAllxUser(userId);
        }

        #endregion GetAllxUser
    }
}
