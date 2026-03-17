namespace Assessment.Application.DTOs;

public class CreateLessonDto
{
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public int Order { get; set; }
}