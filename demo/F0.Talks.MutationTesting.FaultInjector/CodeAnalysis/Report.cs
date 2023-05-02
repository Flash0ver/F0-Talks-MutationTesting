using System.Threading;
using Spectre.Console;

namespace F0.Talks.MutationTesting.FaultInjector.CodeAnalysis;

internal sealed class Report
{
	private int Mutants = 0;
	private int Killed = 0;
	private int Survived = 0;

	internal void IncrementKilled()
	{
		Interlocked.Increment(ref Mutants);
		Interlocked.Increment(ref Killed);
	}

	internal void IncrementSurvived()
	{
		Interlocked.Increment(ref Mutants);
		Interlocked.Increment(ref Survived);
	}

	internal void Increment(bool isKilled)
	{
		if (isKilled)
		{
			IncrementKilled();
		}
		else
		{
			IncrementSurvived();
		}
	}

	internal void WriteToConsole()
	{
		Table table = new Table()
			.AddColumn(nameof(Mutants))
			.AddColumn(nameof(Killed))
			.AddColumn(nameof(Survived));

		float mutants = Mutants;

		float killedPercent = Killed / mutants * 100.0f;
		float survivedPercent = Survived / mutants * 100.0f;

		table.AddRow($"[purple]{Mutants} (100 %)[/]", $"[green]{Killed} ({killedPercent} %)[/]", $"[red]{Survived} ({survivedPercent} %)[/]");

		AnsiConsole.Render(table);
	}
}
