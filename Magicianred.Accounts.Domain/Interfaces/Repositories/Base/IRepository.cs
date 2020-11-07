using Magicianred.Accounts.Domain.Interfaces.Models;

namespace Magicianred.Accounts.Domain.Interfaces.Repositories
{
    /// <summary>
    /// A generic repository
    /// https://cpratt.co/truly-generic-repository/
    /// </summary>
    public interface IRepository : IReadonlyRepository
    {
        void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity;

        void Delete<TEntity>(object id)
            where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity;
    }
}
