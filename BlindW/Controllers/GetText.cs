using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlindW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetText : ControllerBase
    {
        private readonly HttpClient _client;

        public GetText(HttpClient client)
        {
            _client = client;
        }

        [HttpGet("randomText")]
        public async Task<string> GetRandomText(int wordCount)
        {
            // Запрос к внешнему сервису для получения случайных слов
            var response = await _client.GetStringAsync($"https://random-word-api.herokuapp.com/word?number={wordCount}");

            // Возвращаем случайный текст как строку
            return response;
        }
    }
}
