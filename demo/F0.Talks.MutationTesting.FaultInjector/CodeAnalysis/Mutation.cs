using Microsoft.CodeAnalysis;

namespace F0.Talks.MutationTesting.FaultInjector.CodeAnalysis;

internal sealed class Mutation
{
	public Mutation(SyntaxNode originalNode, SyntaxTree originalTree, SyntaxNode mutatedNode, SyntaxTree mutatedTree)
	{
		OriginalNode = originalNode;
		OriginalTree = originalTree;
		MutatedNode = mutatedNode;
		MutatedTree = mutatedTree;
	}

	public SyntaxNode OriginalNode { get; }
	public SyntaxTree OriginalTree { get; }
	public SyntaxNode MutatedNode { get; }
	public SyntaxTree MutatedTree { get; }
}
