using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudents();
        Task<Student> GetStudentById(int id);
        Task<Student> AddAsync(Student student);
        Task<Student> UpdateAsync(int id, Student student);
        Task DeleteAsync(int id);
    }
}
