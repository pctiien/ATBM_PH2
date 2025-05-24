using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;
using ATBM_HTTT_PH2.Repository;
using ATBM_HTTT_PH2.Util;
using System.Globalization;
using System.Data;

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
            try
            {
                string sql = @"INSERT INTO NHANVIEN (MANV, HOTEN, PHAI, NGSINH, LUONG, PHUCAP, DT, VAITRO, MADV)
                              VALUES (:manv, :hoten, :phai, :ngsinh, :luong, :phucap, :dt, :vaitro, :madv)";
                using (var command = new OracleCommand(sql, oracleConnection))
                {
                    command.Parameters.Add("manv", OracleDbType.NVarchar2).Value = nhanVien.MANV.Trim();
                    command.Parameters.Add("hoten", OracleDbType.NVarchar2).Value = nhanVien.HOTEN;
                    command.Parameters.Add("phai", OracleDbType.NVarchar2).Value = String.IsNullOrEmpty(nhanVien.PHAI) ? "Nam" : nhanVien.PHAI;
                    command.Parameters.Add(new OracleParameter("ngsinh", nhanVien.NGSINH?.ToString("dd-MMM-yy", CultureInfo.InvariantCulture)));
                    command.Parameters.Add("luong", OracleDbType.Decimal).Value = nhanVien.LUONG;
                    command.Parameters.Add("phucap", OracleDbType.Decimal).Value = nhanVien.PHUCAP;
                    command.Parameters.Add("dt", OracleDbType.NVarchar2).Value = nhanVien.DT;
                    command.Parameters.Add("vaitro", OracleDbType.NVarchar2).Value = nhanVien.VAITRO;
                    command.Parameters.Add("madv", OracleDbType.NVarchar2).Value = nhanVien.MADV;
                    command.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void Update(NhanVien nhanVien)
        {
            try
            {
                string sql = @"UPDATE NHANVIEN 
               SET HOTEN = :hoten, PHAI = :phai, NGSINH = :ngsinh, 
                   LUONG = :luong, PHUCAP = :phucap, DT = :dt, 
                   VAITRO = :vaitro, MADV = :madv
               WHERE MANV = :manv";

                using (var command = new OracleCommand(sql, oracleConnection))
                {
                    if (oracleConnection.State != ConnectionState.Open)
                        oracleConnection.Open();

                    command.BindByName = true;

                    command.Parameters.Add("hoten", OracleDbType.NVarchar2).Value = nhanVien.HOTEN;
                    command.Parameters.Add("phai", OracleDbType.NVarchar2).Value = string.IsNullOrEmpty(nhanVien.PHAI) ? "Nam" : nhanVien.PHAI;
                    command.Parameters.Add("ngsinh", OracleDbType.Date).Value = nhanVien.NGSINH ?? (object)DBNull.Value;
                    command.Parameters.Add("luong", OracleDbType.Decimal).Value = nhanVien.LUONG ?? (object)DBNull.Value;
                    command.Parameters.Add("phucap", OracleDbType.Decimal).Value = nhanVien.PHUCAP ?? (object)DBNull.Value;
                    command.Parameters.Add("dt", OracleDbType.NVarchar2).Value = nhanVien.DT ?? (object)DBNull.Value;
                    command.Parameters.Add("vaitro", OracleDbType.NVarchar2).Value = nhanVien.VAITRO ?? (object)DBNull.Value;
                    command.Parameters.Add("madv", OracleDbType.NVarchar2).Value = nhanVien.MADV ?? (object)DBNull.Value;
                    command.Parameters.Add("manv", OracleDbType.NVarchar2).Value = nhanVien.MANV ?? "NV001";

                    int affectedRows = command.ExecuteNonQuery();
                    Console.WriteLine($"Số dòng bị ảnh hưởng: {affectedRows}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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