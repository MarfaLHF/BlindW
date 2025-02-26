﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlindW.Data;
using BlindW.Data.Models;
using BlindW.Controllers.Requests;

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
        public async Task<ActionResult<TestResult>> GetTestResult(int id)
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
        public async Task<IActionResult> PutTestResult(int id, TestResult testResult)
        {
            if (id != testResult.TestResultId)
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
        public async Task<ActionResult<TestResult>> PostTestResult(Result testResult)
        {
            var result = new TestResult
            {
                UserId = testResult.UserId,
                TestSettingId = testResult.TestSettingId,
                TestDateTime = DateTime.UtcNow,
                CountCharacters = testResult.CountCharacters,
                TotalTime = testResult.TotalTime,
                Wpm = testResult.Wpm,
                Accuracy = testResult.Accuracy

            };
            _context.TestResults.Add(result);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTestResult", new { id = testResult.TestResultId }, testResult);
        }


        // DELETE: api/TestResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestResult(int id)
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

        private bool TestResultExists(int id)
        {
            return _context.TestResults.Any(e => e.TestResultId == id);
        }


        // GET: api/TestResults/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TestResult>>> GetUserTestResults(string userId)
        {
            var userTestResults = await _context.TestResults
                .Where(tr => tr.UserId == userId)
                .ToListAsync();

            if (userTestResults == null || userTestResults.Count == 0)
            {
                return NotFound();
            }

            return userTestResults;
        }

    }
}
