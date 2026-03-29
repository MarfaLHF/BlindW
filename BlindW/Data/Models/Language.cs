namespace BlindW.Data.Models
{
    public class Language
    {
        public int LanguageId { get; set; }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public virtual ICollection<TestSetting> TestSettings { get; set; } = new List<TestSetting>();
    }
}