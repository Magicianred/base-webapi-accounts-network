namespace Magicianred.Accounts.Domain.Interfaces.Handlers
{
    public interface IPasswordHandler
    {
        string HashPassword(string password);
        bool PasswordMatches(string providedPassword, string passwordHash);
    }
}
