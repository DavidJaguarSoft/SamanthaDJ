using Samantha.Repository;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SamanthaX.Repository {

    public class RecognizedWordRepository {

        #region GetId

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public static RecognizedWordEn GetId(int recognizedWordId) {

            string _query = "[dbo].[RecognizedWord_GetId]";
            System.Data.SqlClient.SqlParameter[] Params = { new System.Data.SqlClient.SqlParameter("@RecognizedWordId", recognizedWordId) };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                RecognizedWordEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new RecognizedWordEn();
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
        public static List<RecognizedWordEn> GetAll(int userId) {

            string _query = "[dbo].[RecognizedWord_GetAll]";
            System.Data.SqlClient.SqlParameter[] Params = { new System.Data.SqlClient.SqlParameter("@UserId", userId) };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                List<RecognizedWordEn> Registers = new List<RecognizedWordEn>();
                RecognizedWordEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new RecognizedWordEn();
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
        public static RecognizedWordEn Save(RecognizedWordEn recWord) {
            try {
                string _query = "[dbo].[RecognizedWord_Save]";
                System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@RecognizedWordId", recWord.RecognizedWordId),
                new System.Data.SqlClient.SqlParameter("@UserId", recWord.UserId),
                new System.Data.SqlClient.SqlParameter("@LanguageId", recWord.LanguageId),
                new System.Data.SqlClient.SqlParameter("@Code", recWord.Code),
                new System.Data.SqlClient.SqlParameter("@WordClassId", recWord.WordClassId),
                new System.Data.SqlClient.SqlParameter("@RelatedWords", recWord.RelatedWords),
                new System.Data.SqlClient.SqlParameter("@DateRegistration", recWord.DateRegistration),
                new System.Data.SqlClient.SqlParameter("@Enabled", recWord.Enabled),
            };
                RecognizedWordEn _record;
                DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                    MainProgram.gSQLconexionSamanthaX.Open();
                }

                System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

                if (_reader.HasRows == true) {
                    if (_reader.Read()) {
                        _record = new RecognizedWordEn();
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
        public static RecognizedWordEn Enable(int recognizedWordId, bool enabled) {

            string _query = "[dbo].[RecognizedWord_Enable]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@RecognizedWordId", recognizedWordId),
                new System.Data.SqlClient.SqlParameter("@Enabled", enabled),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                RecognizedWordEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new RecognizedWordEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion Enable

        #region GetWordClass

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public static List<RecognizedWordEn> GetWordClass(
            int userId,
            int wordClassId
        ) {
            string _query = "[dbo].[RecognizedWord_GetWordClass]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@UserId", userId),
                new System.Data.SqlClient.SqlParameter("@WordClassId", wordClassId),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                List<RecognizedWordEn> Registers = new List<RecognizedWordEn>();
                RecognizedWordEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new RecognizedWordEn();
                    Register.ExtractData(_row);
                    Registers.Add(Register);
                }
                return Registers;
            }
        }

        #endregion GetWordClass

        #region GetWordClassxUser

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public static List<RecognizedWordEn> GetWordClassxUser(
            int userId,
            int wordClassId
        ) {
            string _query = "[dbo].[RecognizedWord_GetWordClassxUser]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@UserId", userId),
                new System.Data.SqlClient.SqlParameter("@WordClassId", wordClassId),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                List<RecognizedWordEn> Registers = new List<RecognizedWordEn>();
                RecognizedWordEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new RecognizedWordEn();
                    Register.ExtractData(_row);
                    Registers.Add(Register);
                }
                return Registers;
            }
        }

        #endregion GetWordClassxUser

        #region GetAllxUser

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public static List<RecognizedWordEn> GetAllxUser(int userId) {

            string _query = "[dbo].[RecognizedWord_GetAllxUser]";
            System.Data.SqlClient.SqlParameter[] Params = { new System.Data.SqlClient.SqlParameter("@UserId", userId) };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                List<RecognizedWordEn> Registers = new List<RecognizedWordEn>();
                RecognizedWordEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new RecognizedWordEn();
                    Register.ExtractData(_row);
                    Registers.Add(Register);
                }
                return Registers;
            }
        }

        #endregion GetAllxUser
    }
}
