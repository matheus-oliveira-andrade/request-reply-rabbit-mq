using Identity.Domain;

namespace Identity.Services;

public interface IAuthService
{
    bool Create(User user);
}