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
            
            // Prevent cascade delete
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Projects) 
                .WithOne(p => p.Client)  
                .HasForeignKey(p => p.ClientID) 
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Address>()
                .HasOne(a => a.Client);

            modelBuilder.Entity<Address>()
                .HasOne(p => p.Project);
        }
    }
}
