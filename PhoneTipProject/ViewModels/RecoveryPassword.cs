using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneTipProject.ViewModels
{
    public class RecoveryPassword
    {
        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا کلمه عبور خود را وارد کنید")]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا تکرار کلمه عبور خود را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("PassWord", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        public string RePassWord { get; set; }
    }
}