using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Sample.Core.Db.DbModels;

namespace Sample.Core.Db.Context
{
    public class AppDbContext : DbContext, IDbContext
    {
        public DbSet<User> Users { get; set; }
        
        public AppDbContext() : base("AppDb")
        {
            Database.SetInitializer<AppDbContext>(null); // disable auto create database
            Configuration.LazyLoadingEnabled = false; // disable lazy loading 
            Configuration.ProxyCreationEnabled = false; // disable proxy generation
        }

        public AppDbContext(string cs) : base(cs)
        {
            Database.SetInitializer<AppDbContext>(null); 
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        void IDbContext.SaveChanges()
        {
            SaveChanges();
        }

        void IDbContext.Add<T>(T entity)
        {
            Set<T>().Add(entity);
        }

        void IDbContext.Update<T>(T entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        void IDbContext.Remove<T>(T entity)
        {
            Set<T>().Remove(entity);
        }

        public IQueryable<TEntity> Get<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }


        public DbRawSqlQuery<TEntity> Query<TEntity>(string sql, object[] parameters)
        {
            return Database.SqlQuery<TEntity>(sql, parameters);
        }
    }
}