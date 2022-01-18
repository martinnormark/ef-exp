using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace EfExp.Benchmarks
{
	public class PostQueryTests
	{
		private readonly ExpDbContext _context;

		public PostQueryTests()
		{
			_context = new ExpDbContext();
		}

		[Params(10, 100, 5000, 10000, 13500)]
		public int IdsAbove;

		[Benchmark]
		public void QueryWithTracking()
		{
			var toDelete = _context.Set<Post>().Where(p => p.Id > IdsAbove).ToList();
		}

		[Benchmark]
		public void QueryNoTracking()
		{
			var toDelete = _context
				.Set<Post>()
				.AsNoTracking()
				.Where(p => p.Id > IdsAbove).ToList();
		}
	}
}