using Lesson.Contracts;
using Shared.Csv;
using System.Collections.Generic;

namespace Lesson.Domain
{
    public class AssignmentRetriever
    {
        public static IList<Assignment> Retrieve()
        {
            return CsvSerializer.Read<Assignment>(@"Data/Assignments.csv");
        }
    }
}
