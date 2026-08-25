using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {

    public static class UserService {

        #region GetNamePassword

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.UserEn GetNamePassword(string name, string password) {
            return Repository.UserRepository.GetNamePassword(name, password);
        }

        #endregion GetNamePassword

        #region UserGetNameToken

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.UserEn UserGetNameToken(string name, string password) {
            return Repository.UserRepository.UserGetNameToken(name, password);
        }

        #endregion UserGetNameToken

        #region Save

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.UserEn Save(Model.Entity.UserEn user) {
            return Repository.UserRepository.Save(user);
        }

        #endregion Save
    }
}
