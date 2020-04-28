using ProjectBlog.DataAccess.Repository;
using ProjectBlog.Entities.Concrete.Info;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.DataAccess.Abstract.Info
{
    public interface IContactDal : IEntityRepository<Contact>
    {
    }
}
