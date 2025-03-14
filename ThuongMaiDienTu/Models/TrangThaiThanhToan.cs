using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("trang_thai_thanh_toan")]
    public class TrangThaiThanhToan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Khóa chính

        [Required]
        [StringLength(255)]
        public string Mo_Ta { get; set; }  // Mô tả trạng thái thanh toán

        // Thiết lập quan hệ với ThanhToan
        public virtual ICollection<ThanhToan>? ThanhToans { get; set; }
    }
}
