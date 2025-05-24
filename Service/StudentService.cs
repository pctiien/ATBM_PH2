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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepo = studentRepository;
        }

        public List<Student> GetStudents()
        {
            return _studentRepo.GetAllStudents();
        }

        public void CreateStudent(Student student)
        {
            _studentRepo.AddStudent(student);
        }
    }
}
