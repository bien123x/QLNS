using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class Luong
    {
        [Key]
        public int MaLuong { get; set; }

        [ForeignKey("NhanVien")]
        public int MaNhanVien { get; set; }
        public NhanVien? NhanVien { get; set; }

        public int Thang { get; set; }
        public int Nam { get; set; }

        [Precision(12, 2)]
        public decimal TienTangCa { get; set; }
        [Precision(12, 2)]
        public decimal KhoanTru { get; set; }
        [Precision(12, 2)]
        public decimal LuongThucNhan { get; set; }
    }
}
