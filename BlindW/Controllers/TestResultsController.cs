using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlindW.Data;
using BlindW.Data.Models;

namespace BlindW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultsController : ControllerBase
    {
        private readonly DataContext _context;

        public TestResultsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TestResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestResult>>> GetTestResults()
        {
            return await _context.TestResults.ToListAsync();
        }

        // GET: api/TestResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestResult>> GetTestResult(string id)
        {
            var testResult = await _context.TestResults.FindAsync(id);

            if (testResult == null)
            {
                return NotFound();
            }

            return testResult;
        }

        // PUT: api/TestResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestResult(string id, TestResult testResult)
        {
            if (id != testResult.Id)
            {
                return BadRequest();
            }

            _context.Entry(testResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestResultExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TestResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestResult>> PostTestResult(TestResult testResult)
        {
            _context.TestResults.Add(testResult);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TestResultExists(testResult.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTestResult", new { id = testResult.Id }, testResult);
        }

        // DELETE: api/TestResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestResult(string id)
        {
            var testResult = await _context.TestResults.FindAsync(id);
            if (testResult == null)
            {
                return NotFound();
            }

            _context.TestResults.Remove(testResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestResultExists(string id)
        {
            return _context.TestResults.Any(e => e.Id == id);
        }
    }
}
