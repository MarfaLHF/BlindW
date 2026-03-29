namespace BlindW.Data.Models
{
    public class TestType
    {
        public int TestTypeId { get; set; }

        public string Name { get; set; } = null!; // по словам / по времени

        public virtual ICollection<TestSetting> TestSettings { get; set; } = new List<TestSetting>();
    }
}