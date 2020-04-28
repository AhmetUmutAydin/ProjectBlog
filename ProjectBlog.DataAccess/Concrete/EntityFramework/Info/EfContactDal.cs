using ProjectBlog.Core.DataAccess.Data;
using ProjectBlog.DataAccess.Abstract.Info;
using ProjectBlog.DataAccess.Repository;
using ProjectBlog.Entities.Concrete.Info;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.DataAccess.Concrete.EntityFramework.Info
{
    public class EfContactDal : EfEntityRepositoryBase<Contact>, IContactDal
    {
        public EfContactDal(DataContext context) : base(context)
        { }


    }
}
