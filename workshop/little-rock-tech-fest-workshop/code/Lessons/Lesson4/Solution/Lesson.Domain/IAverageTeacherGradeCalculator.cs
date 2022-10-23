using Lesson.Contracts;
using System.Collections.Generic;

namespace Lesson.Domain
{
    public interface IAverageTeacherGradeCalculator
    {
        IList<AverageTeacherGrade> Calculate(IEnumerable<Teacher> teachers, IEnumerable<Assignment> assignments);
    }
}