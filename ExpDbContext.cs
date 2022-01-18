using Microsoft.EntityFrameworkCore;

namespace EfExp
{
	public class ExpDbContext : DbContext
	{
		public DbSet<Post>? Post { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
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