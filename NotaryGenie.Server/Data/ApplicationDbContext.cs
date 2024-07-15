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
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentIndex> DocumentIndex { get; set; }

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

            modelBuilder.Entity<DocumentIndex>()
                .HasKey(di => new { di.DocumentID, di.Keyword });

            modelBuilder.Entity<Document>()
                .HasKey(d => d.DocumentID);

            modelBuilder.Entity<Document>()
                .Property(d => d.ClientID).IsRequired();
            modelBuilder.Entity<Document>()
                .Property(d => d.DocumentName).IsRequired();
            modelBuilder.Entity<Document>()
                .Property(d => d.UploadDate).IsRequired();
            modelBuilder.Entity<Document>()
                .Property(d => d.FilePath).IsRequired();

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Client)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.ClientID);

            modelBuilder.Entity<Document>()
                .HasData(
                    new Document { DocumentID = -1, ClientID = 1, DocumentName = "Document1", UploadDate = DateTime.UtcNow, FilePath = "path/to/document1" },
                    new Document { DocumentID = -2, ClientID = 2, DocumentName = "Document2", UploadDate = DateTime.UtcNow, FilePath = "path/to/document2" }
                );
        }
    }
}
