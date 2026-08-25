using Samantha.Repository;
using SamanthaX.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamanthaX.Repository {
    public class RecognizedInstructionRepository {


        #region GetId

        /// <summary>
        ///     
        /// </summary>s
        /// <returns></returns>
        /// <history>
        public static RecognizedInstructionEn GetId(int recognizedInstructionId) {

            string _query = "[dbo].[RecognizedInstruction_GetId]";
            System.Data.SqlClient.SqlParameter[] Params = { new System.Data.SqlClient.SqlParameter("@RecognizedInstructionId", recognizedInstructionId) };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                RecognizedInstructionEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new RecognizedInstructionEn();
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
        public static List<RecognizedInstructionEn> GetAll(int userId) {

            string _query = "[dbo].[RecognizedInstruction_GetAll]";
            System.Data.SqlClient.SqlParameter[] Params = { new System.Data.SqlClient.SqlParameter("@UserId", userId) };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                List<RecognizedInstructionEn> Registers = new List<RecognizedInstructionEn>();
                RecognizedInstructionEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new RecognizedInstructionEn();
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
        public static RecognizedInstructionEn Save(RecognizedInstructionEn oRecIns) {
            try {
                string _query = "[dbo].[RecognizedInstruction_Save]";
                System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@RecognizedInstructionId", oRecIns.RecognizedInstructionId),
                new System.Data.SqlClient.SqlParameter("@InstructionTypeId", 1),    //oRecIns.InstructionTypeId),
                new System.Data.SqlClient.SqlParameter("@UserId", oRecIns.UserId),
                new System.Data.SqlClient.SqlParameter("@LanguageId", oRecIns.LanguageId),
                new System.Data.SqlClient.SqlParameter("@GrammarId", oRecIns.GrammarId),
                new System.Data.SqlClient.SqlParameter("@ProjectId", 1),    //oRecIns.ProjectId),
                new System.Data.SqlClient.SqlParameter("@Code", oRecIns.Code),
                new System.Data.SqlClient.SqlParameter("@Instruction", oRecIns.Instruction),
                new System.Data.SqlClient.SqlParameter("@Description", oRecIns.Description),
                new System.Data.SqlClient.SqlParameter("@VoiceProcessing", oRecIns.VoiceProcessing),
                new System.Data.SqlClient.SqlParameter("@VoiceSolution", oRecIns.VoiceSolution),
                new System.Data.SqlClient.SqlParameter("@VoiceCancel", oRecIns.VoiceCancel),
                new System.Data.SqlClient.SqlParameter("@VoiceFail", oRecIns.VoiceFail),
                new System.Data.SqlClient.SqlParameter("@DateRegistration", oRecIns.DateRegistration),
                new System.Data.SqlClient.SqlParameter("@LastUpdate", oRecIns.LastUpdate),
                new System.Data.SqlClient.SqlParameter("@Enabled", oRecIns.Enabled)
            };
                RecognizedInstructionEn _record;
                DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();

                if (MainProgram.gSQLconexionSamanthaX.State == System.Data.ConnectionState.Closed) {
                    MainProgram.gSQLconexionSamanthaX.Open();
                }

                System.Data.SqlClient.SqlDataReader _reader = _connection.InsertUpdateDelete(MainProgram.gSQLconexionSamanthaX, _query, Params);

                if (_reader.HasRows == true) {
                    if (_reader.Read()) {
                        _record = new RecognizedInstructionEn();
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
        public static RecognizedInstructionEn Enable(int recognizedInstructionId, bool enabled) {

            string _query = "[dbo].[RecognizedInstruction_Enable]";
            System.Data.SqlClient.SqlParameter[] Params = {
                new System.Data.SqlClient.SqlParameter("@RecognizedInstructionId", recognizedInstructionId),
                new System.Data.SqlClient.SqlParameter("@Enabled", enabled),
            };
            DataConnection.ConnectionSQL _connection = new DataConnection.ConnectionSQL();
            System.Data.DataTable _data = _connection.SelectFromStoreProcedure(MainProgram.gSQLconexionSamanthaX, _query, Params);

            if (_data == null) {
                //  Records no found
                return null;
            } else {
                RecognizedInstructionEn Register;

                foreach (System.Data.DataRow _row in _data.Rows) {
                    Register = new RecognizedInstructionEn();
                    Register.ExtractData(_row);
                    return Register;
                }
                return null;
            }
        }

        #endregion Enable
    }
}
