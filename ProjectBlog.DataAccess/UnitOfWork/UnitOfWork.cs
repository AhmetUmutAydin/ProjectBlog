using ProjectBlog.Core.DataAccess.Data;
using ProjectBlog.DataAccess.Abstract.Auth;
using ProjectBlog.DataAccess.Abstract.Info;
using ProjectBlog.DataAccess.Concrete.EntityFramework.Auth;
using ProjectBlog.DataAccess.Concrete.EntityFramework.Info;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        private IUserDal _UserDal;
        public IUserDal UserDal
        {
            get
            {
                if (_UserDal == null)
                    _UserDal = new EfUserDal(context);
                return _UserDal;
            }
        }

        private IUserRoleDal _UserRoleDal;
        public IUserRoleDal UserRoleDal
        {
            get
            {
                if (_UserRoleDal == null)
                    _UserRoleDal = new EfUserRoleDal(context);
                return _UserRoleDal;
            }
        }
        private IContactDal _ContactDal;
        public IContactDal ContactDal
        {
            get
            {
                if (_ContactDal == null)
                    _ContactDal = new EfContactDal(context);
                return _ContactDal;
            }
        }

        private IPersonalInfoDal _PersonalInfoDal;
        public IPersonalInfoDal PersonalInfoDal
        {
            get
            {
                if (_PersonalInfoDal == null)
                    _PersonalInfoDal = new EfPersonalInfoDal(context);
                return _PersonalInfoDal;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public bool save()
        {
            try
            {
                return context.SaveChanges() > 0;
            }
            catch(Exception e) {
                return false;//log tut
            }
        }

    }
}
