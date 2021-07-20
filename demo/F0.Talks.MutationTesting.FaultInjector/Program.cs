using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using F0.Talks.MutationTesting.FaultInjector.CodeAnalysis;
using Spectre.Console;

namespace F0.Talks.MutationTesting.FaultInjector
{
	internal static class Program
	{
		private static async Task Main(string[] args)
		{
			AnsiConsole.WriteLine(string.Join(", ", args));

			const string parent = "..";

			string production = Debugger.IsAttached
				? Path.Combine(parent, parent, parent, parent, "F0.Talks.MutationTesting", "Mathematics", "Calculator.cs")
				: Path.Combine(parent, "F0.Talks.MutationTesting", "Mathematics", "Calculator.cs");
			string test = Debugger.IsAttached
				? Path.Combine(parent, parent, parent, parent, "F0.Talks.MutationTesting.Tests", "Mathematics", "CalculatorTests.cs")
				: Path.Combine(parent, "F0.Talks.MutationTesting.Tests", "Mathematics", "CalculatorTests.cs");

			if (!File.Exists(production) || !File.Exists(test))
			{
				production = Path.Combine(parent, "demo", "F0.Talks.MutationTesting", "Mathematics", "Calculator.cs");
				test = Path.Combine(parent, "demo", "F0.Talks.MutationTesting.Tests", "Mathematics", "CalculatorTests.cs");
			}

			Source source = await Source.CreateAsync(production, test);
			Compiler compiler = await Compiler.CreateAsync(source);
			await Runner.RunAsync(source, compiler);
		}
	}
}
