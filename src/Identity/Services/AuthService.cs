using Identity.Domain;

namespace Identity.Services;

public class AuthService : IAuthService 
{
    public bool Create(User user)
    {
        return true;
    }
}