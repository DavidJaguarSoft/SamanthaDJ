using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.DataConnection {
    public class ConnectionSQL {

        #region Constructor

        /// <summary>
        ///     Class constructor
        /// </summary>
        public ConnectionSQL() {

        }

        #endregion

        #region SelectFromStoreProcedure

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQLconnection"></param>
        /// <param name="sqlSTR_"></param>
        /// <param name="pParametros"></param>
        /// <returns></returns>
        public System.Data.DataTable SelectFromStoreProcedure(System.Data.SqlClient.SqlConnection SQLconnection,
                                                              String sqlSTR_,
                                                              System.Data.SqlClient.SqlParameter[] pParametros = null) {

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(sqlSTR_, SQLconnection);
            System.Data.DataTable _dt = new System.Data.DataTable();

            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = 150;

            if (pParametros != null && pParametros.Count() > 0) {
                foreach (System.Data.SqlClient.SqlParameter item in pParametros) {
                    da.SelectCommand.Parameters.Add(item);
                }
            }

            da.Fill(_dt);
            da.Dispose();

            if (_dt.Rows.Count > 0) {
                return _dt;
            }
            else {
                return null;
            }
        }

        #endregion

        #region InsertUpdateDelete

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SQLconnection"></param>
        /// <param name="sqlSTR_"></param>
        /// <param name="pParametros"></param>
        /// <returns></returns>
        public System.Data.SqlClient.SqlDataReader InsertUpdateDelete(System.Data.SqlClient.SqlConnection SQLconnection,
                                                                      String pCommandString,
                                                                      System.Data.SqlClient.SqlParameter[] pParametros = null) {

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = SQLconnection;
            cmd.CommandText = pCommandString;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            if (!pParametros.Equals(null) && pParametros.Count() > 0) {
                foreach (System.Data.SqlClient.SqlParameter item in pParametros) {
                    cmd.Parameters.Add(item);
                }
            }

            System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return reader;
        }

        #endregion

    }
}
