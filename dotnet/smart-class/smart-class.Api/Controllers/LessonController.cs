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
    //[Authorize]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;
        private readonly IMapper _mapper;

        public LessonController(ILessonService lessonService, IMapper mapper)
        {
            _lessonService = lessonService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LessonDto>>> Get()
        {
            IEnumerable<Lesson> lessons = await _lessonService.GetLessonsAsync();
            return Ok(_mapper.Map<IEnumerable<LessonDto>>(lessons));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> Get(int id)
        {
            Lesson? lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
                return NotFound();
            return Ok(_mapper.Map<LessonDto>(lesson));
        }

        [HttpGet("myLessons")]
        public async Task<ActionResult<IEnumerable<LessonDto>>> GetMyLessons([FromQuery] int courseId)
        {
            // מקבלים את כל השיעורים
            IEnumerable<Lesson> lessons = await _lessonService.GetLessonsAsync();

            // מסננים את השיעורים לפי courseId
            IEnumerable<Lesson> filteredLessons = lessons.Where(lesson => lesson.CourseId == courseId);

            // מחזירים את השיעורים המסוננים במבנה DTO
            return Ok(_mapper.Map<IEnumerable<LessonDto>>(filteredLessons));
        }

        //[Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<ActionResult<Lesson>> Post([FromBody] LessonPostPut lessonPost)
        {
            if (lessonPost == null)
                return BadRequest("Lesson cannot be null."); // בדיקה אם האובייקט ריק

            if (string.IsNullOrWhiteSpace(lessonPost.Name))
                return BadRequest("Lesson name cannot be empty."); // בדיקה אם השם ריק

            // הוספת השיעור
            Lesson addedLesson = await _lessonService.AddLessonAsync(new Lesson
            {
                CourseId = lessonPost.CourseId,
                Name = lessonPost.Name,
                Status = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });

            return CreatedAtAction(nameof(Get), new { id = addedLesson.Id }, addedLesson);
        }

        //[Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Lesson>> Put(int id)
        {
            Lesson? lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
                return NotFound();
            if (lesson.Course.TeacherId != int.Parse(User.FindFirst("Id")?.Value))
                return Forbid();
            Lesson? updatedLesson = await _lessonService.UpdateLessonAsync(id, new Lesson { Status = !lesson.Status });
            return Ok(updatedLesson);
        }

        //[Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lesson>> Delete(int id)
        {
            Lesson? lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
                return NotFound();
            //if (lesson.Course.TeacherId != int.Parse(User.FindFirst("Id")?.Value))
            //    return Forbid();
            Lesson? deletedLesson = await _lessonService.DeleteAsync(id);
            return Ok(deletedLesson);
        }
    }
}