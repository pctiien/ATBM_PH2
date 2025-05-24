using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Service
{
    public interface INhanVienService
    {
        NhanVien GetCurrentUser();
        List<NhanVien> GetByDonVi(string madv);

        List<NhanVien> GetAll();
        void UpdatePhone(string newPhone);
        void AddNhanVien(NhanVien nhanVien);
        void UpdateNhanVien(NhanVien nhanVien);
        void DeleteNhanVien(string manv);
    }
}
