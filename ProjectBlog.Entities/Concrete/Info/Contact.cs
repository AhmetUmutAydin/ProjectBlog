using ProjectBlog.Entities.ComplexTypes.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectBlog.Entities.Concrete.Info
{
    [Table("Contact", Schema = "Info")]
    public class Contact:BaseEntity
    {
        [Required]
        public ContactType Type { get; set; }

        [Required]
        [StringLength(100)]
        public string Value { get; set; }

        [ForeignKey("PersonalInfo")]
        public int PersonalInfoId { get; set; }

        public virtual PersonalInfo PersonalInfo { get; set; }
}
}
