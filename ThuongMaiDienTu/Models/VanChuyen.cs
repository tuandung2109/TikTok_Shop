using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("van_chuyen")]
    public class VanChuyen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Khóa chính

        [Required]
        public int Id_Don_Hang { get; set; }  // Khóa ngoại đến bảng DonHang
        [ForeignKey("Id_Don_Hang")]
        public virtual DonHang? DonHang { get; set; }

        [Required]
        public int Trang_Thai_Id { get; set; }  // Khóa ngoại đến bảng TrangThaiVanChuyen
        [ForeignKey("Trang_Thai_Id")]
        public virtual TrangThaiVanChuyen? TrangThaiVanChuyen { get; set; }

        [Required]
        public DateTime Ngay_Cap_Nhat { get; set; } = DateTime.Now;  // Ngày cập nhật trạng thái
    }
}
