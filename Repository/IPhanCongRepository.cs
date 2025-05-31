using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Repository
{
    public interface IPhanCongRepository
    {
        List<MoMon> GetPhanCongGV(string magv);
        List<MoMon> GetPhanCongHocKyHienTai(int hk, int nam);
        List<MoMon> GetPhanCongTRGDV(string manv);
        List<MoMon> GetPhanCongSinhVien(string masv);

        List<DangKyModel> GetMonMo();
        void AddMoMon(MoMon moMon);
        void UpdateMoMon(MoMon moMon);
        void DeleteMoMon(string mamm);

        List<BangDiemModel> GetBangDiemBySinhVien(string maSV);
        List<BangDiemModel> GetBangDiemByGiangVien(string maGV);

        List<BangDiemModel> GetAllBangDiem();
        bool UpdateDiem(string maSV, string maMM, int? diemQT, int? diemCK, int? diemTH);
    
    }
}