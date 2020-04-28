using ProjectBlog.Entities.ComplexTypes.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.Entities.ComplexTypes.Dtos.Auth
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public ApplicationType ApplicationType { get; set; }
    }
}
