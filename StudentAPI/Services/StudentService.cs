using StudentAPI.Data;
using StudentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentAPI.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;
    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAll()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student?> GetById(int id)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.id == id);
    }

    public async Task<Student> Add(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Student?> Update(int id, Student student)
    {
        var existingStudent = await GetById(id);

        if (existingStudent == null) 
            return null;

        existingStudent.name = student.name;
        existingStudent.age = student.age;
        existingStudent.email = student.email;
        existingStudent.password = student.password;

        await _context.SaveChangesAsync();
        return existingStudent;
    }

    public async Task<bool> Delete(int id)
    {
        var student = await GetById(id);

        if (student == null) 
            return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return true;
    }
}
