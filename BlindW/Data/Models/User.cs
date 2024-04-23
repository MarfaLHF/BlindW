using Microsoft.AspNetCore.Identity;

namespace BlindW.Data.Models
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int TestsTaken { get; set; }
        public int TestsCompleted { get; set; }
        public int BestResult15s { get; set; }
        public int BestResult30s { get; set; }
        public int BestResult60s { get; set; }
        public int BestResult10Words { get; set; }
        public int BestResult25Words { get; set; }
        public int BestResult50Words { get; set; }

        public virtual ICollection<TestResult> TestResults { get; set; }
    }

}
