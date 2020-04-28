using ProjectBlog.DataAccess.Abstract.Auth;
using ProjectBlog.DataAccess.Abstract.Info;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        bool save();
        IUserDal UserDal { get; }
        IUserRoleDal UserRoleDal { get; }
        IPersonalInfoDal PersonalInfoDal { get; }
        IContactDal ContactDal { get; }

    }
}
