using Samantha.Repository;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Repository {

    public class CompanyRepository {

        #region GetId

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        public static CompanyEn GetId(int companyId) {

            string _query = "[dbo].[Company_GetId]";
            System.Data.SqlClient.SqlParameter[] Params = { new System.Data.SqlClient.SqlParameter("@CompanyId", companyId) };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                CompanyEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new CompanyEn();
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
        public static CompanyEn Save(CompanyEn companyEn) {
            try {
                string _query = "[dbo].[Company_Save]";
                System.Data.SqlClient.SqlParameter[] Params = {
                    new System.Data.SqlClient.SqlParameter("@CompanyId", companyEn.CompanyId),
                    new System.Data.SqlClient.SqlParameter("@CompanyTypeId", companyEn.CompanyTypeId),
                    new System.Data.SqlClient.SqlParameter("@Tradename", companyEn.Tradename),
                    new System.Data.SqlClient.SqlParameter("@BusinessName", companyEn.BusinessName),
                    new System.Data.SqlClient.SqlParameter("@Name", companyEn.Name),
                    new System.Data.SqlClient.SqlParameter("@FirstName", companyEn.FirstName),
                    new System.Data.SqlClient.SqlParameter("@LastName", companyEn.LastName),
                    new System.Data.SqlClient.SqlParameter("@FTR", companyEn.FTR),
                    new System.Data.SqlClient.SqlParameter("@PRK", companyEn.PRK),
                    new System.Data.SqlClient.SqlParameter("@Street", companyEn.Street),
                    new System.Data.SqlClient.SqlParameter("@StreetNumber", companyEn.StreetNumber),
                    new System.Data.SqlClient.SqlParameter("@CrossingStreets", companyEn.CrossingStreets),
                    new System.Data.SqlClient.SqlParameter("@Colony", companyEn.Colony),
                    new System.Data.SqlClient.SqlParameter("@City", companyEn.City),
                    new System.Data.SqlClient.SqlParameter("@Municipality", companyEn.Municipality),
                    new System.Data.SqlClient.SqlParameter("@State", companyEn.State),
                    new System.Data.SqlClient.SqlParameter("@Country", companyEn.Country),
                    new System.Data.SqlClient.SqlParameter("@PostalCode", companyEn.PostalCode),
                    new System.Data.SqlClient.SqlParameter("@CellPhoneNumber", companyEn.CellPhoneNumber),
                    new System.Data.SqlClient.SqlParameter("@PhoneNumber", companyEn.PhoneNumber),
                    new System.Data.SqlClient.SqlParameter("@EMail", companyEn.EMail),
                    new System.Data.SqlClient.SqlParameter("@DateRegistration", companyEn.DateRegistration),
                    new System.Data.SqlClient.SqlParameter("@Enabled", companyEn.Enabled),
                };
                CompanyEn _record;
                DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                    MainProgram.gSQLconexionSamanthaX.Open();
                }

                System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

                if (_reader.HasRows == true) {
                    if (_reader.Read()) {
                        _record = new CompanyEn();
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
