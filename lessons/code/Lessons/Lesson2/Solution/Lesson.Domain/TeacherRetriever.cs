using Lesson.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson.Domain
{
    public class TeacherRetriever
    {
        public static IList<Teacher> Retrieve()
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

            return teachers;
        }
    }
}
