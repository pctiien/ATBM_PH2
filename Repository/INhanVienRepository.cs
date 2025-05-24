using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Repository
{
    public interface INhanVienRepository
    {

        List<NhanVien> getAll();
        NhanVien GetById(string manv);
        List<NhanVien> GetByDonVi(string madv);
        void UpdatePhone(string manv, string newPhone);
        void Add(NhanVien nhanVien);
        void Update(NhanVien nhanVien);
        void Delete(string manv);
    }
}