using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reponsitories
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        T FindByID(int id);
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
        public Task SaveChangeAsync();
    }
}
