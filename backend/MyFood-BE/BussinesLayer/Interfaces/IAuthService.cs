using DataBaseLayer.ViewModels.Users;
using System.Threading.Tasks;

namespace BussinesLayer.Interfaces
{
    public interface IAuthService
    {
        Task<string> BuildToken(LoginUserVm user);
    }
}
