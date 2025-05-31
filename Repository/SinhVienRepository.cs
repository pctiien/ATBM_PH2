using System;
using Oracle.ManagedDataAccess.Client;
using ATBM_HTTT_PH2.Model;
using System.Data.Common;
using System.Data;

namespace ATBM_HTTT_PH2.Repository
{
    public class SinhVienRepository : ISinhVienRepository
    {
        private readonly OracleConnection _oracleConnection;
        private OracleConnection _connection;

        public SinhVienRepository(OracleConnection oracleConnection)
        {
            _oracleConnection = oracleConnection;
        }

        public SinhVien GetCurrentSinhVien()
        {
            var username = GetCurrentUsername();
            string query = "SELECT * FROM ATBM_ADMIN.SINHVIEN WHERE MASV = :masv";
            using (var command = new OracleCommand(query, _oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("masv", username));
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new SinhVien
                        {
                            MASV = reader.GetString(0),
                            HOTEN = reader.GetString(1),
                            PHAI = reader.GetString(2),
                            NGSINH = reader.GetDateTime(3),
                            DCHI = reader.GetString(4),
                            SDT = reader.GetString(5),
                            KHOA = reader.GetString(6),
                            TINHTRANG = reader.IsDBNull(7) ? null : reader.GetString(7)
                        };
                    }
                }
            }
            return null;
        }

        public List<SinhVien> GetAllSinhVien(string currentUser, string role)
        {
            var list = new List<SinhVien>();
            string query = "";
            role = role?.Trim().ToUpper();

            if (role == "SINHVIEN")
            {
                query = "SELECT * FROM ATBM_ADMIN.SINHVIEN WHERE MASV = :user";
                using (var command = new OracleCommand(query, _oracleConnection))
                {
                    command.Parameters.Add(new OracleParameter("user", currentUser));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SinhVien
                            {
                                MASV = reader.GetString(0),
                                HOTEN = reader.GetString(1),
                                PHAI = reader.GetString(2),
                                NGSINH = reader.GetDateTime(3),
                                DCHI = reader.GetString(4),
                                SDT = reader.GetString(5),
                                KHOA = reader.GetString(6),
                                TINHTRANG = reader.IsDBNull(7) ? null : reader.GetString(7)
                            });
                        }
                    }
                }
            }
            else if (role == "GV")
            {
                // Lấy mã khoa của giảng viên để lọc sinh viên trong khoa
                string khoaQuery = "SELECT MADV FROM ATBM_ADMIN.NHANVIEN WHERE MANV = :user";
                string maKhoa = "";

                using (var khoaCmd = new OracleCommand(khoaQuery, _oracleConnection))
                {
                    khoaCmd.Parameters.Add(new OracleParameter("user", currentUser));
                    var result = khoaCmd.ExecuteScalar();
                    if (result != null)
                        maKhoa = result.ToString();
                }

                query = "SELECT * FROM ATBM_ADMIN.SINHVIEN WHERE KHOA = :khoa";
                using (var command = new OracleCommand(query, _oracleConnection))
                {
                    command.Parameters.Add(new OracleParameter("khoa", maKhoa));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SinhVien
                            {
                                MASV = reader.GetString(0),
                                HOTEN = reader.GetString(1),
                                PHAI = reader.GetString(2),
                                NGSINH = reader.GetDateTime(3),
                                DCHI = reader.GetString(4),
                                SDT = reader.GetString(5),
                                KHOA = reader.GetString(6),
                                TINHTRANG = reader.IsDBNull(7) ? null : reader.GetString(7)
                            });
                        }
                    }
                }
            }
            else // NV PCTSV, NV PĐT, Admin, v.v.
            {
                query = "SELECT * FROM ATBM_ADMIN.SINHVIEN";
                using (var command = new OracleCommand(query, _oracleConnection))
                {
                    if (query.Contains(":user"))
                        command.Parameters.Add(new OracleParameter("user", currentUser));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new SinhVien
                            {
                                MASV = reader.GetString(0),
                                HOTEN = reader.GetString(1),
                                PHAI = reader.GetString(2),
                                NGSINH = reader.GetDateTime(3),
                                DCHI = reader.GetString(4),
                                SDT = reader.GetString(5),
                                KHOA = reader.GetString(6),
                                TINHTRANG = reader.IsDBNull(7) ? null : reader.GetString(7)
                            });
                        }
                    }
                }
            }
            return list;
        }

        public bool UpdateInfo(string masv, string diaChi, string sdt)
        {
            string query = "UPDATE ATBM_ADMIN.SINHVIEN SET DCHI = :diachi, DT = :sdt WHERE MASV = :masv";
            using (var command = new OracleCommand(query, _oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("diachi", diaChi));
                command.Parameters.Add(new OracleParameter("sdt", sdt));
                command.Parameters.Add(new OracleParameter("masv", masv));
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        private string GetCurrentUsername()
        {
            using (var command = new OracleCommand("SELECT SYS_CONTEXT('USERENV', 'SESSION_USER') FROM DUAL", _oracleConnection))
            {
                return command.ExecuteScalar()?.ToString();
            }
        }

        private SinhVien ExtractSinhVien(OracleDataReader reader)
        {
            return new SinhVien
            {
                MASV = reader.GetString(0),
                HOTEN = reader.GetString(1),
                PHAI = reader.GetString(2),
                NGSINH = reader.GetDateTime(3),
                DCHI = reader.GetString(4),
                SDT = reader.GetString(5),
                KHOA = reader.GetString(6),
                TINHTRANG = reader.IsDBNull(7) ? null : reader.GetString(7)
            };
        }
    }
}