using Lesson.Contracts;
using Shared.Csv;
using System.Collections.Generic;

namespace Lesson.Domain
{
    public class AssignmentRetriever : IAssignmentRetriever
    {
        public IList<Assignment> Retrieve()
        {
            return CsvSerializer.Read<Assignment>(@"Data/Assignments.csv");
        }
    }
}
