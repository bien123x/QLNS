using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class ViTriCongViec
    {
        [Key]
        public int MaViTri { get; set; }

        [Required, StringLength(100)]
        public string TenViTri { get; set; }

        [Required]
        [Precision(3, 2)]
        public decimal HeSoLuong { get; set; }

        public ICollection<NhanVien>? NhanViens { get; set; }
    }
}
