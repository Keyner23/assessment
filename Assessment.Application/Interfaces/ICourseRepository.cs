using Assessment.Domain.Entities;

namespace Assessment.Application.Interfaces;

public interface ICourseRepository
{
    // Obtener todos los cursos (opcionalmente filtrar por estado o borrados)
    Task<IEnumerable<Course>> GetAllAsync(bool includeDeleted = false);
    Task<Course?> GetByIdAsync(Guid id);
    Task<Course> AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(Guid id);
    Task SavechangesAsync();
}