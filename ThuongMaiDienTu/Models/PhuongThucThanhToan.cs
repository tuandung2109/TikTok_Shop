using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("phuong_thuc_thanh_toan")]
    public class PhuongThucThanhToan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Khóa chính

        [Required]
        [StringLength(255)]
        public string Mo_Ta { get; set; }  // Mô tả phương thức thanh toán

        // Thiết lập quan hệ với ThanhToan
        public virtual ICollection<ThanhToan>? ThanhToans { get; set; }
    }
}
