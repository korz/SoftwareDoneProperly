using Lesson.Contracts;
using System.Collections.Generic;

namespace Lesson.Domain
{
    public interface ITeacherStudentCountRetriever
    {
        IList<TeacherStudentCount> Retrieve(IEnumerable<Teacher> teachers, IEnumerable<Student> students);
    }
}