using ProjectBlog.Entities.ComplexTypes.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectBlog.Entities.ComplexTypes.Dtos.Helpers
{
    public class Jwt
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public ApplicationType ApplicationType { get; set; }
        public List<string> Roles { get; set; }
    }
}
