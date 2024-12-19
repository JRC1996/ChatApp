using ChatApp.Models.Response;
using ChatApp.Models.ViewModels;

namespace ChatApp.Models.Services
{
    public interface IUserService
    {

        Task <UserResponse>  Auth(AuthViewmodel model, CancellationToken cancellation);
    }
}
