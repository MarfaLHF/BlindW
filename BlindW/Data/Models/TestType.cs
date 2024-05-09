namespace BlindW.Data.Models
{
    public class TestType
    {
        public int TestTypeId { get; set; }
        public string Name { get; set; } // тип тестирования: по буквам, по времени
        public virtual ICollection<TestSetting> TestSetting { get; set; }

    }
}
