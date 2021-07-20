using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;

namespace F0.Talks.MutationTesting.FaultInjector.CodeAnalysis
{
	internal static class Mutator
	{
		public static async Task<IReadOnlyCollection<Mutation>> MutateAsync(SyntaxTree original)
		{
			SyntaxNode root = await original.GetRootAsync(CancellationToken.None);

			var visitor = new MutationVisitor();
			visitor.Visit(root);

			return visitor.Mutations;
		}
	}
}
