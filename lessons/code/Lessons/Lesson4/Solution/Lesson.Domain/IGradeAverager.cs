using System.Collections.Generic;

namespace Lesson.Domain
{
    public interface IGradeAverager
    {
        int Average(IEnumerable<int> grades);
    }
}