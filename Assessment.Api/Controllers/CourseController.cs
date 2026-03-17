using Assessment.Application.DTOs;
using Assessment.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class CourseController : Controller
{
    private readonly CourseService _service;

    public CourseController(CourseService service)
    {
        _service = service;
    }
    

    [HttpGet]
    public async Task<IActionResult> GetCourse()
    {
        var courses = await _service.GetCourses();
        return Ok(courses);
    }
    

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CourseDto dto)
    {
        await _service.AddAsync(dto);
        return Ok(new { message = "Curso creado correctamente" });
    }
}