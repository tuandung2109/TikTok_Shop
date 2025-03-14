using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("banner")]
    public class Banner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Khóa chính

        [Required]
        [StringLength(255)]
        public string Tieu_De { get; set; }  // Tiêu đề của banner

        [Required]
        public string Hinh_Anh { get; set; }  // Đường dẫn ảnh của banner
    }
}
