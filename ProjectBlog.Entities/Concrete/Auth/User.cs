using ProjectBlog.Entities.ComplexTypes.Enums;
using ProjectBlog.Entities.Concrete.Info;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectBlog.Entities.Concrete.Auth
{
    [Table("User", Schema = "Auth")]
    public class User:BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        [Required]
        public bool Confirmation { get; set; }
        [Required]
        public ApplicationType ApplicationType { get; set; }

        [Required]
        [ForeignKey("PersonalInfo")]
        public int PersonalInfoId { get; set; }

        public virtual List<UserRole> UserRole { get; set; }
        public virtual PersonalInfo PersonalInfo { get; set; }

    }
}
