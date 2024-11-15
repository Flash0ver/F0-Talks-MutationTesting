using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Basic.Reference.Assemblies;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace F0.Talks.MutationTesting.FaultInjector.CodeAnalysis;

internal sealed class Compiler
{
	public static Compiler Create(Source source)
	{
		ImmutableArray<MetadataReference> referenceAssemblies = CreateReferenceAssemblies();

		return new Compiler(source, referenceAssemblies);
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

	private static ImmutableArray<MetadataReference> CreateReferenceAssemblies()
	{
		IEnumerable<PortableExecutableReference> net80 = Net80.References.All;

		PortableExecutableReference xunit = MetadataReference.CreateFromFile(typeof(FactAttribute).Assembly.Location);
		PortableExecutableReference assert = MetadataReference.CreateFromFile(typeof(Assert).Assembly.Location);

		IEnumerable<PortableExecutableReference> references = net80.Append(xunit).Append(assert);

		return ImmutableArray.CreateRange<MetadataReference>(references);
	}
}
