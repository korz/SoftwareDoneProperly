namespace Lesson.Contracts
{
    public class AverageTeacherGrade
    {
        public Teacher Teacher { get; set; }
        public int AverageGrade { get; set; }

        public override string ToString()
        {
            return $"{this.Teacher.FirstName},{this.Teacher.LastName},{this.AverageGrade}";
        }
    }
}
