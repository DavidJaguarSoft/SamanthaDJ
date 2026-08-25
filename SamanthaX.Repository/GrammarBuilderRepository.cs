using Samantha.Repository;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Repository {

    public class GrammarBuilderRepository {

        #region GetId

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static GrammarBuilderEn GetId(int grammarBuilderId) {

            string _query = "[dbo].[GrammarBuilder_GetId]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@GrammarBuilderId", grammarBuilderId)
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                GrammarBuilderEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new GrammarBuilderEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion GetId

        #region GetGrammar

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static List<GrammarBuilderEn> GetGrammar(int grammarId) {

            string _query = "[dbo].[GrammarBuilder_GetGrammar]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@GrammarId", grammarId)
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                List<GrammarBuilderEn> Registers = new List<GrammarBuilderEn>();
                GrammarBuilderEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new GrammarBuilderEn();
                    Register.ExtractData(_row);
                    Registers.Add(Register);
                }
                return Registers;
            }
        }

        #endregion GetGrammar

        #region Save

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static GrammarBuilderEn Save(GrammarBuilderEn oGB) {
            try {
                string _query = "[dbo].[GrammarBuilder_Save]";
                System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@GrammarBuilderId", oGB.GrammarBuilderId),
                new System.Data.SqlClient.SqlParameter("@GrammarId", oGB.GrammarId),
                new System.Data.SqlClient.SqlParameter("@WordClassId", oGB.WordClassId),
                new System.Data.SqlClient.SqlParameter("@Sequence", oGB.Sequence),
                new System.Data.SqlClient.SqlParameter("@DateRegistration", oGB.DateRegistration),
                new System.Data.SqlClient.SqlParameter("@Enabled", oGB.Enabled),
            };
                GrammarBuilderEn _record;
                DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                    MainProgram.gSQLconexionSamanthaX.Open();
                }

                System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

                if (_reader.HasRows == true) {
                    if (_reader.Read()) {
                        _record = new GrammarBuilderEn();
                        _record.ExtractData(_reader);
                        MainProgram.gSQLconexionSamanthaX.Close();
                        return _record;
                    }
                }

                MainProgram.gSQLconexionSamanthaX.Close();
                return null;
            } catch (Exception ex) {
                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Open) {
                    MainProgram.gSQLconexionSamanthaX.Close();
                }
                throw ex;
            }
        }

        #endregion Save

        #region Enable

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static GrammarBuilderEn Enable(int grammarBuilderId, bool enabled) {

            string _query = "[dbo].[GrammarBuilder_Enable]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@GrammarBuilderId", grammarBuilderId),
                new System.Data.SqlClient.SqlParameter("@Enabled", enabled),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                GrammarBuilderEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new GrammarBuilderEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion Enable

        #region Delete

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static bool Delete(int grammarBuilderId) {

            string _query = "[dbo].[GrammarBuilder_Delete]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@GrammarBuilderId", grammarBuilderId),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                return false;
            } else {
                if (_data.Rows.Count > 0) {
                    return true;
                }
                return false;
            }
        }

        #endregion Delete
    }
}
