using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Service
{
    public interface IStudentService
    {
        List<Student> GetStudents();
        void CreateStudent(Student student);
    }
}
