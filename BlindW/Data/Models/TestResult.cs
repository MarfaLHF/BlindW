using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlindW.Data.Models
{
    public class TestResult
    {
        [Key]
        public int TestResultId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("TestSetting")]
        public int TestSettingId { get; set; }
        public DateTime TestDateTime { get; set; }
        public int CountCharacters { get; set; }
        public double TotalTime { get; set; }
        public double Wpm { get; set; }
        public double Accuracy { get; set; }

        public virtual User User { get; set; }
        public virtual TestSetting TestSetting { get; set; }

    }

}
