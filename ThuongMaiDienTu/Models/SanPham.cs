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

        public int Id_Cua_Hang { get; set; } // Mã cửa hàng
        [ForeignKey("Id_Cua_Hang")]
        public virtual CuaHang? CuaHang { get; set; }

        [Required]
        public int Id_Danh_Muc { get; set; } // Mã danh mục
        [ForeignKey("Id_Danh_Muc")]
        public virtual DanhMuc? DanhMuc { get; set; }

        [Required]
        [StringLength(255)]
        public string Ten_San_Pham { get; set; }

        public string Mo_Ta { get; set; } // Mô tả sản phẩm

        [Required]
        [Precision(10, 2)]
        public decimal GiaGoc { get; set; } // Giá gốc

        [Precision(10, 2)]
        public decimal? GiamGia { get; set; } // Mức giảm giá (nếu có)

        [Precision(10, 2)]
        public decimal? GiaKhuyenMai { get; set; } // Giá sau khuyến mãi

        [Required]
        public int SoLuongTon { get; set; } // Số lượng tồn kho

        [Required]
        public DateTime NgayTao = DateTime.Now;

        [StringLength(500)]
        public string HinhAnh { get; set; }
    }
}
