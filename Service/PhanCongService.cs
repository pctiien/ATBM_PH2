using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;
using ATBM_HTTT_PH2.Repository;

namespace ATBM_HTTT_PH2.Service
{
    public class PhanCongService : IPhanCongService
    {
        private readonly IPhanCongRepository _phanCongRepository;

        public PhanCongService(IPhanCongRepository phanCongRepository)
        {
            _phanCongRepository = phanCongRepository;
        }

        public List<DangKyModel> GetDanhSachMonMo()
        {
            return _phanCongRepository.GetMonMo();
        }
        public List<MoMon> GetPhanCongGV(string magv)
        {
            return _phanCongRepository.GetPhanCongGV(magv);
        }

        public List<MoMon> GetPhanCongHocKyHienTai(int hk, int nam)
        {
            return _phanCongRepository.GetPhanCongHocKyHienTai(hk, nam);
        }

        public void AddMoMon(MoMon moMon)
        {
            _phanCongRepository.AddMoMon(moMon);
        }

        public void UpdateMoMon(MoMon moMon)
        {
            _phanCongRepository.UpdateMoMon(moMon);
        }

        public void DeleteMoMon(string mamm)
        {
            _phanCongRepository.DeleteMoMon(mamm);
        }
        public List<MoMon> GetPhanCongTRGDV(string manv)
        {
            return _phanCongRepository.GetPhanCongTRGDV(manv);
        }

        public List<MoMon> GetPhanCongSinhVien(string manv)
        {
            return _phanCongRepository.GetPhanCongSinhVien(manv);
        }

        public List<BangDiemModel> GetBangDiemBySinhVien(string maSV)
        {
            return _phanCongRepository.GetBangDiemBySinhVien(maSV);
        }

        public List<BangDiemModel> GetBangDiemByGiangVien(string maGV)
        {
            return _phanCongRepository.GetBangDiemByGiangVien(maGV);
        }
        public List<BangDiemModel> GetAllBangDiem()
        {
            return _phanCongRepository.GetAllBangDiem();
        }

        public bool UpdateDiem(string maSV, string maMM, int? diemQT, int? diemCK, int? diemTH)
        {
            return _phanCongRepository.UpdateDiem(maSV, maMM, diemQT, diemCK, diemTH);
        }
    }
}