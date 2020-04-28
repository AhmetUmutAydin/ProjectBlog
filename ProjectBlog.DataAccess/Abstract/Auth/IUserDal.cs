using ProjectBlog.DataAccess.Repository;
using ProjectBlog.Entities.Concrete.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProjectBlog.DataAccess.Abstract.Auth
{
    public interface IUserDal : IEntityRepository<User>
    {
        User GetUserByEmail(string email);
        User GetUserWithRole(string username);
    }
}
