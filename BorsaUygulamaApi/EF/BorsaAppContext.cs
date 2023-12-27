using BorsaUygulamaApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BorsaUygulamaApi.EF
{
    public class BorsaAppContext : DbContext
    {
        public DbSet<Uyelik> Uyelik { get; set; }
        public DbSet<Giris> Giris { get; set; }
        public DbSet<HisseAdlari> HisseAd { get; set; }
        public DbSet<HisseBilgileriKayit> HisseBilgiKayit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-4ALBNKR; Database=BorsaDB; User Id=TestUser;Password=123456; TrustServerCertificate=true;Integrated Security=SSPI");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
