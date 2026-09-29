using AutoMapper;
using StudentAPI.DTOs;
using StudentAPI.Models;

namespace StudentAPI.Mappings;

public class StudentProfile : Profile
{
    public StudentProfile()
    {
        CreateMap<Student, StudentDto>();
    }
}
