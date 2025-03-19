using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using smart_class.Api.Entities;
using smart_class.Core.DTOs;
using smart_class.Core.Entities;
using smart_class.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smart_class.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> Get()
        {
            IEnumerable<Student> students = await _studentService.GetStudentsAsync();
            return Ok(_mapper.Map<IEnumerable<StudentDto>>(students));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> Get(int id)
        {
            Student? student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
                return NotFound();
            return Ok(_mapper.Map<StudentDto>(student));
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Post([FromBody] StudentPostPut studentPost)
        {
            if (studentPost == null)
                return BadRequest("Student cannot be null.");
            Student addedStudent = await _studentService.AddStudentAsync(new Student { Name = studentPost.Name });
            return CreatedAtAction(nameof(Get), new { id = addedStudent.Id }, addedStudent);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> Put(int id, [FromBody] StudentPostPut studentPut)
        {
            if (studentPut == null)
                return BadRequest("Student cannot be null.");
            Student? updatedStudent = await _studentService.UpdateStudentAsync(id, new Student { Name = studentPut.Name });
            if (updatedStudent == null)
                return NotFound();
            return Ok(updatedStudent);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            Student? deletedStudent = await _studentService.DeleteAsync(id);
            if (deletedStudent == null)
                return NotFound();
            return Ok(deletedStudent);
        }
    }
}