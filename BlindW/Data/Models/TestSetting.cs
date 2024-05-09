namespace BlindW.Data.Models
{
    public class TestSetting
    {
        public int TestSettingId { get; set; }
        public bool IsPunctuationEnabled { get; set; }
        public bool IsNumbersEnabled { get; set; }
        public int TestTypeId { get; set; }
        public TestType TestType { get; set; }
        public int WordCountId { get; set; }
        public WordCount WordCount { get; set; }
        public int TestDurationId { get; set; }
        public TestDuration TestDuration { get; set; }
        public virtual ICollection<TestResult> TestResult { get; set; }

    }
}
//public int TestSettingId { get; set; }
//public bool IsPunctuationEnabled { get; set; }
//public bool IsNumbersEnabled { get; set; }
//public string TestType { get; set; } // тип тестирования: по буквам, по времени
//public int WordCount { get; set; } // количество слов для тестирования по буквам /25 50
//public int TestDuration { get; set; } // продолжительность теста для тестирования по времени /15 30 60 10 
