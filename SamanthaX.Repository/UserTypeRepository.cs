using Samantha.Repository;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Repository {

    public class UserTypeRepository {

        #region GetId

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static UserTypeEn GetId(int userTypeId) {

            string _query = "[dbo].[UserType_GetId]";
            System.Data.SqlClient.SqlParameter[] Params = { new System.Data.SqlClient.SqlParameter("@UserTypeId", userTypeId) };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                UserTypeEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new UserTypeEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion GetId

        #region "Save"

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static UserTypeEn Save(UserTypeEn userType) {

            string _query = "[dbo].[UserType_Save]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@UserTypeId", userType.UserTypeId),
                new System.Data.SqlClient.SqlParameter("@Code", userType.Code),
                new System.Data.SqlClient.SqlParameter("@Name", userType.Name),
                new System.Data.SqlClient.SqlParameter("@Description", userType.Description),
                new System.Data.SqlClient.SqlParameter("@DateRegistration", userType.DateRegistration),
                new System.Data.SqlClient.SqlParameter("@Enabled", userType.Enabled),
            };
            UserTypeEn _record;
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

            if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                MainProgram.gSQLconexionSamanthaX.Open();
            }

            System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_reader.HasRows == true) {
                if (_reader.Read()) {
                    _record = new UserTypeEn();
                    _record.ExtractData(_reader);
                    MainProgram.gSQLconexionSamanthaX.Close();
                    return _record;
                }
            }

            MainProgram.gSQLconexionSamanthaX.Close();
            return null;
        }

        #endregion

        #region "Delete"

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static UserTypeEn Delete(UserTypeEn userType) {

            string _query = "[dbo].[UserType_Delete]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@UserTypeId", userType.UserTypeId),
                new System.Data.SqlClient.SqlParameter("@Enabled", userType.Enabled),
            };
            UserTypeEn _record;
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

            if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                MainProgram.gSQLconexionSamanthaX.Open();
            }

            System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_reader.HasRows == true) {
                if (_reader.Read()) {
                    _record = new UserTypeEn();
                    _record.ExtractData(_reader);
                    MainProgram.gSQLconexionSamanthaX.Close();
                    return _record;
                }
            }

            MainProgram.gSQLconexionSamanthaX.Close();
            return null;
        }

        #endregion

    }
}
