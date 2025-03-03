using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLNS.Models
{
    public class ChamCong
    {
        [Key]
        public int MaChamCong { get; set; }

        [ForeignKey("NhanVien")]
        public int MaNhanVien { get; set; }
        public NhanVien? NhanVien { get; set; }

        public DateTime NgayLam { get; set; }
        public TimeSpan GioVaoLam { get; set; }
        public TimeSpan GioKetThuc { get; set; }

        // Tính số giờ thiếu
        public double ThieuGio
        {
            get
            {
                TimeSpan gioBatDau = new TimeSpan(8, 0, 0);  // 8:00 AM
                TimeSpan gioKetThuc = new TimeSpan(17, 0, 0); // 5:00 PM

                // Số giờ vào làm trễ
                double gioTre = (GioVaoLam - gioBatDau).TotalHours;

                // Số giờ về sớm
                double gioVeSom = GioKetThuc < gioKetThuc ? (gioKetThuc - GioKetThuc).TotalHours : 0;

                return gioTre + gioVeSom;
            }
        }

        // Tính số giờ tăng ca
        public double TangCa
        {
            get
            {
                TimeSpan gioKetThuc = new TimeSpan(17, 0, 0); // 5:00 PM

                double gioTangCa = GioKetThuc > gioKetThuc ? (GioKetThuc - gioKetThuc).TotalHours : 0;

                return gioTangCa;
            }
        }
    }
}
