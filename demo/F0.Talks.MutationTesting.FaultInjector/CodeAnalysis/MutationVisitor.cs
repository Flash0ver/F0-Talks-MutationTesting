using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace F0.Talks.MutationTesting.FaultInjector.CodeAnalysis
{
	internal sealed class MutationVisitor : CSharpSyntaxWalker
	{
		private readonly List<Mutation> mutations = new();

		public IReadOnlyCollection<Mutation> Mutations => mutations;

		public override void Visit(SyntaxNode? node)
		{
			if (node is BinaryExpressionSyntax binary)
			{
				if (binary.IsKind(SyntaxKind.AddExpression))
				{
					BinaryExpressionSyntax subtraction = SyntaxFactory.BinaryExpression(SyntaxKind.SubtractExpression, binary.Left, binary.Right);
					BinaryExpressionSyntax multiplication = SyntaxFactory.BinaryExpression(SyntaxKind.MultiplyExpression, binary.Left, binary.Right);
					BinaryExpressionSyntax division = SyntaxFactory.BinaryExpression(SyntaxKind.DivideExpression, binary.Left, binary.Right);

					AddMutation(binary, subtraction);
					AddMutation(binary, multiplication);
					AddMutation(binary, division);
				}
			}

			base.Visit(node);
		}

		private void AddMutation(SyntaxNode original, SyntaxNode mutated)
		{
			SyntaxNode node = original.SyntaxTree.GetRoot().ReplaceNode(original, mutated);

			var mutation = new Mutation(original, original.SyntaxTree, mutated, node.SyntaxTree);
			mutations.Add(mutation);
		}
	}
}
