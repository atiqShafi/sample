using System;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Sample.Core.Db.Context
{
    public interface IDbContext : IDisposable
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        void SaveChanges();
        IQueryable<TEntity> Get<TEntity>() where TEntity : class;
        DbRawSqlQuery<TEntity> Query<TEntity>(string sql, object[] parameters);
    }
}
