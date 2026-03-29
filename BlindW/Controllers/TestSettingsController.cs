using BlindW.Controllers.Requests;
using BlindW.Controllers.Responses;
using BlindW.Data;
using BlindW.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlindW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestSettingsController : ControllerBase
    {
        private readonly DataContext _context;

        public TestSettingsController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("find-or-create")]
        public async Task<ActionResult<TestSettingResponse>> FindOrCreate(TestSettingRequest request)
        {
            var testType = await _context.TestTypes.FindAsync(request.TestTypeId);
            if (testType == null)
                return BadRequest("Некорректный TestTypeId.");

            var language = await _context.Languages.FindAsync(request.LanguageId);
            if (language == null)
                return BadRequest("Некорректный LanguageId.");

            if (request.TestTypeId == 1) // по словам
            {
                if (request.WordCountId == null)
                    return BadRequest("Для режима 'по словам' требуется WordCountId.");

                if (request.TestDurationId != null)
                    return BadRequest("Для режима 'по словам' TestDurationId должен быть null.");

                var wordCount = await _context.WordCounts.FindAsync(request.WordCountId);
                if (wordCount == null)
                    return BadRequest("Некорректный WordCountId.");
            }
            else if (request.TestTypeId == 2) // по времени
            {
                if (request.TestDurationId == null)
                    return BadRequest("Для режима 'по времени' требуется TestDurationId.");

                if (request.WordCountId != null)
                    return BadRequest("Для режима 'по времени' WordCountId должен быть null.");

                var duration = await _context.TestDurations.FindAsync(request.TestDurationId);
                if (duration == null)
                    return BadRequest("Некорректный TestDurationId.");
            }
            else
            {
                return BadRequest("Неизвестный тип теста.");
            }

            var existingSetting = await _context.TestSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(ts =>
                    ts.IsPunctuationEnabled == request.IsPunctuationEnabled &&
                    ts.IsNumbersEnabled == request.IsNumbersEnabled &&
                    ts.TestTypeId == request.TestTypeId &&
                    ts.WordCountId == request.WordCountId &&
                    ts.TestDurationId == request.TestDurationId &&
                    ts.LanguageId == request.LanguageId
                );

            if (existingSetting != null)
            {
                return Ok(MapToResponse(existingSetting));
            }

            var newSetting = new TestSetting
            {
                IsPunctuationEnabled = request.IsPunctuationEnabled,
                IsNumbersEnabled = request.IsNumbersEnabled,
                TestTypeId = request.TestTypeId,
                WordCountId = request.WordCountId,
                TestDurationId = request.TestDurationId,
                LanguageId = request.LanguageId
            };

            _context.TestSettings.Add(newSetting);
            await _context.SaveChangesAsync();

            return Ok(MapToResponse(newSetting));
        }

        private static TestSettingResponse MapToResponse(TestSetting testSetting)
        {
            return new TestSettingResponse
            {
                TestSettingId = testSetting.TestSettingId,
                IsPunctuationEnabled = testSetting.IsPunctuationEnabled,
                IsNumbersEnabled = testSetting.IsNumbersEnabled,
                TestTypeId = testSetting.TestTypeId,
                WordCountId = testSetting.WordCountId,
                TestDurationId = testSetting.TestDurationId,
                LanguageId = testSetting.LanguageId
            };
        }
    }
}