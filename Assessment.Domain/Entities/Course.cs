namespace Assessment.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public CourseStatus Status { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navegación
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}