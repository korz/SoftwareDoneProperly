using Lesson.Contracts;
using System.Collections.Generic;

namespace Lesson.Domain
{
    public interface IAssignmentRetriever
    {
        IList<Assignment> Retrieve();
    }
}