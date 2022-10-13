using Lesson.Contracts;
using Shared.Csv;
using System.Collections.Generic;

namespace Lesson.Domain
{
    public class StudentRetriever
    {
        public static IList<Student> Retrieve()
        {
            return CsvSerializer.Read<Student>(@"Data/Students.csv");
        }
    }
}
