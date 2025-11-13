using System.ComponentModel.DataAnnotations;

namespace WordInversionProject.DTOs
{
	public class SearchRequestDto
	{
		[Required(ErrorMessage = "Search word is required")]
		[StringLength(100, MinimumLength = 1)]
		public string Word { get; set;  }
	}
}
