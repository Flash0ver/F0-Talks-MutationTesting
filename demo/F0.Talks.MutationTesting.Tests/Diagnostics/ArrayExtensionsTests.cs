using F0.Talks.MutationTesting.Diagnostics;
using Xunit;

namespace F0.Talks.MutationTesting.Tests.Diagnostics;

public class ArrayExtensionsTests
{
	[Fact]
	public void EnsureNotNull_KeepIfNotNull()
	{
		string[] array = [string.Empty];

		int hashCode = array.GetHashCode();

		ArrayExtensions.EnsureNotNull(ref array);

		Assert.Equal(hashCode, array.GetHashCode());
	}

	[Fact]
	public void EnsureNotNull_InitializeIfNull()
	{
		string[]? array = null;

		ArrayExtensions.EnsureNotNull(ref array);

		Assert.NotNull(array);
		Assert.Equal(1, array.Rank);
		Assert.Empty(array);
	}
}
