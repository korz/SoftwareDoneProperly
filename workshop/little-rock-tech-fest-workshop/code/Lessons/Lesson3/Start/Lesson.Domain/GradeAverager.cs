using System.Collections.Generic;
using System.Linq;

namespace Lesson.Domain
{
    public class GradeAverager
    {
        public static int Average(IEnumerable<int> grades)
        {
            return (int)grades.Average();
        }
    }
}
