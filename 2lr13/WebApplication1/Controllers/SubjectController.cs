using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/subjects")]
    public class SubjectController(ISubjectService subjectService) : ControllerBase
    {
        private readonly ISubjectService _subjectService = subjectService;

        [HttpGet]
        public async Task<List<Subject>> Get() => await _subjectService.GetSubjects();

        [HttpGet("{id}")]
        public async Task<Subject> GetStudent(int id) => await _subjectService.GetSubjectById(id);

        [HttpPost]
        public async Task<Subject> Post([FromBody] Subject data) => await _subjectService.AddAsync(data);

        [HttpPut("{id}")]
        public async Task<Subject> Update([FromBody] Subject data, int id) => await _subjectService.UpdateAsync(id, data);

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _subjectService.DeleteAsync(id);
            return Ok();
        }
    }
}
