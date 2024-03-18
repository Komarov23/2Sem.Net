using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/groups")]
    public class GroupController(IGroupService groupService) : ControllerBase
    {
        private readonly IGroupService _groupService = groupService;

        [HttpGet]
        public async Task<List<Group>> Get() => await _groupService.GetGroups();

        [HttpGet("{id}")]
        public async Task<Group> GetStudent(int id) => await _groupService.GetGroupById(id);

        [HttpPost]
        public async Task<Group> Post([FromBody] Group data) => await _groupService.AddAsync(data);

        [HttpPut("{id}")]
        public async Task<Group> Update([FromBody] Group data, int id) => await _groupService.UpdateAsync(id, data);

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _groupService.DeleteAsync(id);
            return Ok();
        }
    }
}
