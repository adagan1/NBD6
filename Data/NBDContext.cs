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
        }
    }
}
