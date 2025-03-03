using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class NhanVien
    {
        [Key]
        public int MaNhanVien { get; set; }

        [Required, StringLength(100)]
        public string HoTenNV { get; set; }

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        public string GioiTinh { get; set; }

        public string? DiaChi { get; set; }

        [Required, Phone]
        public string SoDT { get; set; }

        [Required]
        public DateTime NgayVaoLam { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        // Liên kết với NguoiDung (Mỗi nhân viên có 1 người dùng)
        [ForeignKey("NguoiDung")]
        public int? MaNguoiDung { get; set; }
        public NguoiDung? NguoiDung { get; set; }

        // Liên kết với Phòng Ban
        [ForeignKey("PhongBan")]
        public int MaPhongBan { get; set; }
        public PhongBan? PhongBan { get; set; }

        // Liên kết với vị trí công việc
        [ForeignKey("ViTriCongViec")]
        public int MaViTri { get; set; }
        public ViTriCongViec? ViTriCongViec { get; set; }

        public ICollection<Luong>? Luongs { get; set; }
        public ICollection<YeuCauNghiPhep>? YeuCauNghiPheps { get; set; }
        public ICollection<ChamCong>? ChamCongs { get; set; }
    }
}
