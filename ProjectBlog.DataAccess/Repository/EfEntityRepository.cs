using Microsoft.EntityFrameworkCore;
using ProjectBlog.Core.DataAccess.Data;
using ProjectBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProjectBlog.DataAccess.Repository
{
    public class EfEntityRepositoryBase<T> : IEntityRepository<T> where T : BaseEntity
    {
        protected readonly DataContext context;

        public EfEntityRepositoryBase(DataContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            //context.Set<T>().Add(entity);
            entity.OperationDate = DateTime.Now;
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
        }
        public void Update(T entity)
        {
            entity.OperationDate = DateTime.Now;
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
        }


        public void StatusDelete(T entity)
        {
            entity.OperationDate = DateTime.Now;
            entity.Status = false;
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
        }
        public void Delete(T entity)
        {
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
        }

        public T Get(Expression<Func<T, bool>> filter = null)
        {
            return context.Set<T>().SingleOrDefault(filter);
        }

        public IQueryable<T> GetList(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? context.Set<T>() : context.Set<T>().Where(filter);
        }

     
    }
}
