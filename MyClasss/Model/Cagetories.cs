using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Categories")]
    public class Categories
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name="Ten loai SP")]
        public string Name { get; set; }
        [Display(Name = "Ten rut gon")]
        public string Slug { get; set; }
        [Display(Name = "Cap cha")]
        public int ParentID { get; set; }
        [Display(Name = "Sap xep")]
        public int Order { get; set; }
        [Required]
        [Display(Name = "Mo ta")]
        public int MetaDesc { get; set; }
        [Required]
        [Display(Name = "Tu khoa")]
        public int MetaKey { get; set; }
        [Display(Name = "Tao boi")]
        public string CreateBy { get; set; }
        [Display(Name = "Ngay tao")]
        public DateTime CreateAt { get; set; }
        [Display(Name = "Cap nhat boi")]
        public int UpdateBy { get; set; }
        [Display(Name = "Ngay cap nhat")]
        public DateTime UpdateAt { get; set; }
        [Display(Name = "Trang thai")]
        public int Status { get; set; }
    }

}
