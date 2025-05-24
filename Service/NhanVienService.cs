using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;
using ATBM_HTTT_PH2.Repository;
using ATBM_HTTT_PH2.Util;

namespace ATBM_HTTT_PH2.Service
{
    public class NhanVienService : INhanVienService
    {
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly SessionContext _sessionContext;

        public NhanVienService(INhanVienRepository nhanVienRepository, SessionContext sessionContext)
        {
            _nhanVienRepository = nhanVienRepository;
            _sessionContext = sessionContext;
        }

        public NhanVien GetCurrentUser()
        {
            return _nhanVienRepository.GetById(_sessionContext.CurrentUser);
        }

        public List<NhanVien> GetByDonVi(string madv)
        {
            return _nhanVienRepository.GetByDonVi(madv);
        }

        public void UpdatePhone(string newPhone)
        {
            _nhanVienRepository.UpdatePhone(_sessionContext.CurrentUser, newPhone);
        }

        public void AddNhanVien(NhanVien nhanVien)
        {
            _nhanVienRepository.Add(nhanVien);
        }

        public void UpdateNhanVien(NhanVien nhanVien)
        {
            _nhanVienRepository.Update(nhanVien);
        }

        public void DeleteNhanVien(string manv)
        {
            _nhanVienRepository.Delete(manv);
        }

        public List<NhanVien> GetAll()
        {
            return _nhanVienRepository.getAll();
        }
    }
}
