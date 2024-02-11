using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Car_Management.Models;

namespace Car_Management.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Car>().HasIndex(c => c.Model).IsUnique();


            modelBuilder.Entity<Manufacturer>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<Manufacturer>().HasIndex(c => c.ContactNo).IsUnique();


            modelBuilder.Entity<CarType>().HasIndex(c => c.Type).IsUnique();
            modelBuilder.Entity<Car>()
            .HasOne(c => c.CarType)
            .WithMany()
            .HasForeignKey(c => c.CarTypeId);

            modelBuilder.Entity<CarTransmissionType>().HasIndex(c => c.Name).IsUnique();


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Car_Management.Models.Manufacturer>? Manufacturer { get; set; }
        public DbSet<Car_Management.Models.CarType>? CarType { get; set; }
        public DbSet<Car_Management.Models.CarTransmissionType>? CarTransmissionType { get; set; }
        public DbSet<Car_Management.Models.Car>? Car { get; set; }
    }
}
