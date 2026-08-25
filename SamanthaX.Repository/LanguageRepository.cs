using Samantha.Repository;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Repository {

    public class LanguageRepository {

        #region GetId

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static LanguageEn GetId(int languageId) {

            string _query = "[dbo].[Language_GetId]";
            System.Data.SqlClient.SqlParameter[] Params = { new System.Data.SqlClient.SqlParameter("@LanguageId", languageId) };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                LanguageEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new LanguageEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion GetId

    }
}
