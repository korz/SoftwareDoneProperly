using Lesson.Console.Composition;
using Lesson.Contracts;
using Lesson.Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson.Console
{
    class Program
    {
        public static ITeacherRetriever TeacherRetriever;
        public static IStudentRetriever StudentRetriever;
        public static IAssignmentRetriever AssignmentRetriever;
        public static ITeacherStudentCountRetriever TeacherStudentCountRetriever;
        public static IAverageTeacherGradeCalculator AverageTeacherGradeCalculator;

        static Program()
        {
            TeacherRetriever = CompositionRoot.Get<ITeacherRetriever>();
            StudentRetriever = CompositionRoot.Get<IStudentRetriever>();
            AssignmentRetriever = CompositionRoot.Get<IAssignmentRetriever>();
            TeacherStudentCountRetriever = CompositionRoot.Get<ITeacherStudentCountRetriever>();
            AverageTeacherGradeCalculator = CompositionRoot.Get<IAverageTeacherGradeCalculator>();
        }

        static void Main(string[] args)
        {
            IList<Teacher> teachers = TeacherRetriever.Retrieve();
            IList<Student> students = StudentRetriever.Retrieve();
            IList<Assignment> assignments = AssignmentRetriever.Retrieve();

            IList<TeacherStudentCount> teacherStudentCounts = TeacherStudentCountRetriever.Retrieve(teachers, students);
            IList<Teacher> teachersWithMoreThan10Students = teacherStudentCounts.Where(x => x.StudentCount > 10).Select(x => x.Teacher).ToList();

            IList<AverageTeacherGrade> averageTeacherGrades = AverageTeacherGradeCalculator.Calculate(teachersWithMoreThan10Students, assignments);

            var top5Teachers = averageTeacherGrades.Take(5).ToList();

            File.WriteAllLines("Top5Teachers.csv", top5Teachers.Select(x => x.ToString()));
        }
    }
}
