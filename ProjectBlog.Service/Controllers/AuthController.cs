using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectBlog.Business.Abstract;
using ProjectBlog.Entities.ComplexTypes.Dtos.Auth;
using ProjectBlog.Entities.ComplexTypes.Dtos.Helpers;
using ProjectBlog.Entities.ComplexTypes.Enums;
using ProjectBlog.Entities.Concrete.Auth;
using ProjectBlog.Entities.Concrete.Info;
using ProjectBlog.Service.Extensions;

namespace ProjectBlog.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private IAuthService authManager;
        private IConfiguration configuration;

        public AuthController(IAuthService authManager,IConfiguration configuration)
        {
            this.authManager = authManager;
            this.configuration = configuration;
        }

         [HttpGet("setAdmin")]
         public ActionResult<User> setAdmin()
         {
            var user= authManager.CreateAdminUser();
            return Ok(user);
         }
        //giriş yap
        [HttpPost("login")]
        public ActionResult<string> Login(LoginDto loginDto)
        {
           ServiceResult<string> login= authManager.Login(loginDto, configuration.GetSection("AppSettings:Token").Value);
            if (login.ResultType == ResultType.Success)
                    return Ok(login.Entity);           
            else
                return NotFound(login.Message);
        }
        //kayıt ol
        [HttpPost("register")]
        public ActionResult<User> Register(RegisterDto registerDto)
        {
            ServiceResult<User> register = authManager.RegisterUser(registerDto);
            if (register.ResultType == ResultType.Success)
                return Ok(register.Entity);
            else
                return NotFound(register.Message);
        }
        //hesabı onayla
        [HttpPost("ConfirmAccount")]
        public ActionResult<User> ConfirmAccount(string key)
        {
            ServiceResult<User> confirm = authManager.ConfirmUser(key);
            if (confirm.ResultType == ResultType.Success)
                return Ok(confirm.Entity);
            else
                return NotFound(confirm.Message);
        }
        // kayıtlı user şifre değiştir
        [HttpPost("ChangePassword")]
        [Authorize]
        public ActionResult<User> ChangePassword(ChangePasswordDto passwordDto)
        {
            ServiceResult<User> change = authManager.ChangePassword(passwordDto,Jwt.UserId);
            if (change.ResultType == ResultType.Success)
                return Ok(change.Entity);
            else
                return NotFound(change.Message);
        }
        //şifremi unuttum post ilk aşama
        [HttpPost("ForgotPassword")]
        public ActionResult<string> ForgotPassword(string email)
        {
            ServiceResult<string> forgot = authManager.ForgotPassword(email);
            if (forgot.ResultType == ResultType.Success)
                return Ok(forgot.Entity);
            else
                return NotFound(forgot.Message);
        }
        //şifremi unuttum maile tıklayınca get
        [HttpGet("GetUsernameForgatPass")]
        public ActionResult<string> GetUsernameForgatPass(string key)
        {
            ServiceResult<string> username = authManager.GetUsernameForForgotPassword(key);
            if (username.ResultType == ResultType.Success)
                return Ok(username.Entity);
            else
                return NotFound(username.Message);
        }
        //Şifremi unuttum Son aşama kayıt
        [HttpPost("ForgotPasswordChange")]
        public ActionResult<User> ForgotPasswordChange(ChangePasswordDto passwordDto)
        {
            ServiceResult<User> change = authManager.ForgotPasswordChange(passwordDto);
            if (change.ResultType == ResultType.Success)
                return Ok(change.Entity);
            else
                return NotFound(change.Message);
        }
        //Kullanıcı Bilgilerini alıyor info ve contactları
        [HttpGet("GetUserInfo")]
        [Authorize]
        public ActionResult<PersonalInfo> GetUserInfo()
        {
            ServiceResult<PersonalInfo> personal = authManager.GetUsersInfoAndContacts(Jwt.UserId);
            if (personal.ResultType == ResultType.Success)
                return Ok(personal.Entity);
            else
                return NotFound(personal.Message);
        }
        //Kullanıcı Bilgileri Güncelleme,Ekleme,Silme
        [HttpPost("postUserInfo")]
        [Authorize]
        public ActionResult<PersonalInfo> GetUserInfo(PersonalInfo personalInfo)
        {
            ServiceResult<PersonalInfo> personal = authManager.SavePersonalInfoContact(personalInfo,Jwt.UserId);
            if (personal.ResultType == ResultType.Success)
                return Ok(personal.Entity);
            else
                return NotFound(personal.Message);
        }
     }
}