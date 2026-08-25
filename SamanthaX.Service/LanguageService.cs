using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {

    public static class LanguageService {

        #region "GetId"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.LanguageEn GetId(int languageId) {
            return Repository.LanguageRepository.GetId(languageId);
        }

        #endregion

    }
}
