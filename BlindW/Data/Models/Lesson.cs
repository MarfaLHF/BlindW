namespace BlindW.Data.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public int LevelId { get; set; }
        public string LessonText { get; set; }

        public virtual Level Level { get; set; }
    }

}
