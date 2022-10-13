using Lesson.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Lesson.Domain
{
    public class AverageTeacherGradeCalculator
    {
        public static IList<AverageTeacherGrade> Calculate(IEnumerable<Teacher> teachers, IEnumerable<Assignment> assignments)
        {
            IList<AverageTeacherGrade> averageTeacherGrades = new List<AverageTeacherGrade>();

            foreach (var teacher in teachers)
            {
                var letterGrades = assignments.Where(y => y.TeacherId == teacher.Id).Select(y => y.LetterGrade).ToList();
                var gradePercentages = letterGrades.Select(x => LetterGradePercentCalculator.Calculate(x));
                var averageGradePercentage = GradeAverager.Average(gradePercentages);

                var averageTeacherGrade = new AverageTeacherGrade
                {
                    Teacher = teacher,
                    AverageGrade = averageGradePercentage
                };

                averageTeacherGrades.Add(averageTeacherGrade);
            }

            return averageTeacherGrades.OrderByDescending(x => x.AverageGrade).ToList();
        }
    }
}
