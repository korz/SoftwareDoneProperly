using Lesson.Contracts;
using System.Collections.Generic;

namespace Lesson.Domain
{
    public interface IStudentRetriever
    {
        IList<Student> Retrieve();
    }
}