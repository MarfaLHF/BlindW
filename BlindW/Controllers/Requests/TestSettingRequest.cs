namespace BlindW.Controllers.Requests
{
    public class TestSettingRequest
    {
        public bool IsPunctuationEnabled { get; set; }
        public bool IsNumbersEnabled { get; set; }

        public int TestTypeId { get; set; }

        public int? WordCountId { get; set; }
        public int? TestDurationId { get; set; }

        public int LanguageId { get; set; }
    }
}