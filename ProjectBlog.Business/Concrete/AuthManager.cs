using Microsoft.IdentityModel.Tokens;
using ProjectBlog.Business.Abstract;
using ProjectBlog.Core.Extensions;
using ProjectBlog.DataAccess.Abstract.Auth;
using ProjectBlog.DataAccess.UnitOfWork;
using ProjectBlog.Entities.ComplexTypes.Dtos.Auth;
using ProjectBlog.Entities.ComplexTypes.Dtos.Helpers;
using ProjectBlog.Entities.Concrete.Auth;
using ProjectBlog.Entities.Concrete.Info;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ProjectBlog.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUnitOfWork uow;


        public AuthManager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        private void createPassword(string password,out byte[] passwordHash,out byte[] passwordSalt)
        {
            using(var crypt=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = crypt.Key;
                passwordHash = crypt.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        private bool VerifyPasswordHash(string password, byte[]userPasswordHash,byte[] userPasswordSalt)
        {
            using (var crypt = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = crypt.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPasswordHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private bool UserExists(string userName)
        {
            var user = uow.UserDal.Get(x => x.Username.Equals(userName));
            //Burda status'e bakmıyoruz sonradan pasif olan user aktif yapılabilir diye
            if (user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public User ChangeUserPassword(ChangePasswordDto passwordDto)
        {
            if (!passwordDto.NewPassword.Equals(passwordDto.ConfirmNewPassword) || String.IsNullOrEmpty(passwordDto.NewPassword) || String.IsNullOrEmpty(passwordDto.ConfirmNewPassword))
                return null;

            byte[] passHash, passSalt;
            createPassword(passwordDto.NewPassword, out passHash, out passSalt);
            passwordDto.User.PasswordHash = passHash;
            passwordDto.User.PasswordSalt = passSalt;

            uow.UserDal.Update(passwordDto.User);
            if (uow.save())
                return passwordDto.User;
            else
                return null;
        }

        public ServiceResult<string> HandleToken(User user, string salt)
        {
            var handle = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(salt);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Username),
                    new Claim("ApplicationType",user.ApplicationType.ToString())
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            foreach (var item in user.UserRole)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, item.Role));
            }

            var token = handle.CreateToken(tokenDescriptor);
            var tokenString = handle.WriteToken(token);

            return new ServiceResult<string>(tokenString,Entities.ComplexTypes.Enums.ResultType.Success);
        }
        public ServiceResult<User> CreateAdminUser()
        {
            User user = new User
            {
                Confirmation = true,
                Status = true,
                Username="admin",
            };

            string pass="admin";
            byte[] passHash, passSalt;
            createPassword(pass,out passHash,out passSalt);
            user.PasswordSalt = passSalt;
            user.PasswordHash = passHash;

            uow.UserDal.Add(user);
            if(uow.save())
                return new ServiceResult<User>(user);
            else
                return new ServiceResult<User>("Kayıt Oluşturulamadı!");

        }

        public ServiceResult<User> RegisterUser(RegisterDto registerDto)
        {
            if (UserExists(registerDto.Username) == true)
            {
                return new ServiceResult<User>("Böyle bir user var işlem yapılamaz");
            }
            if (registerDto.Username.Length<5 || registerDto.Password.Length < 5)
            {
                return new ServiceResult<User>("Şifre veya kullanıcı adı uygun değil");
            }
            byte[] passHash, passSalt;
            createPassword(registerDto.Password, out passHash, out passSalt);


            PersonalInfo personalInfo = new PersonalInfo
            {
                Status=true,
                Name=registerDto.Name,
                Surname=registerDto.Surname,
            };
            uow.PersonalInfoDal.Add(personalInfo);

            User user = new User
            {
                ApplicationType = registerDto.ApplicationType,
                Confirmation = false,
                PasswordHash = passHash,
                PasswordSalt = passSalt,
                Status = true,
                Username = registerDto.Username,
                PersonalInfo=personalInfo
            };
            uow.UserDal.Add(user);

            UserRole userRole = new UserRole
            {
                Status = true,
                user=user,
                Role="User"
            };
            uow.UserRoleDal.Add(userRole);

            Contact contact = new Contact
            {
                PersonalInfo=personalInfo,
                Status=true,
                Type=Entities.ComplexTypes.Enums.ContactType.email,
                Value=registerDto.Email
            };
            uow.ContactDal.Add(contact);

            if (uow.save())//Burda mail atıcaz doğrulama işlemi için
            {
                string confirmationCode=Crypto.Encrypt(registerDto.Username);//Bunun Query Strig ile Geri Gönderildiği bir kod yazılacak
                MailContent mailContent = new MailContent
                {
                    To=registerDto.Email,
                    Body="Başarılı Şekilde Kaydoldunuz.Hesabınız Onaylamak İçin Tıklayınız",
                    Title="Onaylama Maili"
                };
                SendMail.Send(mailContent);
                return new ServiceResult<User>(user);
            }
            else
                return new ServiceResult<User>("Böyle bir user zaten var işlem yapılamaz");

        }

        public ServiceResult<string> Login(LoginDto loginDto,string salt)
        {
            var user = uow.UserDal.GetUserWithRole(loginDto.Username);
            if (user == null)
            {
                return new ServiceResult<string>("Username Hatalı Böyle Bir Hesap Yok",Entities.ComplexTypes.Enums.ResultType.Error);
            }
            if (!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ServiceResult<string>("Şifre Hatalı", Entities.ComplexTypes.Enums.ResultType.Error);
            }
            if (user.Confirmation == false)
            {
                return new ServiceResult<string>("Hesap Onaylanmamış", Entities.ComplexTypes.Enums.ResultType.Error);
            }
            ServiceResult<string>token = HandleToken(user, salt);
            return token;
        }

        public ServiceResult<User> ConfirmUser(string CryptedKey)
        {
            if (String.IsNullOrEmpty(CryptedKey))
                return new ServiceResult<User>("Hatalı Onaylama Linki!");
            var username =Crypto.Decrypt(CryptedKey);
            var user = uow.UserDal.Get(x => x.Username == username && x.Status==true);
            if (user == null)
            {
                return new ServiceResult<User>("Hatalı Onaylama Linki!");
            }
            user.Confirmation = true;
            uow.UserDal.Update(user);
            if(uow.save())
                return new ServiceResult<User>(user);
            else
                return new ServiceResult<User>("Onaylama Başarısız!");
        }
        public ServiceResult<string> ForgotPassword(string email)
        {
            if(String.IsNullOrEmpty(email))
                return new ServiceResult<string>("Email Bilgisi Boş Olamaz");
            var user = uow.UserDal.GetUserByEmail(email);
            if(user==null)
                return new ServiceResult<string>("Böyle Bir Email Kayıtlı Değil");
            string cryptedEmail = Crypto.Encrypt(email);//Bunun Query Strig ile Geri Gönderildiği bir kod yazılacak
            MailContent mailContent = new MailContent
            {
                To = email,
                Body = "Şifrenizi Unuttum Seçeneği İle Şifrenizi Yenilemek İstediniz.Şifrenizi Yenilemek İçin Bu Linke Tıklayıp Şifrenizi Yenileyebilirsiniz.",
                Title = "Şifremi Unuttum!"
            };
            SendMail.Send(mailContent);
            return new ServiceResult<string>("Şifrenizi Yenilemek İçin Lütfen Email Adresinizi Kontrol Ediniz!", Entities.ComplexTypes.Enums.ResultType.Success);
        }

        public ServiceResult<User>ForgotPasswordChange(ChangePasswordDto passwordDto)
        {
            if (String.IsNullOrEmpty(passwordDto.CryptedText))
                return new ServiceResult<User>("Email Adresi Yanlış");

            string cryptedEmail = Crypto.Decrypt(passwordDto.CryptedText);

            if (String.IsNullOrEmpty(cryptedEmail))
                return new ServiceResult<User>("Email Adresi Yanlış");

            var user = uow.UserDal.GetUserByEmail(cryptedEmail);

            if(user==null)
                return new ServiceResult<User>("Email Adresi Yanlış");

            passwordDto.User = user;
            user = ChangeUserPassword(passwordDto);
            if (user != null)
                return new ServiceResult<User>(user);
            else
                return new ServiceResult<User>("İşlem Kaydedilemedi.Hatalı İşlem");
        }
        public ServiceResult<User>ChangePassword(ChangePasswordDto passwordDto,int UserId)
        {
            if (UserId == 0)
                return new ServiceResult<User>("Kullanıcı Girişi Yapınız");
            if(String.IsNullOrEmpty(passwordDto.OldPassword))
                return new ServiceResult<User>("Eski Şifre Alanı Boş Olamaz");

            var user = uow.UserDal.Get(x => x.Id == UserId);
            if(user==null)
                return new ServiceResult<User>("Kullanıcı Bulunamadı");

            if (!VerifyPasswordHash(passwordDto.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return new ServiceResult<User>("Eski Şifre Hatalı");
            }
            passwordDto.User = user;
            user = ChangeUserPassword(passwordDto);

            if (user != null)
                return new ServiceResult<User>(user);
            else
                return new ServiceResult<User>("İşlem Kaydedilemedi.Hatalı İşlem");
        }

        public ServiceResult<string> GetUsernameForForgotPassword(string CryptedKey)
        {
            if (String.IsNullOrEmpty(CryptedKey))
                return new ServiceResult<string>("Email Adresi Yanlış");

            string cryptedEmail = Crypto.Decrypt(CryptedKey);

            if (String.IsNullOrEmpty(cryptedEmail))
                return new ServiceResult<string>("Email Adresi Yanlış");

            var user = uow.UserDal.GetUserByEmail(cryptedEmail);

            if (user == null)
                return new ServiceResult<string>("Email Adresi Yanlış");
            return new ServiceResult<string>(user.Username);
        }

        public ServiceResult<PersonalInfo> GetUsersInfoAndContacts(int userId)
        {
            if (userId == 0)
                return new ServiceResult<PersonalInfo>("Kullanıcı Girişi Yapınız");
            var info = uow.PersonalInfoDal.GetPersonalInfoWithContactByUserId(userId);
            if (info == null)
                return new ServiceResult<PersonalInfo>("Böyle Bir Kullanıcı Bilgisi Yok");
            return new ServiceResult<PersonalInfo>(info);
        }

        public ServiceResult<PersonalInfo> SavePersonalInfoContact(PersonalInfo personalInfo,int userId)
        {
            if(userId == 0)
                return new ServiceResult<PersonalInfo>("Kullanıcı Girişi Yapınız!");
            if (personalInfo == null)
                return new ServiceResult<PersonalInfo>("İnfo Bilgisi Boş Olamaz!");

            return null;
        }
    }
}
