using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectBlog.Entities.Concrete.Auth
{
    [Table("UserRole", Schema = "Auth")]
    public class UserRole:BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User user { get; set; }
    }
}
