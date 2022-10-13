using Lesson.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Lesson.Domain
{
    public class TeacherStudentCountRetriever
    {
        public static IList<TeacherStudentCount> Retrieve(IEnumerable<Teacher> teachers, IEnumerable<Student> students)
        {
            IList<TeacherStudentCount> teacherStudentCounts = new List<TeacherStudentCount>();

            foreach (var teacher in teachers)
            {
                var studentCount = students.Where(x => x.TeacherId == teacher.Id).Count();

                var teacherStudentCount = new TeacherStudentCount
                {
                    Teacher = teacher,
                    StudentCount = studentCount
                };

                teacherStudentCounts.Add(teacherStudentCount);
            }

            return teacherStudentCounts;
        }
    }
}
