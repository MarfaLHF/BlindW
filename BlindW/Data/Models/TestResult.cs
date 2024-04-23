namespace BlindW.Data.Models
{
    public class TestResult
    {
        public int TestResultId { get; set; }
        public string Id { get; set; }
        public DateTime TestDateTime { get; set; }
        public int Wpm { get; set; }
        public decimal Accuracy { get; set; }
        public int NumCharacters { get; set; }
        public int TotalTime { get; set; }

        public virtual User User { get; set; }
    }

}
