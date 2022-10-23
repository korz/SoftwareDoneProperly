using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IList<string> teacherLines = File.ReadAllLines(@"Data/Teachers.csv").Skip(1).ToList();
            IList<Teacher> teachers = new List<Teacher>();

            foreach (var line in teacherLines)
            {
                var splits = line.Split(',');

                var teacher = new Teacher
                {
                    Id = Guid.Parse(splits[0]),
                    FirstName = splits[1],
                    LastName = splits[2],
                    Subject = splits[3],
                    RoomNumber = splits[4]
                };

                teachers.Add(teacher);
            }


            IList<string> studentLines = File.ReadAllLines(@"Data/Students.csv").Skip(1).ToList();
            IList<Student> students = new List<Student>();

            foreach (var line in studentLines)
            {
                var splits = line.Split(',');

                var student = new Student
                {
                    Id = Guid.Parse(splits[0]),
                    FirstName = splits[1],
                    LastName = splits[2],
                    TeacherId = Guid.Parse(splits[3])
                };

                students.Add(student);
            }

            IList<string> assignmentLines = File.ReadAllLines(@"Data/Assignments.csv").Skip(1).ToList();
            IList<Assignment> assignments = new List<Assignment>();

            foreach (var line in assignmentLines)
            {
                var splits = line.Split(',');

                var assignment = new Assignment
                {
                    Id = Guid.Parse(splits[0]),
                    LetterGrade = splits[1],
                    StudentId = Guid.Parse(splits[2]),
                    TeacherId = Guid.Parse(splits[3])
                };

                assignments.Add(assignment);
            }

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

            IList<Teacher> teachersWithMoreThan10Students = teacherStudentCounts.Where(x => x.StudentCount > 10).Select(x => x.Teacher).ToList();

            IList<AverageTeacherGrade> averageTeacherGrades = new List<AverageTeacherGrade>();

            foreach (var teacher in teachersWithMoreThan10Students)
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

            averageTeacherGrades = averageTeacherGrades.OrderByDescending(x => x.AverageGrade).ToList();

            var top5Teachers = averageTeacherGrades.Take(5).ToList();

            File.WriteAllLines("Top5Teachers.csv", top5Teachers.Select(x => x.ToString()));
        }

        public class Teacher
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Subject { get; set; }
            public string RoomNumber { get; set; }
        }

        public class Student
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Guid TeacherId { get; set; }
        }

        public class Assignment
        {
            public Guid Id { get; set; }
            public string LetterGrade { get; set; }
            public Guid StudentId { get; set; }
            public Guid TeacherId { get; set; }
        }

        public class TeacherStudentCount
        {
            public Teacher Teacher { get; set; }
            public int StudentCount { get; set; }
        }

        public class AverageTeacherGrade
        {
            public Teacher Teacher { get; set; }
            public int AverageGrade { get; set; }

            public override string ToString()
            {
                return $"{this.Teacher.FirstName},{this.Teacher.LastName},{this.AverageGrade}";
            }
        }

        public class LetterGradePercentCalculator
        {
            public static int Calculate(string letterGrade)
            {
                switch (letterGrade.ToUpper())
                {
                    case "A+": return 100;
                    case "A": return 95;
                    case "A-": return 90;
                    case "B+": return 89;
                    case "B": return 85;
                    case "B-": return 80;
                    case "C+": return 79;
                    case "C": return 75;
                    case "C-": return 70;
                    case "D+": return 69;
                    case "D": return 65;
                    case "D-": return 60;
                    case "F+": return 0;
                    case "F": return 0;
                    case "F-": return 0;
                    default: return 0;
                }
            }
        }

        public class GradeAverager
        {
            public static int Average(IEnumerable<int> grades)
            {
                return (int)grades.Average();
            }
        }
    }
}
