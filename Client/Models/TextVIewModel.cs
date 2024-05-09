namespace Client.Models
{
    public class TextViewModel
    {
        public string Text { get; set; }
        public int WordCount { get; set; }
        public int CharCount { get; set; }
        public double Wpm { get; set; } 
        public double Accuracy { get; set; }
        public double TotalTime { get; set; }
    }

}
