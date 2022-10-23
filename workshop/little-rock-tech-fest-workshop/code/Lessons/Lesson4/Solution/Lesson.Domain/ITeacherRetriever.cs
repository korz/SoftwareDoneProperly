using Lesson.Contracts;
using System.Collections.Generic;

namespace Lesson.Domain
{
    public interface ITeacherRetriever
    {
        IList<Teacher> Retrieve();
    }
}