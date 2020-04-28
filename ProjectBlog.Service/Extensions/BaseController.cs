using Microsoft.AspNetCore.Mvc;
using ProjectBlog.Entities.ComplexTypes.Dtos.Helpers;
using ProjectBlog.Entities.ComplexTypes.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectBlog.Service.Extensions
{
    public class BaseController:ControllerBase
    {
        private Jwt jwt;
        public Jwt Jwt {
            get {
                if (User != null)
                {
                    jwt = new Jwt();
                    if (User.FindFirst(ClaimTypes.NameIdentifier) != null)
                        jwt.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    if (!String.IsNullOrEmpty(User.FindFirst(ClaimTypes.Name).Value))
                        jwt.Username = User.FindFirst(ClaimTypes.Name).Value;
                    if (User.FindFirst("ApplicationType") != null)
                        jwt.ApplicationType =(ApplicationType)int.Parse(User.FindFirst("ApplicationType").Value);
                    jwt.Roles = User.FindAll(ClaimTypes.Role).Select(i => i.Value).ToList();
                    return jwt;
                }
                else
                    return null;
            }
        }
    }
}
