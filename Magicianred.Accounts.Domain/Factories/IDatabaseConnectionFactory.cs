using System.Data;

namespace Magicianred.Accounts.Domain.Factories
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetConnection();
    }
}
