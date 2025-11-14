using Microsoft.EntityFrameworkCore;
using WordInversionProject.Models;
namespace WordInversionProject.Data
{
	public class ApplicationDbContext: DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<WordInversionRecord> WordInversions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// configure WordInversionRecord entity
			modelBuilder.Entity<WordInversionRecord>(entity =>
			{
				// Define primary key
				entity.HasKey(e => e.Id);

				// Configure OriginalSentence Property
				entity.Property(e => e.OriginalSentence)
					 .IsRequired()
					 .HasMaxLength(1000);

				// Configure InvertedSentence Property
				entity.Property(e => e.InvertedSentence)
					 .IsRequired()
					 .HasMaxLength(1000);

				// Configure CreatedAt with default SQL timestamp
				entity.Property(e => e.CreatedAt)
					 .HasDefaultValueSql("CURRENT_TIMESTAMP");

				// Configure IpAddress property
				entity.Property(e => e.IpAddress)
					 .HasMaxLength(45);

				// Create indexes for search optimization
				entity.HasIndex(e => e.OriginalSentence);
				entity.HasIndex(e => e.InvertedSentence);
			});
		}


	}
}
