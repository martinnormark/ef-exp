using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace EfExp.Benchmarks
{
	public class PostQueryTests
	{
		private ExpDbContext? _context;
		[Params(10, 100, 5000, 10000, 13500)]
		public int N;

		[GlobalSetup]
		public void GlobalSetup()
		{
			_context = new ExpDbContext();
		}

		[Benchmark]
		public void QueryWithTracking()
		{
			var toDelete = _context?.Set<Post>().Where(p => p.Id > N).ToList();
		}

		[Benchmark]
		public void QueryNoTracking()
		{
			var toDelete = _context?
				.Set<Post>()
				.AsNoTracking()
				.Where(p => p.Id > N).ToList();
		}
	}
}