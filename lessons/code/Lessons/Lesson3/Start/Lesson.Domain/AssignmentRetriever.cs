using Lesson.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson.Domain
{
    public class AssignmentRetriever
    {
        public static IList<Assignment> Retrieve()
        {
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

            return assignments;
        }
    }
}
