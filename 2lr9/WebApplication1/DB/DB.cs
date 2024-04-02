using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.DB
{
    public class DB : IDB
    {
        private List<Group> Groups = [];
        private List<Student> Students = [];
        private List<Subject> Subjects = [];
        private List<User> Users = [];

        public DB () {
            Groups = GenerateGroups(10);
            Students = GenerateStudents(10);
            Subjects = GenerateSubjects(10);
            Users = GenerateUsers(10);
        }

        List<Group> IDB.Groups { get => Groups; set => Groups = value; }
        List<Student> IDB.Students { get => Students; set => Students = value; }
        List<Subject> IDB.Subjects { get => Subjects; set => Subjects = value; }
        List<User> IDB.Users { get => Users; set => Users = value; }

        List<Group> GenerateGroups(int count)
        {
            var groups = new List<Group>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new Group { Id = i + 1, Number = 300 + i });
            }
            return groups;
        }

        List<Student> GenerateStudents(int count)
        {
            var students = new List<Student>();
            for (int i = 0; i < count; i++)
            {
                students.Add(new Student { Id = i + 1, Name = "Student " + i.ToString() });
            }
            return students;
        }

        List<Subject> GenerateSubjects(int count)
        {
            var subjects = new List<Subject>();
            for (int i = 0; i < count; i++)
            {
                subjects.Add(new Subject { Id = i + 1, Name = "Subject  " + i.ToString() });
            }
            return subjects;

        }

        List<User> GenerateUsers (int count)
        {
            var users = new List<User>();
            for (int i = 0; i < count; i++)
            {
                users.Add(new User(
                    i + 1, 
                    "User " + i.ToString(), 
                    "LastName " + i.ToString(), 
                    "email" + i.ToString() + "@gmail.com",
                    "password",
                    new DateTime(),
                    new DateTime(),
                    0
                    ));
            }

            return users;
        }
    }
}
