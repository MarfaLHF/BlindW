using BlindW.Data;
using BlindW.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlindW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        private readonly DataContext _context;

        public LeaderboardController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Leaderboard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leaderboard>>> GetLeaderboard()
        {
            var leaders = await _context.Leaderboards
                .Include(l => l.TestResult)
                .OrderByDescending(l => l.TestResult.Wpm)
                .Take(10)
                .ToListAsync();

            return Ok(leaders);
        }

        // POST: api/Leaderboard
        [HttpPost]
        public async Task<ActionResult<Leaderboard>> PostLeaderboard(Leaderboard leaderboard)
        {
            _context.Leaderboards.Add(leaderboard);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLeaderboard), new { id = leaderboard.LeaderboardId }, leaderboard);
        }
    }
}
