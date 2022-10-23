using System.Collections.Generic;
using System.Linq;

namespace Lesson.Domain
{
    public class GradeAverager : IGradeAverager
    {
        public int Average(IEnumerable<int> grades)
        {
            return (int)grades.Average();
        }
    }
}
