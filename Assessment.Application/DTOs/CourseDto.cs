using Assessment.Domain;

namespace Assessment.Application.DTOs;

public class CourseDto
{
        public string Title { get; set; } = null!;

        public CourseStatus Status { get; set; } = CourseStatus.Draft;
}