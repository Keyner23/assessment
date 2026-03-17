using Assessment.Application.Interfaces;
using Assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.Repositories;

public class LessionRepository:ILessonRepository
{
        private readonly ApplicationDbContext _context;

        public LessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todas las lecciones de un curso
        public async Task<IEnumerable<Lesson>> GetAllAsync(Guid courseId, bool includeDeleted = false)
        {
            return await _context.Lecciones
                .Where(l => l.CourseId == courseId && (includeDeleted || !l.IsDeleted))
                .OrderBy(l => l.Order)
                .ToListAsync();
        }

        //  lección por Id
        public async Task<Lesson?> GetByIdAsync(Guid id)
        {
            return await _context.Lecciones
                .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);
        }

        //crear lección
        public async Task<Lesson> AddAsync(Lesson lesson)
        {
            await _context.Lecciones.AddAsync(lesson);
            return lesson;
        }

        // Actualizar lección
        public async Task UpdateAsync(Lesson lesson)
        {
            _context.Lecciones.Update(lesson);
        }

        // Borrar lección (soft delete)
        public async Task DeleteAsync(Guid id)
        {
            var lesson = await _context.Lecciones.FindAsync(id);
            if (lesson != null)
            {
                lesson.IsDeleted = true;
                lesson.UpdatedAt = DateTime.UtcNow;
                _context.Lecciones.Update(lesson);
            }
        }

        // Guardar cambios
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }


