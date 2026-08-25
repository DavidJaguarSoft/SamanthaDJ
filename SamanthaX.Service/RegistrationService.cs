using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {

    public static class RegistrationService {

        #region "GetEMail"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.RegistrationEn GetEMail(String eMail) {
            return Repository.RegistrationRepository.GetEMail(eMail);
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
        public static Model.Entity.RegistrationEn Save(Model.Entity.RegistrationEn registger) {
            return Repository.RegistrationRepository.Save(registger);
        }

        #endregion

        #region "CreateUser"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.RegistrationEn CreateUser(Model.Entity.RegistrationEn registger) {
            return Repository.RegistrationRepository.CreateUser(registger);
        }

        #endregion

    }
}
