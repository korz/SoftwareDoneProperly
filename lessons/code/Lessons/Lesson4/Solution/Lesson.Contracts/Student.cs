using System;

namespace Lesson.Contracts
{
    public class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid TeacherId { get; set; }
    }
}
