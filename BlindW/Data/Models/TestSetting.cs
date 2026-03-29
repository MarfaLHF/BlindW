namespace BlindW.Data.Models
{
    public class TestSetting
    {
        public int TestSettingId { get; set; }

        public bool IsPunctuationEnabled { get; set; }
        public bool IsNumbersEnabled { get; set; }

        public int TestTypeId { get; set; }
        public virtual TestType TestType { get; set; } = null!;

        public int? WordCountId { get; set; }
        public virtual WordCount? WordCount { get; set; }

        public int? TestDurationId { get; set; }
        public virtual TestDuration? TestDuration { get; set; }

        public int LanguageId { get; set; }
        public virtual Language Language { get; set; } = null!;

        public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    }
}