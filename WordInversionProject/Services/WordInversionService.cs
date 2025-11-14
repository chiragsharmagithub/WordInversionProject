using WordInversionProject.DTOs;
using WordInversionProject.Interfaces;
using WordInversionProject.Models;

namespace WordInversionProject.Services
{
	public class WordInversionService: IWordInversionService
	{
		private readonly IWordInversionRepository _repository;
		private readonly ILogger<WordInversionService> _logger;

		public WordInversionService(
			IWordInversionRepository repository, 
			ILogger<WordInversionService> logger)
		{
			_repository = repository;
			_logger = logger;
		}

		public async Task<WordInversionResponseDto> InvertSentenceAsync(string sentence, string ipAddress)
		{
			if(string.IsNullOrWhiteSpace(sentence))
			{
				throw new ArgumentException("Sentence cannot be empty", nameof(sentence));
			}
			try
			{
				var invertedSentence = InvertWords(sentence);
				var record = new WordInversionRecord
				{
					OriginalSentence = sentence.Trim(),
					InvertedSentence = invertedSentence,
					IpAddress = ipAddress ?? "Unknown"
				};
				var savedRecord = await _repository.CreateAsync(record);
				_logger.LogInformation(
					"Word inversion completed. ID: {Id}, Original: {Original}",
					savedRecord.Id, savedRecord.OriginalSentence);

				return MapToDto(savedRecord);
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error during word inversion");
				throw;
			}
		}

		public async Task<IEnumerable<WordInversionResponseDto>> GetAllRecordsAsync()
		{
			try
			{
				var records = await _repository.GetAllAsync();
				return records.Select(MapToDto).ToList();
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error retrieving all records");
				throw;
			}
		}

		public async Task<IEnumerable<WordInversionResponseDto>> SearchByWordAsync(string word)
		{
			if(string.IsNullOrWhiteSpace(word))
			{
				throw new ArgumentException("Search word cannot be empty", nameof(word));
			}
			try
			{
				var records = await _repository.FindByWordAsync(word);
				return records.Select(w => MapToDto(w)).ToList();
			}
			catch(Exception ex)
			{
				_logger.LogError(ex, "Error searching for word : {Word}", word);
				throw;
			}	
		}

		public string InvertWords(string sentence)
		{
			if(string.IsNullOrWhiteSpace(sentence))
			{
				return string.Empty;
			}

			// Split the sentence into words based on space, ignore empty entries
			var words = sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			// For each word, reverse its characters and store the results in a new Collection
			var invertedWords = words.Select(word => new String(word.Reverse().ToArray()));

			// Join the reversed words back into a single string, separating them with spaces
			return string.Join(" ", invertedWords);
		}

		private WordInversionResponseDto MapToDto(WordInversionRecord record)
		{
			return new WordInversionResponseDto
			{
				Id = record.Id,
				OriginalSentence = record.OriginalSentence,
				InvertedSentence = record.InvertedSentence,
				CreatedAt = record.CreatedAt,
				IpAddress = record.IpAddress
			};
		}
	}
}
