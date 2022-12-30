using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhoneTipProject.Models.DataLayer
{
    [Table("PageComments", Schema = "dbo")]
    public class PageComments
    {
        [Key]
        public int CommentID { get; set; }
        [Display(Name = "خبر")]
        [Required]
        public int PageID { get; set; }
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفا نام خود را وارد کنید")]
        [MaxLength(150)]
        public string FullName { get; set; }
        [Display(Name = "ایمیل")]
        [MaxLength(200)]
        public string Email { get; set; }
        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا نظر خود را برای ما ثبت کنید")]
        [MaxLength(500)]
        public string Comment { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateDate { get; set; }

        [ForeignKey("PageID")]
        public virtual Pages Pages { get; set; }

    }
}