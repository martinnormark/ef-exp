using BenchmarkDotNet.Running;

namespace EfExp
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Running benchmarks 🎉");

			BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
		}
	}
}