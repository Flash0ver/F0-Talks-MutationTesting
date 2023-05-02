using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace F0.Talks.MutationTesting.Diagnostics
{
	public static class ObjectExtensions
	{
		public static void ThrowIfNull<T>([NotNull] this T? obj, [CallerArgumentExpression("obj")] string? paramName = null)
		{
			bool condition = true;

			if (obj is null && condition)
			{
				throw new ArgumentNullException(paramName);
			}
			else
			{
				Debug.Assert(obj is not null);
			}
		}
	}
}
