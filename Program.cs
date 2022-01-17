using System.Diagnostics;
using EfExp;
using Microsoft.EntityFrameworkCore;
using System.Linq;

Console.WriteLine("Warming up EF context to database...");

var context = new ExpDbContext();

for (int i = 0; i < 10; i++)
{
	await context.Set<Post>().Where(p => p.Id > 12000).ToListAsync();
}

var count = context.Posts?.Count();
var random = new Random();

var stats = new List<EfStat>();

for (int i = 1; i <= 250; i++)
{
	Console.ForegroundColor = ConsoleColor.Green;
	Console.WriteLine($"==== Sample #{i} ====");
	Console.ForegroundColor = ConsoleColor.Gray;

	var predicate = random.Next(count.GetValueOrDefault());

	var sw = new Stopwatch();

	sw.Start();
	var toDelete = context.Set<Post>().Where(p => p.Id > predicate);

	var fetchQueryable = sw.Elapsed;

	var fullEntity = await toDelete.Select(p => p.Id).ToListAsync();
	var fullEntityEl = sw.Elapsed;

	var onlyIds = await toDelete.Select(p => p.Id).ToListAsync();

	var getIds = sw.Elapsed;

	var perfStats = new EfStat
	{
		Queryable = fetchQueryable,
		FullEntity = fullEntityEl - fetchQueryable,
		OnlyIds = getIds - fullEntityEl
	};

	stats.Add(perfStats);

	Console.WriteLine($"fetchQueryable: {perfStats.Queryable.TotalMilliseconds}");
	Console.WriteLine($"fullEntity: {perfStats.FullEntity.TotalMilliseconds}");
	Console.WriteLine($"onlyIds: {perfStats.OnlyIds.TotalMilliseconds}");

	Console.WriteLine();
}

var avg = new EfStat
{
	Queryable = TimeSpan.FromMilliseconds(stats.Sum(s => s.Queryable.TotalMilliseconds) / stats.Count),
	FullEntity = TimeSpan.FromMilliseconds(stats.Sum(s => s.FullEntity.TotalMilliseconds) / stats.Count),
	OnlyIds = TimeSpan.FromMilliseconds(stats.Sum(s => s.OnlyIds.TotalMilliseconds) / stats.Count),
};

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"AVG: fetchQueryable: {avg.Queryable.TotalMilliseconds}");
Console.WriteLine($"AVG: fullEntity: {avg.FullEntity.TotalMilliseconds}");
Console.WriteLine($"AVG: onlyIds: {avg.OnlyIds.TotalMilliseconds}");

class EfStat
{
	public TimeSpan Queryable { get; set; }
	public TimeSpan FullEntity { get; set; }
	public TimeSpan OnlyIds { get; set; }
}