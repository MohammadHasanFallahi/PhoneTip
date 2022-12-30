using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PhoneTipProject.Models.DataLayer
{
    [Table("PagesGroup",Schema ="dbo")]
    public class PagesGroup
    {
        [Key]
        public int GroupID { get; set; }
        [Display(Name ="عنوان گروه")]
        [Required(ErrorMessage ="لطفا عنوان گروه را مشخص کنید")]
        [MaxLength(150)]
        public string GroupTitle { get; set; }

        public virtual ICollection<Pages> Pages { get; set; }

        public PagesGroup()
        {
            Pages =new List<Pages>();
        }
    }
}