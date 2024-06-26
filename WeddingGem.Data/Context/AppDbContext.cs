using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WeddingGem.Data.Entites;
using WeddingGem.Data.Entites.services;

namespace WeddingGem.Data.Context
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Bidding>()
            .HasMany(b => b.Services)
            .WithMany(s => s.Biddings)
            .UsingEntity<Dictionary<string, object>>(
                "BiddingService",
                j => j
                    .HasOne<Items>()
                    .WithMany()
                    .HasForeignKey("ServiceId")
                    .OnDelete(DeleteBehavior.Restrict), 
                j => j
                    .HasOne<Bidding>()
                    .WithMany()
                    .HasForeignKey("BiddingId")
                    .OnDelete(DeleteBehavior.Cascade));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Items> Services { get; set; }
        public DbSet<cars> cars { get; set; }
        public DbSet<WeddingHall> WeddingHalls { get; set; }
        public DbSet<Entertainment> Entertainment { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<SelfCare> SelfCares { get; set; }
        public DbSet<HoneyMoon> HoneyMoons { get; set; }
        public DbSet<Bidding> Biddings { get; set; }
        public DbSet<Packages> Packages { get; set; }
        public DbSet<Vendor> Vendor { get; set; }

    }
}
