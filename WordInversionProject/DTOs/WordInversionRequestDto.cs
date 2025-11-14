using System.ComponentModel.DataAnnotations;

namespace WordInversionProject.DTOs
{
	public class WordInversionRequestDto
	{
		[Required(ErrorMessage = "Sentence is required")]
		[StringLength(
			1000, 
			MinimumLength = 1, 
			ErrorMessage = "Sentence must be between 1 and 1000 characters")]
		[Display(Name = "Enter your Sentence")]
		public string Sentence { get; set; }
	}
}
