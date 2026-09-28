using StudentAPI.Models;

namespace StudentAPI.Services
{
    public interface IStudentService
    {
        List<Student> GetAll();
        Student? GetStudent(int id);
        Student Add(Student student);
        Student? Update(int id, Student student);)
        bool Delete(int  id);
    }
}
