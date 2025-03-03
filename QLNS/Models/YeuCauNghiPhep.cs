using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class YeuCauNghiPhep
    {
        [Key]
        public int MaNghiPhep { get; set; }

        [ForeignKey("NhanVien")]
        public int MaNhanVien { get; set; }
        public NhanVien? NhanVien { get; set; }

        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int TongSoNgay { get; set; }

        [Required]
        public string LyDo { get; set; }

        public string? TinhTrang { get; set; }
    }
}
