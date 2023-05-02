using System.IO;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace F0.Talks.MutationTesting.FaultInjector.CodeAnalysis;

internal sealed class Source
{
	public static async Task<Source> CreateAsync(string production, string test)
	{
		string productionText = await File.ReadAllTextAsync(production);
		string testText = await File.ReadAllTextAsync(test);

		return new Source(productionText, testText);
	}

	private Source(string productionText, string testText)
	{
		Production = CSharpSyntaxTree.ParseText(productionText);
		Test = CSharpSyntaxTree.ParseText(testText);
	}

	public SyntaxTree Production { get; }
	public SyntaxTree Test { get; }
}
