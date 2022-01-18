using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace EfExp.Benchmarks
{
	public class BulkDeleteTests
	{
		private readonly ExpDbContext _context;

		public BulkDeleteTests()
		{
			_context = new ExpDbContext();
		}

		[Params(10, 100, 5000, 10000, 13500)]
		public int IdsAbove;

		[IterationSetup]
		public void IterationSetup()
		{
			var range = new List<int>();

			for (int i = 1; i < 15000; i++)
			{
				range.Add(i);
			}

			_context.Post?.AddRange(range.Select(i => new Post { Sequence = $"Seq {i}" }));
			_context.SaveChanges();

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
		public void DeleteFullEntityNoTracking()
		{
			var toDelete = _context
				.Set<Post>()
				.AsNoTracking()
				.Where(p => p.Id > IdsAbove);

			_context.RemoveRange(toDelete);
			_context.SaveChanges();
		}
	}
}