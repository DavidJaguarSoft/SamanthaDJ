using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {

    public static class UserTypeService {

        #region "GetId"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.UserTypeEn GetId(int userTypeId) {
            return Repository.UserTypeRepository.GetId(userTypeId);
        }

        #endregion

        #region "Save"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.UserTypeEn Save(Model.Entity.UserTypeEn userType) {
            return Repository.UserTypeRepository.Save(userType);
        }

        #endregion

        #region "Delete"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.UserTypeEn Delete(Model.Entity.UserTypeEn userType) {
            return Repository.UserTypeRepository.Delete(userType);
        }

        #endregion

    }
}
