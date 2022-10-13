using Lesson.Contracts;
using Shared.Csv;
using System.Collections.Generic;

namespace Lesson.Domain
{
    public class TeacherRetriever
    {
        public static IList<Teacher> Retrieve()
        {
            return CsvSerializer.Read<Teacher>(@"Data/Teachers.csv");
        }
    }
}
