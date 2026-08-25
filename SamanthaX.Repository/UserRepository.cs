using Samantha.Repository;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Repository {

    public class UserRepository {

        #region GetNamePassword

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///     
        ///     
        public static UserEn GetNamePassword(string name, string password) {

            string _query = "[dbo].[User_GetNamePassword]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@Name", name),
                new System.Data.SqlClient.SqlParameter("@Password", password)
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                UserEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new UserEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion GetNamePassword

        #region UserGetNameToken

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static UserEn UserGetNameToken(string name, string token) {

            string _query = "[dbo].[User_GetNameToken]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@Name", name),
                new System.Data.SqlClient.SqlParameter("@Token", token)
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                UserEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new UserEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion UserGetNameToken

        #region Save

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///     
        ///     
        public static UserEn Save(UserEn user) {
            try {
                string _query = "[dbo].[User_Save]";
                System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@UserId", user.UserId),
                new System.Data.SqlClient.SqlParameter("@UserTypeId", user.UserTypeId),
                new System.Data.SqlClient.SqlParameter("@LanguageId", user.LanguageId),
                new System.Data.SqlClient.SqlParameter("@Name", user.Name),
                new System.Data.SqlClient.SqlParameter("@Password", user.Password),
                new System.Data.SqlClient.SqlParameter("@Token", user.Token),
                new System.Data.SqlClient.SqlParameter("@DateRegistration", user.DateRegistration),
                new System.Data.SqlClient.SqlParameter("@Enabled", user.Enabled),
            };
                UserEn _record;
                DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                    MainProgram.gSQLconexionSamanthaX.Open();
                }

                System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

                if (_reader.HasRows == true) {
                    if (_reader.Read()) {
                        _record = new UserEn();
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

    }
}
