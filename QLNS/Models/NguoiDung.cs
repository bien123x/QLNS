using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class NguoiDung
    {
        [Key]
        public int MaNguoiDung { get; set; }

        [Required, StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        public string MatKhau { get; set; }

        // Mỗi người dùng chỉ có thể liên kết với một nhân viên
        public NhanVien? NhanVien { get; set; }
    }
}
