using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Service
{
    public interface IPhanCongService
    {
        List<MoMon> GetPhanCongGV(string magv);
        List<MoMon> GetPhanCongHocKyHienTai(int hk, int nam);
        List<MoMon> GetPhanCongTRGDV(string manv);
        List<MoMon> GetPhanCongSinhVien(string manv);

        void AddMoMon(MoMon moMon);
        void UpdateMoMon(MoMon moMon);
        void DeleteMoMon(string mamm);
    }
}