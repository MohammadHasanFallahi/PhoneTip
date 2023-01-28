using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhoneTipProject.ViewModels
{
    public class PageCommentsViewModel
    {
        [Display(Name = "شناسه کامنت")]
        public int CommentID { get; set; }
        [Display(Name = "عنوان خبر")]
        public string PageTitel { get; set; }
        [Display(Name = "نام کاربری")]
        public string FullName { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "نظرات")]
        public string Comment { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ثبت")]
        public DateTime CreateDate { get; set; }
    }
}