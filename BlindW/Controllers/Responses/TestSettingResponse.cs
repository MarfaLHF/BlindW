namespace BlindW.Controllers.Responses
{
    public class TestSettingResponse
    {
        public int TestSettingId { get; set; }

        public bool IsPunctuationEnabled { get; set; }
        public bool IsNumbersEnabled { get; set; }

        public int TestTypeId { get; set; }
        public int? WordCountId { get; set; }
        public int? TestDurationId { get; set; }

        public int LanguageId { get; set; }
    }
}