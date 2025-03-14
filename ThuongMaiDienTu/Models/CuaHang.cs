using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("cua_hang")]
    public class CuaHang
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("ten_cua_hang")]
        public string TenCuaHang { get; set; }

        [MaxLength(255)]
        [Column("mo_ta")]
        public string? MoTa { get; set; }

        [Required]
        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [ForeignKey("NguoiDungId")]
        public NguoiDung? ChuCuaHang { get; set; }

        public ICollection<SanPham>? SanPhams { get; set; }
    }
}
