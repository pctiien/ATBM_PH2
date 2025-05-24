using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ATBM_HTTT_PH2.Model;
using ATBM_HTTT_PH2.Util;
using Oracle.ManagedDataAccess.Client;

namespace ATBM_HTTT_PH2.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly OracleConnectionFactory _connectionFactory;

        public StudentRepository(OracleConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();

            using (var connection = _connectionFactory.createConnection())
            {
                string query = "SELECT ID, NAME, MAJOR FROM STUDENTS";

                using (var command = new OracleCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Major = reader.GetString(2)
                        });
                    }
                }
            }

            return students;
        }

        public void AddStudent(Student student)
        {
            using (var connection = _connectionFactory.createConnection())
            {
                string query = "INSERT INTO STUDENTS (ID, NAME, MAJOR) VALUES (:id, :name, :major)";

                using (var command = new OracleCommand(query, connection))
                {
                    command.Parameters.Add(new OracleParameter("id", student.Id));
                    command.Parameters.Add(new OracleParameter("name", student.Name));
                    command.Parameters.Add(new OracleParameter("major", student.Major));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
