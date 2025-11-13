using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordInversionProject.Models
{
	[Table("WordInversions")]
	public class WordInversionRecord
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required(ErrorMessage = "Original Sentence is required")]
		[StringLength(1000, MinimumLength = 1, ErrorMessage = "Senetence must be between 1 and 1000")]
		[Display(Name = "Original Sentence")]
		public string OriginalSentence { get; set; }

		[Required]
		[StringLength(1000)]
		[Display(Name = "Inverted Sentence")]
		public string InvertedSentence { get; set; }

		[Display(Name = "Created At")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		[StringLength(45)]
		[Display(Name = "Client IP")]
		public string IpAddress { get; set; }

	}
}
