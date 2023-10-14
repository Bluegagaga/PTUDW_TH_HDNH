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
        [Required(ErrorMessage = "Ten loai khong duoc de trong")]
        [Display(Name="Ten loai SP")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Ten rut gon khong duoc de trong")]
        [Display(Name = "Ten rut gon")]
        public string Slug { get; set; }
        [Required(ErrorMessage = "Cap cha khong duoc de trong")]
        [Display(Name = "Cap cha")]
        public int ParentID { get; set; }
        [Required(ErrorMessage = "Sap xep khong duoc de trong")]
        [Display(Name = "Sap xep")]
        public int Order { get; set; }
        [Required(ErrorMessage = "Mo ta khong duoc de trong")]
        [Display(Name = "Mo ta")]
        public string MetaDesc { get; set; }
        [Required(ErrorMessage = "Tu khoa khong duoc de trong")]
        [Display(Name = "Tu khoa")]
        public int MetaKey { get; set; }
        [Required(ErrorMessage = "Nguoi tao khong duoc de trong")]
        [Display(Name = "Tao boi")]
        public string CreateBy { get; set; }
        [Required(ErrorMessage = "Ngay tao khong duoc de trong")]
        [Display(Name = "Ngay tao")]
        public DateTime CreateAt { get; set; }
        [Required(ErrorMessage = "Nguoi cap nhat khong duoc de trong")]
        [Display(Name = "Cap nhat boi")]
        public int UpdateBy { get; set; }
        [Required(ErrorMessage = "Ngay cap nhat khong duoc de trong")]
        [Display(Name = "Ngay cap nhat")]
        public DateTime UpdateAt { get; set; }
        [Required(ErrorMessage = "Trang thai khong duoc de trong")]
        [Display(Name = "Trang thai")]
        public int Status { get; set; }
    }

}
