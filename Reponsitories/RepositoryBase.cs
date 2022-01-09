using Entities.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reponsitories
{
    public class RepositoryBase<T>: IRepositoryBase<T> where T:class
    {
        protected BookStoreDbContext _dbContext;
        public RepositoryBase(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public T FindByID(int id)
        {
            return  _dbContext.Set<T>().Find(id);
        }

        public T Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return entity;
        }
        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
