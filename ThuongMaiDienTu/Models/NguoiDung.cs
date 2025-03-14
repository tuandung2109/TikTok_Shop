using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("nguoi_dung")]
    public class NguoiDung
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("ho_ten")]
        public string? HoTen { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("email")]
        public string? Email { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("mat_khau")]
        public string? MatKhau { get; set; }

        [MaxLength(20)]
        [Column("so_dien_thoai")]
        public string? SoDienThoai { get; set; }

        [Column("vai_tro_id")]
        public int VaiTroId { get; set; }

        [ForeignKey("VaiTroId")]
        public VaiTro? VaiTro { get; set; }

        [Column("ngay_tao")]
        public DateTime NgayTao { get; set; } = DateTime.Now;
    }
}
