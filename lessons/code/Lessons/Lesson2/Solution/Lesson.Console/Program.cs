﻿using Lesson.Contracts;
using Lesson.Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson.Console
{
    class Program
    {
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
