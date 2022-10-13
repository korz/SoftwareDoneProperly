using System;

namespace Lesson.Contracts
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Subject { get; set; }
        public string RoomNumber { get; set; }
    }
}
