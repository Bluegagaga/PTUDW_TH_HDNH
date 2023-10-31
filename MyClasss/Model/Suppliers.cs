using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Suppliers")]
    public class Suppliers
    {
        [Key]
        [Display(Name="Ma so Ncc")]
        public int Id { get; set; }
        [Display(Name = "TenNCC")]
        public string Name { get; set; }
        [Display(Name = "Hinh anh")]
        public string Image { get; set; }
        [Required(ErrorMessage ="Khong duoc de trong")]
        [Display(Name = "Tu khoa")]
        public string Slug { get; set; }
        [Display(Name = "Dat hang")]
        public int? Order { get; set; }
        [Display(Name = "Ten day du")]
        public string FullName { get; set; }
        [Display(Name = "SDT")]
        public string Phone { get; set; }
        [Display(Name = "Mail")]
        public string Email { get; set; }
        [Display(Name = "Link")]
        public string UrlSite { get; set; }
        [Required]
        [Display(Name = "Ten")]
        public string MeteDesc { get; set; }
        [Required]
        public string MetaKey { get; set; }
        [Display(Name = "Tao Boi")]
        public int CreateBy { get; set; }
        [Display(Name = "Ngay tao")]
        public DateTime CreateAt { get; set; }
        [Display(Name = "Cap nhat boi")]
        public int UpdateBy { get; set; }
        [Display(Name = "Ngay cap nhat")]
        public DateTime UpdateAt { get; set; }
        [Display(Name = "Trang thai")]
        public int? Status { get; set; }
    }

}
