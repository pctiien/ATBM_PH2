using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;

namespace ATBM_HTTT_PH2.Repository
{
    public interface IStudentRepository
    {
        List<Student> GetAllStudents();
        void AddStudent(Student student);
    }
}
