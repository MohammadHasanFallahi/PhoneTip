using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneTipProject.Models.DataLayer
{
    [Table("Users", Schema = "dbo")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }
        [Display(Name = "نقش کاربر")]
        [Required(ErrorMessage = "لطفا نقش کاربر را مشخص کنید")]
        public int RoleID { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا نام کاربری خود را مشخص کنید")]
        [MaxLength(250)]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا ایمیل خود را مشخص کنید")]
        [MaxLength(250)]
        public string Email { get; set; }
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا کلمه عبور را مشخص کنید")]
        [MaxLength(200)]
        public string PassWord { get; set; }
        [Display(Name = "کد فعال سازی")]
        [Required(ErrorMessage = "لطفا کد فعال سازی را مشخص کنید")]
        [MaxLength(50)]
        public string ActiveCode { get; set; }
        [Display(Name = "وضعیت کاربر")]
        [Required(ErrorMessage = "لطفا وضعیت کاربر را مشخص کنید")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "لطفا تاریخ ثبت را مشخص کنید")]
        public DateTime RegisterDate { get; set; }

        [ForeignKey("RoleID")]
        public virtual Roles Roles { get; set; }

    }
}