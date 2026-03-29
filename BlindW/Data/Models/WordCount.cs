namespace BlindW.Data.Models
{
    public class WordCount
    {
        public int WordCountId { get; set; }

        public int Count { get; set; }

        public virtual ICollection<TestSetting> TestSettings { get; set; } = new List<TestSetting>();
    }
}