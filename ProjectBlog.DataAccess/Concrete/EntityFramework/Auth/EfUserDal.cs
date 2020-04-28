using Microsoft.EntityFrameworkCore;
using ProjectBlog.Core.DataAccess.Data;
using ProjectBlog.DataAccess.Abstract.Auth;
using ProjectBlog.DataAccess.Repository;
using ProjectBlog.Entities.Concrete.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProjectBlog.DataAccess.Concrete.EntityFramework.Auth
{
    public class EfUserDal: EfEntityRepositoryBase<User>, IUserDal
    { 
         public EfUserDal( DataContext context) : base(context)
         {}

        public User GetUserByEmail(string email)
        {
            var selectedUser = (from contact in context.Contact
                                join personal in context.PersonalInfo on contact.PersonalInfoId equals personal.Id
                                join user in context.User on personal.Id equals user.PersonalInfoId
                                where contact.Status == true && user.Status == true && personal.Status == true && contact.Value.Equals(email)
                                select user).FirstOrDefault();
            return selectedUser;
        }
        public User GetUserWithRole(string username)
        {
            var user = context.User.Include(i => i.UserRole).FirstOrDefault(x => x.Username.Equals(username) && x.Status == true);
            return user;
        }
    }
}
