using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.DTOs;
using StudentAPI.Models;
using StudentAPI.Services;

namespace StudentApi.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly IMapper _mapper;

    public StudentController(
        IStudentService studentService,
        IMapper mapper)
    {
        _studentService = studentService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentService.GetAll();

        var result = _mapper.Map<List<StudentDto>>(students);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _studentService.GetById(id);

        if (student == null)
            return NotFound();

        var result = _mapper.Map<StudentDto>(student);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Student student)
    {
        var result = await _studentService.Add(student);

        var dto = _mapper.Map<StudentDto>(result);

        return CreatedAtAction(
            nameof(GetById),
            new { id = dto.id },
            dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Student student)
    {
        var result = await _studentService.Update(id, student);

        if (result == null)
            return NotFound();

        var dto = _mapper.Map<StudentDto>(result);

        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _studentService.Delete(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}