using Samantha.Repository;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Repository {

    public class CompanyTypeRepository {

        #region GetAll

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static List<CompanyTypeEn> GetAll() {

            string _query = "[dbo].[sp_CompanyType_getAll]";
            System.Data.SqlClient.SqlParameter[] Params = {};
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                List<CompanyTypeEn> Registers = new List<CompanyTypeEn>();
                CompanyTypeEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new CompanyTypeEn();
                    Register.ExtractData(_row);
                    Registers.Add(Register);
                }
                return Registers;
            }
        }

        #endregion GetAll

        #region GetId

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static CompanyTypeEn GetId(int companyTypeId) {

            string _query = "[dbo].[CompanyType_GetId]";
            System.Data.SqlClient.SqlParameter[] Params = { new System.Data.SqlClient.SqlParameter("@companyTypeId", companyTypeId) };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                CompanyTypeEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new CompanyTypeEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion GetId

        #region Save

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static CompanyTypeEn Save(CompanyTypeEn companyType) {

            string _query = "[dbo].[sp_CompanyType_save]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@CompanyTypeId", companyType.CompanyTypeId),
                new System.Data.SqlClient.SqlParameter("@Code", companyType.Code),
                new System.Data.SqlClient.SqlParameter("@Name", companyType.Name),
                new System.Data.SqlClient.SqlParameter("@Description", companyType.Description),
                new System.Data.SqlClient.SqlParameter("@DateRegistration", companyType.DateRegistration),
                new System.Data.SqlClient.SqlParameter("@Enabled", companyType.Enabled),
            };
            CompanyTypeEn _record;
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

            if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                MainProgram.gSQLconexionSamanthaX.Open();
            }

            System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_reader.HasRows == true) {
                if (_reader.Read()) {
                    _record = new CompanyTypeEn();
                    _record.ExtractData(_reader);
                    MainProgram.gSQLconexionSamanthaX.Close();
                    return _record;
                }
            }

            MainProgram.gSQLconexionSamanthaX.Close();
            return null;
        }

        #endregion Save

        #region Delete

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static CompanyTypeEn Delete(CompanyTypeEn companyType) {

            string _query = "[dbo].[sp_CompanyType_delete]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@CompanyTypeId", companyType.CompanyTypeId),
                new System.Data.SqlClient.SqlParameter("@Enabled", companyType.Enabled),
            };
            CompanyTypeEn _record;
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

            if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                MainProgram.gSQLconexionSamanthaX.Open();
            }

            System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_reader.HasRows == true) {
                if (_reader.Read()) {
                    _record = new CompanyTypeEn();
                    _record.ExtractData(_reader);
                    MainProgram.gSQLconexionSamanthaX.Close();
                    return _record;
                }
            }

            MainProgram.gSQLconexionSamanthaX.Close();
            return null;
        }

        #endregion Delete

    }
}
