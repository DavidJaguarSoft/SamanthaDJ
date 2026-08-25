using Samantha.Repository;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Repository {

    public class RegistrationRepository {

        #region GetEMail

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///     
        ///     
        public static RegistrationEn GetEMail(String eMail) {

            string _query = "dbo.[Registration_GetEMail]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@EMail", eMail)
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if(_data == null) {
                //  Records no found
                return null;
            } else {
                RegistrationEn Register;

                foreach(System.Data.DataRow _row in _data.Rows) {
                    Register = new RegistrationEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion GetEMail

        #region Save

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///     
        ///     
        public static RegistrationEn Save(RegistrationEn register) {

            try {
                string _query = "dbo.[Registration_Save]";
                System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@RegistrationId", register.RegistrationId       ),
                new System.Data.SqlClient.SqlParameter("@EMail", register.EMail),
                new System.Data.SqlClient.SqlParameter("@Password", register.Password),
                new System.Data.SqlClient.SqlParameter("@Token", register.Token),
                new System.Data.SqlClient.SqlParameter("@CompanyId", register.CompanyId),
                new System.Data.SqlClient.SqlParameter("@Company", register.Company),
                new System.Data.SqlClient.SqlParameter("@FirstName", register.FirstName),
                new System.Data.SqlClient.SqlParameter("@LastName", register.LastName),
                new System.Data.SqlClient.SqlParameter("@UserId", register.UserId),
                new System.Data.SqlClient.SqlParameter("@LanguageId", register.LanguageId),
                new System.Data.SqlClient.SqlParameter("@CompletedRegistration", register.CompletedRegistration),
                new System.Data.SqlClient.SqlParameter("@DateRegistration", register.DateRegistration),
            };
                RegistrationEn _record;
                DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                    MainProgram.gSQLconexionSamanthaX.Open();
                }

                System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

                if (_reader.HasRows == true) {
                    if (_reader.Read()) {
                        _record = new RegistrationEn();
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

        #region CreateUser

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///     
        ///     
        public static RegistrationEn CreateUser(RegistrationEn register) {

            try {
                string _query = "dbo.[Registration_CreateUser]";
                System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@RegistrationId", register.RegistrationId       ),
                new System.Data.SqlClient.SqlParameter("@EMail", register.EMail),
                new System.Data.SqlClient.SqlParameter("@Password", register.Password),
                new System.Data.SqlClient.SqlParameter("@Token", register.Token),
                new System.Data.SqlClient.SqlParameter("@CompanyId", register.CompanyId),
                new System.Data.SqlClient.SqlParameter("@Company", register.Company),
                new System.Data.SqlClient.SqlParameter("@FirstName", register.FirstName),
                new System.Data.SqlClient.SqlParameter("@LastName", register.LastName),
                new System.Data.SqlClient.SqlParameter("@UserId", register.UserId),
                new System.Data.SqlClient.SqlParameter("@LanguageId", register.LanguageId),
                new System.Data.SqlClient.SqlParameter("@CompletedRegistration", register.CompletedRegistration),
                new System.Data.SqlClient.SqlParameter("@DateRegistration", register.DateRegistration),
            };
                RegistrationEn _record;
                DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                    MainProgram.gSQLconexionSamanthaX.Open();
                }

                System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

                if (_reader.HasRows == true) {
                    if (_reader.Read()) {
                        _record = new RegistrationEn();
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

        #endregion CreateUser
    }
}
