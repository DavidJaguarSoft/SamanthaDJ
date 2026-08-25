using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {

    public static class GrammarService {

        #region GetId

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static GrammarEn GetId(int grmmarId) {
            return Repository.GrammarRepository.GetId(grmmarId);
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
        public static List<Model.Entity.GrammarEn> GetAll(int userId) {
            return Repository.GrammarRepository.GetAll(userId);
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
        public static GrammarEn Save(GrammarEn oGrammar) {
            return Repository.GrammarRepository.Save(oGrammar);
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
        public static GrammarEn Enable(int grammarId, bool enabled) {
            return Repository.GrammarRepository.Enable(grammarId, enabled);
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
        public static List<Model.Entity.GrammarEn> GetAllxUser(int userId) {
            return Repository.GrammarRepository.GetAllxUser(userId);
        }

        #endregion GetAllxUser
    }
}
