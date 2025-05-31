using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Service
{
    public interface IDangKyService
    {
        List<DangKyModel> GetDangKyBySinhVien(string maSV);
        void DangKyMonHoc(string maSV, string maMonMo);
        void HuyDangKyMonHoc(string maSV, string maMonMo);
    }

}
