using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Repository
{
    public class PhanCongRepository : IPhanCongRepository
    {
        private OracleConnection _oracleConnection;

        public PhanCongRepository(OracleConnection oracleConnection)
        {
            _oracleConnection = oracleConnection;
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

    }
}