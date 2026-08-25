using Samantha.Repository;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SamanthaX.Repository {
    
    public class SamanthaVoiceRepository {

        #region GetUser

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///     
        ///     
        public static SamanthaVoiceEn GetUser(int userId, int languageId) {

            string _query = "[dbo].[SamanthaVoice_GetUser]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@UserId", userId),
                new System.Data.SqlClient.SqlParameter("@LanguageId", languageId)
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                SamanthaVoiceEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new SamanthaVoiceEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion GetUser

        #region Save

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///     
        ///     
        public static SamanthaVoiceEn Save(SamanthaVoiceEn svEn) {

            try {
                string _query = "[dbo].[SamanthaVoice_Save]";
                System.Data.SqlClient.SqlParameter[] Params = {
                    new System.Data.SqlClient.SqlParameter("@SamanthaVoiceId", svEn.SamanthaVoiceId),
                    new System.Data.SqlClient.SqlParameter("@UserId", svEn.UserId),
                    new System.Data.SqlClient.SqlParameter("@LanguageId", svEn.LanguageId),
                    new System.Data.SqlClient.SqlParameter("@AIName", svEn.AIName),
                    new System.Data.SqlClient.SqlParameter("@OrderYou", svEn.OrderYou),
                    new System.Data.SqlClient.SqlParameter("@VoiceProcessingDefault", svEn.VoiceProcessingDefault),
                    new System.Data.SqlClient.SqlParameter("@VoiceSolutionDefault", svEn.VoiceSolutionDefault),
                    new System.Data.SqlClient.SqlParameter("@VoiceCancelDefault", svEn.VoiceCancelDefault),
                    new System.Data.SqlClient.SqlParameter("@VoiceFailDefault", svEn.VoiceFailDefault),
                    new System.Data.SqlClient.SqlParameter("@AnExceptionOcurred", svEn.AnExceptionOcurred),
                    new System.Data.SqlClient.SqlParameter("@WordsToIgnore", svEn.WordsToIgnore),
                    new System.Data.SqlClient.SqlParameter("@DateRegistration", svEn.DateRegistration),
                    new System.Data.SqlClient.SqlParameter("@LastUpdate", svEn.LastUpdate),
                    new System.Data.SqlClient.SqlParameter("@Enabled", svEn.Enabled)
                };
                SamanthaVoiceEn _record;
                DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                    MainProgram.gSQLconexionSamanthaX.Open();
                }

                System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

                if (_reader.HasRows == true) {
                    if (_reader.Read()) {
                        _record = new SamanthaVoiceEn();
                        _record.ExtractData(_reader);
                        MainProgram.gSQLconexionSamanthaX.Close();
                        return _record;
                    }
                }

                MainProgram.gSQLconexionSamanthaX.Close();
            } catch (Exception ex) {
                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Open) {
                    MainProgram.gSQLconexionSamanthaX.Close();
                }
                throw new Exception(ex.Message);
            }
            
            return null;
        }

        #endregion Save
    }
}
