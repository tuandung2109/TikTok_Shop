using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("san_pham")]
    public class SanPham
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("ten_san_pham")]
        public string TenSanPham { get; set; }

        [Required]
        [Column("gia_goc")]
        public decimal GiaGoc { get; set; }

        [Required]
        [Column("so_luong_ton")]
        public int SoLuongTon { get; set; }

        [Column("mo_ta")]
        public string? MoTa { get; set; }

        [Column("hinh_anh")]
        [MaxLength(255)]
        public string? HinhAnh { get; set; }

        [Column("id_cua_hang")]
        public int CuaHangId { get; set; }
        public CuaHang? CuaHang { get; set; }

        [Column("id_danh_muc")]
        public int DanhMucId { get; set; }
        public DanhMuc? DanhMuc { get; set; }
    }
}
