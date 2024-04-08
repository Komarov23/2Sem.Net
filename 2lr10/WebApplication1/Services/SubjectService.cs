using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class SubjectService(IDB db) : ISubjectService
    {
        public Task<List<Subject>> GetSubjects()
        {
            return Task.FromResult(db.Subjects.ToList());
        }

        public Task<Subject> GetSubjectById (int id)
        {
            return Task.FromResult(db.Subjects.Find(g => g.Id.Equals(id)));
        }

        public Task<Subject> AddAsync(Subject subject)
        {
            var newSubject = new Subject { Name = subject.Name, Id = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds };
            db.Subjects.Add(newSubject);
            return Task.FromResult(newSubject);
        }

        public Task DeleteAsync(int id)
        {
            var subject = db.Subjects.Find(g => g.Id.Equals(id));

            if (subject != null)
            {
                db.Subjects.Remove(subject);
            }

            return Task.CompletedTask;
        }

        public Task<Subject> UpdateAsync(int id, Subject subject)
        {
            var subjectToUpdate = db.Subjects.FirstOrDefault(s => s.Id.Equals(id));
            if (subjectToUpdate != null)
            {
                subjectToUpdate.Name = subject.Name;
                return Task.FromResult(subjectToUpdate);
            }
            return (Task<Subject>)Task.CompletedTask;
        }
    }
}
