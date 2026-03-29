namespace BlindW.Controllers.Requests
{
    public class GetTextRequest
    {
        public int WordCount { get; set; }
        public string LanguageCode { get; set; } = "en";
        public bool IsNumbersEnabled { get; set; }
        public bool IsPunctuationEnabled { get; set; }
    }
}