using AutoMapper;
using Microsoft.AspNetCore.Authorization; // הוספת ספריית האימות
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
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> Get()
        {
            IEnumerable<Course> courses = await _courseService.GetCoursesAsync();
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> Get(int id)
        {
            Course? course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();
            return Ok(_mapper.Map<CourseDto>(course));
        }

        //[Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<ActionResult<Course>> Post([FromBody] CoursePostPut coursePost)
        {
            if (coursePost == null)
                return BadRequest("Course cannot be null.");

            // קבלת ה-ID של המורה מהטוקן
            var id = User.FindFirst("Id")?.Value;

            if (id == null)
                return Unauthorized("Teacher ID not found in token.");

            // המרה ל-ID של המורה
            int teacherId = int.Parse(id);

            // יצירת הקורס
            Course addedCourse = await _courseService.AddCourseAsync(new Course
            {
                Name = coursePost.Name,
                Description = coursePost.Description,
                TeacherId = teacherId,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now
            });

            return CreatedAtAction(nameof(Get), new { id = addedCourse.Id }, addedCourse);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> Put(int id, [FromBody] CoursePostPut coursePut)
        {
            if (coursePut == null)
                return BadRequest("Course cannot be null.");
            //if (coursePut.TeacherId != int.Parse(User.FindFirst("Id")?.Value))
            //    return Forbid();
            Course? updatedCourse = await _courseService.UpdateCourseAsync(id, new Course { Name = coursePut.Name, Description = coursePut.Description });
            if (updatedCourse == null)
                return NotFound();
            return Ok(updatedCourse);
        }

        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> Delete(int id)
        {
            Course course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();
            if (course.TeacherId != int.Parse(User.FindFirst("Id")?.Value))
                return Forbid();
            Course? deletedCourse = await _courseService.DeleteAsync(id);
            return Ok(deletedCourse);
        }
    }
}