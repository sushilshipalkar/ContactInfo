using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfo.Data.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get();

        TEntity GetById(object id);

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter, string includeProperties = "");

        IEnumerable<TEntity> Get(string includeProperties = "");

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void AddOrUpdate(TEntity entity, int Id = 0);

        IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters);

        DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters);

        void ExecuteSqlCommand(string query, params object[] parameters);

        void AddRange(List<TEntity> entity);
    }
}
