//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore;

//namespace ThuongMaiDienTu.Models
//{
//    [Table("san_pham")]
//    public class SanPham
//    {
//        [Key]
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public int Id { get; set; }

//        public int Id_Cua_Hang { get; set; } // Mã cửa hàng
//        [ForeignKey("Id_Cua_Hang")]
//        public virtual CuaHang? CuaHang { get; set; }

//        [Required]
//        public int Id_Danh_Muc { get; set; } // Mã danh mục
//        [ForeignKey("Id_Danh_Muc")]
//        public virtual DanhMuc? DanhMuc { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string Ten_San_Pham { get; set; }

//        public string? Mo_Ta { get; set; } // Mô tả sản phẩm

//        [Required]
//        [Precision(10, 2)]
//        public decimal Gia_Goc { get; set; } // Giá gốc

//        [Precision(10, 2)]
//        public decimal? Giam_Gia { get; set; } // Mức giảm giá (nếu có)

//        [Precision(10, 2)]
//        public decimal? Gia_Khuyen_Mai { get; set; } // Giá sau khuyến mãi

//        [Required]
//        public int So_Luong_Ton { get; set; } // Số lượng tồn kho

//        [Required]
//        public DateTime Ngay_Tao = DateTime.Now;

//        [StringLength(500)]
//        public string Hinh_Anh { get; set; }
//    }
//}


using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ThuongMaiDienTu.Models
{
    [Table("san_pham")]
    public class SanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Id_Cua_Hang { get; set; }
        [ForeignKey("Id_Cua_Hang")]
        public virtual CuaHang? CuaHang { get; set; }

        [Required]
        public int Id_Danh_Muc { get; set; }
        [ForeignKey("Id_Danh_Muc")]
        public virtual DanhMuc? DanhMuc { get; set; }

        [Required]
        [StringLength(255)]
        public string? Ten_San_Pham { get; set; }

        public string? Mo_Ta { get; set; } // Mô tả sản phẩm

        [Required]
        [Precision(10, 2)]
        public decimal Gia_Goc { get; set; }

        [Precision(10, 2)]
        public decimal? Giam_Gia { get; set; } // ⚠️ Cho phép giá trị NULL

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Precision(10, 2)]
        public decimal? Gia_Khuyen_Mai { get; set; }

        [Required]
        public int So_Luong_Ton { get; set; }

        // [Required]
        public DateTime Ngay_Tao = DateTime.Now;

        [StringLength(500)]
        public string? Hinh_Anh { get; set; }

        [NotMapped]
        public IFormFile? HinhAnhFile { get; set; }
    }
}
