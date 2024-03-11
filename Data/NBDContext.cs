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

        public DbSet<Bid> Bids { get; set; }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Labour> Labours { get; set; }

        public DbSet<Material> Materials { get; set; }

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
                .HasOne(b => b.Project)
                .WithMany(p => p.Bids)
                .HasForeignKey(b => b.ProjectID)
                .IsRequired();

            // Configure the relationships between Bid and Material
            modelBuilder.Entity<Bid>()
                .HasMany(b => b.Materials)
                .WithOne(m => m.Bid)
                .HasForeignKey(m => m.BidID);

            // Configure the relationships between Bid and Labour
            modelBuilder.Entity<Bid>()
                .HasMany(b => b.Labours)
                .WithOne(l => l.Bid)
                .HasForeignKey(l => l.BidID);
        }
    }
}
