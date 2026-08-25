using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {
    
    public class WordClassService {

        #region GetId

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static WordClassEn GetId(int wordClassId) {
            return Repository.WordClassRepository.GetId(wordClassId);
        }

        #endregion GetId

        #region GetAll

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static List<WordClassEn> GetAll(int userId) {
            return Repository.WordClassRepository.GetAll(userId);
        }

        #endregion GetAll

        #region Save

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static WordClassEn Save(WordClassEn oWordClass) {
            return Repository.WordClassRepository.Save(oWordClass);
        }

        #endregion Save

        #region Enable

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static WordClassEn Enable(int wordClassId, bool enabled) {
            return Repository.WordClassRepository.Enable(wordClassId, enabled);
        }

        #endregion Enable

        #region GetAllxUser

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static List<WordClassEn> GetAllxUser(int userId) {
            return Repository.WordClassRepository.GetAllxUser(userId);
        }

        #endregion GetAllxUser
    }
}
