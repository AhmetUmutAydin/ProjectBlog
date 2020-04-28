using Microsoft.EntityFrameworkCore;
using ProjectBlog.Core.DataAccess.Data;
using ProjectBlog.DataAccess.Abstract.Auth;
using ProjectBlog.DataAccess.Abstract.Info;
using ProjectBlog.DataAccess.Repository;
using ProjectBlog.Entities.Concrete.Auth;
using ProjectBlog.Entities.Concrete.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProjectBlog.DataAccess.Concrete.EntityFramework.Info
{
     
    public class EfPersonalInfoDal : EfEntityRepositoryBase<PersonalInfo>, IPersonalInfoDal
    {
        public EfPersonalInfoDal(DataContext context) : base(context)
        { }

        public PersonalInfo GetPersonalInfoWithContactByUserId(int userId)
        {
            var info = (from user in context.User
                        join personal in context.PersonalInfo on user.PersonalInfoId equals personal.Id
                        let contacts = context.Contact.Where(x => x.PersonalInfoId == personal.Id && x.Status == true).ToList()
                        where user.Status == true && personal.Status == true && user.Id == userId
                        select new PersonalInfo
                        {
                             Birthdate=personal.Birthdate,
                             Gender=personal.Gender,
                             Name=personal.Name,
                             NationalNumber=personal.NationalNumber,
                             OperationDate=personal.OperationDate,
                             RowVersion=personal.RowVersion,
                             Status=personal.Status,
                             Id=personal.Id,
                             Surname=personal.Surname,
                             PersonalContacts = contacts
                        }).FirstOrDefault();
            return info;
        }
    }
}
