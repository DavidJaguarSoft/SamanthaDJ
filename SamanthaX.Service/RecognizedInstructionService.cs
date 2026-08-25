using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Service {

    public static class RecognizedInstructionService {

        #region GetId

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///
        /// </history>
        public static RecognizedInstructionEn GetId(int recognizedInstructionId) {
            return Repository.RecognizedInstructionRepository.GetId(recognizedInstructionId);
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
        public static List<Model.Entity.RecognizedInstructionEn> GetAll(int userId) {
            return Repository.RecognizedInstructionRepository.GetAll(userId);
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
        public static RecognizedInstructionEn Save(RecognizedInstructionEn oRecIns) {
            return Repository.RecognizedInstructionRepository.Save(oRecIns);
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
        public static RecognizedInstructionEn Enable(int recognizedInstructionId, bool enabled) {
            return Repository.RecognizedInstructionRepository.Enable(recognizedInstructionId, enabled);
        }

        #endregion Enable


    }
}
