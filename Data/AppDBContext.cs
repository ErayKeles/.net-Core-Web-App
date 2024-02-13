using AspNetCoreIdentityApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentityApp.Data
{
    public class AppDBContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public DbSet<BeeHive> BeeHives { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BeeHive>(entity =>
            {
                entity.Property(e => e.Location).IsRequired();
                entity.Property(e => e.ProductionDate).IsRequired();
                entity.Property(e => e.Capacity).IsRequired();


                entity.HasOne(e => e.User)
                      .WithMany(u => u.BeeHives)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
