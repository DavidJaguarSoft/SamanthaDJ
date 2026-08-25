using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {

    public static class CompanyTypeService {

        #region "GetAll"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static List<Model.Entity.CompanyTypeEn> GetAll() {
            return Repository.CompanyTypeRepository.GetAll();
        }

        #endregion

        #region "GetId"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static Model.Entity.CompanyTypeEn GetId(int companyTypeId) {
            return Repository.CompanyTypeRepository.GetId(companyTypeId);
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
        public static Model.Entity.CompanyTypeEn Save(Model.Entity.CompanyTypeEn companyType) {
            return Repository.CompanyTypeRepository.Save(companyType);
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
        public static Model.Entity.CompanyTypeEn Delete(Model.Entity.CompanyTypeEn companyType) {
            return Repository.CompanyTypeRepository.Delete(companyType);
        }

        #endregion

    }
}
