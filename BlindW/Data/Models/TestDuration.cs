namespace BlindW.Data.Models
{
    public class TestDuration
    {
        public int TestDurationId { get; set; }

        public int Duration { get; set; }

        public virtual ICollection<TestSetting> TestSettings { get; set; } = new List<TestSetting>();
    }
}