using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IGroupService
    {
        Task<List<Group>> GetGroups();
        Task<Group> GetGroupById(int id);
        Task<Group> AddAsync(Group group);
        Task<Group> UpdateAsync(int id, Group group);
        Task DeleteAsync(int id);
    }
}
