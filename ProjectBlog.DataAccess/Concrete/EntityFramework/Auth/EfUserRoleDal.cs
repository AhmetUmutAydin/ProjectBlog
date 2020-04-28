using ProjectBlog.Core.DataAccess.Data;
using ProjectBlog.DataAccess.Abstract.Auth;
using ProjectBlog.DataAccess.Repository;
using ProjectBlog.Entities.Concrete.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.DataAccess.Concrete.EntityFramework.Auth
{
    public class EfUserRoleDal : EfEntityRepositoryBase<UserRole>, IUserRoleDal
    {
        public EfUserRoleDal(DataContext context) : base(context)
        { }
    }
}
