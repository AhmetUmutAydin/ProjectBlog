using ProjectBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProjectBlog.DataAccess.Repository
{
    public interface IEntityRepository<T> where T : BaseEntity
    {
        T Get(Expression<Func<T, bool>> filter = null);

        IQueryable<T> GetList(Expression<Func<T, bool>> filter = null);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
        void StatusDelete(T entity);

    }
}
