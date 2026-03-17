using Assessment.Application.DTOs;
using Assessment.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Assessment.Api.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly LessonService _lessonService;

        public LessonController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        // GET: api/v1/Lesson/{courseId}
        [HttpGet("{courseId:guid}")]
        public async Task<IActionResult> GetLessonsByCourse(Guid courseId)
        {
            var lessons = await _lessonService.GetLessonsByCourseAsync(courseId);
            return Ok(lessons);
        }

        // GET: api/v1/Lesson/lesson/{id}
        [HttpGet("lesson/{id:guid}")]
        public async Task<IActionResult> GetLessonById(Guid id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);
            if (lesson == null) return NotFound();
            return Ok(lesson);
        }

        // POST: api/v1/Lesson
        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromBody] CreateLessonDto dto)
        {
            var lesson = await _lessonService.CreateLessonAsync(dto);
            return CreatedAtAction(nameof(GetLessonById), new { id = lesson.Id }, lesson);
        }

        // // PUT: api/v1/Lesson/{id}
        // [HttpPut("{id:guid}")]
        // public async Task<IActionResult> UpdateLesson(Guid id, [FromBody] UpdateLessonDto dto)
        // {
        //     var updatedLesson = await _lessonService.UpdateLessonAsync(id, dto);
        //     if (updatedLesson == null) return NotFound();
        //     return Ok(updatedLesson);
        // }

        // // DELETE: api/v1/Lesson/{id}
        // [HttpDelete("{id:guid}")]
        // public async Task<IActionResult> DeleteLesson(Guid id)
        // {
        //     var result = await _lessonService.DeleteLessonAsync(id);
        //     if (!result) return NotFound();
        //     return NoContent();
        // }
    }
}
