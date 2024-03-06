using Microsoft.EntityFrameworkCore;
using NBD6.Models;

namespace NBD6.Data
{
    public class NBDContext : DbContext
    {
        public NBDContext(DbContextOptions<NBDContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Bid > Bids { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //FK
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Address)
                .WithOne(a => a.Client)
                .HasForeignKey<Address>(a => a.ClientID);

            modelBuilder.Entity<Address>()
                .HasOne(p => p.Project)
                .WithOne(p => p.Address)
                .HasForeignKey<Project>(p => p.AddressID);

            
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.project)  // Bid has one Project
                .WithMany(p => p.Bids)   // Project has many Bids
                .HasForeignKey(b => b.ProjectID)  // Bid.ProjectID is the foreign key
                .IsRequired();  // Foreign key is required
        }
    }
}
