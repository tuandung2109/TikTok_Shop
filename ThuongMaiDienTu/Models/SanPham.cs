using System;
using System.ComponentModel.DataAnnotations;

namespace ThuongMaiDienTu.Models;

public class SanPham
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string TenSanPham { get; set; }

    [Required]
    public decimal Gia { get; set; }

    [Required]
    public int SoLuong { get; set; }

    public string? MoTa { get; set; }

    public int DanhMucId { get; set; }
    public DanhMuc? DanhMuc { get; set; }

}
