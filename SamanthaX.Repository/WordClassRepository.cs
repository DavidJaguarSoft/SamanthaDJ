using Samantha.Repository;
using SamanthaX.Model.Entity;
using SamanthaX.Model.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Repository {

    public class WordClassRepository {

        #region GetId

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static WordClassEn GetId(int wordClassId) {

            string _query = "[dbo].[WordClass_GetId]";
            System.Data.SqlClient.SqlParameter[] Params = { new System.Data.SqlClient.SqlParameter("@WordClassId", wordClassId) };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if(_data == null) {
                //  Records no found
                return null;
            } else {
                WordClassEn Register;

                foreach(System.Data.DataRow _row in _data.Rows) {
                    Register = new WordClassEn();
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
        public static List<WordClassEn> GetAll(int userId) {

            string _query = "[dbo].[WordClass_GetAll]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@UserId", userId),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                List<WordClassEn> Registers = new List<WordClassEn>();
                WordClassEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new WordClassEn();
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
        public static WordClassEn Save(WordClassEn wordClass) {
            try {
                string _query = "[dbo].[WordClass_Save]";
                System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@WordClassId", wordClass.WordClassId),
                new System.Data.SqlClient.SqlParameter("@UserId", wordClass.UserId),
                new System.Data.SqlClient.SqlParameter("@Code", wordClass.Code),
                new System.Data.SqlClient.SqlParameter("@Name", wordClass.Name),
                new System.Data.SqlClient.SqlParameter("@Description", wordClass.Description),
                new System.Data.SqlClient.SqlParameter("@Example", wordClass.Example),
                new System.Data.SqlClient.SqlParameter("@DateRegistration", wordClass.DateRegistration),
                new System.Data.SqlClient.SqlParameter("@Enabled", wordClass.Enabled),
            };
                WordClassEn _record;
                DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                    MainProgram.gSQLconexionSamanthaX.Open();
                }

                System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

                if (_reader.HasRows == true) {
                    if (_reader.Read()) {
                        _record = new WordClassEn();
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
        public static WordClassEn Enable(int wordClassId, bool enabled) {

            string _query = "[dbo].[WordClass_Enable]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@WordClassId", wordClassId),
                new System.Data.SqlClient.SqlParameter("@Enabled", enabled),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if(_data == null) {
                //  Records no found
                return null;
            } else {
                WordClassEn Register;

                foreach(System.Data.DataRow _row in _data.Rows) {
                    Register = new WordClassEn();
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
        public static List<WordClassEn> GetAllxUser(int userId) {

            string _query = "[dbo].[WordClass_GetAllxUser]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@UserId", userId),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                List<WordClassEn> Registers = new List<WordClassEn>();
                WordClassEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new WordClassEn();
                    Register.ExtractData(_row);
                    Registers.Add(Register);
                }
                return Registers;
            }
        }

        #endregion GetAllxUser
    }
}
