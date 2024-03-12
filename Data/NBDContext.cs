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

        public DbSet<StaffBid> StaffBids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //FK for address
            modelBuilder.Entity<Client>()
                .HasOne(c => c.Address)
                .WithOne(a => a.Client)
                .HasForeignKey<Address>(a => a.ClientID);

            modelBuilder.Entity<Address>()
                .HasOne(p => p.Project)
                .WithOne(p => p.Address)
                .HasForeignKey<Project>(p => p.AddressID);

            //Bid - Collection of projects
            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Project)
                .WithMany(p => p.Bids)
                .HasForeignKey(b => b.ProjectID)
                .IsRequired();

            //Bid - labour/material
            modelBuilder.Entity<Labour>()
                .HasOne(l => l.Bid)
                .WithMany(b => b.Labours)
                .HasForeignKey(l => l.BidID);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.Bid)
                .WithMany(b => b.Materials)
                .HasForeignKey(m => m.BidID);

            // Configure many-to-many relationship
            modelBuilder.Entity<StaffBid>()
                .HasKey(sb => new { sb.StaffID, sb.BidID });

            modelBuilder.Entity<StaffBid>()
                .HasOne(sb => sb.Staff)
                .WithMany(s => s.StaffBids)
                .HasForeignKey(sb => sb.StaffID);

            modelBuilder.Entity<StaffBid>()
                .HasOne(sb => sb.Bid)
                .WithMany(b => b.StaffBids)
                .HasForeignKey(sb => sb.BidID);
        }
    }
}
