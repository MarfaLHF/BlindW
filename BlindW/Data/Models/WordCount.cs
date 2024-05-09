namespace BlindW.Data.Models
{
    public class WordCount
    {
        public int WordCountId { get; set; }
        public int Count { get; set; } // количество слов для тестирования по буквам /25 50
        public virtual ICollection<TestSetting> TestSetting { get; set; }

    }
}
