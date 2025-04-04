using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThuongMaiDienTu.Models
{
    [Table("danh_muc")]
    public class DanhMuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Ten_Danh_Muc { get; set; }
        public bool Trang_Thai { get; set; } = true; // Mặc định là hoạt động
    }
}
