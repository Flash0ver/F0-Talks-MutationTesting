using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace F0.Talks.MutationTesting.Diagnostics;

public static class ArrayExtensions
{
	public static void EnsureNotNull<T>([NotNull] ref T[]? array)
	{
		bool condition = true;

		if (array is null && condition)
		{
			array = Array.Empty<T>();
		}
		else
		{
			Debug.Assert(array is not null);
		}
	}
}
