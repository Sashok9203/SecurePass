using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> filter = null);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null);

        bool Any(Expression<Func<TEntity, bool>> filter = null);

        TEntity? GetByID(object id);

        Task<TEntity?> GetByIDAsync(object id);

        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

    }
}
