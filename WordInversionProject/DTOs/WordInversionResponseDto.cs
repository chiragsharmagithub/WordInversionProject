namespace WordInversionProject.DTOs
{
	public class WordInversionResponseDto
	{
		public int Id { get; set; }
		public string OriginalSentence { get; set; }
		public string InvertedSentence { get; set;  }
		public DateTime CreatedAt { get; set; }
		public string IpAddress { get; set; }
	}
}
