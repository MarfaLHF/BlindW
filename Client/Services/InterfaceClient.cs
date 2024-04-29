using BlindW.Data.Models;
using Refit;

namespace Client.Services
{
    public interface InterfaceClient
    {
        [Get("/Lessons")] 
        Task<IEnumerable<Lesson>> GetLessons();
    }
}
