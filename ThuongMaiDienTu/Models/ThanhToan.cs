using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThuongMaiDienTu.Models
{
    [Table("thanh_toan")]
    public class ThanhToan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }  // Khóa chính

        [Required]
        public int Id_Don_Hang { get; set; }  // Khóa ngoại đến bảng DonHang
        [ForeignKey("Id_Don_Hang")]
        public virtual DonHang? DonHang { get; set; }

        [Required]
        public int Phuong_Thuc_Id { get; set; }  // Khóa ngoại đến bảng PhuongThucThanhToan
        [ForeignKey("Phuong_Thuc_Id")]
        public virtual PhuongThucThanhToan? PhuongThucThanhToan { get; set; }

        [Required]
        public int Trang_Thai_Id { get; set; }  // Khóa ngoại đến bảng TrangThaiThanhToan
        [ForeignKey("Trang_Thai_Id")]
        public virtual TrangThaiThanhToan? TrangThaiThanhToan { get; set; }

        [Required]
        public DateTime Ngay_Tao { get; set; } = DateTime.Now;  // Ngày tạo thanh toán
    }
}
