using System.ComponentModel.DataAnnotations;

namespace EfExp.Models
{
	public class Post
	{
		public int Id { get; set; }

		[Required]
		[StringLength(512)]
		public string? Title { get; set; }

		[Required]
		[StringLength(256)]
		public string? Slug { get; set; }

		[Required]
		[StringLength(1024)]
		public string? Description { get; set; }

		[Required]
		public string? ContentBody { get; set; }

		[Required]
		[StringLength(256)]
		public string? Author { get; set; }

		[Required]
		public DateTime? CreatedDate { get; set; }
		public DateTime? PublishDate { get; set; }
	}
}