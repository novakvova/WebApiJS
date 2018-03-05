using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    [Table("tblRoles")]
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(maximumLength:128)]
        public string Name { get; set; }

        [StringLength(maximumLength:1000)]
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
