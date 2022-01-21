using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using EfExp.Models;
using EfExp.ModelFactories;

namespace EfExp.Benchmarks
{
	public class BulkDeleteTests
	{
		private readonly ExpDbContext _context;
		private readonly PostFactory _postFactory;

		public BulkDeleteTests()
		{
			_context = new ExpDbContext();
			_postFactory = new PostFactory();
		}

		[Params(100, 8000, 13500)]
		public int IdsAbove;

		[IterationSetup]
		public void IterationSetup()
		{
			_context.ChangeTracker.AutoDetectChangesEnabled = false;

			var posts = _postFactory.Create(15000);

			_context.Post?.AddRange(posts);
			_context.SaveChanges();

			_context.ChangeTracker.AutoDetectChangesEnabled = false;
			_context.ChangeTracker.Clear();
		}

		[IterationCleanup]
		public void IterationCleanup()
		{
			_context.ChangeTracker.Clear();
			_context.Database.ExecuteSqlRaw("TRUNCATE TABLE Post");
		}

		[Benchmark]
		public void DeleteFullEntity()
		{
			var toDelete = _context.Set<Post>().Where(p => p.Id > IdsAbove);

			_context.RemoveRange(toDelete);
			_context.SaveChanges();
		}

		[Benchmark]
		public void DeleteFullEntityDetached()
		{
			var toDelete = _context
				.Set<Post>()
				.AsNoTracking()
				.Where(p => p.Id > IdsAbove);

			_context.RemoveRange(toDelete);
			_context.SaveChanges();
		}

		[Benchmark]
		public void DeleteByIdEntityDetached()
		{
			var toDelete = _context
				.Set<Post>()
				.AsNoTracking()
				.Where(p => p.Id > IdsAbove)
				.Select(p => new Post { Id = p.Id });

			_context.RemoveRange(toDelete);
			_context.SaveChanges();
		}
	}
}