using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    [Table("tblUsers")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(maximumLength:255)]
        public string Name { get; set; }

        [EmailAddress]
        [Required,StringLength(maximumLength:128)]
        public string Email { get; set; }

        [StringLength(maximumLength:50)]
        public string Telephone { get; set; }

        [Required, StringLength(maximumLength:128)]
        public string Password { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

    }
}
