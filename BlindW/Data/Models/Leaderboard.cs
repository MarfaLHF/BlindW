using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlindW.Data.Models
{
    public class Leaderboard
    {
        [Key]
        public int LeaderboardId { get; set; }

        [Required]
        [ForeignKey(nameof(TestResult))]
        public int TestResultId { get; set; }

        [Required]
        public string RankingType { get; set; }

        public virtual TestResult TestResult { get; set; }
    }
}
