using ProjectBlog.DataAccess.Repository;
using ProjectBlog.Entities.Concrete.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.DataAccess.Abstract.Auth
{
    public interface IUserRoleDal :  IEntityRepository<UserRole>
    {
    }
}
