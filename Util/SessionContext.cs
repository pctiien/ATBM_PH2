using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace ATBM_HTTT_PH2.Util
{
    public class SessionContext
    {
        private readonly OracleConnection oracleConnection;

        public SessionContext(OracleConnection _oracleConnection)
        {
            oracleConnection = _oracleConnection;
        }

        public string CurrentUser
        {
            get
            {
                using (var command = new OracleCommand("SELECT SYS_CONTEXT('USERENV', 'SESSION_USER') FROM DUAL", oracleConnection))
                {
                    return command.ExecuteScalar()?.ToString();
                }
            }
        }

        public string GetUserRole()
        {
            string sql = "SELECT VAITRO FROM NHANVIEN WHERE MANV = :manv";
            using (var command = new OracleCommand(sql, oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("manv", CurrentUser));
                var result = command.ExecuteScalar();
                Console.WriteLine(result);
                return result?.ToString() ?? "SINHVIEN"; 
            }
        }
    }
}
