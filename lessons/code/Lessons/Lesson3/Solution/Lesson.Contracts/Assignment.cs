using System;

namespace Lesson.Contracts
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public string LetterGrade { get; set; }
        public Guid StudentId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
