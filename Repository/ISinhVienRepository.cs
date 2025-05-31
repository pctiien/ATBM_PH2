using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Repository
{
    public interface ISinhVienRepository
    {
        SinhVien GetCurrentSinhVien();
        List<SinhVien> GetAllSinhVien(string currentUser, string role);

        bool UpdateInfo(string masv, string diaChi, string sdt);
    }
}