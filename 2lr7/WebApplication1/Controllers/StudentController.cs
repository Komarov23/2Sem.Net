using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController(IStudentService studentService) : ControllerBase
    {
        private readonly IStudentService _studentService = studentService;

        [HttpGet]
        public async Task<List<Student>> Get() => await _studentService.GetStudents();

        [HttpGet("{id}")]
        public async Task<Student> GetStudent (int id) => await _studentService.GetStudentById(id);

        [HttpPost]
        public async Task<Student> Post([FromBody] Student data) => await _studentService.AddAsync(data);

        [HttpPut("{id}")]
        public async Task<Student> Update([FromBody] Student data, int id) => await _studentService.UpdateAsync(id, data);

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            return Ok();
        }
    }
}
