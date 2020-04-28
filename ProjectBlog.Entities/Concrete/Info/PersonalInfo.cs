using ProjectBlog.Entities.ComplexTypes.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectBlog.Entities.Concrete.Info
{
    [Table("PersonalInfo", Schema = "Info")]
    public class PersonalInfo:BaseEntity
    {
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Surname { get; set; }
        public Nullable<Gender> Gender { get; set; }
        public Nullable<DateTime> Birthdate { get; set; }
        [StringLength(12)]
        public string NationalNumber { get; set; }
        public virtual List<Contact> PersonalContacts { get; set; }

    }
}
