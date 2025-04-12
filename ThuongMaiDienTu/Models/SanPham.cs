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

        // ✅ Thêm danh sách đánh giá (Navigation Property)
        public virtual ICollection<DanhGia> DanhGias { get; set; } = new List<DanhGia>();

        [NotMapped]
        public IFormFile? HinhAnhFile { get; set; }

        //[StringLength(10, MinimumLength = 10, ErrorMessage = "Varifykey phải có đúng 10 ký tự")]
        //public string? Varifykey { get; set; } // Cho phép NULL, xóa [Required]

    }
}
