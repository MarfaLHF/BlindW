namespace BlindW.Data.Models
{
    public class Level
    {
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public string LevelDescription { get; set; }

        public virtual ICollection<Lesson> Lessons { get; set; }
    }

}
