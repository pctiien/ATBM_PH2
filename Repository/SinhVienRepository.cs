using System;
using Oracle.ManagedDataAccess.Client;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Repository
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private readonly OracleConnection _oracleConnection;

        public SinhVienRepository(OracleConnection oracleConnection)
        {
            _oracleConnection = oracleConnection;
        }

        public SinhVien GetCurrentSinhVien()
        {
            SinhVien sinhVien = null;

                string query = "SELECT * FROM SINHVIEN WHERE MASV = SYS_CONTEXT('USERENV', 'SESSION_USER')";
                using (OracleCommand cmd = new OracleCommand(query, _oracleConnection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sinhVien = new SinhVien
                            {
                                MASV = reader["MASV"].ToString(),
                                HOTEN = reader["HOTEN"].ToString(),
                                PHAI = reader["PHAI"].ToString(),
                                NGSINH = Convert.ToDateTime(reader["NGSINH"]),
                                DCHI = reader["DCHI"].ToString(),
                                SDT = reader["DT"].ToString(),
                                TINHTRANG = reader["TINHTRANG"].ToString(),
                                KHOA = reader["KHOA"].ToString()
                            };
                        }
                    }
                
                }
            return sinhVien;
        }
    }
}