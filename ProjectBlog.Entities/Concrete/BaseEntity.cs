using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectBlog.Entities.Concrete
{
    public class BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public DateTime OperationDate { get; set; }

        [Required]
        [Timestamp]
        public Byte[] RowVersion { get; set; }
    }
}
