using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;
using ATBM_HTTT_PH2.Repository;
using ATBM_HTTT_PH2.Util;

namespace UniversityManagementSystem.Repositories
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly OracleConnection oracleConnection;

        public NhanVienRepository(OracleConnection _oracleConnection)
        {
            oracleConnection = _oracleConnection;
        }

        public NhanVien GetById(string manv)
        {
            string sql = "SELECT * FROM VW_NVCB_NHANVIEN WHERE MANV = :manv";
            using (var command = new OracleCommand(sql,oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("manv", manv));
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new NhanVien
                        {
                            MANV = reader["MANV"].ToString(),
                            HOTEN = reader["HOTEN"].ToString(),
                            PHAI = reader["PHAI"].ToString(),
                            NGSINH = reader["NGSINH"] != DBNull.Value ? Convert.ToDateTime(reader["NGSINH"]) : null,
                            LUONG = reader["LUONG"] != DBNull.Value ? Convert.ToDecimal(reader["LUONG"]) : null,
                            PHUCAP = reader["PHUCAP"] != DBNull.Value ? Convert.ToDecimal(reader["PHUCAP"]) : null,
                            DT = reader["DT"].ToString(),
                            VAITRO = reader["VAITRO"].ToString(),
                            MADV = reader["MADV"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public List<NhanVien> GetByDonVi(string madv)
        {
            var nhanViens = new List<NhanVien>();
            string sql = "SELECT * FROM VW_TRGDV_NHANVIEN WHERE MADV = :madv";
            using (var command = new OracleCommand(sql, oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("madv", madv));
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nhanViens.Add(new NhanVien
                        {
                            MANV = reader["MANV"].ToString(),
                            HOTEN = reader["HOTEN"].ToString(),
                            PHAI = reader["PHAI"].ToString(),
                            NGSINH = reader["NGSINH"] != DBNull.Value ? Convert.ToDateTime(reader["NGSINH"]) : null,
                            DT = reader["DT"].ToString(),
                            VAITRO = reader["VAITRO"].ToString(),
                            MADV = reader["MADV"].ToString()
                        });
                    }
                }
            }
            return nhanViens;
        }

        public void UpdatePhone(string manv, string newPhone)
        {
            string sql = "BEGIN PKG_NHANVIEN.UPDATE_PHONE(:manv, :newPhone); END;";
            using (var command = new OracleCommand(sql, oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("manv", manv));
                command.Parameters.Add(new OracleParameter("newPhone", newPhone));
                command.ExecuteNonQuery();
            }
        }

        public void Add(NhanVien nhanVien)
        {
            string sql = @"INSERT INTO NHANVIEN (MANV, HOTEN, PHAI, NGSINH, LUONG, PHUCAP, DT, VAITRO, MADV)
                              VALUES (:manv, :hoten, :phai, :ngsinh, :luong, :phucap, :dt, :vaitro, :madv)";
            using (var command = new OracleCommand(sql, oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("manv", nhanVien.MANV));
                command.Parameters.Add(new OracleParameter("hoten", nhanVien.HOTEN));
                command.Parameters.Add(new OracleParameter("phai", nhanVien.PHAI));
                command.Parameters.Add(new OracleParameter("ngsinh", nhanVien.NGSINH));
                command.Parameters.Add(new OracleParameter("luong", nhanVien.LUONG));
                command.Parameters.Add(new OracleParameter("phucap", nhanVien.PHUCAP));
                command.Parameters.Add(new OracleParameter("dt", nhanVien.DT));
                command.Parameters.Add(new OracleParameter("vaitro", nhanVien.VAITRO));
                command.Parameters.Add(new OracleParameter("madv", nhanVien.MADV));
                command.ExecuteNonQuery();
            }
        }

        public void Update(NhanVien nhanVien)
        {
            string sql = @"UPDATE NHANVIEN 
                              SET HOTEN = :hoten, PHAI = :phai, NGSINH = :ngsinh, 
                                  LUONG = :luong, PHUCAP = :phucap, DT = :dt, 
                                  VAITRO = :vaitro, MADV = :madv
                              WHERE MANV = :manv";
            using (var command = new OracleCommand(sql, oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("manv", nhanVien.MANV));
                command.Parameters.Add(new OracleParameter("hoten", nhanVien.HOTEN));
                command.Parameters.Add(new OracleParameter("phai", nhanVien.PHAI));
                command.Parameters.Add(new OracleParameter("ngsinh", nhanVien.NGSINH));
                command.Parameters.Add(new OracleParameter("luong", nhanVien.LUONG));
                command.Parameters.Add(new OracleParameter("phucap", nhanVien.PHUCAP));
                command.Parameters.Add(new OracleParameter("dt", nhanVien.DT));
                command.Parameters.Add(new OracleParameter("vaitro", nhanVien.VAITRO));
                command.Parameters.Add(new OracleParameter("madv", nhanVien.MADV));
                command.ExecuteNonQuery();
            }
        }

        public void Delete(string manv)
        {
            string sql = "DELETE FROM NHANVIEN WHERE MANV = :manv";
            using (var command = new OracleCommand(sql, oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("manv", manv));
                command.ExecuteNonQuery();
            }
        }

        public List<NhanVien> getAll()
        {
            var nhanViens = new List<NhanVien>();
            string sql = "SELECT * FROM NHANVIEN";
            using (var command = new OracleCommand(sql, oracleConnection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nhanViens.Add(new NhanVien
                        {
                            MANV = reader["MANV"].ToString(),
                            HOTEN = reader["HOTEN"].ToString(),
                            PHAI = reader["PHAI"].ToString(),
                            NGSINH = reader["NGSINH"] != DBNull.Value ? Convert.ToDateTime(reader["NGSINH"]) : null,
                            DT = reader["DT"].ToString(),
                            VAITRO = reader["VAITRO"].ToString(),
                            MADV = reader["MADV"].ToString(),
                            LUONG = Decimal.Parse(reader["LUONG"].ToString()),
                            PHUCAP = Decimal.Parse(reader["PHUCAP"].ToString())
                        });
                    }
                }
            }
            return nhanViens;
        }
    }
}