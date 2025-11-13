using Microsoft.EntityFrameworkCore;
using WordInversionProject.Data;
using WordInversionProject.Interfaces;
using WordInversionProject.Models;

namespace WordInversionProject.Repositories
{
	public class WordInversionRepository : IWordInversionRepository
	{
		private readonly ApplicationDbContext _context;

		public WordInversionRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<WordInversionRecord> CreateAsync(WordInversionRecord record)
		{
			try
			{
				await _context.WordInversions.AddAsync(record);
				await _context.SaveChangesAsync();
				return record;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<IEnumerable<WordInversionRecord>> GetAllAsync()
		{
			try
			{
				var records = await _context.WordInversions
									     .OrderByDescending(x => x.CreatedAt)
									     .ToListAsync();
				return records;
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task<IEnumerable<WordInversionRecord>> FindByWordAsync(string word)
		{
			var lowerWord = word.ToLower();
			try
			{
				var records =  await _context.WordInversions
							.Where(r => r.OriginalSentence.ToLower().Contains(lowerWord) ||
									     r.InvertedSentence.ToLower().Contains(lowerWord))
							.OrderByDescending(x => x.CreatedAt)
							.ToListAsync();
				return records;
			}
			catch(Exception ex)
			{
				throw;
			}
		}


	}
}
