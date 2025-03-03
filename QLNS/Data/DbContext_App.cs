using Microsoft.EntityFrameworkCore;
using QLNS.Models;

namespace QLNS.Data
{
    public class DbContext_App : DbContext
    {
        public DbContext_App(DbContextOptions<DbContext_App> options) : base(options) { }

        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<ViTriCongViec> ViTriCongViecs { get; set; }
        public DbSet<Luong> Luongs { get; set; }
        public DbSet<YeuCauNghiPhep> YeuCauNghiPheps { get; set; }
        public DbSet<ChamCong> ChamCongs { get; set; }
    }
}
