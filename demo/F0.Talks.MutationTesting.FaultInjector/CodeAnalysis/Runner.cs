﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Spectre.Console;
using Xunit;
using Xunit.Sdk;

namespace F0.Talks.MutationTesting.FaultInjector.CodeAnalysis;

internal static class Runner
{
	private static readonly Type fact = typeof(FactAttribute);

	public static async Task RunAsync(Source source, Compiler compiler)
	{
		if (await InitialTestAsync(compiler))
		{
			return;
		}

		IReadOnlyCollection<Mutation> mutants = await Mutator.MutateAsync(source.Production);
		int padding = mutants.Count.ToString().Length;

		var report = new Report();
		int i = 0;
		foreach (IGrouping<SyntaxNode, Mutation> group in mutants.GroupBy(static mutant => mutant.OriginalNode))
		{
			SyntaxNode mutated = group.Key;
			AnsiConsole.MarkupLine($"[bold]{mutated}[/]");

			foreach (Mutation mutant in group)
			{
				bool hasFailed = await MutationTestAsync(compiler, i++, mutant, padding);

				report.Increment(hasFailed);
			}
		}

		report.WriteToConsole();
	}

	private static async Task<bool> InitialTestAsync(Compiler compiler)
	{
		CSharpCompilation compilation = compiler.Compile();

		AssemblyLoadContext context = await EmitAssemblyAsync(compilation);
		Assembly assembly = context.Assemblies.Single();

		bool hasFailed = RunTest(assembly);
		if (hasFailed)
		{
			var style = new Style(Color.Red);
			AnsiConsole.Console.WriteLine("At least one test is failing!", style);
		}

		context.Unload();

		return hasFailed;
	}

	private static async Task<bool> MutationTestAsync(Compiler compiler, int id, Mutation mutant, int padding)
	{
		CSharpCompilation compilation = compiler.Compile(id, mutant.MutatedTree);

		AssemblyLoadContext context = await EmitAssemblyAsync(compilation);
		Assembly assembly = context.Assemblies.Single();

		bool hasFailed = RunTest(assembly);
		if (hasFailed)
		{
			AnsiConsole.MarkupLine($"\t{id.ToString($"D{padding}")}: Mutant [green]{mutant.MutatedNode}[/] killed");
		}
		else
		{
			AnsiConsole.MarkupLine($"\t{id.ToString($"D{padding}")}: Mutant [red]{mutant.MutatedNode}[/] survived");
		}

		context.Unload();

		return hasFailed;
	}

	private static async Task<AssemblyLoadContext> EmitAssemblyAsync(CSharpCompilation compilation)
	{
		await using var stream = new MemoryStream();
		EmitResult result = compilation.Emit(stream);

		if (result.Success)
		{
			long position = stream.Seek(0, SeekOrigin.Begin);
			Debug.Assert(position == 0);

			var context = new AssemblyLoadContext(null, true);
			_ = context.LoadFromStream(stream);
			return context;
		}
		else
		{
			IEnumerable<Diagnostic> diagnostics = result.Diagnostics
				.Where(static diagnostic => diagnostic.Severity is not DiagnosticSeverity.Hidden);

			Table table = new Table()
				.AddColumn("")
				.AddColumn("Code")
				.AddColumn("Description");

			foreach (Diagnostic diagnostic in diagnostics)
			{
				table.AddRow(Severity(diagnostic.Severity), diagnostic.Id, diagnostic.GetMessage());
			}

			AnsiConsole.Write(table);

			throw new ArgumentException($"{nameof(Compilation)}", nameof(compilation));
		}
	}

	private static string Severity(DiagnosticSeverity severity)
	{
		return severity switch
		{
			DiagnosticSeverity.Info => $"[blue]i[/]",
			DiagnosticSeverity.Warning => $"[yellow]![/]",
			DiagnosticSeverity.Error => $"[red]x[/]",
			_ => throw new InvalidEnumArgumentException(nameof(severity), (int)severity, typeof(DiagnosticSeverity)),
		};
	}

	private static bool RunTest(Assembly assembly)
	{
		Type? type = assembly.GetType("F0.Talks.MutationTesting.Tests.Mathematics.CalculatorTests");
		Debug.Assert(type is not null);

		object? instance = Activator.CreateInstance(type);
		Debug.Assert(instance is not null);

		IEnumerable<MethodInfo> methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
		IEnumerable<MethodInfo> tests = methods.Where(static method => method.CustomAttributes.Any(static attribute => attribute.AttributeType == fact));

		bool hasFailed = false;

		foreach (MethodInfo test in tests)
		{
			if (InvokeTest(test, instance) is not null)
			{
				hasFailed = true;
				break;
			}
		}

		return hasFailed;
	}

	private static Exception? InvokeTest(MethodInfo method, object obj)
	{
		Exception? exception = null;

		try
		{
			object? value = method.Invoke(obj, []);

			Debug.Assert(value is null);
		}
		catch (TargetInvocationException ex) when (ex.InnerException is XunitException)
		{
			exception = ex;
		}
		catch (Exception ex)
		{
			AnsiConsole.WriteException(ex);
			throw;
		}

		return exception;
	}
}
