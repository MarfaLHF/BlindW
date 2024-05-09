namespace BlindW.Data.Models
{
    public class TestDuration
    {
        public int TestDurationId { get; set; }
        public int Duration { get; set; } // продолжительность теста для тестирования по времени /15 30 60 10 
        public virtual ICollection<TestSetting> TestSetting { get; set; }

    }
}
