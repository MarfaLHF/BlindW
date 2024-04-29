using BlindW.Data.Models;
using Client.Models;
using Refit;

namespace Client.Services
{
    public interface InterfaceClient
    {
        [Post("/register")]
        Task<ApiResponse> RegisterUser(RegisterModel model);

    }
}
