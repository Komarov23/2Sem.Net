using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetSubjects();
        Task<Subject> GetSubjectById(int id);
        Task<Subject> AddAsync(Subject group);
        Task<Subject> UpdateAsync(int id, Subject group);
        Task DeleteAsync(int id);
    }
}
