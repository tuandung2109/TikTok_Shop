using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("danh_muc")]
    public class DanhMuc
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("ten_danh_muc")]
        public string TenDanhMuc { get; set; }

        [Column("mo_ta")]
        public string? MoTa { get; set; }

        public ICollection<SanPham>? SanPhams { get; set; }
    }
}
