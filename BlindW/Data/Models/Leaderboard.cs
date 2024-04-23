namespace BlindW.Data.Models
{
    public class Leaderboard
    {
        public int LeaderboardId { get; set; }
        public int TestResultId { get; set; }
        public string RankingType { get; set; }

        public virtual TestResult TestResult { get; set; }
    }
}
