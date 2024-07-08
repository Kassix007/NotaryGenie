using Microsoft.EntityFrameworkCore;
using NotaryGenie.Server.Models;
namespace NotaryGenie.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Notary> Notaries { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Deed> Deeds { get; set; }
        public DbSet<ClientDeed> ClientDeeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientDeed>()
                .HasKey(cd => new { cd.ClientID, cd.DeedID });

            modelBuilder.Entity<ClientDeed>()
                .HasOne(cd => cd.Client)
                .WithMany(c => c.ClientDeeds)
                .HasForeignKey(cd => cd.ClientID);

            modelBuilder.Entity<ClientDeed>()
                .HasOne(cd => cd.Deed)
                .WithMany(d => d.ClientDeeds)
                .HasForeignKey(cd => cd.DeedID);
        }
    }
}
