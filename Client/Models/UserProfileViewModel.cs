using BlindW.Data.Models;

namespace Client.Models
{
    public class UserProfileViewModel
    {
        public User User { get; set; }
        public IEnumerable<TestResult> TestResults { get; set; }
    }

}
