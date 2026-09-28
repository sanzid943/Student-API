using StudentAPI.Models;

namespace StudentAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly List<Student> _students = new()
        {
            new Student
            {
                id= 100,
                name= "Rahim",
                email= "rahim@gmail.com",
                age= 20,
                password= "12345"
            },

            new Student
            {
                id= 101,
                name= "Karim",
                email= "karim@gmail.com",
                age= 25,
                password= "abcdef"
            }
        };

        public List<Student> GetAll()
        {
            return _students;
        }

        public Student? GetById(int id)
        {
            return _students.FirstOrDefault(x=> x.id == id);
        }

        public Student Add(Student student)
        {
            int newId= _students.Count== 0 ? 1 : _students.Max(x=> x.Id) + 1;
            
            student.id = newId;
            
            _students.Add(student);
            
            return student;
        }

        public Student? Update(int id, Student student)
        {
            var existingStudent = GetById(id);

            if (existingStudent == null) 
                return null;

            existingStudent.name = student.name;
            existingStudent.age = student.age;
            existingStudent.email = student.email;
            existingStudent.password = student.password;

            return existingStudent;
        }

        public bool Delete(int id)
        {
            var student = GetById(id);

            if (student == null) 
                return false;

            _students.Remove(student);

            return true;
        }
    }
}
