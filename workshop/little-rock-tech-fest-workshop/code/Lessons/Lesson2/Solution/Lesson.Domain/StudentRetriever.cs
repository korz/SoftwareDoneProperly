using Lesson.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson.Domain
{
    public class StudentRetriever
    {
        public static IList<Student> Retrieve()
        {
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

            return students;
        }
    }
}
