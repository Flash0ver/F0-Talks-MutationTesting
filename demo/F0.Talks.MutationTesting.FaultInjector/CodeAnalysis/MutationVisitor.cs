using System.Collections.Generic;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace F0.Talks.MutationTesting.FaultInjector.CodeAnalysis;

internal sealed class MutationVisitor : CSharpSyntaxWalker
{
	private readonly List<Mutation> mutations = [];

	public IReadOnlyCollection<Mutation> Mutations => mutations;

	public override void Visit(SyntaxNode? node)
	{
		if (node is BinaryExpressionSyntax binary)
		{
			if (IsArithmetic(binary))
			{
				AddArithmeticMutations(binary);
			}
		}

		base.Visit(node);
	}

	private static bool IsArithmetic(BinaryExpressionSyntax binary)
	{
		return binary.Kind() is SyntaxKind.AddExpression or SyntaxKind.SubtractExpression or SyntaxKind.MultiplyExpression or SyntaxKind.DivideExpression or SyntaxKind.ModuloExpression;
	}

	private void AddArithmeticMutations(BinaryExpressionSyntax binary)
	{
		if (!binary.IsKind(SyntaxKind.AddExpression))
		{
			SyntaxToken operatorToken = SyntaxFactory.Token(SyntaxFactory.TriviaList(), SyntaxKind.PlusToken, SyntaxFactory.TriviaList(SyntaxFactory.Space));
			BinaryExpressionSyntax addMutator = SyntaxFactory.BinaryExpression(SyntaxKind.AddExpression, binary.Left, operatorToken, binary.Right);

			AddMutation(binary, addMutator);
		}
		if (!binary.IsKind(SyntaxKind.SubtractExpression))
		{
			SyntaxToken operatorToken = SyntaxFactory.Token(SyntaxFactory.TriviaList(), SyntaxKind.MinusToken, SyntaxFactory.TriviaList(SyntaxFactory.Space));
			BinaryExpressionSyntax subtractMutator = SyntaxFactory.BinaryExpression(SyntaxKind.SubtractExpression, binary.Left, operatorToken, binary.Right);

			AddMutation(binary, subtractMutator);
		}
		if (!binary.IsKind(SyntaxKind.MultiplyExpression))
		{
			SyntaxToken operatorToken = SyntaxFactory.Token(SyntaxFactory.TriviaList(), SyntaxKind.AsteriskToken, SyntaxFactory.TriviaList(SyntaxFactory.Space));
			BinaryExpressionSyntax multiplyMutator = SyntaxFactory.BinaryExpression(SyntaxKind.MultiplyExpression, binary.Left, operatorToken, binary.Right);

			AddMutation(binary, multiplyMutator);
		}
		if (!binary.IsKind(SyntaxKind.DivideExpression))
		{
			SyntaxToken operatorToken = SyntaxFactory.Token(SyntaxFactory.TriviaList(), SyntaxKind.SlashToken, SyntaxFactory.TriviaList(SyntaxFactory.Space));
			BinaryExpressionSyntax divideMutator = SyntaxFactory.BinaryExpression(SyntaxKind.DivideExpression, binary.Left, operatorToken, binary.Right);

			AddMutation(binary, divideMutator);
		}
		if (!binary.IsKind(SyntaxKind.ModuloExpression))
		{
			SyntaxToken operatorToken = SyntaxFactory.Token(SyntaxFactory.TriviaList(), SyntaxKind.PercentToken, SyntaxFactory.TriviaList(SyntaxFactory.Space));
			BinaryExpressionSyntax moduloMutator = SyntaxFactory.BinaryExpression(SyntaxKind.ModuloExpression, binary.Left, operatorToken, binary.Right);

			AddMutation(binary, moduloMutator);
		}
	}

	private void AddMutation(SyntaxNode original, SyntaxNode mutated)
	{
		SyntaxNode node = original.SyntaxTree.GetRoot(CancellationToken.None).ReplaceNode(original, mutated);

		var mutation = new Mutation(original, original.SyntaxTree, mutated, node.SyntaxTree);
		mutations.Add(mutation);
	}
}
