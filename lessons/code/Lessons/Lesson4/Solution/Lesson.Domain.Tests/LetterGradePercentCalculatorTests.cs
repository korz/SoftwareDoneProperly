using NUnit.Framework;
using System;

namespace Lesson.Domain.Tests
{
    public class LetterGradePercentCalculatorTests
    {
        [Test]
        [TestCase("A+", 100)]
        [TestCase("A", 95)]
        [TestCase("A-", 90)]
        [TestCase("B+", 89)]
        [TestCase("B", 85)]
        [TestCase("B-", 80)]
        [TestCase("C+", 79)]
        [TestCase("C", 75)]
        [TestCase("C-", 70)]
        [TestCase("D+", 69)]
        [TestCase("D", 65)]
        [TestCase("D-", 60)]
        [TestCase("F+", 0)]
        [TestCase("F", 0)]
        [TestCase("F-", 0)]
        [TestCase("G", 0)]
        public void Calculate(string letterGrade, int expectedPercentage)
        {
            var calculated = LetterGradePercentCalculator.Calculate(letterGrade);

            Assert.AreEqual(expectedPercentage, calculated);
        }
    }
}
