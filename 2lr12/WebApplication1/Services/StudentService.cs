using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class StudentService(IDB db) : IStudentService
    {
        public Task<List<Student>> GetStudents()
        {
            return Task.FromResult(db.Students.ToList());
        }

        public Task<Student> GetStudentById(int id)
        {
            return Task.FromResult(db.Students.Find(s => s.Id.Equals(id)));
        }

        public Task<Student> AddAsync(Student student)
        {
            var newStudent = new Student { Name = student.Name, GroupId = student.GroupId, Id = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds };
            db.Students.Add(newStudent);
            return Task.FromResult(newStudent);
        }

        public Task DeleteAsync(int id)
        {
            var student = db.Students.Find(s => s.Id.Equals(id));

            if (student != null)
            {
                db.Students.Remove(student);
            }

            return Task.CompletedTask;
        }

        public Task<Student> UpdateAsync(int id, Student student)
        {
            var studentToUpdate = db.Students.FirstOrDefault(s => s.Id.Equals(id));
            if (studentToUpdate != null)
            {
                studentToUpdate.Name = student.Name;
                studentToUpdate.GroupId = student.GroupId;
                return Task.FromResult(studentToUpdate);
            }
            return (Task<Student>)Task.CompletedTask;
        }
    }
}
