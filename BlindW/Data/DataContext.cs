using BlindW.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlindW.Data
{
    public class DataContext : IdentityDbContext<User>
    {

        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<TestSetting> TestSettings { get; set; }
        public DbSet<TestType> TestTyps { get; set; }
        public DbSet<WordCount> WordCounts { get; set; }
        public DbSet<TestDuration> TestDurations { get; set; }
        public DbSet<Leaderboard> Leaderboards { get; set; }

    }
}
