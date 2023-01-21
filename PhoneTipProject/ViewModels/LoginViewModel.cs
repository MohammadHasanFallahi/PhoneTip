using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneTipProject.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name ="ایمیل")]
        [Required(ErrorMessage ="لطفا ایمیل خود را جهت ورود به سایت وارد کنید")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name ="کلمه عبور")]
        [Required(ErrorMessage ="لطفا کلمه عبور خود را جهت ورود به سایت وارد کنید")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Display(Name ="مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}