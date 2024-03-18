using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IDB
    {
        List<Group> Groups { get; set; }
        List<Student> Students { get; set; }
        List<Subject> Subjects { get; set; }
    }
}
