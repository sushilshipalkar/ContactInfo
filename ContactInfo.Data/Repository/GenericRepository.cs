using ContactInfo.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfo.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;
        protected DbSet<TEntity> entities;
        string errorMessage = string.Empty;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            context.Configuration.LazyLoadingEnabled = false;
            this.entities = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get()
        {
            IEnumerable<TEntity> query = entities;
            return query.ToList();
        }

        /// <summary>
        /// Retrieve data by Id and Related data by passing table name
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> Id, string includeProperties = "")
        {
            IQueryable<TEntity> query = entities;
            query = query.Where(Id);
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return query.ToList();
        }

        /// <summary>
        /// Retrieve all rows and Related data by passing table name
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        /// <CreatedByDate>Dk, 4 Jan,2016</CreatedByDate>
        public virtual IEnumerable<TEntity> Get(string includeProperties = "")
        {
            IQueryable<TEntity> query = entities;

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query.ToList();
        }

        public TEntity GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public void Insert(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                this.Entities.Add(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Update(TEntity entity)
        {
            try
            {
                entities.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                this.context.SaveChanges();
                //_unitOfWork.Dispose();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = entities.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            entities.Attach(entityToDelete);
            entities.Remove(entityToDelete);
            this.context.SaveChanges();
        }

        public virtual IQueryable<TEntity> Table
        {
            get
            {
                return this.Entities;
            }
        }

        private IDbSet<TEntity> Entities
        {
            get
            {
                if (entities == null)
                {
                    entities = context.Set<TEntity>();
                }
                return entities;
            }
        }

        public virtual void AddOrUpdate(TEntity entity, int Id = 0)
        {
            if (Id > 0)
            {
                var attachedEntity = this.Entities.Find(Id);
                if (attachedEntity != null && context.Entry(attachedEntity).State != EntityState.Detached)
                {
                    context.Entry(attachedEntity).State = EntityState.Detached;
                }

                entities.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                entities.Add(entity);
            }
        }

        public virtual void AddRange(List<TEntity> entity)
        {
            entities.AddRange(entity);
            this.context.SaveChanges();
        }

        public virtual IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            context.Database.CommandTimeout = 120;
            return context.Database.SqlQuery<TEntity>(query, parameters);
        }

        /// <summary>
        /// Execute Stored procedure / Plain SQL Query with Parameter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
        {
            return context.Database.SqlQuery<T>(sql, parameters);
        }

        public virtual void ExecuteSqlCommand(string query, params object[] parameters)
        {
            context.Database.CommandTimeout = 120;
            context.Database.ExecuteSqlCommand(query, parameters);
        }

    }
}
