using SiteYonetimApp.Application.Features.Auth.Login;
using SiteYonetimApp.Domain.Entities;

namespace SiteYonetimApp.Application.Services
{
    public interface IJwtProvider
    {
        Task<LoginCommandResponse> CreateToken(AppUser user);
    }
}
