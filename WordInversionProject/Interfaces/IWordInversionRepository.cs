using WordInversionProject.Models;

namespace WordInversionProject.Interfaces
{
	public interface IWordInversionRepository
	{
		Task<WordInversionRecord> CreateAsync(WordInversionRecord record);
		Task<IEnumerable<WordInversionRecord>> GetAllAsync();
		Task<IEnumerable<WordInversionRecord>> FindByWordAsync(string word);
	}
}
