using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> GetAll()
        {
            IEnumerable<Teacher> teachers = await _teacherService.GetTeachersAsync();
            return Ok(_mapper.Map<IEnumerable<TeacherDto>>(teachers));
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> Get(int id)
        {
            Teacher? teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound();
            return Ok(_mapper.Map<TeacherDto>(teacher));
        }

        //[Authorize(Roles = "Teacher")]
        [HttpGet("me")]
        public async Task<ActionResult<TeacherDto>> GetMe()
        {
            var userId = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID cannot be null or empty.");

            if (!int.TryParse(userId, out int id))
                return BadRequest("User ID is not a valid integer.");

            Teacher? teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound();
            return Ok(_mapper.Map<TeacherDto>(teacher));
        }

        [Authorize(Roles = "Teacher")]
        [HttpGet("myCourses")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetMyCourses()
        {
            var userId = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID cannot be null or empty.");

            if (!int.TryParse(userId, out int id))
                return BadRequest("User ID is not a valid integer.");

            Teacher? teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound();
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(teacher.Courses));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Teacher>> Post([FromBody] TeacherPostPut teacherPost)
        {
            if (teacherPost == null)
                return BadRequest("Teacher cannot be null.");

            Teacher addedTeacher = await _teacherService.AddTeacherAsync(new Teacher
            {
                Name = teacherPost.Name,
                Password = "1234",
                Email = teacherPost.Email,
                InstitutionId = 2, // או להחליף עם InstitutionId אמיתי
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
            return CreatedAtAction(nameof(Get), new { id = addedTeacher.Id }, addedTeacher);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Teacher>> Put(int id, [FromBody] TeacherPostPut teacherPut)
        {
            if (teacherPut == null)
                return BadRequest("Teacher cannot be null.");

            Teacher? teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound();

            var institutionId = User.FindFirst("InstitutionId")?.Value;
            if (string.IsNullOrEmpty(institutionId) || !int.TryParse(institutionId, out int instId) || teacher.InstitutionId != instId)
                return Forbid();

            Teacher? updatedTeacher = await _teacherService.UpdateTeacherAsync(id, new Teacher
            {
                Name = teacherPut.Name,
                Email = teacherPut.Email,
                UpdatedAt = DateTime.Now
            });
            return Ok(updatedTeacher);
        }

        //[Authorize(Roles = "Teacher")]
        [HttpPut("me")]
        public async Task<ActionResult<Teacher>> PutMe([FromBody] string password)
        {
            var userId = User.FindFirst("Id")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest("User ID cannot be null or empty.");

            if (!int.TryParse(userId, out int id))
                return BadRequest("User ID is not a valid integer.");

            Teacher? teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound();

            Teacher? updatedTeacher = await _teacherService.UpdateTeacherAsync(id, new Teacher
            {
                Password = password,
                UpdatedAt = DateTime.Now
            });
            return Ok(updatedTeacher);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> Delete(int id)
        {
            Teacher? teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound();

            var institutionId = User.FindFirst("InstitutionId")?.Value;
            if (string.IsNullOrEmpty(institutionId) || !int.TryParse(institutionId, out int instId) || teacher.InstitutionId != instId)
                return Forbid();

            Teacher? deletedTeacher = await _teacherService.DeleteAsync(id);
            return Ok(deletedTeacher);
        }
    }
}