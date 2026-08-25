using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {
    
    public static class GrammarBuilderService {

        #region GetId

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static GrammarBuilderEn GetId(int grammarBuilderId) {
            return Repository.GrammarBuilderRepository.GetId(grammarBuilderId);
        }

        #endregion GetId

        #region GetGrammar

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static List<Model.Entity.GrammarBuilderEn> GetGrammar(int grammarId) {
            return Repository.GrammarBuilderRepository.GetGrammar(grammarId);
        }

        #endregion GetGrammar

        #region Save

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static GrammarBuilderEn Save(GrammarBuilderEn oGB) {
            return Repository.GrammarBuilderRepository.Save(oGB);
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
        public static GrammarBuilderEn Enable(int grammarBuilderId, bool enabled) {
            return Repository.GrammarBuilderRepository.Enable(grammarBuilderId, enabled);
        }

        #endregion Enable

        #region Delete

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static bool Delete(int grammarBuilderId) {
            return Repository.GrammarBuilderRepository.Delete(grammarBuilderId);
        }

        #endregion Delete

    }
}
