using Samantha.Repository;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Repository {

    public class GrammarRepository {

        #region GetId

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static GrammarEn GetId(int grammarId) {

            string _query = "[dbo].[Grammar_GetId]";
            System.Data.SqlClient.SqlParameter[] Params = { new System.Data.SqlClient.SqlParameter("@GrammarId", grammarId) };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                GrammarEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new GrammarEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion GetId

        #region GetAll

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static List<GrammarEn> GetAll(int userId) {

            string _query = "[dbo].[Grammar_GetAll]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@UserId", userId),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                List<GrammarEn> Registers = new List<GrammarEn>();
                GrammarEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new GrammarEn();
                    Register.ExtractData(_row);
                    Registers.Add(Register);
                }
                return Registers;
            }
        }

        #endregion GetAll

        #region Save

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static GrammarEn Save(GrammarEn oGrammar) {

            try {
                string _query = "[dbo].[Grammar_Save]";
                System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@GrammarId", oGrammar.GrammarId),
                new System.Data.SqlClient.SqlParameter("@UserId", oGrammar.UserId),
                new System.Data.SqlClient.SqlParameter("@LanguageId", oGrammar.LanguageId),
                new System.Data.SqlClient.SqlParameter("@Code", oGrammar.Code),
                new System.Data.SqlClient.SqlParameter("@Name", oGrammar.Name),
                new System.Data.SqlClient.SqlParameter("@Description", oGrammar.Description),
                new System.Data.SqlClient.SqlParameter("@DateRegistration", oGrammar.DateRegistration),
                new System.Data.SqlClient.SqlParameter("@Enabled", oGrammar.Enabled),
            };
                GrammarEn _record;
                DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                    MainProgram.gSQLconexionSamanthaX.Open();
                }

                System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

                if (_reader.HasRows == true) {
                    if (_reader.Read()) {
                        _record = new GrammarEn();
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
        public static GrammarEn Enable(int grammarId, bool enabled) {

            string _query = "[dbo].[Grammar_Enable]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@GrammarId", grammarId),
                new System.Data.SqlClient.SqlParameter("@Enabled", enabled),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                GrammarEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new GrammarEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion Enable

        #region GetAllxUser

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static List<GrammarEn> GetAllxUser(int userId) {

            string _query = "[dbo].[Grammar_GetAllxUser]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@UserId", userId),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                List<GrammarEn> Registers = new List<GrammarEn>();
                GrammarEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new GrammarEn();
                    Register.ExtractData(_row);
                    Registers.Add(Register);
                }
                return Registers;
            }
        }

        #endregion GetAllxUser
    }
}
