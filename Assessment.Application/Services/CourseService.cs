using Assessment.Application.DTOs;
using Assessment.Application.Interfaces;
using Assessment.Domain.Entities;

namespace Assessment.Application.Services;

public class CourseService
{
    private readonly ICourseRepository _courseRepository;
    
    public CourseService(ICourseRepository repository)
    {
        _courseRepository = repository;
    }
    
    public Task<IEnumerable<Course>> GetCourses() => _courseRepository.GetAllAsync();

    public async Task AddAsync(CourseDto dto)
    {
        var course = new Course()
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _courseRepository.AddAsync(course);
        await _courseRepository.SavechangesAsync();
    }
}