using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATBM_HTTT_PH2.Model;
using ATBM_HTTT_PH2.Repository;

namespace ATBM_HTTT_PH2.Service
{
    public class DangKyService : IDangKyService
    {
        private readonly IDangKyRepository _repository;

        public DangKyService(IDangKyRepository repository)
        {
            _repository = repository;
        }

        public List<DangKyModel> GetDangKyBySinhVien(string maSV)
        {
            return _repository.GetBySinhVien(maSV);
        }

        public void DangKyMonHoc(string maSV, string maMonMo)
        {
            _repository.Insert(maSV, maMonMo);
        }

        public void HuyDangKyMonHoc(string maSV, string maMonMo)
        {
            _repository.Delete(maSV, maMonMo);
        }
    }
}
