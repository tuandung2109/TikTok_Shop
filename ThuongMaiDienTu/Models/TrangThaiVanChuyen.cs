using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("trang_thai_van_chuyen")]
    public class TrangThaiVanChuyen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Khóa chính

        [Required]
        [StringLength(255)]
        public string Mo_Ta { get; set; }  // Mô tả trạng thái vận chuyển

        // Thiết lập quan hệ với VanChuyen
        public virtual ICollection<VanChuyen>? VanChuyens { get; set; }
    }
}
