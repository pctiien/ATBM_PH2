using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;
using ATBM_HTTT_PH2.Repository;

namespace ATBM_HTTT_PH2.Service
{
    public class SinhVienService : ISinhVienService
    {
        private readonly ISinhVienRepository _sinhVienRepository;

        public SinhVienService(ISinhVienRepository sinhVienRepository)
        {
            _sinhVienRepository = sinhVienRepository;
        }
        public List<SinhVien> GetAllSinhVien(string currentUser, string role)
        {
            return _sinhVienRepository.GetAllSinhVien(currentUser, role);
        }

        public SinhVien GetCurrentSinhVien()
        {
            return _sinhVienRepository.GetCurrentSinhVien();
        }

        public bool UpdateSinhVienInfo(string masv, string diaChi, string sdt)
        {
            return _sinhVienRepository.UpdateInfo(masv, diaChi, sdt);
        }
    }
}
