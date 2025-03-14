using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("trang_thai_don_hang")]
    public class TrangThaiDonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Khóa chính

        [Required]
        [StringLength(255)]
        public string Mo_Ta { get; set; } // Mô tả trạng thái

        // Danh sách đơn hàng có trạng thái này
        public virtual ICollection<DonHang>? DonHangs { get; set; }
    }
}
