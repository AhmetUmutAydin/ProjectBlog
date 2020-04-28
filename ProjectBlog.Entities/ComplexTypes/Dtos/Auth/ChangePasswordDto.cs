using ProjectBlog.Entities.Concrete.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.Entities.ComplexTypes.Dtos.Auth
{
    public class ChangePasswordDto
    {
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string OldPassword { get; set; }//Normal şifre değiştirmede kullanılır ilk 3 ü kullanılır
        public string CryptedText { get; set; }//Şifremi unuttum da kullanılır ilk 2si  ve bu kullanılır
        public User User { get; set; }

    }
}
