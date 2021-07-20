using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Testing;
using Xunit;

namespace F0.Talks.MutationTesting.FaultInjector.CodeAnalysis
{
	internal sealed class Compiler
	{
		public static async Task<Compiler> CreateAsync(Source source)
		{
			ImmutableArray<MetadataReference> referenceAssembly = await CreateReferenceAssembliesAsync();

			return new Compiler(source, referenceAssembly);
		}

		private readonly Source source;
		private readonly ImmutableArray<MetadataReference> referenceAssemblies;

		private Compiler(Source source, ImmutableArray<MetadataReference> referenceAssemblies)
		{
			this.source = source;
			this.referenceAssemblies = referenceAssemblies;
		}

		public CSharpCompilation Compile()
		{
			return Compile("Mutation-Testing", source.Production, source.Test);
		}

		public CSharpCompilation Compile(int id, SyntaxTree mutated)
		{
			return Compile($"Mutation-Testing-{id}", mutated, source.Test);
		}

		private CSharpCompilation Compile(string assemblyName, SyntaxTree production, SyntaxTree test)
		{
			return CSharpCompilation.Create(assemblyName)
				.AddSyntaxTrees(production, test)
				.AddReferences(referenceAssemblies)
				.WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
		}

		private static async Task<ImmutableArray<MetadataReference>> CreateReferenceAssembliesAsync()
		{
			ReferenceAssemblies referenceAssemblies = ReferenceAssemblies.Net.Net50;

			ImmutableArray<MetadataReference> references = await referenceAssemblies.ResolveAsync(LanguageNames.CSharp, CancellationToken.None);

			PortableExecutableReference xunit = MetadataReference.CreateFromFile(typeof(FactAttribute).Assembly.Location);
			PortableExecutableReference assert = MetadataReference.CreateFromFile(typeof(Assert).Assembly.Location);

			return references.AddRange(new[] { xunit, assert });
		}
	}
}
