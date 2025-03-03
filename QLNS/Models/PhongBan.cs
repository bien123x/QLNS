using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class PhongBan
    {
        [Key]
        public int MaPhongBan { get; set; }

        [Required, StringLength(100)]
        public string TenPhongBan { get; set; }

        public ICollection<NhanVien>? NhanViens { get; set; }
    }
}
