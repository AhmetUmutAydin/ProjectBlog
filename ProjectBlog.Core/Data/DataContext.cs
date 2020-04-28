using Microsoft.EntityFrameworkCore;
using ProjectBlog.Entities.Concrete;
using ProjectBlog.Entities.Concrete.Auth;
using ProjectBlog.Entities.Concrete.Info;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.Core.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<PersonalInfo> PersonalInfo { get; set; }


    }
}
