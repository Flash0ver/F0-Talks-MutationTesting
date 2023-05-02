#if DEBUG && NETFRAMEWORK
using System;

namespace F0.Talks.MutationTesting.Tests;

internal sealed class DebugAssertException : Exception
{
	public DebugAssertException(string? message)
		: base(message)
	{
	}
}
#endif
