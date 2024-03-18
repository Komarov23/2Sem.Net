using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class GroupService(IDB db) : IGroupService
    {
        public Task<List<Group>> GetGroups()
        {
            return Task.FromResult(db.Groups.ToList());
        }

        public Task<Group> GetGroupById(int id)
        {
            return Task.FromResult(db.Groups.Find(g => g.Id.Equals(id)));
        }

        public Task<Group> AddAsync(Group group)
        {
            var newGroup = new Group { Number = group.Number, Id = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds };
            db.Groups.Add(newGroup);
            return Task.FromResult(newGroup);
        }

        public Task DeleteAsync(int id)
        {
            var group = db.Groups.Find(g => g.Id.Equals(id));

            if (group != null)
            {
                db.Groups.Remove(group);
            }

            return Task.CompletedTask;
        }

        public Task<Group> UpdateAsync(int id, Group group)
        {
            var groupToUpdate = db.Groups.FirstOrDefault(s => s.Id.Equals(id));
            if (groupToUpdate != null)
            {
                groupToUpdate.Number = group.Number;
                return Task.FromResult(groupToUpdate);
            }
            return (Task<Group>)Task.CompletedTask;
        }
    }
}
