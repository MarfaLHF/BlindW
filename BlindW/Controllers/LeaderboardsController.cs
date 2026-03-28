using BlindW.Data;
using BlindW.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlindW.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardsController : ControllerBase
    {
        private readonly DataContext _context;

        public LeaderboardsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopLeaderboard(int count = 10)
        {
            var top = await _context.Leaderboards
                .OrderByDescending(l => l.TestResult.Wpm)
                .ThenByDescending(l => l.TestResult.Accuracy)
                .Take(count)
                .Select((l) => new
                {
                    leaderboardId = l.LeaderboardId,
                    testResultId = l.TestResultId,
                    userId = l.TestResult.UserId,
                    userName = l.TestResult.User.UserName,
                    email = l.TestResult.User.Email,
                    firstName = l.TestResult.User.FirstName,
                    lastName = l.TestResult.User.LastName,
                    login = l.TestResult.User.Login,
                    wpm = l.TestResult.Wpm,
                    accuracy = l.TestResult.Accuracy,
                    countCharacters = l.TestResult.CountCharacters,
                    totalTime = l.TestResult.TotalTime,
                    testDateTime = l.TestResult.TestDateTime
                })
                .ToListAsync();

            return Ok(top);
        }
    }
}
