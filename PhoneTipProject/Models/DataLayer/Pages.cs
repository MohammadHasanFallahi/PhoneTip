using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhoneTipProject.Models.DataLayer
{
    [Table("Pages", Schema = "dbo")]
    public class Pages
    {
        [Key]
        public int PageID { get; set; }
        [Display(Name = "گروه صفحه")]
        [Required(ErrorMessage = "لطفا گروه خبر را مشخص کنید")]
        public int GroupID { get; set; }
        [Display(Name = "عنوان خبر")]
        [Required(ErrorMessage = " لطفا عنوان خبر را مشخص کنید")]
        [MaxLength(250)]
        public string Titel { get; set; }
        [Display(Name = "توضیح مختصر")]
        [Required(ErrorMessage = "لطفا توضیح مختصر را وارد کنید")]
        [MaxLength(350)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [Display(Name = "متن خبر")]
        [Required(ErrorMessage = "لطفا متن خبر را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [Display(Name = "بازدید")]
        public int Visit { get; set; }
        [Display(Name = "تصویر")]
        public string ImageUrl { get; set; }
        [Display(Name = "اسلایدر")]
        public bool ShowInSlider { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        [DisplayFormat(DataFormatString ="{0 :yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }

        [ForeignKey("GroupID")]
        public virtual PagesGroup PagesGroup { get; set; }

        public ICollection<PageComments> PageComments { get; set; }

        public Pages()
        {
            PageComments = new List<PageComments>();
        }

    }
}