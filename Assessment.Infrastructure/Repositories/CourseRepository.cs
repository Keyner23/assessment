using Assessment.Application.Interfaces;
using Assessment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assessment.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository  
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync(bool includeDeleted = false)
        {
            return await _context.Cursos
                .Where(c => includeDeleted || !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(Guid id)
        {
            return await _context.Cursos.FindAsync(id);
        }

        public async Task<Course> AddAsync(Course course)
        {
            await _context.Cursos.AddAsync(course);
            return course;
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Cursos.Update(course);
        }

        public async Task DeleteAsync(Guid id)
        {
            var course = await _context.Cursos.FindAsync(id);
            if (course != null)
                course.IsDeleted = true; 
        }
        

        public async Task SavechangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
