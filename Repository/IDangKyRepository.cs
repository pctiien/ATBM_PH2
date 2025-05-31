using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Repository
{
    public interface IDangKyRepository
    {
        List<DangKyModel> GetBySinhVien(string maSV);
        void Insert(string maSV, string maMonMo);
        void Delete(string maSV, string maMonMo);
    }


}
