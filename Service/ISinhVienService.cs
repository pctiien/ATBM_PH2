using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Service
{
    public interface ISinhVienService
    {
        SinhVien GetCurrentSinhVien();

        List<SinhVien> GetAllSinhVien(string currentUser, string role);
        bool UpdateSinhVienInfo(string masv, string diaChi, string sdt);
    }
}