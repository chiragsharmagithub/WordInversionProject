using WordInversionProject.DTOs;

namespace WordInversionProject.Interfaces
{
	public interface IWordInversionService
	{
		Task<WordInversionResponseDto> InvertSentenceAsync(string sentence, string ipAddress);
		Task<IEnumerable<WordInversionResponseDto>> GetAllRecordsAsync();
		Task<IEnumerable<WordInversionResponseDto>> SearchByWordAsync(string word);
	}
}
