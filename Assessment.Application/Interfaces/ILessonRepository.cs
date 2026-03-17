using Assessment.Domain.Entities;

namespace Assessment.Application.Interfaces;

public interface ILessonRepository
{
 
        Task<IEnumerable<Lesson>> GetAllAsync(Guid courseId, bool includeDeleted = false);
        Task<Lesson?> GetByIdAsync(Guid id);
        Task<Lesson> AddAsync(Lesson lesson);
        Task UpdateAsync(Lesson lesson);
        Task DeleteAsync(Guid id); 
        Task SaveChangesAsync();


}