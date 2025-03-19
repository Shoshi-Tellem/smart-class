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
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _teacherService = teacherService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> Get()
        {
            IEnumerable<Teacher> teachers = await _teacherService.GetTeachersAsync();
            return Ok(_mapper.Map<IEnumerable<TeacherDto>>(teachers));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> Get(int id)
        {
            Teacher? teacher = await _teacherService.GetTeacherByIdAsync(id);
            if (teacher == null)
                return NotFound();
            return Ok(_mapper.Map<TeacherDto>(teacher));
        }

        [HttpPost]
        public async Task<ActionResult<Teacher>> Post([FromBody] TeacherPostPut teacherPost)
        {
            if (teacherPost == null)
                return BadRequest("Teacher cannot be null.");
            Teacher addedTeacher = await _teacherService.AddTeacherAsync(new Teacher { Name = teacherPost.Name });
            return CreatedAtAction(nameof(Get), new { id = addedTeacher.Id }, addedTeacher);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Teacher>> Put(int id, [FromBody] TeacherPostPut teacherPut)
        {
            if (teacherPut == null)
                return BadRequest("Teacher cannot be null.");
            Teacher? updatedTeacher = await _teacherService.UpdateTeacherAsync(id, new Teacher { Name = teacherPut.Name });
            if (updatedTeacher == null)
                return NotFound();
            return Ok(updatedTeacher);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Teacher>> Delete(int id)
        {
            Teacher? deletedTeacher = await _teacherService.DeleteAsync(id);
            if (deletedTeacher == null)
                return NotFound();
            return Ok(deletedTeacher);
        }
    }
}