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

			modelBuilder.Entity<WordInversionRecord>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.HasIndex(e => e.OriginalSentence);
				entity.HasIndex(e => e.InvertedSentence);
				entity.HasIndex(e => e.CreatedAt);
			});
		}


	}
}
