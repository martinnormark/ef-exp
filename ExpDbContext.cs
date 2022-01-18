using Microsoft.EntityFrameworkCore;

namespace EfExp
{
	public class ExpDbContext : DbContext
	{
		public DbSet<Post>? Post { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var range = new List<int>();

			for (int i = 1; i < 15000; i++)
			{
				range.Add(i);
			}

			modelBuilder.Entity<Post>().HasData(range.Select(i => new Post { Id = i, Sequence = $"Seq {i}" }));
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlServer($"Server=.;Database=EfExp;Trusted_Connection=True;");
	}

	public class Post
	{
		public int Id { get; set; }
		public string? Sequence { get; set; }
	}
}