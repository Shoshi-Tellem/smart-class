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
    [Authorize]
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

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<ActionResult<Lesson>> Post([FromBody] LessonPostPut lessonPost)
        {
            if (lessonPost == null)
                return BadRequest("Lesson cannot be null.");
            Lesson addedLesson = await _lessonService.AddLessonAsync(new Lesson { Status = lessonPost.Status, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });
            return CreatedAtAction(nameof(Get), new { id = addedLesson.Id }, addedLesson);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Lesson>> Put(int id)
        {
            Lesson? lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null)
                return NotFound();
            Lesson? updatedLesson = await _lessonService.UpdateLessonAsync(id, new Lesson { Status = !lesson.Status, UpdatedAt = DateTime.Now });
            return Ok(updatedLesson);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Lesson>> Delete(int id)
        {
            Lesson? deletedLesson = await _lessonService.DeleteAsync(id);
            if (deletedLesson == null)
                return NotFound();
            return Ok(deletedLesson);
        }
    }
}