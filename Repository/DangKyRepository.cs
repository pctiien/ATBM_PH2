using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Repository
{
    public class DangKyRepository : IDangKyRepository
    {
        private readonly OracleConnection _oracleConnection;

        public DangKyRepository(OracleConnection oracleConnection)
        {
            _oracleConnection = oracleConnection;
        }

        public List<DangKyModel> GetBySinhVien(string maSV)
        {
            var list = new List<DangKyModel>();
            string query = @"
                SELECT DK.MAMM, HP.TENHP, NV.MANV, NV.HOTEN, HP.SOTC
                FROM ATBM_ADMIN.DANGKY DK
                JOIN ATBM_ADMIN.MOMON M ON DK.MAMM = M.MAMM
                JOIN ATBM_ADMIN.HOCPHAN HP ON M.MAHP = HP.MAHP
                JOIN ATBM_ADMIN.NHANVIEN NV ON M.MAGV = NV.MANV
                WHERE NV.VAITRO = 'GV' AND DK.MASV = :masv";

            using (var command = new OracleCommand(query, _oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("masv", maSV));
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

        public void Insert(string maSV, string maMonMo)
        {
            string query = "INSERT INTO ATBM_ADMIN.DANGKY (MASV, MAMM) VALUES (:masv, :mon)";
            using (var command = new OracleCommand(query, _oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("masv", maSV));
                command.Parameters.Add(new OracleParameter("mon", maMonMo));
                command.ExecuteNonQuery();
            }
        }

        public void Delete(string maSV, string maMonMo)
        {
            string query = "DELETE FROM ATBM_ADMIN.DANGKY WHERE MASV = :masv AND MAMM = :mon";
            using (var command = new OracleCommand(query, _oracleConnection))
            {
                command.Parameters.Add(new OracleParameter("masv", maSV));
                command.Parameters.Add(new OracleParameter("mon", maMonMo));
                command.ExecuteNonQuery();
            }
        }
    }
}
