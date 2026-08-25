using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {
    
    public class SamanthaVoiceService {

        #region GetUser

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.SamanthaVoiceEn GetUser(int userId, int languageId) {
            return Repository.SamanthaVoiceRepository.GetUser(userId, languageId);
        }

        #endregion GetUser

        #region Save

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.SamanthaVoiceEn Save(SamanthaVoiceEn svEn) {
            return Repository.SamanthaVoiceRepository.Save(svEn);
        }

        #endregion Save
    }
}
