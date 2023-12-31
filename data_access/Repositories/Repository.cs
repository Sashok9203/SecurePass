﻿using data_access.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace data_access.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal SecurePassDBContext? context = null;

        internal DbSet<TEntity> dbSet;

        public Repository(SecurePassDBContext? context)
        {

            context ??= new SecurePassDBContext();
            this.dbSet = context.Set<TEntity>();
        }

        
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async virtual Task<TEntity?> GetByIDAsync(object id)
        {
           return await dbSet.FindAsync(id);
        }

        public virtual TEntity? GetByID(object id)
        {
            return  dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
           await dbSet.AddAsync(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity? entityToDelete = dbSet.Find(id);
            if (entityToDelete != null) Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context?.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            if (context != null) context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        TEntity? IRepository<TEntity>.GetByID(object id)
        {
            throw new NotImplementedException();
        }

        public TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                return query.FirstOrDefault(filter);
            else 
            return query.FirstOrDefault(); 
        }

        public bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                return query.Any(filter);
            else
                return query.Any();
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            if (filter != null)
                return await query.FirstOrDefaultAsync(filter);
            else
                return await query.FirstOrDefaultAsync();
        }
    }
}
