using ProjectBlog.Entities.ComplexTypes.Dtos.Auth;
using ProjectBlog.Entities.ComplexTypes.Dtos.Helpers;
using ProjectBlog.Entities.Concrete.Auth;
using ProjectBlog.Entities.Concrete.Info;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.Business.Abstract
{
    public interface IAuthService
    {
        ServiceResult<string> HandleToken(User user, string salt);
        ServiceResult<User> CreateAdminUser();
        ServiceResult<User> RegisterUser(RegisterDto registerDto);
        ServiceResult<string> Login(LoginDto loginDto,string salt);
        ServiceResult<User> ConfirmUser(string CryptedKey);
        ServiceResult<User> ChangePassword(ChangePasswordDto passwordDto, int UserId);
        ServiceResult<string> ForgotPassword(string email);
        ServiceResult<string> GetUsernameForForgotPassword(string CryptedKey);

        ServiceResult<User> ForgotPasswordChange(ChangePasswordDto passwordDto);
        ServiceResult<PersonalInfo> GetUsersInfoAndContacts(int userId);
        ServiceResult<PersonalInfo> SavePersonalInfoContact(PersonalInfo personalInfo,int userId);

    }
}
