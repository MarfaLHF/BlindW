using BlindW.Data.Models;

namespace Client.Models
{
    public class Result
    {
        public int TestResultId { get; set; }
        public string UserId { get; set; }
        public int TestSettingId { get; set; }
        public int CountCharacters { get; set; }
        public double TotalTime { get; set; }
        public double Wpm { get; set; }
        public double Accuracy { get; set; }
    }
}
