using System;

namespace Lesson.Domain
{
    public class LetterGradePercentCalculator : ILetterGradePercentCalculator
    {
        public int Calculate(string letterGrade)
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
}
