using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneTipProject.ViewModels
{
    public class RegistersViewModel
    {
        [Display(Name ="نام کاربری")]
        [Required(ErrorMessage ="لطفا نام کاربری را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا ایمیل خود را وارد کنید")]
        [EmailAddress(ErrorMessage ="لطفا ایمیل خود را به درستی ثبت کنید")]
        public string Email { get; set; }
        [Display(Name ="کلمه عبور")]
        [Required(ErrorMessage ="لطفا کلمه عبور خود را وارد کنید")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا تکرار کلمه عبور خود را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("PassWord",ErrorMessage ="کلمه های عبور مغایرت دارند")]
        public string RePassWord { get; set; }
    }
}