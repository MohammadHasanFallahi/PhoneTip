using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneTipProject.Models.DataLayer
{
    [Table("Roles", Schema = "dbo")]
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا عنوان نقش را مشخص کنید")]
        [MaxLength(250)]
        public string RoleTitel { get; set; }
        [Display(Name = "نام نقش")]
        [Required(ErrorMessage = "لطفا نام نقش را مشخص کنید")]
        [MaxLength(150)]
        public string RoleName { get; set; }

        public virtual ICollection<Users> Users { get; set; }

        public Roles()
        {
            Users = new List<Users>();
        }
    }
}