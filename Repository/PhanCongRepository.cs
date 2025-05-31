using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using ATBM_HTTT_PH2.Model;
using System.Data.Common;

namespace ATBM_HTTT_PH2.Repository
{
    public class PhanCongRepository : IPhanCongRepository
    {
        private OracleConnection _oracleConnection;

        public PhanCongRepository(OracleConnection oracleConnection)
        {
            _oracleConnection = oracleConnection;
        }

        public List<DangKyModel> GetMonMo()
        {
            var list = new List<DangKyModel>();
            string query = @"
                SELECT M.MAMM, HP.TENHP, NV.MANV, NV.HOTEN, HP.SOTC
                FROM ATBM_ADMIN.MOMON M
                JOIN ATBM_ADMIN.HOCPHAN HP ON M.MAHP = HP.MAHP
                JOIN ATBM_ADMIN.NHANVIEN NV ON M.MAGV = NV.MANV
                WHERE NV.VAITRO = 'GV'";

            using (var command = new OracleCommand(query, _oracleConnection))
            {
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new DangKyModel
                    {
                        MaMonMo = reader.GetString(0),
                        TenHocPhan = reader.GetString(1),
                        MaGV = reader.GetString(2),
                        HoTenGV = reader.GetString(3),
                        SoTinChi = reader.GetInt32(4)
                    });
                }
            }

            return list;
        }
        public List<MoMon> GetPhanCongGV(string magv)
        {
            List<MoMon> phanCongList = new List<MoMon>();
            try
            {
                string query = "SELECT MAMM, MAHP, MAGV, HK, NAM FROM VW_GV_MOMON WHERE MAGV = :MAGV";
                using (OracleCommand cmd = new OracleCommand(query, _oracleConnection))
                {
                    cmd.Parameters.Add(new OracleParameter("MAGV", magv));
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            phanCongList.Add(new MoMon
                            {
                                MAMM = reader["MAMM"].ToString(),
                                MAHP = reader["MAHP"].ToString(),
                                MAGV = reader["MAGV"].ToString(),
                                HK = Convert.ToInt32(reader["HK"]),
                                NAM = Convert.ToInt32(reader["NAM"])
                            });
                        }
                    }

                }
            }catch(Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy phân công giảng viên: {ex.Message}");
                throw ex;
            }
            return phanCongList;
        }
        public List<MoMon> GetPhanCongHocKyHienTai(int hk, int nam)
        {
            List<MoMon> phanCongList = new List<MoMon>();
            try
            {

                string query = "SELECT MAMM, MAHP, MAGV, HK, NAM FROM VW_MOMON_NV_PDT WHERE HK = :HK AND NAM = :NAM";
                using (OracleCommand cmd = new OracleCommand(query, _oracleConnection))
                {
                    cmd.Parameters.Add(new OracleParameter("HK", hk));
                    cmd.Parameters.Add(new OracleParameter("NAM", nam));
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            phanCongList.Add(new MoMon
                            {
                                MAMM = reader["MAMM"].ToString(),
                                MAHP = reader["MAHP"].ToString(),
                                MAGV = reader["MAGV"].ToString(),
                                HK = Convert.ToInt32(reader["HK"]),
                                NAM = Convert.ToInt32(reader["NAM"])
                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy phân công giảng viên: {ex.Message}");
                throw ex;
            }
            return phanCongList;
        }


        public void AddMoMon(MoMon moMon)
        {
                string query = "INSERT INTO VW_MOMON_NV_PDT (MAMM, MAHP, MAGV, HK, NAM) VALUES (:MAMM, :MAHP, :MAGV, :HK, :NAM)";
                using (OracleCommand cmd = new OracleCommand(query, _oracleConnection))
                {
                    cmd.Parameters.Add(new OracleParameter("MAMM", moMon.MAMM));
                    cmd.Parameters.Add(new OracleParameter("MAHP", moMon.MAHP));
                    cmd.Parameters.Add(new OracleParameter("MAGV", moMon.MAGV));
                    cmd.Parameters.Add(new OracleParameter("HK", moMon.HK));
                    cmd.Parameters.Add(new OracleParameter("NAM", moMon.NAM));
                    cmd.ExecuteNonQuery();
                }
            
        }

        public void UpdateMoMon(MoMon moMon)
        {
                string query = "UPDATE VW_MOMON_NV_PDT SET MAHP = :MAHP, MAGV = :MAGV, HK = :HK, NAM = :NAM WHERE MAMM = :MAMM";
                using (OracleCommand cmd = new OracleCommand(query, _oracleConnection))
                {
                    cmd.Parameters.Add(new OracleParameter("MAHP", moMon.MAHP));
                    cmd.Parameters.Add(new OracleParameter("MAGV", moMon.MAGV));
                    cmd.Parameters.Add(new OracleParameter("HK", moMon.HK));
                    cmd.Parameters.Add(new OracleParameter("NAM", moMon.NAM));
                    cmd.Parameters.Add(new OracleParameter("MAMM", moMon.MAMM));
                    cmd.ExecuteNonQuery();
                }
            
        }

        public void DeleteMoMon(string mamm)
        {

                string query = "DELETE FROM VW_MOMON_NV_PDT WHERE MAMM = :MAMM";
                using (OracleCommand cmd = new OracleCommand(query, _oracleConnection))
                {
                    cmd.Parameters.Add(new OracleParameter("MAMM", mamm));
                    cmd.ExecuteNonQuery();
                }
         
        }

        public List<MoMon> GetPhanCongTRGDV(string manv)
        {
            List<MoMon> phanCongList = new List<MoMon>();

                string query = "SELECT * FROM ATBM_ADMIN.VW_MOMON_TRGDV";
                using (OracleCommand cmd = new OracleCommand(query, _oracleConnection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            phanCongList.Add(new MoMon
                            {
                                MAHP = reader["MAHP"].ToString(),
                                MAGV = reader["MAGV"].ToString(),
                                HK = Convert.ToInt32(reader["HK"]),
                                NAM = Convert.ToInt32(reader["NAM"]),
                                MAMM = reader["MAMM"].ToString()
                            });
                        }
                    }
                }
            
            return phanCongList;
        }

        public List<MoMon> GetPhanCongSinhVien(string masv)
        {
            List<MoMon> phanCongList = new List<MoMon>();
            
                string query = "SELECT MAMM, MAHP, MAGV, HK, NAM FROM ATBM_ADMIN.VW_MOMON_SINHVIEN";
                using (OracleCommand cmd = new OracleCommand(query, _oracleConnection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            phanCongList.Add(new MoMon
                            {
                                MAMM = reader["MAMM"].ToString(),
                                MAHP = reader["MAHP"].ToString(),
                                MAGV = reader["MAGV"].ToString(),
                                HK = Convert.ToInt32(reader["HK"]),
                                NAM = Convert.ToInt32(reader["NAM"])
                            });
                        }
                    }
                }
            
            return phanCongList;
        }

        public List<BangDiemModel> GetBangDiemBySinhVien(string maSV)
        {
            var list = new List<BangDiemModel>();
            string query = "SELECT MASV, HOTEN, MAMM, MAHP, TENHP, SOTC, DIEMQT, DIEMCK, DIEMTH, DIEMTK, HK, NAM FROM ATBM_ADMIN.VW_BANGDIEM WHERE MASV = :masv";

            using (var cmd = new OracleCommand(query, _oracleConnection))
            {
                cmd.Parameters.Add(new OracleParameter("masv", maSV));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BangDiemModel
                        {
                            MaSV = reader.GetString(0),
                            HoTen = reader.GetString(1),
                            MaMM = reader.GetString(2),
                            MaHP = reader.GetString(3),
                            TenHocPhan = reader.GetString(4),
                            SoTinChi = reader.GetInt32(5),
                            DiemQT = reader.IsDBNull(6) ? null : reader.GetInt32(6),
                            DiemCK = reader.IsDBNull(7) ? null : reader.GetInt32(7),
                            DiemTH = reader.IsDBNull(8) ? null : reader.GetInt32(8),
                            DiemTK = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                            HocKy = reader.GetInt32(10),
                            Nam = reader.GetInt32(11)
                        });
                    }
                }
            }
            return list;
        }

        public List<BangDiemModel> GetBangDiemByGiangVien(string maGV)
        {
            var list = new List<BangDiemModel>();
            string query = "SELECT MASV, HOTEN, MAMM, MAHP, TENHP, SOTC, DIEMQT, DIEMCK, DIEMTH, DIEMTK, HK, NAM FROM ATBM_ADMIN.VW_BANGDIEM_GV WHERE MAGV = :magv";

            using (var cmd = new OracleCommand(query, _oracleConnection))
            {
                cmd.Parameters.Add(new OracleParameter("magv", maGV));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BangDiemModel
                        {
                            MaSV = reader.GetString(0),
                            HoTen = reader.GetString(1),
                            MaMM = reader.GetString(2),
                            MaHP = reader.GetString(3),
                            TenHocPhan = reader.GetString(4),
                            SoTinChi = reader.GetInt32(5),
                            DiemQT = reader.IsDBNull(6) ? null : reader.GetInt32(6),
                            DiemCK = reader.IsDBNull(7) ? null : reader.GetInt32(7),
                            DiemTH = reader.IsDBNull(8) ? null : reader.GetInt32(8),
                            DiemTK = reader.IsDBNull(9) ? null : reader.GetInt32(9),
                            HocKy = reader.GetInt32(10),
                            Nam = reader.GetInt32(11)
                        });
                    }
                }
            }
            return list;
        }

        public bool UpdateDiem(string maSV, string maMM, int? diemQT, int? diemCK, int? diemTH)
        {
            string query = "UPDATE ATBM_ADMIN.DANGKY SET DIEMQT = :qt, DIEMCK = :ck, DIEMTH = :th WHERE MASV = :masv AND MAMM = :mamm";

            using (var cmd = new OracleCommand(query, _oracleConnection))
            {
                cmd.Parameters.Add(new OracleParameter("qt", diemQT.HasValue ? (object)diemQT.Value : DBNull.Value));
                cmd.Parameters.Add(new OracleParameter("ck", diemCK.HasValue ? (object)diemCK.Value : DBNull.Value));
                cmd.Parameters.Add(new OracleParameter("th", diemTH.HasValue ? (object)diemTH.Value : DBNull.Value));
                cmd.Parameters.Add(new OracleParameter("masv", maSV));
                cmd.Parameters.Add(new OracleParameter("mamm", maMM));
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<BangDiemModel> GetAllBangDiem()
        {
            List<BangDiemModel> list = new List<BangDiemModel>();
            string query = @"
                SELECT DK.MASV, SV.HOTEN, DK.MAMM, M.MAHP, HP.TENHP, HP.SOTC,
                        DK.DIEMQT, DK.DIEMCK, DK.DIEMTK, DK.DIEMTH, M.HK, M.NAM
                FROM ATBM_ADMIN.DANGKY DK
                JOIN ATBM_ADMIN.SINHVIEN SV ON DK.MASV = SV.MASV
                JOIN ATBM_ADMIN.MOMON M ON DK.MAMM = M.MAMM
                JOIN ATBM_ADMIN.HOCPHAN HP ON M.MAHP = HP.MAHP
    ";

            using (OracleCommand cmd = new OracleCommand(query, _oracleConnection))
            {
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new BangDiemModel
                        {
                            MaSV = reader.GetString(0),
                            HoTen = reader.GetString(1),
                            MaMM = reader.GetString(2),
                            MaHP = reader.GetString(3),
                            TenHocPhan = reader.GetString(4),
                            SoTinChi = reader.GetInt32(5),
                            DiemQT = reader.IsDBNull(6) ? null : (int?)reader.GetInt32(6),
                            DiemCK = reader.IsDBNull(7) ? null : (int?)reader.GetInt32(7),
                            DiemTK = reader.IsDBNull(8) ? null : (int?)reader.GetInt32(8),
                            DiemTH = reader.IsDBNull(9) ? null : (int?)reader.GetInt32(9),
                            HocKy = reader.GetInt32(10),
                            Nam = reader.GetInt32(11)
                        });
                    }
                }
            }

            return list;
        }


    }
}