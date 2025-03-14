using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("vai_tro")]
    public class VaiTro
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("ten_vai_tro")]
        public string TenVaiTro { get; set; }

        public ICollection<NguoiDung>? NguoiDungs { get; set; }
    }
}
