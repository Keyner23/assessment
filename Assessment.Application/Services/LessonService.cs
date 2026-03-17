

using Assessment.Application.DTOs;
using Assessment.Application.Interfaces;
using Assessment.Domain.Entities;

namespace Assessment.Application.Services
{
    public class LessonService
    {
        private readonly ILessonRepository _repository;

        public LessonService(ILessonRepository repository)
        {
            _repository = repository;
        }

        // Crear lección
        public async Task<LessonDto> CreateLessonAsync(CreateLessonDto dto)
        {
            var lesson = new Lesson
            {
                Id = Guid.NewGuid(),
                CourseId = dto.CourseId,
                Title = dto.Title,
                Order = dto.Order,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _repository.AddAsync(lesson);
            await _repository.SaveChangesAsync();

            return new LessonDto
            {
                Id = lesson.Id,
                CourseId = lesson.CourseId,
                Title = lesson.Title,
                Order = lesson.Order
            };
        }

        // // Actualizar lección
        // public async Task<LessonDto?> UpdateLessonAsync(Guid id, UpdateLessonDto dto)
        // {
        //     var lesson = await _repository.GetByIdAsync(id);
        //     if (lesson == null) return null;
        //
        //     lesson.Title = dto.Title;
        //     lesson.Order = dto.Order;
        //     lesson.UpdatedAt = DateTime.UtcNow;
        //
        //     await _repository.UpdateAsync(lesson);
        //     await _repository.SaveChangesAsync();
        //
        //     return new LessonDto
        //     {
        //         Id = lesson.Id,
        //         CourseId = lesson.CourseId,
        //         Title = lesson.Title,
        //         Order = lesson.Order
        //     };
        // }

        // Borrar lección
        public async Task<bool> DeleteLessonAsync(Guid id)
        {
            var lesson = await _repository.GetByIdAsync(id);
            if (lesson == null) return false;

            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return true;
        }

        // Obtener todas las lecciones de un curso
        public async Task<IEnumerable<LessonDto>> GetLessonsByCourseAsync(Guid courseId)
        {
            var lessons = await _repository.GetAllAsync(courseId);
            return lessons.Select(l => new LessonDto
            {
                Id = l.Id,
                CourseId = l.CourseId,
                Title = l.Title,
                Order = l.Order
            });
        }

        // Obtener lección por id
        public async Task<LessonDto?> GetLessonByIdAsync(Guid id)
        {
            var lesson = await _repository.GetByIdAsync(id);
            if (lesson == null) return null;

            return new LessonDto
            {
                Id = lesson.Id,
                CourseId = lesson.CourseId,
                Title = lesson.Title,
                Order = lesson.Order
            };
        }
    }
}
