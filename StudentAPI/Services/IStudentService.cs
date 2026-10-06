using StudentAPI.Models;

namespace StudentAPI.Services;

public interface IStudentService
{
    Task<List<Student>> GetAll();
    Task<Student?> GetById(int id);
    Task<Student> Add(Student student);
    Task<Student?> Update(int id, Student student);
    Task<bool> Delete(int id);
}
