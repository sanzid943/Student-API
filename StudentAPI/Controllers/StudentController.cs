using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;
using StudentAPI.Services;

[ApiController]
[Route("api/[controller]")]
namespace StudentAPI.Controllers
{
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService= studentService;
            _mapper= mapper;
        }


        // get: api/student

        [HttpGet]
        public IActionResult GetAll()
        {
            var student = _studentService.GetAll();

            var result = _mapper.Map<List<StudentDto>>(student);

            return Ok(result);
        }


        // get: api/student/1

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _studentService.GetById(id);

            if (student == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<StudentDto>(student);

            return Ok(result);
        }


        // post: api/student

        [HttpGet]
        public IActionResult Add(Student student)
        {
            var result = _studentService.Add(student);

            var dto = _mapper.Map<StudentDto>(result);

            return CreatedAtAction(
                nameof(GetById), 
                new {id= dto.Id}, 
                dto);
        }

        // put: api/student/1

        [HttpPut("{id})")]
        public IActionResult Update(int id, Student student)
        {
            var result = _studentService.Update(id, student);

            if(result==null)
                return NotFound();

            var dto= _mapper.Map<StudentDto>(result);

            return Ok(dto);
        }

        // delete: api/student/1

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _studentService.Delete(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
