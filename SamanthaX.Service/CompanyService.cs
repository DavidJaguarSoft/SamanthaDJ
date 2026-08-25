using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {

    public static class CompanyService {

        #region GetId

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.CompanyEn GetId(int companyId) {
            return Repository.CompanyRepository.GetId(companyId);
        }

        #endregion GetId

        #region Save

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.CompanyEn Save(Model.Entity.CompanyEn company) {
            return Repository.CompanyRepository.Save(company);
        }

        #endregion Save
    }
}
